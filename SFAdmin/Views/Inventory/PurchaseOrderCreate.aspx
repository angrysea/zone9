<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<PurchaseOrderMaintCreateData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/javascript">

        function OnClick()
        {
            if(document.form1.distributor.value == 0)
            {
                alert("A distributor must be selected!");
                document.form1.distributor.focus();
                return false;
            }

            if(document.form1.shippingmethod.value == 0)
            {
                alert("A shipping method must be selected!");
                document.form1.shippingmethod.focus();
                return false;
            }

            if(AreProductsSelected() == false)
                return false;

            if(QtyToOrderOK() == false)
                return false;

            return true;
        }

        function AreProductsSelected()
        {
            <%foreach(Product product in ViewData.Model.products) {%>
                if(document.form1.product<%=product.ProductNo%>.checked)
                    return true;
            <%}%>

            alert("At least one product must be selected.");
            return false;
        }

        function QtyToOrderOK()
        {
            var valid = '0123456789';
            <%foreach(Product product in ViewData.Model.products) {%>
                if(document.form1.product<%=product.ProductNo%>.checked) {
                    if(document.form1.qtytoorder<%=product.ProductNo%>.value.length == 0 || 
                        IsValid(document.form1.qtytoorder<%=product.ProductNo%>.value, valid) == false)
                    {
                        alert("An invalid quantity to order was entered.");
                        document.form1.qtytoorder<%=product.ProductNo%>.focus();
                        return false;
                    }
                    var qtytoorder = parseInt(document.form1.qtytoorder<%=product.ProductNo%>.value);
                    if(qtytoorder == 0)
                    {
                        alert("A zero quantity to order is not valid.");
                        document.form1.qtytoorder<%=product.ProductNo%>.focus();
                        return false;
                    }
                }
            <%}%>

            return true;
        }
        
        function OnClickCancel() {
            window.location = "/Home/Index";
        }
        
    </script>

    <form name="form1" action="/Inventory/CreatePurchaseOrder" method="post" onload="OnLoadSpecification()">
    <div class="frame" id="input-screen">
        <fieldset>
            <label for="distributorname">Distributor:</label>
            <select name="distributorname" id="distributorname">
                <option value="">< Select ></option>
                <%foreach (Distributor distributor in ViewData.Model.distributors)
                  {%>
                <option 
                    value="<%=distributor.Name%>">
                    <%=distributor.Name%>
                </option>
                <%}%>
            </select>
            <br />
            <label for="shippingmethod">Shipping Method:</label>
            <select name="shippingmethod" id="shippingmethod">
                <option value="">< Select ></option>
                <%foreach (ShippingMethod shippingMethod in ViewData.Model.shippingMethods)
                  {%>
                <option 
                    value="<%=shippingMethod.Code%>">
                    <%=shippingMethod.Code%>
                </option>
                <%}%>
            </select>
            <br />
            <label for="dropship">Drop Shipping:</label>
            <input type="checkbox" class="radioStyle" name="dropship" id="dropship />
            <br />
        </fieldset>
        <div class="lists">
            <table class="listtable">
                <tr>
                    <th>Select Product</th>
                    <th>Inventory ID</th>
                    <th>Name</th>
                    <th>Mfgr</th>
                    <th>Default Distributor</th>
                    <th>Our Cost</th>
                    <th>Quantity On Hand</th>
                    <th>Quantity To Order</th>
                </tr>
                <%foreach(Product product in ViewData.Model.products) {%>
                <tr>
                    <td><input type="checkbox" name="product<%=product.ProductNo%>" /></td>
                    <td><a href="/Products/ProductMaint/<%=product.ProductNo%>"><%=product.ProductNo%></a></td>
                    <td><%=product.Name%></td>
                    <td><%=product.Manufacturer%></td>
                    <td><%=product.Distributor%></td>
                    <td><%=string.Format("{0:#,#.00}", product.OurCost)%></td>
                    <td><%=string.Format("{0:#,#}", product.Quantity)%></td>
                    <td><input type="text" name="qtytoorder<%=product.ProductNo%>" size="5" value="<%=product.QuantityToOrder%>"/></td>
                </tr>
                <%}%>
            </table>
        </div>
        <div class="actions">
        <ul>
            <li><input class="button" type="button" value="Update" onclick="OnClick()" /></li>
            <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
