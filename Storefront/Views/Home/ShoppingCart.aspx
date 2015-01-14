<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ShoppingCartViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme;
%>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        $("#Checkout").click(function(e) {
            e.preventDefault();
            window.location = "/Checkout/Index/<%=ViewData.Model.cookie%>";
        });

        $("#updateqty").click(function(e) {
            $("#Cart").submit();
        });
    });
</script>
    <form id="Cart" action="/Home/UpdateQuantity" method="post">
    <input name="cookie" type="hidden" value="<%=ViewData.Model.cookie%>" />
	<div class="shoppingcart">
        <%IList<string> errors = ViewData.Model.errors; 
        if (errors != null) {%>
        <div class="stripe">
        <ul class="error">  
        <% foreach (string error in errors) { %>
            <li><%= Html.Encode(error) %></li>
        <%}%>
        </ul>
        </div>
        <%}%>
        
        <span class="title">Shopping Cart</span> 

        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <br />
        <%if (ViewData.Model.shoppingCart.Count > 0){%>
        
        <table class="lists">
            <tr>
                <th>Product</th>
                <th class="numericth">Unit Price</th>
                <th class="numericth">Quantity</th>
                <th class="numericth">Price</th>
                <th>Date Added</th>
            </tr>
            <%
            double totalPrice = 0.0;
            foreach (ShoppingCartListItem shoppingCart in ViewData.Model.shoppingCart)
            {
                totalPrice += (shoppingCart.OurPrice * shoppingCart.Quantity);
            %>
            <tr>
                <td>
                    <a title="<%=company.Keyword%> - <%=shoppingCart.Product%> <%=shoppingCart.Name%>" href="/Product/Index/<%=shoppingCart.Product%>">
                    <img class="productimage" 
                        alt="<%=company.Keyword%> - <%=shoppingCart.Product%> <%=shoppingCart.Name%>" 
                        src="/Content/images/products/<%=shoppingCart.ImageURLSmall%>" />
                    </a>
                </td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", shoppingCart.OurPrice)%></td>
                <td class="numerictd"><input class="qty" name="qty_<%=shoppingCart.Product%>" value="<%=string.Format("{0:#}", shoppingCart.Quantity)%>"/></td>
                <td class="numerictd"><%=string.Format("{0:$#,#.00}", shoppingCart.OurPrice * shoppingCart.Quantity)%></td>
                <td><%=string.Format("{0:MM/dd/yyyy}", shoppingCart.AddedDate)%></td>
                <td><a class="nounderline" href="/Home/RemoveFromCart/<%=shoppingCart.Product%>">remove</a></td>
            </tr>
            <tr>
                <td colspan="4">
                    <a href="/Product/Index/<%=shoppingCart.Product%>"><%=shoppingCart.Name%></a>
                    <br/>
                    <span>Product number: </span><span class="text"><%=shoppingCart.Product%>, <%=shoppingCart.Availability%></span>
                    <input type="hidden" id="product"+<%=shoppingCart.Product%> value="<%=shoppingCart.Product%>"/>
                </td>
            </tr>
            <tr>
                <td><input type="hidden" name="<%=shoppingCart.Product%>" value="<%=string.Format("{0:#}", shoppingCart.Quantity)%>"/><br /></td>
            </tr>
            <%}%>
            <tr>
                <td>Total</td>
                <td class="numerictd"><%=string.Format("{0:$#,#.00}", totalPrice)%></td>
                <td />
                <td colspan="3" class="numerictd"><a id="updateqty" href="#">Update Quantities</a></td>
            </tr>
            <tr>
                <td><a href="/Home/Index">Continue Shopping</a></td>
            </tr>
            <tr>
                <td colspan="3" />
                <td colspan="3"><input class="button" type="submit" value="Checkout" id="Checkout" /></td>
            </tr>
        </table>
        <%}else{%>
        <div class="indexstripe">
            <span>There are no items in the shopping cart.</span>
        </div>
        <%}%>
        <div class="indexstripe">
            <div class="left">
                <%if(company.FreeShipping > 0){%>
                    <div class="freeshipping">
                        <div class="freeshippingtitle">Free Shipping</div>
                        <img src="/Content/images/box.gif" />
                        <div>(on orders of<br /> $<%=string.Format("{0:#,#}", company.FreeShippingMin)%> or more)</div>
                    </div>
                <%}%>
            </div>
            <div class="cartright">
                <span class="title">About the Shopping Cart: </span><br />
                <span class="text">Items in your Shopping Cart always reflect the price when added. Items remain in your Shopping Cart for 90 days.</span>
            </div>
        </div>            
    <br />
    <div class="indexstripe">
        <span class="title">Recently Viewed Products</span>
    </div>
    <div class="stripe">
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

    </div>
    </form>
</asp:Content>
