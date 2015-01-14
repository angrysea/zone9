<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SFViewData>" %>
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
        <div class="stripe">
            <div class="description"><%=company.Description %></div>
        </div>

    <%if(ViewData.Model.bBreadcrumbs){%>
    <%= Html.BreadCrumbs()%>
    <%}%>
        
        <div class="stripe">
            <% foreach(Manufacturer manufacturer in ViewData.Model.manufacturers) {
                   if (width > 2)
                   {
                       width = 0;
                       %><br /><%
                   }
                   width++;
            %>
                <div class="manufacturer">
                    <a href="/Search/Manufacturer/<%=manufacturer.Name%>">
                    <%if(!string.IsNullOrEmpty(manufacturer.Logo)){%>
                        <img class="productimage"
                            alt="<%= company.Keyword %> - <%=manufacturer.LongName %>"
		                    src="/Content/images/<%= manufacturer.Logo %>" />
		                    <br />
                    <%}%>
                    <%=manufacturer.LongName%>
                    </a>
                    <div class="manufacturertitle"><%=manufacturer.ShortDescription%></div>
                </div>
            <%}%>
        </div>
    </div>
</asp:Content>
