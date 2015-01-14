using System;
using System.Web;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Security.Permissions;
using System.Data.Common;
using System.Data;
using System.Web.Caching;
using Storefront.Models;
using Storefront.Aspects;
using StorefrontModel;
using log4net;

[SqlClientPermission(SecurityAction.Demand, Unrestricted = true)]
public class SFSiteMapProvider : StaticSiteMapProvider
{
    private const string errmsg1 = "Missing node ID";
    private const string errmsg2 = "Duplicate node ID";
    private const string errmsg3 = "Missing parent ID";
    private const string errmsg4 = "Invalid parent ID";
    private const string errmsg5 = "Empty or missing connectionStringName";
    private const string errmsg6 = "Missing connection string";
    private const string errmsg7 = "Empty connection string";
    private const string errmsg8 = "Invalid sqlCacheDependency";
    private const string cacheDependencyName = "__SiteMapCacheDependency";
    private string database, table;
    private bool cacheDependency = false;
    
    private Dictionary<int, SiteMapNode> nodes = new Dictionary<int, SiteMapNode>(16);
    private readonly object siteLock = new object();
    private SiteMapNode root;
    protected StorefrontEntities context;
    private static ILog log = log4net.LogManager.GetLogger("SFSiteMapProvider");

    public SFSiteMapProvider()
    {
        context = new StorefrontEntities();
    }

    [LogMethodCall]
    public override void Initialize(string name, NameValueCollection config)
    {
        if (config == null)
        {
            log.Error("Config is null throwing null arguments exception: ");
            throw new ArgumentNullException("config");
        }

        if (String.IsNullOrEmpty(name))
            name = "SqlSiteMapProvider";

        if (string.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "SQL site map provider");
        }

        base.Initialize(name, config);

        string connect = config["connectionStringName"];

        if (String.IsNullOrEmpty(connect))
            throw new ProviderException(errmsg5);
        config.Remove("connectionStringName");

        if (WebConfigurationManager.ConnectionStrings[connect] == null)
            throw new ProviderException(errmsg6);

        connect = WebConfigurationManager.ConnectionStrings[connect].ConnectionString;
        if (String.IsNullOrEmpty(connect))
            throw new ProviderException(errmsg7);
        
        // Initialize SQL cache dependency info
        string dependency = config["sqlCacheDependency"];

        if (!String.IsNullOrEmpty(dependency))
        {
            if (String.Equals(dependency, "CommandNotification",
                StringComparison.InvariantCultureIgnoreCase))
            {
                SqlDependency.Start(connect);
                cacheDependency = true;
            }
            else
            {
                string[] info = dependency.Split(new char[] { ':' });
                if (info.Length != 2)
                    throw new ProviderException(errmsg8);
                database = info[0];
                table = info[1];
            }
            config.Remove("sqlCacheDependency");
        }
   
        if (config["securityTrimmingEnabled"] != null)
            config.Remove("securityTrimmingEnabled");

        if (config.Count > 0)
        {
            string attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
                log.Error("Unrecognized attribute: " + attr);
                throw new ProviderException("Unrecognized attribute: " + attr);
        }
    }

    [LogMethodCall]
    public override SiteMapNode BuildSiteMap()
    {
        lock (siteLock)
        {
            if (root != null)
                return root;

            List<SiteMapEntry> siteMapEntries = context.SiteMapEntry.Select();

            SqlCacheDependency dependency = null;
            if (!string.IsNullOrEmpty(database) && !string.IsNullOrEmpty(table))
            {
                try { 
                    dependency = new SqlCacheDependency(database,table);
                } 
                catch (DatabaseNotEnabledForNotificationException exDBDis) 
                {
                    try {
                        SqlCacheDependencyAdmin.EnableNotifications(database); 
                    } 
                    catch (UnauthorizedAccessException exPerm) 
                    {
                        log.Error("UnauthorizedAccessException", exPerm);
                    } 
                } 
                catch (TableNotEnabledForNotificationException exTabDis) 
                { 
                    try 
                    {
                        SqlCacheDependencyAdmin.EnableTableForNotifications(database, table); 
                    } 
                    catch (SqlException exc) 
                    {
                        log.Error("sqlexeception: ", exc);
                    } 
                } 
            }

            foreach (SiteMapEntry siteMapEntry in siteMapEntries)
            {
                if (root == null)
                {
                    root = CreateSiteMapNode(siteMapEntry);
                    AddNode(root, null);
                }
                else
                {
                    SiteMapNode node = CreateSiteMapNode(siteMapEntry);
                    AddNode(node, GetParentNode(siteMapEntry.Parent));
                }
            }

            if (dependency != null)
            {
                HttpRuntime.Cache.Insert( cacheDependencyName,
                                          new object(), 
                                          dependency,
                                          Cache.NoAbsoluteExpiration,
                                          Cache.NoSlidingExpiration,
                                          CacheItemPriority.NotRemovable,
                                          new CacheItemRemovedCallback(OnSiteMapChanged) );
            }
            return root;
        }
    }

    protected override SiteMapNode GetRootNodeCore()
    {
        lock (siteLock)
        {
            BuildSiteMap();
            return root;
        }
    }

    // Helper methods
    [LogMethodCall]
    private SiteMapNode CreateSiteMapNode(SiteMapEntry siteMapEntry)
    {
        if (nodes.ContainsKey(siteMapEntry.ID))
            throw new ProviderException(errmsg2);

        string[] rolelist = null;
        if (!string.IsNullOrEmpty(siteMapEntry.Roles))
            rolelist = siteMapEntry.Roles.Split(new char[] { ',', ';' }, 512);

        SiteMapNode node = new SiteMapNode( this,
                                            siteMapEntry.ID.ToString(),
                                            siteMapEntry.Url,
                                            siteMapEntry.Title,
                                            siteMapEntry.Description, 
                                            rolelist, 
                                            null, 
                                            null, 
                                            null);

        nodes.Add(siteMapEntry.ID, node);
        return node;
    }

    private SiteMapNode GetParentNode(int parentID)
    {
        if (!nodes.ContainsKey(parentID))
        {
            log.Error(errmsg4);
            throw new ProviderException(errmsg4);
        }

        return nodes[parentID];
    }

    void OnSiteMapChanged(string key, object item, CacheItemRemovedReason reason)
    {
        lock (siteLock)
        {
            if (key == cacheDependencyName && reason == CacheItemRemovedReason.DependencyChanged)
            {
                Clear();
                nodes.Clear();
                root = null;
            }
        }
    }

    public override SiteMapNode FindSiteMapNode(string rawUrl)
    {
        if (rawUrl.Contains("/Account/"))
        {
            if (!rawUrl.Contains("/Account/YourAccount"))
            {
                rawUrl = rawUrl.Substring(0, rawUrl.LastIndexOf('/'));
            }
        }
        return base.FindSiteMapNode(rawUrl);
    }
}