using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorefrontModel
{
    public class SearchProcessor
    {
        private Entities context;

        public SearchProcessor(Entities context)
        {
            this.context = context;
        }

        public int LandingPageSearch(string name, SearchRequest request)
        {
            int count = 0;

            string query = BuildSearch(request);
            if (!string.IsNullOrEmpty(query))
            {
                context.SearchResultItem.Delete(name);
                context.SearchResult.Delete(name);
                context.SearchBreadCrumb.Delete(name);
                count = PerformSearchItems(query.ToString(), name, null);
            }
            return count;
        }

        public int SearchItems(SearchRequest request)
        {
            int count = 0;
            string query = BuildSearch(request);
            if (!string.IsNullOrEmpty(query))
            {
                DeleteUsersSearches(request.CustomerNo);
                count = PerformSearchItems(query, request.SearchId, request.CustomerNo);
                updateSearchCounts(request);
            }
            return count;
        }

        public string BuildSearch(SearchRequest request)
        {
            StringBuilder select = new StringBuilder("SELECT DISTINCT Product.ProductNo");
            StringBuilder from = new StringBuilder(" FROM Product");
            StringBuilder where = new StringBuilder(" where");
            StringBuilder orderby = new StringBuilder(" order by ");

            bool bWhere = false;
            if (request.ViewOnlyAvailable>0)
            {
                from.Append(" INNER JOIN Availability ON Product.Availability = Availability.Code");
                where.Append(" Availability.ExpectedWait=0");
                bWhere = true;
            }

            SortFields sortfield = null;

            char ctab = 'a';

            if (!string.IsNullOrEmpty(request.Categories))
            {
                string[] categories = request.Categories.Split(',');
                foreach (string category in categories)
                {
                    from.Append(" INNER JOIN ProductCategories ");
                    from.Append(ctab);
                    from.Append(" ON Product.ProductNo=");
                    from.Append(ctab);
                    from.Append(".Product and ");
                    from.Append(ctab);
                    from.Append(".Category='");
                    from.Append(category);
                    from.Append("'");
                    ctab++;
                }
            }

            if (!string.IsNullOrEmpty(request.SearchPhrase))
            {
                StringBuilder searchPhrase = new StringBuilder();
                char[] seps = { ' ', ',', '\n', '\r' };
                string[] values = request.SearchPhrase.Split(seps);
                foreach (string token in values)
                {
                    string word = token.Trim();
                    if (word.Length > 3)
                    {
                        if (checkThreeLetterWords(word))
                        {
                            searchPhrase.Append(word);
                            searchPhrase.Append(", ");
                            from.Append(" INNER JOIN Keywords ");
                            from.Append(ctab);
                            from.Append(" ON Product.ProductNo=");
                            from.Append(ctab);
                            from.Append(".ProductNo");
                            if (!bWhere)
                                bWhere = true;
                            else
                                where.Append(" and");
                            where.Append(" ");
                            where.Append(ctab);
                            where.Append(".Keyword like '%");
                            where.Append(word);
                            where.Append("%'");
                            ctab++;
                        }
                    }
                }
                request.SearchPhrase = searchPhrase.ToString();
                request.SearchPhrase = request.SearchPhrase.Substring(0, request.SearchPhrase.Length - 2);
            }

            if (request.Manufacturer != null && request.Manufacturer.Length > 0)
            {
                if (!bWhere)
                    bWhere = true;
                else
                    where.Append(" and");
                where.Append(" Product.Manufacturer='");
                where.Append(request.Manufacturer);
                where.Append("'");
            }

            if (request.EndPrice > 0)
            {
                if (!bWhere)
                    bWhere = true;
                else
                    where.Append(" and");
                where.Append(" Product.OurPrice >= ");
                where.Append(request.StartPrice.ToString());
                where.Append(" and Product.OurPrice <= ");
                where.Append(request.EndPrice.ToString());
            }

            if (request.Catalog != null && request.Catalog.Length > 0)
            {
                if (!bWhere)
                    bWhere = true;
                else
                    where.Append(" and");
                where.Append(" Product.Catalog = '");
                where.Append(request.Catalog);
                where.Append("'");
            }


            if (string.IsNullOrEmpty(request.SortField))
            {
                request.SortField = "productno";
            }
            sortfield = context.SortFields.Where(request.SortField);

            if (sortfield != null && !string.IsNullOrEmpty(sortfield.SortKey))
            {
                /*
                if (sortfield.SortKey.StartsWith("specid="))
                {
                    query.Append(" LEFT OUTER JOIN ProductSpecifications ");
                    query.Append(ctab);
                    query.Append(" ON Product.ProductNo=");
                    query.Append(ctab);
                    query.Append(".Product and ");
                    query.Append(ctab);
                    query.Append(".Value=");
                    query.Append(sortfield.Fieldname);

                    if (sortfield.Fieldtype == "numeric")
                    {
                        orderby.Append(ctab);
                        orderby.Append(".Description ");
                        orderby.Append(sortfield.Direction);
                    }
                    else
                    {
                        orderby.Append(ctab);
                        orderby.Append(".Value ");
                        orderby.Append(sortfield.Direction);
                    }
                    ctab++;
                }
                else if
                */
                if (sortfield.Fieldname.StartsWith("views") ||
                            sortfield.Fieldname.StartsWith("sold"))
                {
                    from.Append(" LEFT OUTER JOIN ProductRanking ");
                    from.Append(ctab);
                    from.Append(" ON Product.ProductNo=");
                    from.Append(ctab);
                    from.Append(".Product");
                    orderby.Append(ctab);
                    orderby.Append(".");
                    orderby.Append(sortfield.Fieldname);
                    orderby.Append(" ");
                    orderby.Append(sortfield.Direction);

                    select.Append(",");
                    select.Append(ctab);
                    select.Append(".");
                    select.Append(sortfield.Fieldname);
                    ctab++;
                }
                else if (sortfield.Fieldname.StartsWith("Category"))
                {
                    orderby.Append("Product.Category");
                    orderby.Append(" ");
                    orderby.Append(sortfield.Direction);
                    select.Append(",Product.Category");

                }
                else if (sortfield.Fieldname != null)
                {
                    orderby.Append(sortfield.Fieldname);
                    orderby.Append(" ");
                    orderby.Append(sortfield.Direction);

                    if (sortfield.Fieldname != null && sortfield.Fieldname != "Product.ProductNo")
                    {
                        select.Append(",");
                        select.Append(sortfield.Fieldname);
                    }
                }
            }
            else if (request.EndPrice > 0)
            {
                orderby.Append("Product.OurPrice");
            }
            else
            {
                orderby.Append("Product.Name");
            }

            select.Append(from.ToString());

            if (bWhere)
            {
                select.Append(where.ToString());
            }

            if (sortfield != null && !string.IsNullOrEmpty(sortfield.Fieldname))
            {
                select.Append(orderby.ToString());
            }

            return select.ToString();
        }

        public int PerformSearchItems(string query, string searchId, string customerNo)
        {
            SearchResult searchResult = new SearchResult();
            searchResult.SearchId = searchId;
            searchResult.CustomerNo = customerNo;
            searchResult.Search = query;
            searchResult.SearchTime = DateTime.Now;

            int i = 0;
            List<ProductNoListItem> products = context.ProductNoListItem.Query(query);
            foreach (ProductNoListItem productNo in products)
            {
                SearchResultItem searchResultItem = new SearchResultItem();
                searchResultItem.SearchId = searchResult.SearchId;
                searchResultItem.ProductNo = productNo.ProductNo;
                searchResultItem.Idx = i;
                context.SearchResultItem.Insert(searchResultItem);
                i++;
            }
            searchResult.ProductsFound = i;
            context.SearchResult.Insert(searchResult);
            return i;
        }

        private bool checkThreeLetterWords(string token) 
        {
            bool bret = true;
            if( token.ToLower()=="the" ||
                token.ToLower()=="and" ||
                token.ToLower()=="for" ||
                token.ToLower()=="you" ||
                token.ToLower()=="out" ||
                token.ToLower()=="our" ||
                token.ToLower()=="are" ||
                token.ToLower()=="any" ||
                token.ToLower()=="yet" ||
                token.ToLower()=="but" ||
                token.ToLower()=="not" ||
                token.ToLower()=="has" ||
                token.ToLower()=="its" ||
                token.ToLower()=="use" ||
                token.ToLower()=="can" ||
                token.ToLower()=="buy" ||
                token.ToLower()=="had") 
            {
                bret = false;
            }
            return bret;
        }

        public List<SearchListItem> GetSearchResults(string searchId, int start, int end)
        {
            string[] parameters = { "searchid", "startidx", "endidx" };
            object[] values = { searchId, start, end };
            List<SearchListItem> items =
                context.SearchListItem.Execute("GetSearchResults", parameters, values);
            return items;
        }

        public void DeleteUsersSearches(string customerNo)
        {
            string[] parameters = { "CustomerNo", "SearchTime" };
            object[] values = { customerNo, DateTime.Today };
            List<SearchResult> searchResults = context.SearchResult.Select(parameters, values);
            foreach (SearchResult searchResult in searchResults)
            {
                context.SearchResultItem.Delete(searchResult.SearchId);
                context.SearchBreadCrumb.Delete(searchResult.SearchId);
            }
            context.SearchResult.Delete(parameters, values);
            context.SearchRequest.Delete(parameters, values);
        }

        public int updateSearchCounts(SearchRequest request)
        {
            StringBuilder type = new StringBuilder();
            StringBuilder search = new StringBuilder();
            if (!string.IsNullOrEmpty(request.Manufacturer))
            {
                type.Append("Manufacturer");
                search.Append(request.Manufacturer);
            }

            if (!string.IsNullOrEmpty(request.Categories))
            {
                if (type.Length > 0)
                {
                    type.Append(",");
                }
                type.Append("Category");

                if (search.Length > 0)
                {
                    search.Append(" ");
                }
                search.Append(request.Categories);
            }

            if (!string.IsNullOrEmpty(request.SearchPhrase))
            {
                if (type.Length > 0)
                {
                    type.Append(",");
                }
                type.Append("Raw Search");
                if (search.Length > 0)
                {
                    search.Append(" ");
                }
                search.Append(request.SearchPhrase);
            }

            string[] parameters = { "Type", "Search" };
            object[] values = { type.ToString(), search.ToString() };


            SearchRanking searchRanking = context.SearchRanking.First(parameters, values);

            if (searchRanking != null)
            {
                searchRanking.Count++;
                searchRanking.SearchTime = DateTime.Now;
                context.SearchRanking.Update(searchRanking);
            }
            else
            {
                searchRanking = new SearchRanking();
                searchRanking.Type = type.ToString();
                searchRanking.Search = search.ToString();
                searchRanking.Count = 1;
                searchRanking.SearchTime = DateTime.Now;
                context.SearchRanking.Insert(searchRanking);
            }
            return searchRanking.Count;
        }

    }
}
