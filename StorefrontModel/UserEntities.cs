// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
    public partial class Entities
    {
        private EntityDAO<ProductNoListItem> productNoListItem = null;
        public EntityDAO<ProductNoListItem> ProductNoListItem
        {
            get
            {
                if (productNoListItem == null)
                    productNoListItem = new EntityDAO<ProductNoListItem>(this, new ProductNoListItem(), "ProductNo");
                return productNoListItem;
            }

            set
            {
                productNoListItem = value;
            }
        }

        private EntityDAO<ListProduct> listProduct = null;
        public EntityDAO<ListProduct> ListProduct
        {
            get
            {
                if (listProduct == null)
                    listProduct = new EntityDAO<ListProduct>(this, new ListProduct(), "ProductNo");
                return listProduct;
            }

            set
            {
                listProduct = value;
            }
        }

        private EntityDAO<ItemsInCart> itemsInCart = null;
        public EntityDAO<ItemsInCart> ItemsInCart
        {
            get
            {
                if (itemsInCart == null)
                    itemsInCart = new EntityDAO<ItemsInCart>(this, new ItemsInCart(), "Cookie");
                return itemsInCart;
            }

            set
            {
                itemsInCart = value;
            }
        }
 
        private EntityDAO<ShoppingCartListItem> shoppingCartListItem = null;
        public EntityDAO<ShoppingCartListItem> ShoppingCartListItem
        {
            get
            {
                if (shoppingCartListItem == null)
                    shoppingCartListItem = new EntityDAO<ShoppingCartListItem>(this, new ShoppingCartListItem(), "ID");
                return shoppingCartListItem;
            }

            set
            {
                shoppingCartListItem = value;
            }
        }

        private EntityDAO<SearchListItem> searchListItem = null;
        public EntityDAO<SearchListItem> SearchListItem
        {
            get
            {
                if (searchListItem == null)
                    searchListItem = new EntityDAO<SearchListItem>(this, new SearchListItem(), "ProductNo");
                return searchListItem;
            }

            set
            {
                searchListItem = value;
            }
        }

        private EntityDAO<TotalCharges> totalCharges = null;
        public EntityDAO<TotalCharges> TotalCharges
        {
            get
            {
                if (totalCharges == null)
                    totalCharges = new EntityDAO<TotalCharges>(this, new TotalCharges(), "");
                return totalCharges;
            }

            set
            {
                totalCharges = value;
            }
        }

        private EntityDAO<SalesOrderIdList> salesOrderIdList = null;
        public EntityDAO<SalesOrderIdList> SalesOrderIdList
        {
            get
            {
                if (salesOrderIdList == null)
                    salesOrderIdList = new EntityDAO<SalesOrderIdList>(this, new SalesOrderIdList(), "");
                return salesOrderIdList;
            }

            set
            {
                salesOrderIdList = value;
            }
        }
       private EntityDAO<NoteCount> noteCount = null;
       public EntityDAO<NoteCount> NoteCount
        {
            get
            {
                if (noteCount == null)
                    noteCount = new EntityDAO<NoteCount>(this, new NoteCount(), "ReferenceNo");
                return noteCount;
            }

            set
            {
                noteCount = value;
            }
        }
    }
}
