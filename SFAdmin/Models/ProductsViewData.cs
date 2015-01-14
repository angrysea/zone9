using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SFAdmin.Models;
using StorefrontModel;

namespace SFAdmin.Models
{
    public class ProductsViewData : SFViewData
    {
        public List<Manufacturer> manufacturers { get; set; }
        public List<Product> products { get; set; }
        public string manufacturer { get; set; }
    }

    public class RebuildKeywordsViewData : SFViewData
    {
        public List<Manufacturer> manufacturers { get; set; }
        public string manufacturer { get; set; }
        public int rebuildcount { get; set; }
    }

    public class RebuildSearchViewData : SFViewData
    {
        public List<Manufacturer> manufacturers { get; set; }
        public List<Category> categories { get; set; }
        public string searchname { get; set; }
        public int rebuildcount { get; set; }
    }

    public class ProductGroup
    {
        public ProductGroup()
        {
            catagories = new List<Category>();            
        }

        public GroupType grouptype { get; set; }
        public List<Category> catagories { get; set; }
    }

    public class ProductViewData : SFViewData
    {
        public Product product { get; set; }
        public Details details { get; set; }
        public List<Manufacturer> manufacturers { get; set; }
        public List<Distributor> distributors { get; set; }
        public List<ProductGroup> groups { get; set; }
        public List<ProductCategories> productCategories { get; set; }
        public List<Specification> specifications { get; set; }
        public Dictionary<string, string> productSpecifications { get; set; }
        public List<ProductAvailability> productAvailability { get; set; }
        public List<Availability> availabilities { get; set; }
        public string nextProduct { get; set; }
        public string prevProduct { get; set; }
        public int nCategories { get; set; }
        public int nSpecifications { get; set; }
    }

    public class VariationsViewData : SFViewData
    {
        public Product product { get; set; }
        public Details details { get; set; }
        public List<Manufacturer> manufacturers { get; set; }
        public List<Distributor> distributors { get; set; }
        public List<Variation> variations { get; set; }
    }

    public class DistributorsViewData : SFViewData
    {
        public List<Distributor> distributors { get; set; }
    }

    public class DistributorViewData : SFViewData
    {
        public Distributor distributor { get; set; }
    }

    public class ManufacturersViewData : SFViewData
    {
        public List<Manufacturer> manufacturers { get; set; }
    }

    public class ManufacturerViewData : SFViewData
    {
        public Manufacturer manufacturer { get; set; }
    }

    public class CategoryViewData : SFViewData
    {
        public Category category { get; set; }
        public List<GroupType> groupTypes { get; set; }
        public List<Category> categories { get; set; }
    }

    public class CategoriesViewData : SFViewData
    {
        public List<Category> categories { get; set; }
    }

    public class GroupTypeViewData : SFViewData
    {
        public GroupType groupType { get; set; }
        public List<Catalog> catalogs { get; set; }
    }

    public class GroupTypesViewData : SFViewData
    {
        public List<GroupType> groupTypes { get; set; }
    }

    public class SpecificationViewData : SFViewData
    {
        public Specification specification { get; set; }
    }

    public class SpecificationsViewData : SFViewData
    {
        public List<Specification> specifications { get; set; }
    }

    public class StartSimilarProductsViewData : SFViewData
    {
        public List<Manufacturer> manufacturers { get; set; }
        public List<Product> products { get; set; }
        public string manufacturer { get; set; }
    }

    public class SimilarProductsViewData : SFViewData
    {
        public Product product { get; set; }
        public Details details { get; set; }
        public string nextProduct { get; set; }
        public string prevProduct { get; set; }
        public List<SimilarProduct> similarProducts { get; set; }
        public List<ListProduct> listProducts { get; set; }
        public List<ListProduct> manufacturerProducts { get; set; }
        public List<Manufacturer> manufacturers { get; set; }
        public string manufacturer { get; set; }
    }

    public class FeaturedGroupViewData : SFViewData
    {
        public List<FeaturedGroup> featuredGroup { get; set; }
    }

    public class FeaturedProductViewData : SFViewData
    {
        public FeaturedGroup featuredGroup { get; set; }
        public List<Manufacturer> manufacturers { get; set; }
        public string manufacturer { get; set; }
        public List<ListProduct> featuredProducts { get; set; }
    }

    public class PricingWizardViewData : SFViewData
    {
        public List<Manufacturer> manufacturers { get; set; }
    }

    public class ViewDataUploadFilesResult
    {
        public string Name { get; set; }
        public int Length { get; set; }
    }
}
