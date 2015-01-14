<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LinkCategoriesViewData>" %>
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
    <h3 class="heading">Links</h3>
    
    <%if(ViewData.Model.bBreadcrumbs){%>
    <%= Html.BreadCrumbs()%>
    <%}%>
    
    <div class="searchstripe">
        <span class="title">Link to <%=company.Name%></span><br />
        <p>Link popularity is fast becoming one of the highest weighted criteria used in ranking your 
            site in the search engines. The more related sites linking to yours the higher your sites 
            ranking climbs in the search engine results. We like to link with sites that complement ours 
            or can offer our clients a valid service. We will not exchange links with any website that is 
            unduly offensive or fraudulently boastful in its claims.</p>

        <span class="title">How do I swap links with <%=company.Name%>?</span><br />
        <p><a href="/Links/Signup">Please click to submit your site for listing consideration or click to modify your current listing.</a></p>

        <span class="title">Please visit our link partners within the following categories:</span>
    </div>

    <%foreach(LinkCategory category in ViewData.Model.categories) { %>
        <div class="searchstripe">
            <a href="/Links/Category/<%=category.Name%>"><%=category.LongName%>&nbsp;<img alt="<%=company.Keyword1%> and <%=company.Keyword2%> - <%=category.LongName%>" src="/Content/images/arrowright.gif"/></a>
        </div>
    <%}%>
</div>
</asp:Content>
