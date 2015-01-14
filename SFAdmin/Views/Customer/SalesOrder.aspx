<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<SalesOrderViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        SalesOrder salesOrder = ViewData.Model.DetailedSalesOrder.SalesOrder;
        Customer customer = ViewData.Model.Customer;
        Address billingAddress = ViewData.Model.BillingAddress;
        Address shippingAddress = ViewData.Model.ShippingAddress;
        ShippingMethod shippingMethod = ViewData.Model.ShippingMethod;
        CreditCard creditCard = ViewData.Model.CreditCard;
    %>

    <script src="/Scripts/jquery.simplemodal.js" type="text/javascript"></script>
        
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            $("#Notes").click(function(event) {
                window.open("/Customer/Notes/<%=salesOrder.SalesOrderID%>", "mywindow", "location=0,status=0,toolbar=0,scrollbars=1,menubar=0,directories=0,resizable=0,width=600,height=500");
            });

            $("#Delete").click(function(event) {
                event.preventDefault();
                var answer = confirm("Are you sure you want to delete this sales order?")
                if (answer) {
                    window.location.href = "/Customer/DeleteSalesOrder/<%=salesOrder.SalesOrderID%>";
                }
            });

            $("#Cancel").click(function(event) {
                event.preventDefault();
                window.location.href = "/Customer/SalesOrders/";
            });

            $("#DropShip").click(function(event) {
                if ($("#distributor").val() == "0") {
                    alert("A distributor must be selected when drop shipping an order.");
                    $("#distributor").focus();
                    event.preventDefault();
                }
                else 
                {
                    var answer = confirm("Are you sure you want to drop ship this sales order?");
                    if (!answer) {
                        event.preventDefault();
                    }
                }
            });
        });
        
    </script>
    
<div class="frame" id="input-screen">
    <div class="salesorderstripe">
        <div class="salesorderCol">
                <span>ID: <%=salesOrder.SalesOrderID%></span>
                <span>Date: <%=string.Format("{0:MM/dd/yyyy}", salesOrder.Creationdate)%></span>
                <span>Status: <%=salesOrder.Status%></span>
                <span>Mail Server Msg: <%=salesOrder.Emailstatus%></span>
                <span>Optimize Shipping: <%=salesOrder.OptimizeShipping==1?"Yes":"No"%></span>
                <span>Drop Ship Requested: <%=salesOrder.DropShipped==1?"Yes":"No"%></span>
            <%if(salesOrder.Referencenumber>0) {%>
                <span>Purchase Order: <a href="/Logistics/PurchaseOrderUpdate/<%=salesOrder.Referencenumber%>"><%=salesOrder.Referencenumber%></a></span>
            <%}%>
            </div>
        <div class="salesorderCol">
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
        <div class="salesorderCol">
            <span>Payment Method: </span>
            <span><%=creditCard.Type%> : <%=creditCard.Number%></span>
            <span>Exp : <%=creditCard.Expmonth%>/<%=creditCard.Expyear%></span>
            <span>Card Holder: <%=creditCard.Cardholder%></span>
            <br />
        </div>
    </div>
    <div class="salesorderstripe">

    <table cellspacing="1" cellpadding="2" border="0" width="600">
    <tr>
        <th>Product</th>
        <th>Unit Price</th>
        <th>Quantity</th>
        <th>Product Total</th>
        <th>Product Status</th>
    </tr>
    <tr>
        <td colspan="5">
            <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td><img src="/Content/images/spacer.gif" width="600" height="2"></td>
                </tr>
            </table>
        </td>
    </tr>
    <% foreach (SalesOrderItem salesorderitem in ViewData.Model.DetailedSalesOrder.Items) {%>
    <tr>
        <td><a href="/Products/ProductMaint/<%=salesorderitem.ProductNo%>"><%=salesorderitem.Manufacturer%> <%=salesorderitem.ProductName%></a>
            <br/>Product number: <%=salesorderitem.ProductNo%>, <%=salesorderitem.Status%>
        </td>
        <td align="right"><%=string.Format("{0:#,#.00}", salesorderitem.Unitprice)%></td>
        <td align="center"><%=string.Format("{0:#,#}", salesorderitem.Quantity)%></td>
        <td align="right"><%=string.Format("{0:#,#.00}", salesorderitem.Quantity * salesorderitem.Unitprice)%></td>
        <td align="right"><%=salesorderitem.Status%></td>
    </tr>
    <%}%>
    <tr>
        <td colspan="5">
            <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td><img src="/Content/images/spacer.gif" width="600" height="1"></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="right">Merchandise Total:</td>
        <td align="right"><%=string.Format("{0:#,#.00}", salesOrder.TotalCost)%></td>
    </tr>
    <%if(salesOrder.Discount>0.0){%>
    <tr>
        <td colspan="3" align="right"><%=salesOrder.DiscountDescription%></td>
        <td align="right"><font color="red">- <%=string.Format("{0:#,#.00}", salesOrder.Discount)%></font></td>
        <td></td>
    </tr>
    <%}%>
    <tr>
        <td colspan="1"></td>
        <td colspan="4" align="right">
            <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td><img src="/Content/images/spacer.gif" width="250" height="1"></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="right">Shipping Costs (<%=shippingMethod.Description%>):</td>
        <td align="right"><%=string.Format("{0:#,#.00}", salesOrder.Shipping+salesOrder.Handling)%></td>
    </tr>
    <tr>
        <td colspan="3" align="right"><%=salesOrder.TaxesDescription.Length>0?salesOrder.TaxesDescription:"Tax"%>:</td>
        <td align="right"><%=string.Format("{0:#,#.00}", salesOrder.Taxes)%></td>
    </tr>
    <tr>
        <td colspan="1"></td>
        <td colspan="4" align="right">
            <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td><img src="/Content/images/spacer.gif" width="250" height="1"></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="right">Grand Total:</td>
        <td align="right"><%=string.Format("{0:#,#.00}", salesOrder.Total)%></td>
    </tr>
    </table>
    <br/>
    <form name="form1" action="/Customer/DropshipSalesOrder" method="post">
        <input type="hidden" name="referenceno" value="<%=salesOrder.SalesOrderID%>"/>
        <table border="0">
            <tr>
                <td colspan="3">From:</td>
            </tr>
            <tr>
                <td valign="bottom">
                    <select name="distributor" id="distributor">
                        <option value="0">< select ></option>
                        <%foreach(Distributor distributor in ViewData.Model.distributors){%>
                            <option value="<%=distributor.Name%>"><%=distributor.Name%></option>
                        <%}%>
                    </select>
                    <input  class="button" type="submit" id="DropShip" value="DropShip Order"/>
                </td>
                
                <%if(salesOrder.Status=="New"){%>
                <td valign="bottom">
                    <input class="button" 
                            type="button" 
                            value="Delete Order" 
                            id="Delete" />
                </td>
                <%}%>

                <td valign="bottom">
                    <input  class="button" 
                            type="button" 
                            id="Notes"
                            value="Notes(<%=ViewData.Model.noteCount%>)"/>

                </td>
                <td valign="bottom">
                    <input class="button" 
                            type="button" 
                            value="Cancel" 
                            id="Cancel" />
                </td>
            </tr>
        </table>
        <div class="salesorderstripe">
            <%if(salesOrder.DropShipped==1) { %>
            <font color="red">A (drop ship) was already requested for this order.  By requesting a (drop ship) again, a Drop Ship Order request will be sent to the distributor.</font>
            <br />
            <%} %>
            <span><a href="/Customer/SalesOrderDropShipMail/<%=salesOrder.SalesOrderID%>">Printable Drop Ship Order</a></span>
        </div>
    </form>
    
    </div>
</div>

</asp:Content>
