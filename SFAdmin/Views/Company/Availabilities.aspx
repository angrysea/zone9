<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<AvailabilitiesViewData>" %>
<%@ Import Namespace="SFAdmin.Models" %>
<%@ Import Namespace="StorefrontModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="lists">
        <h1>Status Codes</h1>
        <table class="listtable">
            <tr>
                <th>Code</th>
                <th>Description</th>
                <th>Expected Wait</th>
                <th>Prioriy</th>
            </tr>
        <%
            foreach (Availability availability in ViewData.Model.availabilities)
            {
        %>
                <tr>
                    <td>
                        <a href="/Company/AvailabilityMaint/<%=availability.Code%>"><%=availability.Code%></a>
                    </td>
                    <td><%=availability.Description%></td>
                    <td><%=availability.ExpectedWait%></td>
                    <td><%=availability.Priority%></td>
                </tr>
        <%
            }
        %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Company/AvailabilityMaint/">Add a new Status Code</a>
        </div>
    </div>

</asp:Content>


