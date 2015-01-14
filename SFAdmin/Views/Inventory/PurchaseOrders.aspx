<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<PurchaseOrderViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script language="javascript" type="text/javascript">
        
        function OnClickCancel() {
            window.location = "/Home/Index";
        }

        function OnClick() {
        }
        
        function OnSubmitForm()
        {
            if(ValidateDate(document.form1.fromdate) == false)
            {
                return false;
            }

            if(ValidateDate(document.form1.todate) == false)
            {
                return false;
            }

            return true;
        }

        function OnClickOpenPurchaseOrders()
        {
            document.form1.submit();
        }
    </script>

    <form name="form1" action="/Customers/PurchaseOrders" method="post">
    <div class="shortframe" id="input-screen">
        <fieldset>
            <legend>Purchase Orders</legend>
            <br />
            <label for="fromdate">From:</label>
            <input name="fromdate" type="text" onchange="OnChangeDate()"/>
            <br />
            <label for="todate">To:</label>
            <input name="todate" type="text" onchange="OnChangeDate()"/>
            <br />
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="button" value="go" onclick="OnClick()" /></li>
            <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
        </ul>
        </div>
    </div>
    </form>
    <div class="widelists">
        <table class="listtable">
            <tr>
                <th>ID</th>
                <th>Creation Date</th>
                <th>Sales Order ID</th>
                <th class="numericth">Total Cost</th>
                <th>Distributor</th>
                <th>Shipping Method</th>
                <th>Tracking #</th>
                <th>Status</th>
            </tr>
            <%foreach (PurchaseOrder purchaseOrder in ViewData.Model.purchaseOrders){%>
            <tr>
                <td><a href="/Inventory/PurchaseOrderMaint/<%=purchaseOrder.ID%>"><%=purchaseOrder.ID%></a></td>
                <td nowrap="nowrap"><%=string.Format("{0:MM/dd/yyyy}", purchaseOrder.CreationDate)%></td>
                <td>
                <%if(purchaseOrder.ReferenceNumber>0){%>
                <a href="/Customer/Salesorder/<%=purchaseOrder.ReferenceNumber%>"><%=purchaseOrder.ReferenceNumber%></a>
                <%}%>
                </td>
                <td><%=string.Format("{0:#,#.00}", purchaseOrder.Total)%></td>
                <td><%=purchaseOrder.Distributor%></td>
                <td><%=purchaseOrder.ShippingMethod%></td>
                <td><%=purchaseOrder.TrackingNumber==null?"":purchaseOrder.TrackingNumber%></td>
                <td><%=purchaseOrder.Status==null?"":purchaseOrder.Status%></td>
            </tr>
            <%}%>
        </table>
        <br/>
    </div>
</asp:Content>
