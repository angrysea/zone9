<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<StartSimilarProductsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    bool update = ViewData.Model.update;
%>

<script language="javascript" type="text/javascript">
    function OnChangeManufacturer() {
        document.form1.submit();
    }

</script>

<form name="form1" action="/Products/StartSimilarProducts" method="post">
<div class="wideframe" id="input-screen">
    <fieldset>
        <legend>Similar Products Wizard</legend>
        <h2>Step 1 - Select a Product</h2>
        <label for="manufacturer">Manufacturer:</label>
        <select name="manufacturer" onchange="OnChangeManufacturer()">
            <option value="">< Select ></option>
            <option 
            value="all"
                <%if (ViewData.Model.manufacturer == "all"){%>selected="selected"<%}%>>
                All
            </option>
            <%foreach(Manufacturer manufacturer in ViewData.Model.manufacturers) {%>
            <option 
                <%if (ViewData.Model.manufacturer == manufacturer.Name){%>
                selected="selected"
                <%}%>
                value="<%=manufacturer.Name%>">
                <%=manufacturer.Name%>
            </option>
            <%}%>
        </select>
    </fieldset>
    <div class="lists">
        <h1>Products</h1>
        <table class="listtable">
            <tr>
                <th>Product</th>
                <th>Name</th>
                <th class="numericth">List Price</th>
                <th class="numericth">Our Price</th>
                <th>Mfgr</th>
                <th>Distributor</th>
                <th>Status</th>
            </tr>
            <%if (ViewData.Model.products != null){foreach (Product product in ViewData.Model.products){%>
            <tr>
                <td><a href="/Products/SimilarProductsActions/<%=product.ProductNo%>/none"><%=product.ProductNo%></a></td>
                <td><%=product.Name%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", product.ListPrice)%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", product.OurPrice)%></td>
                <td><%=product.Manufacturer%></td>
                <td><%=product.Distributor%></td>
                <td><%=product.Availability%></td>
            </tr>
            <%}}%>
        </table>
    </div>
</div>
</form>
</asp:Content>
