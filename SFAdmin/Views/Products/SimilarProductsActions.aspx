<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<SimilarProductsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    bool update = ViewData.Model.update;
    Product product = ViewData.Model.product;
    Details details = ViewData.Model.details;
    string nextProduct = ViewData.Model.nextProduct;
    string prevProduct = ViewData.Model.prevProduct;
%>

<script language="javascript" type="text/javascript">
    function OnChangeManufacturer() {
        document.form1.submit();
    }
    function OnClickCancel() {
        window.location = "/Products/StartSimilarProducts";
    }
</script>

<form name="form1" action="/Products/SimilarProductsActions" method="post">
<input name="productno" type="hidden" value="<%=product.ProductNo%>" />
<div class="frame" id="input-screen">
    <fieldset>
        <legend>Similar Products Wizard</legend>
        <h3>Step 2 - Choose Related Products</h3>
        
            <ul class="navigation" id="topbar">
                <%if(prevProduct != null && prevProduct.Length>0){%>
                <li class="left">
                    <a href="/Products/SimilarProductsActions/<%=prevProduct%>/<%=ViewData.Model.manufacturer%>" >
                        <img src="/Content/images/arrowleft.gif" style="border-style: none" /><%=prevProduct%>
                    </a>
                </li>
                <%}%>
                <%if(nextProduct != null && nextProduct.Length>0){%>
                <li class="right">
                    <a href="/Products/SimilarProductsActions/<%=nextProduct%>/<%=ViewData.Model.manufacturer%>"><%=nextProduct%>
                        <img src="/Content/images/arrowright.gif" style="border-style: none" />
                    </a>
                </li>
                <%}%>
            </ul>            
        
        <br />
        <table class="listtable">
            <tr>
                <th>Manufacturer</th>
                <th>Name</th>
                <th>ProductNo</th>
            </tr>
            <tr>
                <td><%=product.Manufacturer%></td>
                <td><%=product.Name %></td>
                <td><%=product.ProductNo%></td>
            </tr>
            <tr>
                <td colspan="3"><img border="0" src="/Content/images/products/<%=details.ImageURLSmall%>" /></td>
            </tr>
        </table>
        <br />
        <h2>Product Description:</h2>
        <textarea name="description" readonly="readonly"><%=details.Description%></textarea>
        <br/>
        <br/>
        <table class="listtable">
            <tr>
                <th class="numericth">List Price</th>
                <th class="numericth">Our Price</th>
            </tr>
            <tr>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", product.ListPrice)%></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", product.OurPrice)%></td>
            </tr>
        </table>
        <h2>Similar Products:</h2>
        <%
            foreach(ListProduct listProduct in ViewData.Model.listProducts) 
            {
        %>
            <img alt="" src="/Content/images/products/<%=listProduct.ImageURLSmall%>" border="0"/>
            <br />
            <%=listProduct.Manufacturer%> <%=listProduct.Name%>
            <a href="/Products/RemoveSimilarProducts/<%=product.ProductNo%>/<%=listProduct.ProductNo%>/<%=ViewData.Model.manufacturer%>">remove</a><br />
        <%
            }
        %>
        <br /><br /><br />
        <legend>Select a Manufacturer</legend>
        <label for="manufacturer">Manufacturer:</label>
        <select name="manufacturer" id="manufacturer" onchange="OnChangeManufacturer();">
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
    <% 
        if (ViewData.Model.manufacturerProducts != null)
        {
            foreach (ListProduct manufacturerProduct in ViewData.Model.manufacturerProducts)
            {
                bool bFound = false;
                if (ViewData.Model.similarProducts != null)
                {
                    foreach (SimilarProduct similarProduct in ViewData.Model.similarProducts)
                    {
                        if (similarProduct.SimilarProductNo.Equals(manufacturerProduct.ProductNo))
                        {
                            bFound = true;
                            break;
                        }
                    }
                }
                if (!bFound) {
                %>
                <br />
                <img src="/Content/images/products/<%=manufacturerProduct.ImageURLSmall%>" border="0"/>
                <br />
                <%=manufacturerProduct.Manufacturer%> <%=manufacturerProduct.Name%>
                <a href="/Products/AddSimilarProducts/<%=product.ProductNo%>/<%=manufacturerProduct.ProductNo%>/<%=ViewData.Model.manufacturer%>">add</a><br />
               <%}%>
           <%}
       }%>
    </fieldset>

    <div class="actions">
    <ul>
        <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
    </ul>
    </div>
    
</div>
</form>
</asp:Content>
