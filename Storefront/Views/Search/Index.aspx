<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SearchViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme;
    
%>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        initNavigation("/Home/Index");
        $("#sortby").focus();

        $("#sortby").change(function(e) {
            $("#sortbyform").submit();
            return true;
        });
    });
</script>
    <form id="sortbyform" action="/Search/SortOrder" method="post" >
    <input type="hidden" name="page" value="<%=ViewData.Model.page %>" />
    <input type="hidden" name="searchId" value="<%=ViewData.Model.searchId %>" />
    
    <div class="mainsearch">
        <div class="searchstripe">
        <%if (ViewData.Model.searchBreadCrumbs != null){%>
        <a href="/Home/Index">Home&nbsp;<img alt="" src="/Content/images/arrowright.gif" style="border-style: none" /></a>
        <%foreach (SearchBreadCrumb crumb in ViewData.Model.searchBreadCrumbs){%>
        <a href="/Search/<%=crumb.Url%>"><%=crumb.Description%>&nbsp;<img alt="" src="/Content/images/arrowright.gif" style="border-style: none" /></a>
        <%}}else{%>
        <h2>Search Results for: <%=ViewData.Model.keywordsearch%></h2>
        <%}%>
        </div>

        <div class="searchstripe">
            <label for="sortby">Sort By:</label>
            <select name="sortby" id="sortby">
            <%foreach (SortFields sortField in ViewData.Model.sortFields) { %>
                <option value="<%=sortField.SortKey%>" <%=sortField.SortKey==ViewData.Model.sortBy?"selected='selected'":"" %>><%=sortField.Description%></option>
            <%} %>
            </select>
        </div>
        <div class="searchstripe">
            <div class="description"><%=company.Description %></div>
            </div>
        <div class="searchstripe">
            <ul class="search">
                <% if (ViewData.Model.page > 1){%>
                <li class="left">
                    <a href="/Search/Index/<%=ViewData.Model.searchId%>/<%=ViewData.Model.page-1%>"><img alt="" src="/Content/images/arrowleft.gif" style="border-style: none" />&nbsp;Previous Results</a>
                </li>
                <%}%>
                <%if(ViewData.Model.items.Count>(theme.Searchresultrow*theme.Searchresultcol)) { %>
                <li class="right">
                    <a href="/Search/Index/<%=ViewData.Model.searchId%>/<%=ViewData.Model.page+1%>">More Results&nbsp;<img alt="" src="/Content/images/arrowright.gif" style="border-style: none" /></a>
                </li>
                <%}%>
            </ul>       
        </div>
        <%if (ViewData.Model.items==null || ViewData.Model.items.Count == 0)
          {
            %><div class="searchstripe"><h2>No Items match your selection criteria!</h2></div> <%
          }
            for(int j = 0, current=0; j<theme.Searchresultrow && ViewData.Model.items.Count>current; j++) { %>
        <div class="searchstripe">
        <%  
            for(int i = 0; i<theme.Searchresultcol && ViewData.Model.items.Count>current; i++, current++) {
                SearchListItem searchItem = ViewData.Model.items[current];%>
            <div class="featuredproduct">
                    <a href="/Product/Index/<%=searchItem.ProductNo%>">
                        <img class="productimage"
                            alt="<%= company.Keyword %> - <%=searchItem.Manufacturer %> <%=searchItem.Name %>"
		                    src="/Content/images/products/<%= searchItem.ImageURLSmall %>" />
                    </a>
                    <span class="featuredtitle"><%=searchItem.Manufacturer%>&nbsp;<%=searchItem.Name%></span>
	                <span class="strike"><%= string.Format("{0:$#,#.00}", searchItem.ListPrice)%></span>
	                <span class="red"><%= string.Format("{0:$#,#.00}", searchItem.OurPrice)%></span>
            </div>
        <%}%>
        </div>
        <%}%>
        <div class="searchstripe">
            <ul class="search">
                <% if (ViewData.Model.page > 1){%>
                <li class="left">
                    <a href="/Search/Index/<%=ViewData.Model.searchId%>/<%=ViewData.Model.page-1%>"><img alt="" src="/Content/images/arrowleft.gif" style="border-style: none" />&nbsp;Previous Results</a>
                </li>
                <%}%>
                <%if(ViewData.Model.items.Count>(theme.Searchresultrow*theme.Searchresultcol)) { %>
                <li class="right">
                    <a href="/Search/Index/<%=ViewData.Model.searchId%>/<%=ViewData.Model.page+1%>">More Results&nbsp;<img alt="" src="/Content/images/arrowright.gif" style="border-style: none" /></a>
                </li>
                <%}%>
            </ul>       
        </div>
    </div>
    </form>
</asp:Content>
