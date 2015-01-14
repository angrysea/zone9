<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SalesOrderViewData>" %>
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
    DetailedSalesOrder details = ViewData.Model.DetailedSalesOrder;
    SalesOrder salesOrder = ViewData.Model.DetailedSalesOrder.SalesOrder;
%>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        initNavigation("/Checkout/Index");
        $("#Continue").click(function(e) {
            window.location = "/Home/Index";
        });

    });
</script>

<%
    if (ViewData.Model.FailedAuthorization)
    {
        IList<string> errors = ViewData.Model.errors;
        if (errors != null)
        {%>
            <div>
                <ul class="error">
                <% foreach (string error in errors)
                   { %>
                    <li><%= Html.Encode(error)%></li>
                <%}%>
                </ul>
            </div>
        <%}
    }
    else
    {
%>
    <p>Your order number is: <%=salesOrder.SalesOrderID%>.</p><br />
    <p>You will receive an e-mail confirmation of your order. You may also print this page for your records.</p>
        <div class="salesorderstripe">
            <h3>You Purchased:</h3>
            <div class="salesorderCol">
                <span>Billing Information:</span>
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
            <div class="stripecol">
                <span>Payment Method: </span>
                <span><%=creditCard.Type%> : <%=creditCard.Number%></span>
                <span>Exp : <%=creditCard.Expmonth%>/<%=creditCard.Expyear%></span>
                <span>Card Holder: <%=creditCard.Cardholder%></span>
                <br />
            </div>
            <div class="stripecol">
                <span>Shipping Information:</span>
                <span><%=shippingAddress.FullName%></span>
                <span><%=shippingAddress.Address1%></span>
                <%if (shippingAddress.Address2 != null && shippingAddress.Address2.Length > 0)
                  {%>
                <span><%=shippingAddress.Address2%></span>
                <% } if (shippingAddress.Address3 != null && shippingAddress.Address3.Length > 0)
                  {%>
                <span><%=shippingAddress.Address3%></span><%}%>
                <span><%=shippingAddress.City%>, <%=shippingAddress.State%>, <%=shippingAddress.Zip%></span>
                <span><%=shippingAddress.Country%></span>
                <span>Phone: <%=shippingAddress.Phone%></span>
            </div>
    </div>
    <div class="salesorderstripe">
    <%if (details.Items.Count > 0)
      {%>
    <table class="lists">
        <tr>
            <th>Product</th>
            <th class="numericth">Unit Price</th>
            <th>Quantity</th>
            <th class="numericth">Item Total</th>
        </tr>

        <%foreach (SalesOrderItem salesorderitem in details.Items)
          {%>
            <tr>
                <td align="left" width="350"><%=salesorderitem.Manufacturer%> <%=salesorderitem.ProductName%>
                    <br/>Item number: <%=salesorderitem.ProductNo%>, <%=salesorderitem.Status%>
                </td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", salesorderitem.Unitprice)%></td>
                <td class="numerictd"><%=string.Format("{0:#}", salesorderitem.Quantity)%></td>
                <td class="numerictd"><%=string.Format("{0:$#,#.00}", salesorderitem.Quantity * salesorderitem.Unitprice)%></td>
            </tr>
        <%}%>
        <tr>
            <td colspan="3"  class="numerictd"><b>Merchandise Total:</b></td>
            <td class="numerictd"><%=string.Format("{0:$#,#.00}", salesOrder.TotalCost)%></td>
        </tr>
        <%if (salesOrder.Discount > 0.0)
          { %>
        <tr>
            <td colspan="3" class="numerictd"><%=salesOrder.DiscountDescription%></td>
            <td class="numerictd"><font color="red">-<%=string.Format("{0:$#,#.00}", salesOrder.Discount)%></font></td>
            <td></td>
        </tr>
        <%}%>
        <tr>
            <td colspan="3"  class="numerictd">Shipping Costs(<%=salesOrder.ShippingMethod%>):</td>
            <td class="numerictd"><%=string.Format("{0:$#,#.00}", salesOrder.Shipping + salesOrder.Handling)%></td>
        </tr>
        <%if (salesOrder.Taxes > 0){ %>
        <tr>
            <td colspan="3"  class="numerictd"><%=salesOrder.TaxesDescription%>:</td>
            <td class="numerictd"><%=string.Format("{0:$#,#.00}", salesOrder.Taxes)%></td>
        </tr>
        <%}%>
        <tr>
            <td colspan="3"  class="numerictd"><b>Grand Total:</b></td>
            <td class="numerictd"><%=string.Format("{0:$#,#.00}", salesOrder.Total)%></td>
        </tr>
    </table>
    <%}%>
    </div>
<%}%>
</asp:Content>
