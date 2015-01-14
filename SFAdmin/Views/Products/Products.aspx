<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<ProductsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        $("#manufacturer").change(function(e) {
            window.location = "/Products/Products/"+$('#manufacturer').val();
        });
    });
</script>
<div class="lists">
    <fieldset>
        <legend>Products</legend>
        <label for="manufacturer">Manufacturer:</label>
        <select name="manufacturer" id="manufacturer"">
            <option value="">< select ></option>
            <option value="all"
                <%if (ViewData.Model.manufacturer == "all"){%>selected="selected"<%}%>>
                All
            </option>
            <%foreach(Manufacturer manufacturer in ViewData.Model.manufacturers){%>
            <option 
                <%if (ViewData.Model.manufacturer == manufacturer.Name){%>
                selected="selected"
                <%}%>
                value="<%=manufacturer.Name%>"><%=manufacturer.Name%>
            </option>
            <%}%>
        </select>
<br/><br/>
    <table class="lists">
    <tr>
        <th>Product</th> 
        <th>Name</th> 
        <th class="numericth">Quantity</th>
        <th class="numericth">List Price</th>
        <th class="numericth">Our Cost</th>
        <th class="numericth">Our Price</th>
        <th>Manufacturer</th>
        <th>Distributor</th>
        <th>Status</th>
    </tr>
    <%
        foreach(Product product in ViewData.Model.products)
        {
    %>
            <tr>
                <td><a href="/Products/ProductMaint/<%=product.ProductNo%>" ><%=product.ProductNo%></a></td>
                <td><%=product.Name.Length > 20 ? product.Name.Substring(0, 20) : product.Name%></td>
                <td class="numerictd"><%=string.Format("{0:#,#}", product.Quantity)%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", product.ListPrice)%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", product.OurCost)%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", product.OurPrice)%></td>
                <td><%=product.Manufacturer%></td>
                <td><%=product.Distributor%></td>
                <td><%=product.Availability%></td> 
            </tr>
    <%
        }
    %>
</table>
</fieldset>
    <div class="actions">
        <a href="/Products/ProductMaint">Add a new Product</a>
    </div>
</div>
</asp:Content>
