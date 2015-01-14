<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CarriersViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="lists">
        <h1>Carriers</h1>
        <table class="listtable">
            <tr>
                <th>Code</th>
                <th>Name</th>
                <th>Description</th>
            </tr>
            <%
                foreach (Carrier carrier in ViewData.Model.carriers)
                {
            %>
                    <tr>
                        <td>
                            <a href="/Company/CarrierMaint/<%=carrier.Code%>"><%=carrier.Code%></a>
                        </td>
                        <td><%=carrier.Name%></td>
                        <td><%=carrier.Description%></td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Company/CarrierMaint">Add a new Carrier</a>
        </div>    
    </div>
</asp:Content>


