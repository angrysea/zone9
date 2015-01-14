<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CheckoutViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Customer customer = ViewData.Model.customer;
    Users users = ViewData.Model.user;
    Address shippingAddress = ViewData.Model.ShippingAddress;
%>

<script src="/Scripts/jquery.simplemodal.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    $(document).ready(function() {
        initNavigation("/Checkout/Index");
        $("#cardholder").focus();
        
        $("#Cancel").click(function(e) {
            window.location = "/Checkout/CancelCheckout/<%=customer.CustomerNo%>";
        });

        $("#continue").click(function(e) {
            $("#form1").submit();
        });
    });
    
</script>

<form id="form1" action="/Checkout/SelectShippingMethod" method="post" >
<input name="customer" id="customer" type="hidden" value="<%=customer.CustomerNo%>" />
<div class="frame">
    <div>
        <%IList<string> errors = ViewData.Model.errors; if (errors != null) {%>
        <ul class="error">
        <% foreach (string error in errors) { %>
            <li><%= Html.Encode(error) %></li>
        <%}%>
        </ul>
        <%}%>
    </div>
    <div class="form-container">
        <fieldset>
        <legend>Select Shipping Method</legend> 
        <div class="indexstripe">
            <h3>Choose a shipping method:</h3>
            <%foreach (ShippingMethod sm in ViewData.Model.ShippingMethods){ %>
            <input name="shippingmethod" 
                    value="<%=sm.Code%>" 
                    <%=sm.DefaultMethod>0?"checked='checked'":"" %>
                    type="radio" 
                    class="radioStyle"/>
            <span class="label"><%=sm.Description%></span>
            <br />
            <%} %>
        </div>
        </fieldset>
        <div class="indexstripe">
        <span class="text">Shipping To: <%=shippingAddress.FullName%>, 
        <%=shippingAddress.Address1%>, 
        <%=string.IsNullOrEmpty(shippingAddress.Address2)==false?shippingAddress.Address2+", ":""%><%=string.IsNullOrEmpty(shippingAddress.Address3) == false ? shippingAddress.Address3 + ", " : ""%><%=shippingAddress.City%>, <%=shippingAddress.State%>, <%=shippingAddress.Zip%>, <%=shippingAddress.Country%></span>
        <%if (ViewData.Model.shoppingCart.Count > 0){%>
        <table class="itemlist">
            <%foreach (ShoppingCartListItem shoppingCart in ViewData.Model.shoppingCart) {%>
            <tr>
                <td><%=shoppingCart.Name%></td>
                <td><%=string.Format("{0:$#,#.00}", shoppingCart.OurPrice)%> - Quantity: <%=string.Format("{0:#}", shoppingCart.Quantity)%>, <%=shoppingCart.Availability%></td>
            </tr>                    
            <%}%>
        </table>
        <%}%>                
        <br />
        <div class="continue">
            <a id="continue" href="#" >Continue</a>
        </div>
        </div>
    </div>
</div>
</form>
</asp:Content>
