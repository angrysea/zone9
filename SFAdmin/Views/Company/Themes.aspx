<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CompanyViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="lists">
        <h1>Themes</h1>
        <table class="listtable">
            <tr>
                <th>Name</th>
            </tr>
            <%
                foreach(SiteTheme t in ViewData.Model.themes)
                {
            %>
                    <tr>
                        <td>
                            <a href="/Company/ThemeMaint/<%=t.Name%>"><%=t.Name%></a>
                        </td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Company/ThemeMaint/0">Add a new Theme</a>
        </div>
    </div>
</asp:Content>


