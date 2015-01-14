<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<GroupTypesViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="lists">
        <h1>Group Types</h1>
        <table class="listtable">
            <tr>
                <th>Name</th>
                <th>Description</th>
            </tr>
            <%
                foreach (GroupType groupType in ViewData.Model.groupTypes)
                {
            %>
                    <tr>
                        <td>
                            <a href="/Products/GroupTypeMaint/<%=groupType.Name%>"><%=groupType.Name%></a>
                        </td>
                        <td><%=groupType.Description%></td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Products/GroupTypeMaint">Add a new Group Type</a>
        </div>    
    </div>
</asp:Content>


