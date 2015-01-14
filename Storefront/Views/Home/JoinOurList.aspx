<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<JoinViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
%>
<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        initNavigation("/Home/Index");
        $("#search").focus();
    });
</script>

<div class="form-container">
    <%if(ViewData.Model.bBreadcrumbs){%>
    <%= Html.BreadCrumbs()%>
    <%}%>
        
	<div class="msg">
        <%if(ViewData.Model.Joined){ %>
            <%=ViewData.Model.EMail%> has been subscribed to the <%=company.Name%> e-mail list. Thank you.
        <%} else {%>
            <%=ViewData.Model.EMail%> is already on the <%=company.Name%> e-mail list. Thank you.
        <%}%>
        <br />
        We respect your privacy. <%=company.Name%> does not share e-mail addresses with third parties. Instructions for unsubscribing come with every e-mail.
    </div>
    <div>
        <a href="/Home/Index">Continue Shopping</a>    
    </div>
</div>
</asp:Content>
