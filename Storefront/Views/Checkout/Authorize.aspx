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

<form name="form1" action="/Checkout/SalesOrder/<%=customer.CustomerNo%>" method="post">
    <div>
        <h2>Please wait while we authorize your credit card...</h2>
        <img src="/Content/images/wait.gif" />
    </div>
</form>
<script language="javascript" type="text/javascript">
    document.form1.submit();
</script>

</asp:Content>
