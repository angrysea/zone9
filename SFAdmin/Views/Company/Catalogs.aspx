<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CatalogsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="lists">
        <h1>Catalogs</h1>
        <table class="listtable">
            <tr>
                <th>Name</th>
                <th>Description</th>
            </tr>
            <%foreach(Catalog catalog in ViewData.Model.catalogs) {%>
                <tr>
                    <td>
                        <a href="/Company/CatalogMaint/<%=catalog.Name%>">
                            <%=catalog.Name%>
                        </a>
                    </td>
                    <td><%=catalog.Description%></td>
                </tr>
            <%}%>
        </table>
        <br/>
        <div class="actions">
            <a href="/Company/CatalogMaint">Add a new Catalog</a>
        </div>    
    </div>
</asp:Content>


