<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<SpecificationsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="lists">
        <h1>Specification</h1>
        <table class="listtable">
            <tr>
                <th>Name</th>
                <th>Description</th>
            </tr>
            <tr>
                <td colspan="10">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td><img src="/Content/images/blank.gif" alt="" width="450" height="1" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%
                foreach (Specification specification in ViewData.Model.specifications)
                {
            %>
                    <tr>
                        <td>
                            <a href="/Products/SpecificationMaint/<%=specification.Name%>"><%=specification.Name%></a>
                        </td>
                        <td><%=specification.Description%></td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
                <table border="0">
                    <tr>
                        <td><a href="/Products/SpecificationMaint">Add a new Specification</a></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


</asp:Content>


