<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GroupViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme;
    int width = 0;
%>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        initNavigation("/Home/Index");
        $("#search").focus();
    });
</script>

    <div class="main">
        <div class="indexstripe">
        <a href="/Home/Index">Home&nbsp;<img alt="" src="/Content/images/arrowright.gif" style="border-style: none" /></a>
        </div>

        <div class="indexstripe">
            <div class="description"><%=company.Description %></div>
        </div>
        
    <%if(ViewData.Model.bBreadcrumbs){%>
    <%= Html.BreadCrumbs()%>
    <%}%>
        
        
        <div class="indexstripe">
            <%foreach(Category category in ViewData.Model.categories) {
                   if (width > 2)
                   {
                       width = 0;
                       %><br /><%
                   }
                   width++;
            %>
            <div class="manufacturer">
                <div>
                    <a  href="/Search/Category/<%=category.Name%>"
                        title="<%=company.Keyword1%> and <%=company.Keyword2%> - <%=category.Name%>">
                    <%if(!string.IsNullOrEmpty(category.Logo)){%>
                        <img class="productimage"
                            alt="<%= company.Keyword %> - <%=category.LongName %>"
		                    src="/Content/images/<%= category.Logo %>" />
		                    <br />
                    <%}%>
                        <%=category.LongName%>
                    </a>
                    <div class="manufacturertitle"><%=category.Description%></div>
                </div>		    
            </div>
            <%}%>
        </div>
    </div>
</asp:Content>
