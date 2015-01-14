<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LinksViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme;    
%>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
    });
</script>
<div class="frame">
    <fieldset>
        <legend><%=ViewData.Model.category%> Links</legend> 
    </fieldset>

    <%if(ViewData.Model.bBreadcrumbs){%>
    <%= Html.BreadCrumbs()%>
    <%}%>

    <%foreach(Link link in ViewData.Model.links) { %>
        <div class="searchstripe">
            <a href="<%=link.Url%>"><%=link.SiteName%>&nbsp;<img alt="<%=company.Keyword1%> and <%=company.Keyword2%>" src="/Content/images/arrowright.gif"/></a><br />
            <p><%=link.Description%></p>
        </div>
    <%}%>
</div>
</asp:Content>
