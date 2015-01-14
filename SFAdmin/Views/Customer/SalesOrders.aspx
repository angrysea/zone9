<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<SalesOrdersViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            createDateField(document.form1.fromdate);
            createDateField(document.form1.todate);

            $("#fromdate").focus();

            $("#Cancel").click(function(event) {
                window.location = "/Home/Index";
            });

            $("#Go").click(function(e) {
                if ($("#opensalesorders:checked").val()!==null)
                    return true;

                if ($("#fromdate").val().length < 10) {
                    alert("Invalid From Date!");
                    $("#fromdate").focus();
                    e.preventDefault();
                    return false;
                }
                if ($("#todate").val().length < 10) {
                    alert("Invalid To Date!");
                    $("#todate").focus();
                    e.preventDefault();
                    return false;
                }
            });
        });

    </script>
    <div class="frame" id="input-screen">
    <form name="form1" action="/Customer/SalesOrders" method="post">
    <div class="shortframe">
        <fieldset>
            <legend>Sales Orders</legend>
            <br />
            <label for="fromdate">From:</label>
            <input name="fromdate" id="fromdate" type="text"/>
            <br />
            <label for="todate">To:</label>
            <input name="todate" id="todate" type="text"/>
            <br />
            <label for="opensalesorders">Open Sales Orders:</label>
            <input name="opensalesorders" id="opensalesorders" type="checkbox" class="radioStyle" checked="checked"/>
            <span>Retrieve only Open Sales Orders (ignore date range)</span>
            <br />
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="Go" id="Go" /></li>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </div>
    </form>
    <table class="listtable">
            <tr>
                <th>Order Number</th>
                <th>Order Date</th>
                <th>Customer</th>
                <th class="numericth">Order Amount</th>
                <th class="numericth">Taxes</th>
                <th class="numericth">COGS</th>
                <th class="numericth">Discounts</th>
                <th class="numericth">Gross Margin</th>
                <th>Drop Ship Requested</th>
                <th>Order Status</th>
            </tr>
            <%
                if (ViewData.Model.salesOrders != null)
                {

                    double totalOrderAmount = 0.0;
                    double totalTaxes = 0.0;
                    double totalShipping = 0.0;
                    double totalMerchandise = 0.0;
                    double totalCogs = 0.0;
                    double totalDiscounts = 0.0;
                    double totalGrossMargin = 0.0;
                    bool orders = false;

                    foreach (SalesOrderView salesOrderView in ViewData.Model.salesOrders)
                    {
                        orders = true;
                        SalesOrder salesOrder = salesOrderView.salesOrder;
                        double cogs = 0.0;
                        foreach (SalesOrderItemView salesOrderItemView in salesOrderView.salesOrderItemsView)
                        {
                            SalesOrderItem salesOrderItem = salesOrderItemView.salesOrderItem;
                            Product product = salesOrderItemView.product;
                            cogs += (product.OurCost * salesOrderItem.Quantity);
                        }
                        double grossMargin = salesOrder.TotalCost - cogs - salesOrder.Discount;

                        Customer customer = salesOrderView.customer;
            %>
            <tr>
                <td><a href="/Customer/SalesOrder/<%=salesOrder.SalesOrderID%>"><%=salesOrder.SalesOrderID%></a></td>
                <td><%=string.Format("{0:MM/dd/yyyy}", salesOrder.Creationdate)%></td>
                <td><a href="/Customer/Customer/<%=customer.CustomerNo%>"><%=customer.Fullname%></a></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", salesOrder.Total)%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", salesOrder.Taxes)%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", cogs)%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", salesOrder.Discount)%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", grossMargin)%></td>
                <td><%=salesOrder.DropShipped == 1 ? "yes" : "no"%></td>
                <td><%=salesOrder.Status%></td>
            </tr>
            <%
                totalOrderAmount += salesOrder.Total;
                totalTaxes += salesOrder.Taxes;
                totalShipping += salesOrder.Shipping;
                totalMerchandise += salesOrder.TotalCost;
                totalCogs += cogs;
                totalDiscounts += salesOrder.Discount;
                totalGrossMargin += grossMargin;
                    }

                    if (orders == true)
                    {
            %>
                <tr>
                    <td colspan="3" />
                    <td class="numerictd"><%=string.Format("{0:#,#.00}", totalOrderAmount)%></td>
                    <td class="numerictd"><%=string.Format("{0:#,#.00}", totalTaxes)%></td>
                    <td class="numerictd"><%=string.Format("{0:#,#.00}", totalCogs)%></td>
                    <td class="numerictd"><%=string.Format("{0:#,#.00}", totalDiscounts)%></td>
                    <td class="numerictd"><%=string.Format("{0:#,#.00}", totalGrossMargin)%></td>
                </tr>
            <%}else{%>
            <tr>
                <td colspan="5"><h2>There are no orders in the system for this time period.</h2></td>
            </tr>
            <%} }%>
        </table>
    </div>
</asp:Content>
