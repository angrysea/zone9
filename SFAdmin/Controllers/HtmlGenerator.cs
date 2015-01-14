using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using SFAdmin.Models;
using SFAdmin.Aspects;
using StorefrontModel;

namespace SFAdmin.Controllers
{
    public class HtmlGenerator
    {
        [LogMethodCall]
        static public string GetListProducts(StorefrontEntities context, string manufacturer)
        {
            List<ListProduct> products = null;
            StringBuilder builder = new StringBuilder();
            if (manufacturer != null && manufacturer.Length > 0 && manufacturer != "all")
            {
                products = context.ListProduct.Execute("GetProductListByManufacturer",
                                                        "manufacturer",
                                                        manufacturer);
            }
            else
            {
                products = context.ListProduct.Execute("GetProductList");
            }

            if(products.Count>0)
            {
                string rbformat = "<td><input class='radioStyle' type='checkbox' id='{0}' name='product{0}' />{1}{2}</td>";
                string imgformat = "<td><img src='/Content/images/products/{0}' border='0'/></td></tr>";
                builder.Append("<br/><table>");
                builder.Append("<tr><td colspan='3'><h3>Select Products</h3></td></tr>");
                foreach (ListProduct product in products)
                {
                    builder.Append("<tr><td><img src='/Content/images/blank.gif' width='120px' height='1px' border='0'/></td>");
                    builder.AppendFormat(rbformat, product.ProductNo, product.Manufacturer, product.Name);
                    builder.AppendFormat(imgformat, product.ImageURLSmall);
                }
                builder.Append("</table>");
            }
            return builder.ToString();
        }


        [LogMethodCall]
        static public string GetRadioListProducts(StorefrontEntities context, string manufacturer)
        {
            List<ListProduct> products = null;
            StringBuilder builder = new StringBuilder();

            string selformat = "<label for='{0}'>Product:</label><select id='{0}' name='{0}'>\r\n" +
                                "<option value=''>< Select ></option>\r\n{1}\r\n </select>";
            string optformat = "\t<option value='{0}'>{1}{2} <img src='/Content/images/products/{3}' " +
                                "border='0'/></option>\r\n";

            if (manufacturer != null && manufacturer.Length > 0 && manufacturer != "all")
            {
                string[] parameters = { "manufacturer" };
                string[] values = { manufacturer };
                products = context.ListProduct.Execute("GetListProductsByManufacturer",
                                                 parameters,
                                                 values);
                foreach (ListProduct product in products)
                {
                    builder.AppendFormat(optformat, product.ProductNo, product.Manufacturer, product.Name, product.ImageURLSmall);
                }
            }
            if (builder.ToString().Length > 0)
            {
                return string.Format(selformat, "featuredproduct", builder.ToString());
            }
            else
            {
                return "<label for='featuredproduct'>Product:</label><select id='featuredproduct' name='featuredproduct'>" +
                    "<option value=''>< Select Manufacturer ></option></select>";
            }
        }

    }
}
