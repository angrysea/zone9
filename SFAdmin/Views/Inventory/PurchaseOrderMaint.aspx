<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<PurchaseOrdersViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    PurchaseOrder purchaseorder = ViewData.Model.purchaseorder;
%>
    <table id="headingTable" border="0">
        <tr>
            <td><img src="/Content/images/spacer.gif" width="5" height="5"></td>
        </tr>
        <tr>
            <td class="producttitle">Receive Purchase Order</td>
        </tr>
        <tr>
            <td><br /></td>
        </tr>
    </table>

    <form name="form1" action="/Inventory/PurchaseOrderReceive" method="get" onsubmit="return(OnSubmitForm());">
    <table border="0" width="100">
        <tr>
            <td>ID:</td>
            <td><%=purchaseorder.ID%></td>
        </tr>
        <tr>
            <td>Date:</td>
            <td><%=string.Format("{0:MM/dd/yyyy}", purchaseorder.CreationDate)%></td>
        </tr>
        <tr>
            <td>Status:</td>
            <td><%=purchaseorder.Status%></td>
        </tr>
        <tr>
            <td>Distributor:</td>
            <td><%=purchaseorder.Distributor%></td>
        </tr>
        <tr>
            <td>Shipping Method:</td>
            <td><%=purchaseorder.ShippingMethod%></td>
        </tr>
        <tr>
            <td>Tracking #:</td>
            <td><%=purchaseorder.TrackingNumber==null?"":purchaseorder.TrackingNumber%></td>
        </tr>
    </table>
    <br />
    <div class="widelists">
        <table class="listtable">            
            <tr>
                <th>Product</th>
                <th nowrap="nowrap">Product Name</th>
                <th nowrap="nowrap">Manufacturer</th>
                <th nowrap="nowrap">Unit Cost</th>
                <th nowrap="nowrap">Quantity Ordered</th>
                <th nowrap="nowrap">Quantity Recieved to Date</th>
                <th nowrap="nowrap">Receiving</th>
                <th nowrap="nowrap">Total Cost</th>
            </tr>
            <%foreach(PurchaseOrderItem purchaseorderproduct in ViewData.Model.purchaseOrderItems){%>
                <tr>
                    <td><%=purchaseorderproduct.Product%></td>
                    <td><%=purchaseorderproduct.ProductName%></td>
                    <td><%=purchaseorderproduct.Manufacturer%></td>
                    <td><%=string.Format("{0:#,#.00}", purchaseorderproduct.Ourcost)%></td>
                    <td><%=purchaseorderproduct.Quantity%></td>
                    <td><%=string.Format("{0:#,#.00}", purchaseorderproduct.Quantityreceived)%></td>
                    <td><input type="text" maxlength="6" name="qtyreceiving<%=purchaseorderproduct.Product%>" value="0" style="width: 6em" /></td>
                    <td><%=string.Format("{0:#,#.00}", purchaseorderproduct.Ourcost * purchaseorderproduct.Quantity)%></td>
                </tr>
            <%}%>
            <tr>
                <td colspan="6"></td>
                <td>Total Cost:</td>
                <td><%=string.Format("{0:#,#.00}", purchaseorder.Total)%></td>
            </tr>
        </table>
        <br />
    </div>
    <div class="actions">
    <ul>
        <li><input class="button" type="button" value="Receive Order" onclick="OnClick()" /></li>
        <li><input class="button" type="button" value="Delete Order" onclick="OnClickDelete()" /></li>
        <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
    </ul>
    </div>
    </form>
</asp:Content>
