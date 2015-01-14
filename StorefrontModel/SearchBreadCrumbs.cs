using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StorefrontModel;

namespace StorefrontModel
{
    public class SearchBreadCrumbs
    {
        private List<SearchBreadCrumb> crumbs;

        public SearchBreadCrumbs()
        {
            crumbs = new List<SearchBreadCrumb>();
        }

        public SearchBreadCrumbs(List<SearchBreadCrumb> crumbs)
        {
            this.crumbs = crumbs;
        }

        public SearchBreadCrumbs(Entities context, string searchId)
        {
            crumbs = context.SearchBreadCrumb.Select(searchId);
        }

        public void CopyBreadCrumbs(Entities context, string fromSearchId, string toSearchId)
        {
            List<SearchBreadCrumb> from = context.SearchBreadCrumb.Select(fromSearchId);

            foreach (SearchBreadCrumb crumb in from)
            {
                crumb.SearchId = toSearchId;
                crumbs.Add(crumb);
            }
        }

        public string GetDescription()
        {
            if(crumbs.Count>0)
                return crumbs[0].Description;
            return null;
        }

        public void Add(string search, string description, string url)
        {
            SearchBreadCrumb crumb = new SearchBreadCrumb();

            crumb.SearchId = search;
            crumb.Description = description;
            crumb.Url = url;

            int idx = IndexOfCrumb(crumb);
            if (idx>0 && idx + 1 < crumbs.Count)
            {
                SearchBreadCrumb[] arrayOfCrumbs = crumbs.ToArray();
                for (int i = idx + 1; idx < arrayOfCrumbs.Length; idx++)
                {
                    crumbs.Remove(arrayOfCrumbs[i]);
                }
            }
            else
            {
                crumbs.Add(crumb);
            }
        }

        public void Save(Entities context)
        {
            foreach (SearchBreadCrumb crumb in crumbs)
            {
                context.SearchBreadCrumb.Insert(crumb);
            }
        }

        private int IndexOfCrumb(SearchBreadCrumb crumb)
        {
            int i = 0;
            bool bFound = false;
            for (; i < crumbs.Count; i++)
            {
                if (AreEqual(crumb, crumbs[i]))
                {
                    bFound = true;
                    break;
                }
            }
            if (!bFound)
                i = -1;

            return i;
        }

        private bool AreEqual(SearchBreadCrumb lCrumb, SearchBreadCrumb rCrumb)
        {
            return  (lCrumb.SearchId == lCrumb.SearchId &&
                    lCrumb.Description == lCrumb.Description &&
                    lCrumb.Url == lCrumb.Url);
        }
    }
}
