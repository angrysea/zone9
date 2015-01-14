<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DetailsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company;
    SiteTheme theme = ViewData.Model.theme;
    Product product = ViewData.Model.product;
    Details details = ViewData.Model.details; 
%>
    <script src="/Scripts/lytebox.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            initLytebox();
        });
    </script>

    <form id="form1" action="/Home/AddToCart" method="post">
    <input type="hidden" name="product" value="<%=product.ProductNo%>" />
	<div class="form-container">
	    <div class="details">
                <span  class="title"><%=product.Manufacturer%>: </span><span class="text"><%=product.Name%></span>
                <br />
                <span class="title">Product: </span><span class="text"><%=product.ProductNo%></span>
                <br />
            
            <div class="dtlsleft">
                <%if (details.ImageURLLarge != null && details.ImageURLLarge.Length > 0){%>
                <a rel="lytebox" title="<%=details.ImageURLLarge%>" href="/Content/images/products/<%=details.ImageURLLarge%>">
                    <img class="productimage" src="/Content/images/products/<%=details.ImageURLMedium%>" />
                </a>
                <%}else{%>
                <img src="/Content/images/products/<%=details.ImageURLMedium%>" />
                <%}%>
            </div>

            <div class="dtlsright">
                <span class="title">List Price: </span><span><del><%=string.Format("{0:$#,#.00}", product.ListPrice)%></del></span>
                <br />
                <span class="title">Your Price: </span><span><font color="red"><%=string.Format("{0:$#,#.00}", product.OurPrice)%></font></span>
                <br />
                <span class="title">Availability: </span><span class="text"><%=ViewData.Model.availability.Description%></span>
                <br />
                <br />
                <input class="button" type="submit" value="Add To Cart" id="AddToCart" />
            </div>
            <br />
            
            <span class="title">Status: </span>
            <span class="text"><%=ViewData.Model.availability.Description%></span>
            <br />
            <span class="title">Quantity in Stock: </span>
            <span class="text"><%=product.Quantity%></span>
            
            <%if (details.Description != null && details.Description.Length > 0){%>
            <br />
            <br />
            <span class="title">Product Description:</span>
            <br />        
            <span><%=details.Description%></span>
            <%}%>
            <br />
            <br />
            <span class="title">Specifications:</span><br />
            <table class="lists">
            <%foreach (ProductSpecification productSpecification in ViewData.Model.productSpecifications){%>
                <tr>
                    <td><span class="title"><%=productSpecification.Name%>:</span></td>
                    <td><span class="text"><%=productSpecification.Value%></span></td>
                </tr>
            <%}%>
            </table>
            <br />
            
            <div>
            <%foreach(ListProduct similarproduct in ViewData.Model.similarProducts) {%>
            <div>
                <a href="/Product/Index/<%=similarproduct.ProductNo%>">
                    <img 
                        alt="<%=company.Keyword%> - <%=similarproduct.Manufacturer%> <%=similarproduct.Name%>" 
                        src="/Content/images/products/<%=similarproduct.ImageURLSmall%>" 
                        class="productimage"/>
                </a>
                <br />
                <span><%=similarproduct.Manufacturer%> <%=similarproduct.Name%></span>
                <br />
                <span class="strike"><%=string.Format("{0:#,#.00}", similarproduct.ListPrice)%></span>
                <span class="red"><%=string.Format("{0:#,#.00}", similarproduct.OurPrice)%></span>
                <span><%=similarproduct.Description%></span>
            </div>
            <%}%>
            </div>
            <div class="manufacturer">
                <span  class="title">About <%=ViewData.Model.manufacturer.LongName%></span>
                <br />
                <span><%=ViewData.Model.manufacturer.Description%></span>
            </div>                    
	        <br />
    </div>
    </div>
	</form>
    <div class="indexstripe">
        <span class="title">Recently Viewed Products</span>
    </div>
    <div class="stripe large">
        <%
            int i = 0;
            foreach (ListProduct recentlyViewed in ViewData.Model.recentlyViewed)
            {
                i++;
                if (i > theme.RecentlyViewedCount)
                    break;
        %>
                <div class="featuredproduct">
                    <a href="/Product/Index/<%=recentlyViewed.ProductNo%>">
                        <img 
                            alt="<%=company.Keyword%> - <%=recentlyViewed.Manufacturer%> <%=recentlyViewed.Name%>" 
                            src="/Content/images/products/<%=recentlyViewed.ImageURLSmall%>" 
                             class="productimage"/>
                    </a>
                    <span class="featuredtitle"><%=recentlyViewed.Manufacturer%> <%=recentlyViewed.Name%></span>
	                <span class="strike"><%= string.Format("{0:$#,#.00}", recentlyViewed.ListPrice)%></span>
	                <span class="red"><%= string.Format("{0:$#,#.00}", recentlyViewed.OurPrice)%></span>
                </div>
        <%
            }
        %>
    </div>
</asp:Content>
