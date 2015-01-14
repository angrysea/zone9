<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SFAdmin.Models.CreatePackingslipsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/javascript">


        $(document).ready(function() {
            
            $("#Cancel").click(function(event) {
                event.preventDefault();
                window.location.href = "/Customer/SalesOrders/";
            });

            $("#Create").click(function(event) {
                if(AreItemsSelected() == false || 
                    QtyToShipOK() == false ||
                    CheckEntireOrdersShipped() == false )
                {
                    event.preventDefault();
                    return false;
                }
                return true;
            });
            
            <%
                foreach (DetailedSalesOrder detailedSalesOrder in ViewData.Model.salesOrders) 
                {
                    foreach (SalesOrderItem salesOrderItem in detailedSalesOrder.Items) 
                    {
            %>
                        createIntegerField(document.getElementById("qtytoship<%=salesOrderItem.SOItem%>"));
            <%      
                    }
                }
            %>

        });

        function AreItemsSelected() {
            <%
                foreach (DetailedSalesOrder detailedSalesOrder in ViewData.Model.salesOrders) 
                {
                    foreach (SalesOrderItem salesOrderItem in detailedSalesOrder.Items) 
                    {
            %>
                        if($("#item<%=salesOrderItem.SOItem%>").attr('checked')) 
                        {
                            return true;
                        }
            <%      
                    }
                }
            %>

            alert("At least one item must be selected for shipment.");
            return false;
        }

        function QtyToShipOK()
        {
            <%
                foreach (DetailedSalesOrder detailedSalesOrder in ViewData.Model.salesOrders) 
                {
                    SalesOrder salesOrder = detailedSalesOrder.SalesOrder;
                    foreach (SalesOrderItem salesOrderItem in detailedSalesOrder.Items ) 
                    {
            %>
                        if($("#item<%=salesOrderItem.SOItem%>").attr('checked')) 
                        {
                            var qtyToShip = $("#qtytoship<%=salesOrderItem.SOItem%>");
                            if(qtyToShip.val().length==0)
                            {
                                alert("An invalid quantity to ship was entered.");
                                qtyToShip.focus();
                                return false;
                            }
                            if(qtyToShip == "0")
                            {
                                alert("A zero quantity to ship is not valid.");
                                qtyToShip.focus();
                                return false;
                            }
                            if( qtyToShip > <%=salesOrderItem.Quantity%>)
                            {
                                alert("Quantity to ship cannot be greater than the quantity ordered.");
                                qtyToShip.focus();
                                return false;
                            }
                        }
            <%
                    }
                }
            %>

            return true;
        }

        function CheckEntireOrdersShipped()
        {
            <%
                foreach (DetailedSalesOrder detailedSalesOrder in ViewData.Model.salesOrders) 
                {
                    SalesOrder salesOrder = detailedSalesOrder.SalesOrder;
                    foreach (SalesOrderItem salesOrderItem in detailedSalesOrder.Items ) 
                    {
            %>
                        if($("#item<%=Html.Encode(salesOrderItem.SOItem)%>").attr('checked'))
                        {
                            /* Not optimizing shipping for this order? */
                        <%
                            if(salesOrder.OptimizeShipping==0)
                            {
                        %>
                                /* Make sure all items in this order are going to be shipped */
                                if(AreAllItemsShipping('<%=Html.Encode(salesOrderItem.SOItem)%>') == false)
                                {
                                    var answer = confirm("The customer did not Optimize Shipping for order <%=Html.Encode(salesOrderItem.SOItem)%>.  Are you sure you want to partially ship this order?");
                                    if(answer != true)
                                    {
                                        return false;
                                    }
                                }
                        <%
                            }
                        %>
                        }
            <%
                    }
                }
            %>

            return true;
        }

        function AreAllItemsShipping(salesorderid)
        {
            <%
                foreach (DetailedSalesOrder detailedSalesOrder in ViewData.Model.salesOrders) 
                {
                    SalesOrder salesOrder = detailedSalesOrder.SalesOrder;
                    foreach (SalesOrderItem salesOrderItem in detailedSalesOrder.Items ) 
                    {
            %>
                    if(salesorderid=="<%=salesOrderItem.SalesOrder%>")
                        if($("#item<%=Html.Encode(salesOrderItem.SOItem)%>").attr('checked') == false)
                            return false;

                        var qtytoship = $("#qtytoship<%=Html.Encode(salesOrderItem.SOItem)%>").val();
                        if(qtytoship != <%=salesOrderItem.Quantity%>)
                            return false;
            <%
                    }
                }
            %>

            return true;
        }
    </script>
    
    <div class="wideframe" id="input-screen">
    <form name="form1" action="/Logistics/CreatePackingSlips" method="post">
        <fieldset>
            <legend>Create Packing Slips</legend>

            <table class="listtable">
            <tr>
                <th>Select</th>
                <th>Sales</th>
                <th>Shipping </th>
                <th>Created</th>
                <th>Product ID</th>
                <th class="center">Qty</th>
                <th class="center">Qty</th>
                <th class="center">Qty</th>
                <th>Status</th>
            </tr>
            <tr>
                <th>Item</th>
                <th>OrderID</th>
                <th>Optimized</th>
                <th></th>
                <th></th>
                <th>Ordered</th>
                <th>Shipped</th>
                <th>ToShip</th>
                <th></th>
            </tr>
            
            <%
                int count = 0;
                foreach (DetailedSalesOrder detailedSalesOrder in ViewData.Model.salesOrders) 
                {
                    SalesOrder salesOrder = detailedSalesOrder.SalesOrder;
                    foreach (SalesOrderItem salesOrderItem in detailedSalesOrder.Items ) 
                    {
                        count++;
            %>
                        <tr>
                            <td><input type="checkbox" class="radioStyle" name="item<%=Html.Encode(salesOrderItem.SOItem)%>" id="item<%=Html.Encode(salesOrderItem.SOItem)%>" /></td>
                            <td><a href="/Customer/SalesOrder/<%=Html.Encode(salesOrderItem.SalesOrder)%>"><%=Html.Encode(salesOrderItem.SalesOrder)%></a></td>
                            <td class="center"><%=salesOrder.OptimizeShipping==1?"yes":"no"%></td>
                            <td><%=string.Format("{0:MM/dd/yyyy}", salesOrder.Creationdate)%></td>
                            <td><a href="/Products/ProductMaint/<%=salesOrderItem.ProductNo%>"><%=salesOrderItem.ProductNo%></a></td>
                            <td class="center"><%=Html.Encode(salesOrderItem.Quantity)%></td>
                            <td class="center"><%=Html.Encode(salesOrderItem.Shipped)%></td>
                            <td class="center"><input type="text" class="small" name="qtytoship<%=Html.Encode(salesOrderItem.SOItem)%>" id="qtytoship<%=Html.Encode(salesOrderItem.SOItem)%>" size="5" value="<%=salesOrderItem.Quantity - salesOrderItem.Shipped%>"/></td>
                            <td><%=salesOrderItem.Status!=null?salesOrderItem.Status:""%></td>
                        </tr>
            <%
                    }
                }

                if(count == 0)
                {
            %>
                        <tr>
                            <td colspan="9"><h2>There are no new sales orders in the system.</h2></td>
                        </tr>
            <%
                }
            %>
            </table>
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="Create" id="Create" /></li>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </form>
    </div>
</asp:Content>
