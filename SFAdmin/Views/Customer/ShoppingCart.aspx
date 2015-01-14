<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<ShoppingCartViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Shopping Cart</h1>
    <h3>User ID:</h3><%=ViewData.Model.UserID%>
    <div class="lists">
        <table class="listtable">
            <tr>
                <th>Product</th>
                <th>Unit Price</th>
                <th>Quantity</th>
                <th>Price</th>
                <th>Date Added</th>
            </tr>
            <%
            double totalPrice = 0.0;
            bool productsfound = false;
            foreach (ShoppingCartListItem shoppingCartItem in ViewData.Model.shoppingCart)
            {
                productsfound = true;
                totalPrice += (shoppingCartItem.OurPrice * shoppingCartItem.Quantity);
            %>
            <tr>
                <td>
                    <a href="/Products/ProductMaint/<%=shoppingCartItem.Product%>"><%=shoppingCartItem.Name%></a>
                    <br/><span>Product number: <%=shoppingCartItem.Product%>, <%=shoppingCartItem.Availability%></span>
                </td>
                <td><%=string.Format("{0:#,#.00}", shoppingCartItem.OurPrice)%></td>
                <td><%=string.Format("{0:#.00}", shoppingCartItem.Quantity)%></td>
                <td><%=string.Format("{0:#,#.00}", shoppingCartItem.OurPrice * shoppingCartItem.Quantity)%></td>
                <td><%=string.Format("{0:MM/dd/yyyy}", shoppingCartItem.AddedDate)%></td>
            </tr>
            <tr>
            <%}if(productsfound){%>
                <td colspan="2" />
                <td>Total</td>
                <td><%=string.Format("{0:#,#.00}", totalPrice)%></td>
            <%}else{%>
                <td colspan="5" >There are no products in the shopping cart.</td>
            <%}%>
            </tr>
        </table>
    </div>
</asp:Content>
