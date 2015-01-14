using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Storefront.Models;
using Storefront.Aspects;
using StorefrontModel;

namespace Storefront.Controllers
{
    [HandleError]
    public class SearchController : StorefrontController
    {
        private SearchProcessor SearchProcessor { get; set; }

        public SearchController()
        {
            SearchProcessor = new SearchProcessor(context);
        }

        [LogMethodCall]
        public ActionResult Index(string name, string page)
        {
            SearchViewData viewData = new SearchViewData();
            SearchRequest searchRequest = context.SearchRequest.Where(name);
            bool bKeyWordSearch = false;
            if (string.IsNullOrEmpty(searchRequest.SearchPhrase))
                viewData.bInSearch = true;
            else
                bKeyWordSearch = true;
            AddMasterData(viewData);
            viewData.searchId = name;
            viewData.sortFields = context.SortFields.Select(" order by Sortorder", true);

            if (searchRequest == null)
            {
                //TODO: Error stuff here
            }

            viewData.sortBy = searchRequest.SortField;

            if (!bKeyWordSearch)
            {
                if (!string.IsNullOrEmpty(searchRequest.Manufacturer))
                {
                    viewData.bBrandMenu = false;
                }

                HashSet<string> categorySet = null;
                if (!string.IsNullOrEmpty(searchRequest.Categories))
                {
                    categorySet = new HashSet<string>();
                    foreach (string cat in searchRequest.Categories.Split(','))
                    {
                        categorySet.Add(cat);
                    }
                }

                viewData.groups = new List<ProductGroup>();
                foreach (GroupType groupType in
                    context.GroupType.Execute("GetGroups", "level", searchRequest.Level))
                {
                    bool bIncludeGroup = true;
                    ProductGroup group = new ProductGroup();
                    group.grouptype = groupType;
                    List<Category> catagories = context.Category.Select("GroupType", groupType.Name);
                    foreach (Category category in catagories)
                    {
                        if (categorySet != null && categorySet.Contains(category.Name))
                        {
                            bIncludeGroup = false;
                            break;
                        }
                        group.catagories.Add(category);
                    }
                    if (bIncludeGroup)
                        viewData.groups.Add(group);
                }
            }

            int startIdx=0, endIdx=0, itemsPerPage=0;
            int nPage = int.Parse(page);
            itemsPerPage = viewData.theme.Searchresultrow *
                        viewData.theme.Searchresultcol;
            startIdx = (itemsPerPage * nPage) - itemsPerPage;
            endIdx = itemsPerPage * nPage;

            viewData.items = SearchProcessor.GetSearchResults(name, startIdx, endIdx);
            viewData.searchId = name;
            viewData.page = nPage;
            if (!bKeyWordSearch)
            {
                viewData.searchBreadCrumbs = context.SearchBreadCrumb.Select(viewData.searchId);
            }
            else
            {
                viewData.keywordsearch = searchRequest.SearchPhrase;
                viewData.bInSearch = false;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Category(string name, string page)
        {
            string id = null;
            if (string.IsNullOrEmpty(page))
            {
                id = name;
            }
            else
            {
                id = page;
            }
            Category item = context.Category.Where(id);
            return SearchItems(name, item.LongName, page, false);
        }

        [LogMethodCall]
        public ActionResult Manufacturer(string name, string page)
        {
            string id = null;
            if (string.IsNullOrEmpty(page))
            {
                id = name;
            }
            else
            {
                id = page;
            }

            Manufacturer item = context.Manufacturer.Where(id);
            return SearchItems(name, item.LongName, page, true);
        }

        [LogMethodCall]
        private ActionResult SearchItems(string name, string description, string page, bool bManufacturer)
        {
            SearchProcessor processor = new SearchProcessor(context);
            SearchRequest request = context.SearchRequest.Where(name);
            StringBuilder builder = new StringBuilder("Index/");
            Company company = (Company)Session["company"];

            if (request == null)
            {
                request = CreateLandingPageSearch(processor, name, description, bManufacturer);
            }

            if (string.IsNullOrEmpty(page))
            {
                page = name;
            }

            string cookie = GetStorefrontCookie(Request);
            if (!string.IsNullOrEmpty(cookie))
            {
                Customer customer = context.Customer.Where(cookie);
                request.SearchId = getGuid();
                builder.Append(request.SearchId);
                builder.Append("/1");

                if (bManufacturer)
                {
                    request.Manufacturer = page;
                }
                else
                {
                    request.Level++;
                    if (string.IsNullOrEmpty(request.Categories))
                    {
                        request.Categories = page;
                    }
                    else
                    {
                        request.Categories += "," + page;
                    }
                }

                if (company.InStockOnly > 0)
                {
                    request.ViewOnlyAvailable = 1;
                }
                else if (customer != null)
                {
                    request.CustomerNo = customer.CustomerNo;
                    request.ViewOnlyAvailable = customer.ViewOnlyAvailability;
                }
                else
                {
                    request.CustomerNo = cookie;
                }

                request.LandingPage = 0;
                request.SearchTime = DateTime.Now;
                processor.SearchItems(request);
                context.SearchRequest.Insert(request);
                SearchBreadCrumbs crumbs = new SearchBreadCrumbs();
                crumbs.CopyBreadCrumbs(context, name, request.SearchId);
                crumbs.Add(request.SearchId, description, builder.ToString());
                crumbs.Save(context);
            }
            else
            {
                builder.Append(request.SearchId);
                builder.Append("/1");
            }

            return RedirectToAction(builder.ToString(), "Search");
        }

        [LogMethodCall]
        private SearchRequest CreateLandingPageSearch(SearchProcessor processor, 
                                                string name, 
                                                string description, 
                                                bool bManufacturer )
        {
            SearchRequest request = new SearchRequest();
            StringBuilder builder = new StringBuilder("Index/");
            request.ViewOnlyAvailable = 0;
            request.Level = 1;

            request.SearchId = name;
            builder.Append(request.SearchId);
            builder.Append("/1");
            if (bManufacturer)
            {
                request.Manufacturer = name;
            }
            else
            {
                Category item = context.Category.Where(name);
                request.Categories = item.Name;
                description = item.LongName;
            }
            request.LandingPage = 1;
            request.SearchTime = DateTime.Now;
            request.Description = description;
            processor.LandingPageSearch(name, request);
            context.SearchRequest.Insert(request);

            return request;
        }

        [LogMethodCall]
        public ActionResult SortOrder(string searchId,
                                        string page,
                                        string sortby)
        {
            SearchProcessor processor = new SearchProcessor(context);
            SearchRequest request = context.SearchRequest.Where(searchId);
            StringBuilder builder = new StringBuilder("Index/");

            string cookie = GetStorefrontCookie(Request);

            if (request.LandingPage > 0)
            {
                // This should never happen if it is a landing page this
                // Must be a robot and will not execute the sorting code.
                builder.Append(request.SearchId);
                builder.Append("/1");
            }
            else
            {
                builder.Append(request.SearchId);
                builder.Append("/1");
                request.SearchTime = DateTime.Now;
                request.SortField = sortby;
                context.SearchRequest.Update(request);
                context.SearchResult.Delete(request.SearchId);
                context.SearchResultItem.Delete(request.SearchId);
                processor.SearchItems(request);
            }

            return RedirectToAction(builder.ToString(), "Search");
        }

        public List<SearchRanking> SearchCountReport()
        {
            return context.SearchRanking.Select(" order by count desc ", true);
        }
    }
}