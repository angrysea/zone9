using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.IO;
using SFAdmin.Models;
using SFAdmin.Aspects;
using StorefrontModel;

namespace SFAdmin.Controllers
{
    [HandleError]
    public class ProductsController : StorefrontController
    {
        [LogMethodCall]
        public ActionResult Products(string id)
        {
            ProductsViewData viewData = new ProductsViewData();
            AddMasterData(viewData);
            viewData.manufacturers = context.Manufacturer.Select();
            if (id != null && id.Length > 0)
            {
                viewData.products = context.Product.Select("Manufacturer", id);
            }
            else
            {
                viewData.products = context.Product.Select();
            }
            viewData.manufacturer = id;
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ProductMaint(string id)
        {
            ProductViewData viewData = new ProductViewData();

            AddMasterData(viewData);

            if (id != null && id.Length > 0)
            {
                viewData.update = true;
                viewData.product = context.Product.Where(id);
                viewData.details = context.Details.Where(id);
                viewData.nextProduct = getNextProduct(id);
                viewData.prevProduct = getPrevProduct(id);
            }
            else
            {
                viewData.update = false;
            }

            viewData.manufacturers = context.Manufacturer.Select();
            viewData.distributors = context.Distributor.Select();
            viewData.groups = new List<ProductGroup>();
            int nCategories = 0;
            foreach (GroupType groupType in context.GroupType.Select())
            {
                ProductGroup group = new ProductGroup();
                group.grouptype = groupType;

                List<Category> catagories = context.Category.Select("GroupType", groupType.Name);
                foreach(Category category in catagories) 
                {
                    group.catagories.Add(category);
                }
                nCategories += group.catagories.Count();
                viewData.groups.Add(group);
            }

            viewData.productCategories = context.ProductCategories.Select("Product", id);
            viewData.specifications = context.Specification.Select();
            List <ProductSpecification> specifications = context.ProductSpecification.Select("Product", id);
            viewData.productSpecifications = new Dictionary<string, string>();
            foreach (ProductSpecification specification in specifications)
            {
                viewData.productSpecifications.Add(specification.Name, specification.Value);
            }
            viewData.availabilities = context.Availability.Select();
            viewData.productAvailability = context.ProductAvailability.Select("Product", id);
            viewData.nSpecifications = viewData.specifications.Count();
            viewData.nCategories = nCategories;

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ProductAction(
                        string mode,
                        string productno,
                        string gtin,
                        string nCategories,
                        string nSpecifications,    
                        string manufacturer,
                        string manufacturerproductNo,
                        string productname,
                        string listprice,
                        string ourcost,
                        string ourprice,
                        string qty,
                        string shippingweight,
                        string height,
                        string length,
                        string Width,
                        string distributor,
                        string distributorproductNo,
                        string handlingfee,
                        string description,
                        string status
            )
        {
            Dictionary<string, string> specifications = new Dictionary<string, string>();
            List<string> categories = new List<string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.StartsWith("specification"))
                {
                    string name = key.Substring(13);
                    string value = Request.Form[key];
                    if (value != null && value.Length > 0)
                    {
                        specifications.Add(name, value);
                    }
                }
                else if (key.StartsWith("category"))
                {
                    if (Request.Form[key] == "on")
                    {
                        categories.Add(key.Substring(8));
                    }
                }
            }
            
            Product product = null;
            Details details = null;
            if (mode.ToUpper().Equals("I"))
            {
                product = new Product();
                details = new Details();
            }
            else
            {
                product = context.Product.Where(productno);
                details = context.Details.Where(productno);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Product.Delete(product);
                context.Details.Delete(details);
            }
            else
            {
                product.ProductNo = productno;
                product.GTIN = gtin;
                details.Product = productno;
                product.Name = productname;
                product.Manufacturer = manufacturer;
                product.ListPrice = double.Parse(listprice);
                product.OurCost = double.Parse(ourcost);
                product.OurPrice = double.Parse(ourprice);
                product.Quantity = Int32.Parse(qty);
                product.Distributor = distributor;
                product.Availability = status;
                details.Description = description;
                details.ManufacturerProduct = manufacturerproductNo;
                details.ShippingWeight = double.Parse(shippingweight);
                details.Height = double.Parse(height);
                details.Length = double.Parse(length);
                details.Width = double.Parse(Width);
                details.DistributorProduct = distributorproductNo;
                details.HandlingCharges = double.Parse(handlingfee);
                details.ImageURLSmall = productno + "small.gif";
                details.ImageURLMedium = productno + "medium.gif";
                details.ImageURLLarge = productno + "large.gif";
                if (mode.ToUpper().Equals("I"))
                {
                    ProductRanking ranking = new ProductRanking();
                    ranking.Product = product.ProductNo;
                    context.Product.Insert(product);
                    context.Details.Insert(details);
                }
                else
                {
                    context.Product.Update();
                    context.Details.Update();
                    context.ProductSpecification.Delete(productno);
                    context.ProductCategories.Delete(productno);
                }

                ProductSpecification specification = new ProductSpecification();
                specification.Product = productno;
                foreach (string spec in specifications.Keys)
                {
                    specification.Name = spec;
                    specification.Value = specifications[spec];
                    context.ProductSpecification.Insert(specification);
                }

                ProductCategories productCategories = new ProductCategories();
                productCategories.Product = productno;
                foreach (string cat in categories)
                {
                    productCategories.Category = cat;
                    context.ProductCategories.Insert(productCategories);
                }
            }

            return RedirectToAction("ProductMaint/" + productno, "Products");
        }


        [LogMethodCall]
        public ActionResult VariationsMaint(string id)
        {
            VariationsViewData viewData = new VariationsViewData();

            AddMasterData(viewData);

            if (id != null && id.Length > 0)
            {
                viewData.update = true;
                viewData.product = context.Product.Where(id);
                viewData.details = context.Details.Where(id);
            }
            else
            {
                viewData.update = false;
            }

            viewData.manufacturers = context.Manufacturer.Select();
            viewData.distributors = context.Distributor.Select();
            viewData.variations = context.Variation.Select("Product", id);

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Distributors()
        {
            DistributorsViewData viewData = new DistributorsViewData();

            AddMasterData(viewData);

            viewData.distributors = context.Distributor.Select();
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult DistributorMaint(string id)
        {
            DistributorViewData viewData = new DistributorViewData();

            AddMasterData(viewData);

            if (id==null || id.Length == 0)
            {
                viewData.distributor = null;
                viewData.update = false;
            }
            else
            {
                viewData.distributor = context.Distributor.Where(id);
                viewData.update = true;
            }

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult DistributorAction(
                        string mode,
                        string name,
                        string description,
                        string dropshipfee,
                        string email,
                        string address1,
                        string address2,
                        string address3,
                        string city,
                        string state,
                        string zipcode,
                        string country,
                        string telephone )
        {
            Distributor distributor = null;
            if (mode.ToUpper().Equals("I"))
            {
                distributor = new Distributor();
            }
            else
            {
                distributor = context.Distributor.Where(name);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Distributor.Delete(distributor);
            }
            else
            {
                distributor.Name = name;
                distributor.Description = description;
                if (dropshipfee != null && dropshipfee.Length > 0)
                    distributor.Dropshipfee = double.Parse(dropshipfee);
                distributor.Email = email;
                distributor.Address1 = address1;
                distributor.Address2 = address2;
                distributor.Address3 = address3;
                distributor.City = city;
                distributor.State = state;
                distributor.Zip = zipcode;
                distributor.Country = country;
                distributor.Phone = telephone;
                if (mode.ToUpper().Equals("I"))
                {
                    context.Distributor.Insert(distributor);
                }
                else
                {
                    context.Distributor.Update();
                }
            }

            return RedirectToAction("Distributors","Products");
        }

        [LogMethodCall]
        public ActionResult Manufacturers()
        {
            ManufacturersViewData viewData = new ManufacturersViewData();

            AddMasterData(viewData);

            viewData.manufacturers = context.Manufacturer.Select();

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ManufacturerMaint(string id)
        {
            ManufacturerViewData viewData = new ManufacturerViewData();

            AddMasterData(viewData);

            if (id == null || id.Length == 0)
            {
                viewData.manufacturer = null;
                viewData.update = false;
            }
            else
            {
                viewData.manufacturer = context.Manufacturer.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ManufacturerAction(
                                            string name,
                                            string mode,
                                            string active,	
                                            string longname,
                                            string description,
                                            string longdescription,
                                            string prefix,
                                            string markup,
                                            string logo,
                                            string url			
)
        {
            Manufacturer manufacturer = null;
            if (mode.ToUpper().Equals("I"))
            {
                manufacturer = new Manufacturer();
            }
            else
            {
                manufacturer = context.Manufacturer.Where(name);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Manufacturer.Delete(manufacturer);
            }
            else
            {
                if (active != null && active.ToUpper().Equals("ON"))
                    manufacturer.Active = 1;
                else
                    manufacturer.Active = 0;
                manufacturer.Name = name;
                manufacturer.LongName = longname;
                manufacturer.ShortDescription = description;
                manufacturer.Description = longdescription;
                manufacturer.Prefix = prefix;
                if (!string.IsNullOrEmpty(markup))
                    manufacturer.MarkUp = double.Parse(markup) / 100;
                else
                    manufacturer.MarkUp = 0;
                manufacturer.Logo = logo;
                manufacturer.URL = url;

                if (mode.ToUpper().Equals("I"))
                {
                    context.Manufacturer.Insert(manufacturer);
                }
                else
                {

                    context.Manufacturer.Update();
                }
            }

            return RedirectToAction("Manufacturers", "Products");
        }

        [LogMethodCall]
        public ActionResult Categories()
        {
            CategoriesViewData viewData = new CategoriesViewData();

            AddMasterData(viewData);

            viewData.categories = context.Category.Select();

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CategoryMaint(string id)
        {
            CategoryViewData viewData = new CategoryViewData();

            AddMasterData(viewData);

            viewData.groupTypes = context.GroupType.Select();
            viewData.categories = context.Category.Select();

            if (id==null||id.Length==0)
            {
                viewData.category = null;
                viewData.update = false;
            }
            else
            {
                viewData.category = context.Category.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CategoryAction(
                                        string mode,
                                        string name,
                                        string groupType,
                                        string sortOrder,
                                        string url,
                                        string active,
                                        string longName,
                                        string startPrice,
                                        string endPrice,
                                        string description,
                                        string parent )
        {
            Category category = null;
            if (mode.ToUpper().Equals("I"))
            {
                category = new Category();
            }
            else
            {
                category = context.Category.Where(name);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Category.Delete(category);
            }
            else
            {
                category.Name = name;
                if (groupType != null && groupType.Length > 0)
                    category.GroupType = groupType;
                if (sortOrder != null && sortOrder.Length > 0)
                    category.SortOrder = Int32.Parse(sortOrder);
                category.URL = url;
                if (active != null && active.Equals("on"))
                    category.Active = 1;
                else
                    category.Active = 0;
                category.LongName = longName;
                if (startPrice != null && startPrice.Length > 0)
                    category.StartPrice = double.Parse(startPrice);
                else
                    category.StartPrice = 0.0;
                if (endPrice != null && endPrice.Length > 0)
                    category.EndPrice = double.Parse(endPrice);
                else
                    category.EndPrice = 0.0;
                category.Description = description;
                category.Parent = parent;

                if (mode.ToUpper().Equals("I"))
                {
                    context.Category.Insert(category);
                }
                else
                {
                    context.Category.Update();
                }
            }
            return RedirectToAction("categories", "Products");
        }

        [LogMethodCall]
        public ActionResult GroupTypes()
        {
            GroupTypesViewData viewData = new GroupTypesViewData();

            AddMasterData(viewData);

            viewData.groupTypes = context.GroupType.Select();

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult GroupTypeMaint(string id)
        {
            GroupTypeViewData viewData = new GroupTypeViewData();

            AddMasterData(viewData);
            viewData.catalogs = context.Catalog.Select();

            if (id==null || id.Length == 0)
            {
                viewData.groupType = null;
                viewData.update = false;
            }
            else
            {
                viewData.groupType = context.GroupType.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult GroupTypeAction(
                                        string mode,
                                        string name,
                                        string type,
                                        string catalog,
                                        string sortorder,
                                        string description,
                                        string image
                    )
        {
            GroupType groupType = null;
            if (mode.ToUpper().Equals("I"))
            {
                groupType = new GroupType();
            }
            else
            {
                groupType = context.GroupType.Where(name);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.GroupType.Delete(groupType);
            }
            else
            {
                groupType.Name = name;
                groupType.Type = type;
                if (catalog != null && catalog.Length > 0)
                    groupType.Catalog = catalog;
                groupType.Description = description;
                if (sortorder != null && sortorder.Length > 0)
                    groupType.Sortorder = Int32.Parse(sortorder);
                groupType.Image = image;

                if (mode.ToUpper().Equals("I"))
                {
                    context.GroupType.Insert(groupType);
                }
                else
                {
                    context.GroupType.Update();
                }
            }
            return RedirectToAction("GroupTypes", "Products");
        }

        [LogMethodCall]
        public ActionResult Specifications()
        {
            SpecificationsViewData viewData = new SpecificationsViewData();

            AddMasterData(viewData);

            viewData.specifications = context.Specification.Select();

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult SpecificationMaint(string id)
        {
            SpecificationViewData viewData = new SpecificationViewData();

            AddMasterData(viewData);

            if (id==null || id.Length==0)
            {
                viewData.specification = null;
                viewData.update = false;
            }
            else
            {
                viewData.specification = context.Specification.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult SpecificationAction(
                                        string mode,
                                        string name,
                                        string description,
                                        string type,
                                        string minValue,
                                        string maxValue)
        {
            Specification specification = null;
            if (mode.ToUpper().Equals("I"))
            {
                specification = new Specification();
            }
            else
            {
                specification = context.Specification.Where(name);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Specification.Delete(specification);
            }
            else
            {
                specification.Name = name;
                specification.Description = description;
                specification.Type = type;
                specification.MinValue = minValue;
                specification.MaxValue = maxValue;

                if (mode.ToUpper().Equals("I"))
                {
                    context.Specification.Insert(specification);
                }
                else
                {
                    context.Specification.Update();
                }
            }
            return RedirectToAction("Specifications", "Products");
        }

        [LogMethodCall]
        public ActionResult StartSimilarProducts(string id, string manufacturer)
        {
            StartSimilarProductsViewData viewData = new StartSimilarProductsViewData();

            AddMasterData(viewData);

            viewData.manufacturers = context.Manufacturer.Select();
            viewData.manufacturer = manufacturer;
            if (!string.IsNullOrEmpty(id))
            {
                viewData.products = context.Product.Select("Manufacturer", id);
            }
            else if (manufacturer != null && manufacturer.Length > 0)
            {
                if (manufacturer == "all")
                {
                    viewData.products = context.Product.Select();
                }
                else
                {
                    viewData.products = context.Product.Select("Manufacturer", manufacturer);
                }
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult SimilarProductsActions(string productno, string manufacturer)
        {
            SimilarProductsViewData viewData = new SimilarProductsViewData();

            AddMasterData(viewData);
            viewData.manufacturers = context.Manufacturer.Select();
            if (!string.IsNullOrEmpty(productno))
            {
                viewData.product = context.Product.Where(productno);
                viewData.details = context.Details.Where(productno);
                viewData.nextProduct = getNextProduct(productno);
                viewData.prevProduct = getPrevProduct(productno);
                string [] parameters = {"product"};
                string[] values = { productno };
                viewData.listProducts = context.ListProduct.Execute("GetSimularProductList", parameters, values);
                viewData.similarProducts = context.SimilarProduct.Select("Product", productno);
            }
            viewData.manufacturer = manufacturer;
            if (!string.IsNullOrEmpty(manufacturer) && manufacturer!="none")
            {
                string[] parameters = { "manufacturer" };
                string[] values = { manufacturer };
                viewData.manufacturerProducts =
                        context.ListProduct.Execute("GetProductListByManufacturer", 
                                                    parameters, 
                                                    values);
                string[] parameters2 = { "manufacturer", "product" };
                string[] values2 = { manufacturer, productno };
                viewData.similarProducts =
                    context.SimilarProduct.Execute("GetSimularProductsByManufacturer",
                                                    parameters2,
                                                    values2);
            }

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult RemoveSimilarProducts(string productno, string similarproductno, string manufacturer)
        {
            string[] parameters = { "Product", "SimilarProductNo" };
            string[] values = { productno, similarproductno };
            context.SimilarProduct.Delete(parameters, values);
            return RedirectToAction("SimilarProductsActions", "Products", new { productno, manufacturer });
        }

        [LogMethodCall]
        public ActionResult AddSimilarProducts(string productno, string similarproductno, string manufacturer)
        {
            SimilarProduct similarProduct = new SimilarProduct();
            similarProduct.Product = productno;
            similarProduct.SimilarProductNo = similarproductno;

            context.SimilarProduct.Insert(similarProduct);

            return RedirectToAction("SimilarProductsActions", "Products", new { productno, manufacturer });
        }

        [LogMethodCall]
        public ActionResult FeaturedGroups()
        {
            FeaturedGroupViewData viewData = new FeaturedGroupViewData();

            AddMasterData(viewData);

            viewData.featuredGroup = context.FeaturedGroup.Select();

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult FeaturedGroup(string id)
        {
            FeaturedProductViewData viewData = new FeaturedProductViewData();

            AddMasterData(viewData);
            
            viewData.manufacturers = context.Manufacturer.Select();
            viewData.manufacturer = null;

            if (id != null && id.Length > 0)
            {
                viewData.update = true;
                viewData.featuredGroup = context.FeaturedGroup.Where(id);
                viewData.featuredProducts = context.ListProduct.Execute("GetFeaturedProductList", "Name", id);
            }
            else
            {
                viewData.featuredGroup = null;
                viewData.update = false;
            }

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult FeaturedProductAction(string mode,
                                                    string name,
                                                    string heading,
                                                    string comments, 
                                                    string sortorder,
                                                    string active,
                                                    string manufacturer)
        {
            List<string> products = new List<string>();
            List<string> featured = new List<string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.StartsWith("product"))
                {
                    if(Request.Form[key]=="on")
                        products.Add(key.Substring(7));
                }
                else if (key.StartsWith("featured"))
                {
                    if (Request.Form[key] == "on")
                        featured.Add(key.Substring(8));
                }
            }

            FeaturedGroup featuredGroup = null;
            if (mode.ToUpper().Equals("I"))
            {
                featuredGroup = new FeaturedGroup();
                featuredGroup.Name = name;
            }
            else
            {
                featuredGroup = context.FeaturedGroup.Where(name);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.FeaturedGroup.Delete(name);
                context.FeaturedProducts.Delete(name);
            }
            else
            {
                featuredGroup.Heading = heading;
                featuredGroup.Comments = comments;
                featuredGroup.Sortorder = Int32.Parse(sortorder);
                featuredGroup.Active = (short)(active=="on"?1:0);
                
                if (mode.ToUpper().Equals("I"))
                {
                    context.FeaturedGroup.Insert(featuredGroup);
                }
                else
                {
                    context.FeaturedGroup.Update();
                    foreach(string toDelete in featured) {
                        string [] parameters = {"Name", "Product" };
                        string [] values = {name, toDelete};
                        context.FeaturedProducts.ExecuteProc("DeleteFeaturedProduct", parameters, values);
                    }
                }

                FeaturedProducts featuredProducts = new FeaturedProducts();
                foreach (string product in products)
                {
                    featuredProducts.Name = name;
                    featuredProducts.Product = product;
                    context.FeaturedProducts.Insert(featuredProducts);
                }
            }
            return RedirectToAction("FeaturedGroups", "Products");
        }

        [LogMethodCall]
        public ActionResult PricingWizard()
        {
            PricingWizardViewData viewData = new PricingWizardViewData();

            AddMasterData(viewData);

            viewData.manufacturers = context.Manufacturer.Select();

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult PricingWizardUpdate(string id)
        {
            context.Product.ExecuteProc("PricingWizard", null, null);
            return RedirectToAction("PricingWizard", "Products" );
        }

        [LogMethodCall]
        public ActionResult RebuildKeywords(string manufacturer)
        {
            RebuildKeywordsViewData viewData = new RebuildKeywordsViewData();
            AddMasterData(viewData);
            viewData.manufacturers = context.Manufacturer.Select();

            if (Request.HttpMethod != "POST")
            {
                return View(viewData);
            }
            
            KeywordProcessor processor = new KeywordProcessor(context);
            viewData.rebuildcount = processor.RebuildKeywords(manufacturer);
            viewData.manufacturer = manufacturer;

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult RebuildSearch(string type, string manufacturer, string category)
        {
            RebuildSearchViewData viewData = new RebuildSearchViewData();
            AddMasterData(viewData);
            viewData.manufacturers = context.Manufacturer.Select();
            viewData.categories = context.Category.Select();

            if (Request.HttpMethod != "POST")
            {
                return View(viewData);
            }

            SearchProcessor processor = new SearchProcessor(context);
            string searchName = null;
            string description = null;
            if (type == "category")
            {
                searchName = category;
                Category item = context.Category.Where(searchName);
                description = item.LongName;
            }
            else
            {
                searchName = manufacturer;
                Manufacturer item = context.Manufacturer.Where(searchName);
                description = item.LongName;
            }

            SearchRequest request = context.SearchRequest.Where(searchName);
            if (request == null)
            {
                request = new SearchRequest();
                request.SearchId = searchName;
                request.SearchTime = DateTime.Now;
                if (type == "category")
                {
                    request.Categories = category;
                }
                else
                {
                    request.Manufacturer = manufacturer;
                }
                viewData.rebuildcount = processor.LandingPageSearch(searchName, request);
                context.SearchRequest.Insert(request);
                StringBuilder builder = new StringBuilder("Index/");
                builder.Append(request.SearchId);
                builder.Append("/1");
                SearchBreadCrumb crumb = new SearchBreadCrumb();
                crumb.SearchId = request.SearchId;
                crumb.Description = description;
                crumb.Url = builder.ToString();
                context.SearchBreadCrumb.Insert(crumb);
            }
            else
            {
                viewData.rebuildcount = processor.LandingPageSearch(searchName, request);
            }
            viewData.searchname = searchName;
            return View(viewData);
        }

        public string GetListProducts()
        {
            return HtmlGenerator.GetListProducts(context, Request["p"]);
        }

        public string GetRadioListProducts()
        {
            return HtmlGenerator.GetListProducts(context, Request["p"]);
        }

        [LogMethodCall]
        public string UploadFiles()
        {
            var r = new List<ViewDataUploadFilesResult>();
            
            foreach (string file in Request.Files)
            {
                string name = Request.Form[file + "txt"];
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                string savedFileName =
                   AppDomain.CurrentDomain.BaseDirectory +
                   "Content/images/products/" + name;
                hpf.SaveAs(savedFileName);
                r.Add(new ViewDataUploadFilesResult()
                {
                    Name = name,
                    Length = hpf.ContentLength
                });
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul> ");
            foreach (ViewDataUploadFilesResult res in r)
            {
                sb.Append("<li>File Name: ");
                sb.Append(res.Name);
                sb.Append("File Size: ");
                sb.Append(res.Length);
                sb.Append("</li>");
            }
            sb.Append("</ul>");

            return sb.ToString();
        }

        private string getNextProduct(string id)
        {
            string nextProductNo = null;
            ProductNoListItem productNo = context.ProductNoListItem.First("GetNextProduct", "ProductNo", id);
            if (productNo != null)
                nextProductNo = productNo.ProductNo;
            return nextProductNo;
        }

        private string getPrevProduct(string id)
        {
            string prevProductNo = null;
            ProductNoListItem productNo = context.ProductNoListItem.First("GetPrevProduct", "ProductNo", id);
            if (productNo != null)
                prevProductNo = productNo.ProductNo;
            return prevProductNo;
        }
    }
}
