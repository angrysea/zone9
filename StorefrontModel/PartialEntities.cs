// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public partial class Entities
	{
        private EntityDAO<Admins> admins = null;
        public EntityDAO<Admins> Admins
        {
            get
            {
                if (admins == null)
                    admins = new EntityDAO<Admins>(this, new Admins(), "Email");
                return admins;
            }

            set
            {
                admins = value;
            }
        }

        private EntityDAO<DistributorInventory> distributorInventory = null;
		public EntityDAO<DistributorInventory> DistributorInventory
		{
			get
			{
				if (distributorInventory == null)
					distributorInventory = new EntityDAO<DistributorInventory>(this, new DistributorInventory(), "ProductNo");
				return distributorInventory;
			}

			set
			{
				distributorInventory = value;
			}
		}

		private EntityDAO<Article> article = null;
		public EntityDAO<Article> Article
		{
			get
			{
				if (article == null)
					article = new EntityDAO<Article>(this, new Article(), "Name");
				return article;
			}

			set
			{
				article = value;
			}
		}

		private EntityDAO<EMailRecord> eMailRecord = null;
		public EntityDAO<EMailRecord> EMailRecord
		{
			get
			{
				if (eMailRecord == null)
					eMailRecord = new EntityDAO<EMailRecord>(this, new EMailRecord(), "Email");
				return eMailRecord;
			}

			set
			{
				eMailRecord = value;
			}
		}

		private EntityDAO<InvoiceItem> invoiceItem = null;
		public EntityDAO<InvoiceItem> InvoiceItem
		{
			get
			{
				if (invoiceItem == null)
					invoiceItem = new EntityDAO<InvoiceItem>(this, new InvoiceItem(), "Invoice");
				return invoiceItem;
			}

			set
			{
				invoiceItem = value;
			}
		}

		private EntityDAO<Category> category = null;
		public EntityDAO<Category> Category
		{
			get
			{
				if (category == null)
					category = new EntityDAO<Category>(this, new Category(), "Name");
				return category;
			}

			set
			{
				category = value;
			}
		}

        private EntityDAO<LinkCategory> linkCategory = null;
        public EntityDAO<LinkCategory> LinkCategory
        {
            get
            {
                if (linkCategory == null)
                    linkCategory = new EntityDAO<LinkCategory>(this, new LinkCategory(), "Name");
                return linkCategory;
            }

            set
            {
                linkCategory = value;
            }
        }

        private EntityDAO<PayFlowPro> payFlowPro = null;
		public EntityDAO<PayFlowPro> PayFlowPro
		{
			get
			{
				if (payFlowPro == null)
					payFlowPro = new EntityDAO<PayFlowPro>(this, new PayFlowPro(), "HostAddress");
				return payFlowPro;
			}

			set
			{
				payFlowPro = value;
			}
		}

		private EntityDAO<MailConfiguration> mailConfiguration = null;
		public EntityDAO<MailConfiguration> MailConfiguration
		{
			get
			{
				if (mailConfiguration == null)
					mailConfiguration = new EntityDAO<MailConfiguration>(this, new MailConfiguration(), "MailHost");
				return mailConfiguration;
			}

			set
			{
				mailConfiguration = value;
			}
		}

		private EntityDAO<LandingPage> landingPage = null;
		public EntityDAO<LandingPage> LandingPage
		{
			get
			{
				if (landingPage == null)
					landingPage = new EntityDAO<LandingPage>(this, new LandingPage(), "Page");
				return landingPage;
			}

			set
			{
				landingPage = value;
			}
		}

		private EntityDAO<ShippingMethod> shippingMethod = null;
		public EntityDAO<ShippingMethod> ShippingMethod
		{
			get
			{
				if (shippingMethod == null)
					shippingMethod = new EntityDAO<ShippingMethod>(this, new ShippingMethod(), "Code");
				return shippingMethod;
			}

			set
			{
				shippingMethod = value;
			}
		}

		private EntityDAO<SalesOrder> salesOrder = null;
		public EntityDAO<SalesOrder> SalesOrder
		{
			get
			{
				if (salesOrder == null)
					salesOrder = new EntityDAO<SalesOrder>(this, new SalesOrder(), "SalesOrderID");
				return salesOrder;
			}

			set
			{
				salesOrder = value;
			}
		}

		private EntityDAO<PurchaseOrder> purchaseOrder = null;
		public EntityDAO<PurchaseOrder> PurchaseOrder
		{
			get
			{
				if (purchaseOrder == null)
					purchaseOrder = new EntityDAO<PurchaseOrder>(this, new PurchaseOrder(), "ID");
				return purchaseOrder;
			}

			set
			{
				purchaseOrder = value;
			}
		}

		private EntityDAO<RecurringOrder> recurringOrder = null;
		public EntityDAO<RecurringOrder> RecurringOrder
		{
			get
			{
				if (recurringOrder == null)
					recurringOrder = new EntityDAO<RecurringOrder>(this, new RecurringOrder(), "ID");
				return recurringOrder;
			}

			set
			{
				recurringOrder = value;
			}
		}

		private EntityDAO<RecurringOrderItem> recurringOrderItem = null;
		public EntityDAO<RecurringOrderItem> RecurringOrderItem
		{
			get
			{
				if (recurringOrderItem == null)
					recurringOrderItem = new EntityDAO<RecurringOrderItem>(this, new RecurringOrderItem(), "ID");
				return recurringOrderItem;
			}

			set
			{
				recurringOrderItem = value;
			}
		}

		private EntityDAO<Store> store = null;
		public EntityDAO<Store> Store
		{
			get
			{
				if (store == null)
					store = new EntityDAO<Store>(this, new Store(), "Name");
				return store;
			}

			set
			{
				store = value;
			}
		}

		private EntityDAO<SearchCountItem> searchCountItem = null;
		public EntityDAO<SearchCountItem> SearchCountItem
		{
			get
			{
				if (searchCountItem == null)
					searchCountItem = new EntityDAO<SearchCountItem>(this, new SearchCountItem(), "Name");
				return searchCountItem;
			}

			set
			{
				searchCountItem = value;
			}
		}

        private EntityDAO<SearchRanking> searchRanking = null;
        public EntityDAO<SearchRanking> SearchRanking
		{
			get
			{
                if (searchRanking == null)
                    searchRanking = new EntityDAO<SearchRanking>(this, new SearchRanking(), "Type");
                return searchRanking;
			}

			set
			{
                searchRanking = value;
			}
		}

        private EntityDAO<Task> task = null;
		public EntityDAO<Task> Task
		{
			get
			{
				if (task == null)
					task = new EntityDAO<Task>(this, new Task(), "Name");
				return task;
			}

			set
			{
				task = value;
			}
		}

		private EntityDAO<SearchRequest> searchRequest = null;
        public EntityDAO<SearchRequest> SearchRequest
		{
			get
			{
                if (searchRequest == null)
                    searchRequest = new EntityDAO<SearchRequest>(this, new SearchRequest(), "SearchId");
                return searchRequest;
			}

			set
			{
                searchRequest = value;
			}
		}

        private EntityDAO<SearchBreadCrumb> searchBreadCrumb = null;
        public EntityDAO<SearchBreadCrumb> SearchBreadCrumb
        {
            get
            {
                if (searchBreadCrumb == null)
                    searchBreadCrumb = new EntityDAO<SearchBreadCrumb>(this, new SearchBreadCrumb(), "SearchId");
                return searchBreadCrumb;
            }

            set
            {
                searchBreadCrumb = value;
            }
        }

        private EntityDAO<SearchResult> searchResult = null;
        public EntityDAO<SearchResult> SearchResult
        {
            get
            {
                if (searchResult == null)
                    searchResult = new EntityDAO<SearchResult>(this, new SearchResult(), "SearchId");
                return searchResult;
            }

            set
            {
                searchResult = value;
            }
        }

        private EntityDAO<SearchResultItem> searchResultItem = null;
        public EntityDAO<SearchResultItem> SearchResultItem
        {
            get
            {
                if (searchResultItem == null)
                    searchResultItem = new EntityDAO<SearchResultItem>(this, new SearchResultItem(), "SearchId");
                return searchResultItem;
            }

            set
            {
                searchResultItem = value;
            }
        }

        private EntityDAO<Coupon> coupon = null;
        public EntityDAO<Coupon> Coupon
        {
            get
            {
                if (coupon == null)
                    coupon = new EntityDAO<Coupon>(this, new Coupon(), "Code");
                return coupon;
            }

            set
            {
                coupon = value;
            }
        }

        private EntityDAO<CouponTrx> couponTrx = null;
        public EntityDAO<CouponTrx> CouponTrx
        {
            get
            {
                if (couponTrx == null)
                    couponTrx = new EntityDAO<CouponTrx>(this, new CouponTrx(), "Code");
                return couponTrx;
            }

            set
            {
                couponTrx = value;
            }
        }

        private EntityDAO<SelectFields> selectFields = null;
		public EntityDAO<SelectFields> SelectFields
		{
			get
			{
				if (selectFields == null)
					selectFields = new EntityDAO<SelectFields>(this, new SelectFields(), "ID");
				return selectFields;
			}

			set
			{
				selectFields = value;
			}
		}

		private EntityDAO<WebStat> webStat = null;
		public EntityDAO<WebStat> WebStat
		{
			get
			{
				if (webStat == null)
					webStat = new EntityDAO<WebStat>(this, new WebStat(), "Customer");
				return webStat;
			}

			set
			{
				webStat = value;
			}
		}

		private EntityDAO<SalesOrderItem> salesOrderItem = null;
		public EntityDAO<SalesOrderItem> SalesOrderItem
		{
			get
			{
				if (salesOrderItem == null)
					salesOrderItem = new EntityDAO<SalesOrderItem>(this, new SalesOrderItem(), "SOItem");
				return salesOrderItem;
			}

			set
			{
				salesOrderItem = value;
			}
		}

		private EntityDAO<Customer> customer = null;
		public EntityDAO<Customer> Customer
		{
			get
			{
				if (customer == null)
					customer = new EntityDAO<Customer>(this, new Customer(), "CustomerNo");
				return customer;
			}

			set
			{
				customer = value;
			}
		}

		private EntityDAO<ProductAvailability> productAvailability = null;
		public EntityDAO<ProductAvailability> ProductAvailability
		{
			get
			{
				if (productAvailability == null)
					productAvailability = new EntityDAO<ProductAvailability>(this, new ProductAvailability(), "Product");
				return productAvailability;
			}

			set
			{
				productAvailability = value;
			}
		}

		private EntityDAO<ShoppingCart> shoppingCart = null;
		public EntityDAO<ShoppingCart> ShoppingCart
		{
			get
			{
				if (shoppingCart == null)
					shoppingCart = new EntityDAO<ShoppingCart>(this, new ShoppingCart(), "Cookie");
				return shoppingCart;
			}

			set
			{
				shoppingCart = value;
			}
		}

		private EntityDAO<SortFields> sortFields = null;
		public EntityDAO<SortFields> SortFields
		{
			get
			{
				if (sortFields == null)
					sortFields = new EntityDAO<SortFields>(this, new SortFields(), "SortKey");
				return sortFields;
			}

			set
			{
				sortFields = value;
			}
		}

		private EntityDAO<ProductSpecification> productSpecification = null;
		public EntityDAO<ProductSpecification> ProductSpecification
		{
			get
			{
				if (productSpecification == null)
					productSpecification = new EntityDAO<ProductSpecification>(this, new ProductSpecification(), "Product");
				return productSpecification;
			}

			set
			{
				productSpecification = value;
			}
		}

		private EntityDAO<Variation> variation = null;
		public EntityDAO<Variation> Variation
		{
			get
			{
				if (variation == null)
					variation = new EntityDAO<Variation>(this, new Variation(), "Product");
				return variation;
			}

			set
			{
				variation = value;
			}
		}

		private EntityDAO<ProductRanking> productRanking = null;
		public EntityDAO<ProductRanking> ProductRanking
		{
			get
			{
				if (productRanking == null)
					productRanking = new EntityDAO<ProductRanking>(this, new ProductRanking(), "Product");
				return productRanking;
			}

			set
			{
				productRanking = value;
			}
		}

		private EntityDAO<Details> details = null;
		public EntityDAO<Details> Details
		{
			get
			{
				if (details == null)
					details = new EntityDAO<Details>(this, new Details(), "Product");
				return details;
			}

			set
			{
				details = value;
			}
		}

		private EntityDAO<WishList> wishList = null;
		public EntityDAO<WishList> WishList
		{
			get
			{
				if (wishList == null)
					wishList = new EntityDAO<WishList>(this, new WishList(), "Code");
				return wishList;
			}

			set
			{
				wishList = value;
			}
		}

		private EntityDAO<UserTotals> userTotals = null;
		public EntityDAO<UserTotals> UserTotals
		{
			get
			{
				if (userTotals == null)
					userTotals = new EntityDAO<UserTotals>(this, new UserTotals(), "Sourceurl");
				return userTotals;
			}

			set
			{
				userTotals = value;
			}
		}

		private EntityDAO<RecentlyViewed> recentlyViewed = null;
		public EntityDAO<RecentlyViewed> RecentlyViewed
		{
			get
			{
				if (recentlyViewed == null)
					recentlyViewed = new EntityDAO<RecentlyViewed>(this, new RecentlyViewed(), "Cookie");
				return recentlyViewed;
			}

			set
			{
				recentlyViewed = value;
			}
		}

		private EntityDAO<WishListItem> wishListItem = null;
		public EntityDAO<WishListItem> WishListItem
		{
			get
			{
				if (wishListItem == null)
					wishListItem = new EntityDAO<WishListItem>(this, new WishListItem(), "Code");
				return wishListItem;
			}

			set
			{
				wishListItem = value;
			}
		}

		private EntityDAO<PurchaseOrderItem> purchaseOrderItem = null;
		public EntityDAO<PurchaseOrderItem> PurchaseOrderItem
		{
			get
			{
				if (purchaseOrderItem == null)
					purchaseOrderItem = new EntityDAO<PurchaseOrderItem>(this, new PurchaseOrderItem(), "PurchaseOrder");
				return purchaseOrderItem;
			}

			set
			{
				purchaseOrderItem = value;
			}
		}

        private EntityDAO<CheckoutTrx> checkoutTrx = null;
        public EntityDAO<CheckoutTrx> CheckoutTrx
        {
            get
            {
                if (checkoutTrx == null)
                    checkoutTrx = new EntityDAO<CheckoutTrx>(this, new CheckoutTrx(), "Customer");
                return checkoutTrx;
            }

            set
            {
                checkoutTrx = value;
            }
        }

        private EntityDAO<CreditCard> creditCard = null;
		public EntityDAO<CreditCard> CreditCard
		{
			get
			{
				if (creditCard == null)
					creditCard = new EntityDAO<CreditCard>(this, new CreditCard(), "CardID");
				return creditCard;
			}

			set
			{
				creditCard = value;
			}
		}

		private EntityDAO<ProductCategories> productCategories = null;
		public EntityDAO<ProductCategories> ProductCategories
		{
			get
			{
				if (productCategories == null)
					productCategories = new EntityDAO<ProductCategories>(this, new ProductCategories(), "Product");
				return productCategories;
			}

			set
			{
				productCategories = value;
			}
		}

		private EntityDAO<CustomerReview> customerReview = null;
		public EntityDAO<CustomerReview> CustomerReview
		{
			get
			{
				if (customerReview == null)
					customerReview = new EntityDAO<CustomerReview>(this, new CustomerReview(), "Customer");
				return customerReview;
			}

			set
			{
				customerReview = value;
			}
		}

		private EntityDAO<StateCode> stateCode = null;
		public EntityDAO<StateCode> StateCode
		{
			get
			{
				if (stateCode == null)
					stateCode = new EntityDAO<StateCode>(this, new StateCode(), "Code");
				return stateCode;
			}

			set
			{
				stateCode = value;
			}
		}

		private EntityDAO<DataFeed> dataFeed = null;
		public EntityDAO<DataFeed> DataFeed
		{
			get
			{
				if (dataFeed == null)
					dataFeed = new EntityDAO<DataFeed>(this, new DataFeed(), "Name");
				return dataFeed;
			}

			set
			{
				dataFeed = value;
			}
		}

		private EntityDAO<Company> company = null;
		public EntityDAO<Company> Company
		{
			get
			{
				if (company == null)
					company = new EntityDAO<Company>(this, new Company(), "Name");
				return company;
			}

			set
			{
				company = value;
			}
		}

		private EntityDAO<EMailList> eMailList = null;
		public EntityDAO<EMailList> EMailList
		{
			get
			{
				if (eMailList == null)
					eMailList = new EntityDAO<EMailList>(this, new EMailList(), "Email");
				return eMailList;
			}

			set
			{
				eMailList = value;
			}
		}

		private EntityDAO<SourceURLItem> sourceURLItem = null;
		public EntityDAO<SourceURLItem> SourceURLItem
		{
			get
			{
				if (sourceURLItem == null)
					sourceURLItem = new EntityDAO<SourceURLItem>(this, new SourceURLItem(), "Name");
				return sourceURLItem;
			}

			set
			{
				sourceURLItem = value;
			}
		}

		private EntityDAO<GroupType> groupType = null;
		public EntityDAO<GroupType> GroupType
		{
			get
			{
				if (groupType == null)
					groupType = new EntityDAO<GroupType>(this, new GroupType(), "Name");
				return groupType;
			}

			set
			{
				groupType = value;
			}
		}

		private EntityDAO<Specification> specification = null;
		public EntityDAO<Specification> Specification
		{
			get
			{
				if (specification == null)
					specification = new EntityDAO<Specification>(this, new Specification(), "Name");
				return specification;
			}

			set
			{
				specification = value;
			}
		}

		private EntityDAO<Product> product = null;
		public EntityDAO<Product> Product
		{
			get
			{
				if (product == null)
					product = new EntityDAO<Product>(this, new Product(), "ProductNo");
				return product;
			}

			set
			{
				product = value;
			}
		}

		private EntityDAO<Availability> availability = null;
		public EntityDAO<Availability> Availability
		{
			get
			{
				if (availability == null)
					availability = new EntityDAO<Availability>(this, new Availability(), "Code");
				return availability;
			}

			set
			{
				availability = value;
			}
		}

		private EntityDAO<SiteTheme> siteTheme = null;
		public EntityDAO<SiteTheme> SiteTheme
		{
			get
			{
				if (siteTheme == null)
					siteTheme = new EntityDAO<SiteTheme>(this, new SiteTheme(), "Name");
				return siteTheme;
			}

			set
			{
				siteTheme = value;
			}
		}

		private EntityDAO<Link> link = null;
		public EntityDAO<Link> Link
		{
			get
			{
				if (link == null)
					link = new EntityDAO<Link>(this, new Link(), "Url");
				return link;
			}

			set
			{
				link = value;
			}
		}

		private EntityDAO<Carrier> carrier = null;
		public EntityDAO<Carrier> Carrier
		{
			get
			{
				if (carrier == null)
					carrier = new EntityDAO<Carrier>(this, new Carrier(), "Code");
				return carrier;
			}

			set
			{
				carrier = value;
			}
		}

        private EntityDAO<FeaturedGroup> featuredGroup = null;
        public EntityDAO<FeaturedGroup> FeaturedGroup
        {
            get
            {
                if (featuredGroup == null)
                    featuredGroup = new EntityDAO<FeaturedGroup>(this, new FeaturedGroup(), "Name");
                return featuredGroup;
            }

            set
            {
                featuredGroup = value;
            }
        }

        private EntityDAO<FeaturedProducts> featuredProducts = null;
        public EntityDAO<FeaturedProducts> FeaturedProducts
        {
            get
            {
                if (featuredProducts == null)
                    featuredProducts = new EntityDAO<FeaturedProducts>(this, new FeaturedProducts(), "Name");
                return featuredProducts;
            }

            set
            {
                featuredProducts = value;
            }
        }

        private EntityDAO<Catalog> catalog = null;
		public EntityDAO<Catalog> Catalog
		{
			get
			{
				if (catalog == null)
					catalog = new EntityDAO<Catalog>(this, new Catalog(), "Name");
				return catalog;
			}

			set
			{
				catalog = value;
			}
		}

		private EntityDAO<CountryCode> countryCode = null;
		public EntityDAO<CountryCode> CountryCode
		{
			get
			{
				if (countryCode == null)
					countryCode = new EntityDAO<CountryCode>(this, new CountryCode(), "Code");
				return countryCode;
			}

			set
			{
				countryCode = value;
			}
		}

		private EntityDAO<Note> note = null;
		public EntityDAO<Note> Note
		{
			get
			{
				if (note == null)
                    note = new EntityDAO<Note>(this, new Note(), "Referencenumber");
				return note;
			}

			set
			{
				note = value;
			}
		}

		private EntityDAO<SimilarProduct> similarProduct = null;
		public EntityDAO<SimilarProduct> SimilarProduct
		{
			get
			{
				if (similarProduct == null)
					similarProduct = new EntityDAO<SimilarProduct>(this, new SimilarProduct(), "Product");
				return similarProduct;
			}

			set
			{
				similarProduct = value;
			}
		}

		private EntityDAO<RelatedProduct> relatedProduct = null;
		public EntityDAO<RelatedProduct> RelatedProduct
		{
			get
			{
				if (relatedProduct == null)
					relatedProduct = new EntityDAO<RelatedProduct>(this, new RelatedProduct(), "ProductNo");
				return relatedProduct;
			}

			set
			{
				relatedProduct = value;
			}
		}

		private EntityDAO<Manufacturer> manufacturer = null;
		public EntityDAO<Manufacturer> Manufacturer
		{
			get
			{
				if (manufacturer == null)
					manufacturer = new EntityDAO<Manufacturer>(this, new Manufacturer(), "Name");
				return manufacturer;
			}

			set
			{
				manufacturer = value;
			}
		}

		private EntityDAO<Users> users = null;
		public EntityDAO<Users> Users
		{
			get
			{
				if (users == null)
                    users = new EntityDAO<Users>(this, new Users(), "Email");
				return users;
			}

			set
			{
				users = value;
			}
		}

		private EntityDAO<ProviderConfiguration> providerConfiguration = null;
		public EntityDAO<ProviderConfiguration> ProviderConfiguration
		{
			get
			{
				if (providerConfiguration == null)
					providerConfiguration = new EntityDAO<ProviderConfiguration>(this, new ProviderConfiguration(), "ApplicationName");
				return providerConfiguration;
			}

			set
			{
				providerConfiguration = value;
			}
		}

		private EntityDAO<Distributor> distributor = null;
		public EntityDAO<Distributor> Distributor
		{
			get
			{
				if (distributor == null)
					distributor = new EntityDAO<Distributor>(this, new Distributor(), "Name");
				return distributor;
			}

			set
			{
				distributor = value;
			}
		}

		private EntityDAO<SalesTax> salesTax = null;
		public EntityDAO<SalesTax> SalesTax
		{
			get
			{
				if (salesTax == null)
					salesTax = new EntityDAO<SalesTax>(this, new SalesTax(), "State");
				return salesTax;
			}

			set
			{
				salesTax = value;
			}
		}

		private EntityDAO<GiftCertificate> giftCertificate = null;
		public EntityDAO<GiftCertificate> GiftCertificate
		{
			get
			{
				if (giftCertificate == null)
					giftCertificate = new EntityDAO<GiftCertificate>(this, new GiftCertificate(), "Code");
				return giftCertificate;
			}

			set
			{
				giftCertificate = value;
			}
		}

		private EntityDAO<Address> address = null;
		public EntityDAO<Address> Address
		{
			get
			{
				if (address == null)
					address = new EntityDAO<Address>(this, new Address(), "AddressID");
				return address;
			}

			set
			{
				address = value;
			}
		}

		private EntityDAO<Invoice> invoice = null;
		public EntityDAO<Invoice> Invoice
		{
			get
			{
				if (invoice == null)
					invoice = new EntityDAO<Invoice>(this, new Invoice(), "InvoiceID");
				return invoice;
			}

			set
			{
				invoice = value;
			}
		}

		private EntityDAO<Packingslip> packingslip = null;
		public EntityDAO<Packingslip> Packingslip
		{
			get
			{
				if (packingslip == null)
                    packingslip = new EntityDAO<Packingslip>(this, new Packingslip(), "PackingSlipId");
				return packingslip;
			}

			set
			{
				packingslip = value;
			}
		}

		private EntityDAO<PackingslipItem> packingslipItem = null;
		public EntityDAO<PackingslipItem> PackingslipItem
		{
			get
			{
				if (packingslipItem == null)
					packingslipItem = new EntityDAO<PackingslipItem>(this, new PackingslipItem(), "PackingSlip");
				return packingslipItem;
			}

			set
			{
				packingslipItem = value;
			}
		}

		private EntityDAO<AffiliateProgram> affiliateProgram = null;
		public EntityDAO<AffiliateProgram> AffiliateProgram
		{
			get
			{
				if (affiliateProgram == null)
					affiliateProgram = new EntityDAO<AffiliateProgram>(this, new AffiliateProgram(), "Name");
				return affiliateProgram;
			}

			set
			{
				affiliateProgram = value;
			}
		}

		private EntityDAO<Affiliate> affiliate = null;
		public EntityDAO<Affiliate> Affiliate
		{
			get
			{
				if (affiliate == null)
					affiliate = new EntityDAO<Affiliate>(this, new Affiliate(), "Name");
				return affiliate;
			}

			set
			{
				affiliate = value;
			}
		}

		private EntityDAO<AffiliateCommission> affiliateCommission = null;
		public EntityDAO<AffiliateCommission> AffiliateCommission
		{
			get
			{
				if (affiliateCommission == null)
					affiliateCommission = new EntityDAO<AffiliateCommission>(this, new AffiliateCommission(), "ID");
				return affiliateCommission;
			}

			set
			{
				affiliateCommission = value;
			}
		}

		private EntityDAO<CCTransaction> cCTransaction = null;
		public EntityDAO<CCTransaction> CCTransaction
		{
			get
			{
				if (cCTransaction == null)
					cCTransaction = new EntityDAO<CCTransaction>(this, new CCTransaction(), "Trx");
				return cCTransaction;
			}

			set
			{
				cCTransaction = value;
			}
		}

		private EntityDAO<AffiliateCommissionLevel> affiliateCommissionLevel = null;
		public EntityDAO<AffiliateCommissionLevel> AffiliateCommissionLevel
		{
			get
			{
				if (affiliateCommissionLevel == null)
					affiliateCommissionLevel = new EntityDAO<AffiliateCommissionLevel>(this, new AffiliateCommissionLevel(), "ID");
				return affiliateCommissionLevel;
			}

			set
			{
				affiliateCommissionLevel = value;
			}
		}

        private EntityDAO<Keywords> keywords = null;
        public EntityDAO<Keywords> Keywords
        {
            get
            {
                if (keywords == null)
                    keywords = new EntityDAO<Keywords>(this, new Keywords(), "ProductNo");
                return keywords;
            }

            set
            {
                keywords = value;
            }
        }

        private EntityDAO<SiteMapEntry> siteMapEntry = null;
        public EntityDAO<SiteMapEntry> SiteMapEntry
        {
            get
            {
                if (siteMapEntry == null)
                    siteMapEntry = new EntityDAO<SiteMapEntry>(this, new SiteMapEntry(), "ID");
                return siteMapEntry;
            }

            set
            {
                siteMapEntry = value;
            }
        }
    }
}
