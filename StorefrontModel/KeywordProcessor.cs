using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorefrontModel
{
    public class KeywordProcessor
    {
        private Entities context;
        private Keywords keyword;
        private string productNo;
        int count = 0;

        public int Count { 
            get
            {
                return count;
            }
        }

        public KeywordProcessor(Entities context)
        {
            this.context = context;
            keyword = new Keywords();
        }


        public void UpdateKeyWords(Product product)
        {
            context.Keywords.Delete(product.ProductNo);
            AddKeyWords(product);
        }

        public int RebuildKeywords(string manufacturer)
        {
            List<Product> products = null;
            if(manufacturer!=null&&manufacturer.Length>0)
            {
                products = context.Product.Select("Manufacturer", manufacturer);
            }
            else
            {
                products = context.Product.Select();
            }

            foreach(Product product in products)
            {
                context.Keywords.Delete(product.ProductNo);
                AddKeyWords(product);
            }

            return count;
        }

        private void AddKeyWords(Product product)
        {
            productNo = product.ProductNo;
            addNow(product.Manufacturer);
            addNow(product.Distributor);
            addNow(product.ProductNo);
            addNow(product.Name);
            Details detail = context.Details.Where(product.ProductNo);
            if (detail != null)
            {
                addNow(detail.ManufacturerProduct);
                addDescription(detail.Description);
            }

            List<Category> categories =
                context.Category.Execute("GetCategoriesByProduct", "ProductNo", product.ProductNo);

            foreach (Category category in categories)
            {
                addNow(category.Name);
            }

            List<ProductSpecification> specifications =
                context.ProductSpecification.Select("Product", product.ProductNo);

            foreach (ProductSpecification specification in specifications)
            {
                addNow(specification.Name);
                addDescription(specification.Value);
            }
        }

        private void addNow(string word)
        {
            word = word.Trim();
            if (word.Length > 3)
            {
                if (checkThreeLetterWords(word))
                {
                    string[] parameters = { "keyword", "product" };
                    object[] values = { word, productNo };
                    context.Keywords.ExecuteProc("InsertKeyword", parameters, values);
                    count++;
                }
            }
        }

        private void addDescription(string description)
        {
            char[] seps = { ' ', ',', '\n', '\r' };
            string[] values = description.Split(seps);
            foreach (string token in values)
            {
                addNow(token);
            }
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
    }
}
