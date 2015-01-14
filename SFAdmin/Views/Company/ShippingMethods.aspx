<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<ShippingMethodsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="lists">
        <h1>Shipping Methods</h1>
        <table class="listtable">
            <tr>
                <th>Code</th>
                <th>Description</th>
                <th>Country</th>
            </tr>
        <%
            foreach(ShippingMethod shippingMethod in ViewData.Model.shippingMethods)
            {
        %>
                <tr>
                    <td>
                        <a href="/Company/ShippingMethodMaint/<%=shippingMethod.Code%>"><%=shippingMethod.Code%></a>
                    </td>
                    <td><%=shippingMethod.Description%></td>
                    <td><%=shippingMethod.Country%></td>
                </tr>
        <%
            }
        %>
        </table>
        <br/>

        <div class="actions">
            <a href="/Company/ShippingMethodMaint/">Add a new ShippingMethod</a>
        </div>
</div>        


</asp:Content>


