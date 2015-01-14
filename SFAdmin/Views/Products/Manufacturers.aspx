<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<ManufacturersViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="lists">
        <h1>Manufacturers</h1>
        <table class="listtable">
            <tr>
                <td><h2>Name</h2></td>
                <td nowrap="nowrap" align="right"><h2>Markup</h2></td>
                <td nowrap="nowrap" align="center"><h2>Active</h2></td>
            </tr>
            <%
                foreach(Manufacturer manufacturer in ViewData.Model.manufacturers)
                {
            %>
                    <tr>
                        <td>
                            <a href="/Products/ManufacturerMaint/<%=manufacturer.Name%>"><%=manufacturer.Name%></a>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <%=manufacturer.MarkUp*100.0%>%
                        </td>
                        <td nowrap="nowrap" align="center">
                            <%=(manufacturer.Active==1?"yes":"no")%>
                        </td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Products/ManufacturerMaint/">Add a new Manufacturer</a>
        </div>
    </div>
</asp:Content>
