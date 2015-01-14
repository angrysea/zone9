using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;
using Storefront.Models;

namespace Storefront.Helpers
{

    public static class BreadCrumbHelper
    {
        public static string BreadCrumbs(this HtmlHelper helper)
        {
            if (SiteMap.CurrentNode != null)
            {
                Stack<SiteMapNode> breadcrumbs = new Stack<SiteMapNode>();
                SiteMapNode current = SiteMap.CurrentNode;
                StringBuilder sb = null;

                do
                {
                    breadcrumbs.Push(current);
                    current = current.ParentNode;
                } while (current != null);

                if (breadcrumbs.Count > 0)
                {
                    sb = new StringBuilder();
                    sb.Append("<div class='searchstripe'>");
                    while (breadcrumbs.Count > 0)
                    {
                        current = breadcrumbs.Pop();
                        if (breadcrumbs.Count > 0)
                        {
                            sb.Append("<a href='");
                            sb.Append(current.Url);
                            sb.Append("'>");
                            sb.Append(helper.Encode(current.Title));
                            sb.Append("&nbsp;<img alt='' src='/Content/images/arrowright.gif'/></a>");
                        }
                        else
                        {
                            sb.Append("<a href='#'>");
                            sb.Append(helper.Encode(current.Title));
                            sb.Append("&nbsp;<img alt='' src='/Content/images/arrowright.gif'/></a>");
                        }
                    }
                    sb.Append("</div>");
                    return sb.ToString();
                }
            }
            return null;
        }
    }
}

