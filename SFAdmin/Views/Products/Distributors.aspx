<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<DistributorsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="lists">
        <h1>Distributors</h1>
        <table class="listtable">
            <tr>
                <th>Name</th>
                <th>Description</th>
            </tr>
            <%
                foreach(Distributor distributor in ViewData.Model.distributors)
                {
            %>
                    <tr>
                        <td>
                            <a href="/Products/DistributorMaint/<%=distributor.Name%>"><%=distributor.Name%></a>
                        </td>
                        <td><%=distributor.Description%></td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Products/DistributorMaint">Add a new Distributor</a>
        </div>    
    </div>
</asp:Content>


