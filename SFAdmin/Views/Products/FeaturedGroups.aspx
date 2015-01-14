<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<FeaturedGroupViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="lists">
        <h1>Featured Groups</h1>
        <table class="listtable">
            <tr>
                <th>Name</th>
                <th>Type</th>
                <th>Heading</th>
                <th>Active</th>
            </tr>
            <%
                foreach (FeaturedGroup featuredGroup in ViewData.Model.featuredGroup)
                {
            %>
                    <tr>
                        <td>
                            <a href="/Products/FeaturedGroup/<%=featuredGroup.Name%>"><%=featuredGroup.Name%></a>
                        </td>
                        <td><%=featuredGroup.Type%></td>
                        <td><%=featuredGroup.Heading%></td>
                        <td><%=featuredGroup.Active%></td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Products/FeaturedGroup">Add new Featured Products</a>
        </div>
    </div>
</asp:Content>


