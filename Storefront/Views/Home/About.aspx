<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SFViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <%  Company company = ViewData.Model.company;
    SiteTheme theme = ViewData.Model.theme; 
%>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            initNavigation("/Home/Contactus");
            $("#search").focus();
        });
    </script> 

	<div class="form-container">
        <h3 class="heading">About</h3>
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
            
        <div class="msg"><%=company.Name%></div>
        <br />
        <div class="msg"><%=company.Description%></div>
        <br />
        <p>
        <%IList<string> errors = ViewData.Model.errors; if (errors != null) {%>
        <ul class="error">
        <% foreach (string error in errors) { %>
            <li><%= Html.Encode(error) %></li>
        <%}%>
        </ul>
        </p>
        <%}%>
    </div>
</asp:Content>
