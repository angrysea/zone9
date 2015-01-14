<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CheckoutViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme;
    Customer customer = ViewData.Model.customer;
    CreditCard creditCard = ViewData.Model.CreditCard;
    Address shippingAddress = ViewData.Model.ShippingAddress;
    Address billingAddress = ViewData.Model.BillingAddress;
%>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        initNavigation("/Checkout/Index");
        $("#Procced").click(function(e) {
            e.preventDefault();
            window.location = "/Checkout/Authorize/<%=ViewData.Model.customer.CustomerNo%>";
        });
        $("#Cancel").click(function(e) {
            window.location = "/Checkout/CancelCheckout/<%=customer.CustomerNo%>";
        });

    });
</script>

<%IList<string> errors = ViewData.Model.errors; if (errors != null) {%>
    <div>
        <ul class="error">
        <% foreach (string error in errors) { %>
            <li><%= Html.Encode(error) %></li>
        <%}%>
        </ul>
    </div>
<%}%>

<form id="Cart" action="" method="post">
<input name="cookie" type="hidden" value="<%=ViewData.Model.cookie%>" />
    <div class="somain">
        <div class="salesorderstripe">
            <h3>Shipping Information</h3>
            <div class="salesorderCol">
                <span><%=shippingAddress.FullName%></span>
                <span><%=shippingAddress.Address1%></span>
                <%if (shippingAddress.Address2 != null && shippingAddress.Address2.Length > 0){%>
                <span><%=shippingAddress.Address2%></span>
                <% } if (shippingAddress.Address3 != null && shippingAddress.Address3.Length > 0){%>
                <span><%=shippingAddress.Address3%></span><%}%>
                <span><%=shippingAddress.City%>, <%=shippingAddress.State%>, <%=shippingAddress.Zip%></span>
                <span><%=shippingAddress.Country%></span>
                <span>Phone: <%=shippingAddress.Phone%></span>
            </div>
        </div>
        <div class="salesorderstripe">
            <%if (ViewData.Model.shoppingCart.Count > 0){%>
            <table class="narrowlist">
                <tr>
                    <th>Product</th>
                    <th class="numericth">Unit Price</th>
                    <th>Quantity</th>
                    <th class="numericth">Price</th>
                </tr>
                <%
                double totalPrice = 0.0;
                foreach (ShoppingCartListItem shoppingCart in ViewData.Model.shoppingCart)
                {
                    totalPrice += (shoppingCart.OurPrice * shoppingCart.Quantity);
                %>
                <tr>
                    <td>
                        <%=shoppingCart.Name%>
                        <br/>
                        <span>Product number: <%=shoppingCart.Product%>,</span>
                        <span><%=shoppingCart.Availability%></span>
                    </td>
                    <td class="numerictd"><%=string.Format("{0:#,#.00}", shoppingCart.OurPrice)%></td>
                    <td class="numerictd"><%=string.Format("{0:#}", shoppingCart.Quantity)%></td>
                    <td class="numerictd"><%=string.Format("{0:$#,#.00}", shoppingCart.OurPrice * shoppingCart.Quantity)%></td>
                </tr>
                <%}%>
                <tr>
                    <td colspan="2"/>
                    <td>Total</td>
                    <td class="numerictd"><%=string.Format("{0:$#,#.00}", totalPrice)%></td>
                </tr>
                <tr/>
                <tr>
                    <td><a href="/Checkout/CancelCheckout/<%=customer.CustomerNo%>">Cancel</a></td>
                    <td colspan="1" />
                    <td colspan="2" class="numerictd"><a href="/Checkout/UpdateQuantity">Update Quantities</a></td>
                </tr>
                <tr/>
            </table>
            <div class="indexstripe">
                <div class="continue">
                    <a id="Procced" href="#" >Procced</a>
                </div>
            </div>
            <%}else{%>
            <span>There are no items selected for purchase.</span>
            <%}%>
        </div>
    </div>
    <div class="soright">
            <h3>Billing Information</h3>
            <div class="salesorderCol">
                <span><%=billingAddress.FullName%></span>
                <span><%=billingAddress.Address1%></span>
                <%if (billingAddress.Address2 != null && billingAddress.Address2.Length > 0)
                  {%>
                <span><%=billingAddress.Address2%></span>
                <% } if (billingAddress.Address3 != null && billingAddress.Address3.Length > 0)
                  {%>
                <span><%=billingAddress.Address3%></span><%}%>
                <span><%=billingAddress.City%>, <%=billingAddress.State%>, <%=billingAddress.Zip%></span>
                <span><%=billingAddress.Country%></span>
                <span>Phone: <%=billingAddress.Phone%></span>
            </div>
            <div class="salesorderCol">
                <span class="coltitle">Payment Method:</span>
                <span><%=creditCard.Type%>: <%=creditCard.Number%></span>
                <span>Exp: <%=creditCard.Expmonth%>/<%=creditCard.Expyear%></span>
                <span>Card Holder: <%=creditCard.Cardholder%></span>
            </div>
            <div>
            <div class="salesorderCol">
            <%if(company.FreeShipping > 0){%>
                <div class="freeshipping">
                    <div class="freeshippingtitle">Free Shipping</div>
                    <img src="/Content/images/box.gif" />
                    <div>(on orders of<br /> $<%=string.Format("{0:#,#}", company.FreeShippingMin)%> or more)</div>
                </div>
            <%}%>
            </div>
            </div>
    </div>                
    
</form>
</asp:Content>
