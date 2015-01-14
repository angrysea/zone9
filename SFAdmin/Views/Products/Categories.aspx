<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CategoriesViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="lists">
        <h1>Categories</h1>
        <table class="listtable">
            <tr>
                <th>Name</th>
                <th>Long Name</th>
                <th>Group</th>
            </tr>
            <%
                foreach(Category category in ViewData.Model.categories)
                {
            %>
                    <tr>
                        <td>
                            <a href="/Products/CategoryMaint/<%=category.Name%>"><%=category.Name%></a>
                        </td>
                        <td><%=category.LongName%></td>
                        <td><%=category.GroupType%></td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Products/CategoryMaint">Add a new Category</a>
        </div>
    </div>
</asp:Content>


