<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RecentOrdersViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Customer customer = ViewData.Model.customer;
 %>    
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            $("#Cancel").click(function(event) {
                window.location = "/Home/Index";
            });
        });

    </script>

<form name="form1" action="/Customer/SalesOrders" method="post">
<div id="input-screen">
    <fieldset>
        <legend>Sales Orders</legend>
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <br />
        <label for="daterange">See More:</label>
        <select id ="daterange">
            <option value="0">Open Sales Orders</option>
            <option value="6">Orders placed in the last 6 months</option>
            <option value="12">Orders placed in the last Year</option>
            <option value="99">All Sales Orders</option>
        </select>
        <br />
    </fieldset>
    <%if (ViewData.Model.RecentOrders != null)
      {
        foreach (DetailedSalesOrder detailed in ViewData.Model.RecentOrders)
        {
            SalesOrder salesOrder = detailed.SalesOrder;
    %>
        <div class="stripe">
            <div class="stripecol">
                <span>Order Date: <%=string.Format("{0:MM/dd/yyyy}", salesOrder.Creationdate)%></span>
                <span>Total: <%=string.Format("{0:$#,#.00}", salesOrder.Total)%></span>
                <span class="small">Order #: <%=salesOrder.SalesOrderID%></span>
            </div>
            <div class="stripecol2">
            <table class="itemlist">
                <tr>
                    <th colspan="2">Items</th>
                    <th><a  class="right" href="/Account/SalesOrder/<%=salesOrder.SalesOrderID%>">View Order</a></th>
                </tr>
                <%foreach (SalesOrderItem salesorderitem in detailed.Items){%>
                <tr>
                    <td/>
                    <td><%=string.Format("{0:#}", salesorderitem.Quantity)%> of <%=salesorderitem.Manufacturer%> <%=salesorderitem.ProductName%> <%=string.Format("{0:#,#.00}", salesorderitem.Unitprice)%></td>
                    <td/>
                </tr>
                <%}%>
            </table>
        </div>
        </div>
        <%}%>
    <%}%>
</div>
</form>
</asp:Content>
