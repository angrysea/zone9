<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IndexViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme;
    int i = 0;
%>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        initNavigation("/Home/Index");
        $("#search").focus();
    });
</script>

        <div class="main">
            <div class="indexstripe">
                <div class="description"><%=company.Description %></div>
            </div>
            <%foreach (FeaturedGroupsViewData featuredGroups in ViewData.Model.featuredGroups){%>
            <div class="indexstripe">
                    <span class="title"><%=featuredGroups.featuredGroup.Heading%></span><br />
                    <%=featuredGroups.featuredGroup.Comments%>
            </div>
            <div class="stripe">
                <div>
                    <%i=0;
                      foreach (ListProduct listProduct in featuredGroups.listProducts)
                      {
                          i++;
                          if (i > theme.Featuredproductcount)
                              break;
                      %>
                    <div class="featuredproduct">
                        <a href="/Product/Index/<%=listProduct.ProductNo%>">
                            <img class="productimage"
                                alt="<%= company.Keyword %> - <%=listProduct.Manufacturer %> <%=listProduct.Name %>"
			                    src="/Content/images/products/<%= listProduct.ImageURLSmall %>" />
                        </a>
                        <span class="featuredtitle"><%=listProduct.Manufacturer%>&nbsp;<%=listProduct.Name%></span>
		                <span class="strike"><%= string.Format("{0:$#,#.00}", listProduct.ListPrice)%></span>
		                <span class="red"><%= string.Format("{0:$#,#.00}", listProduct.OurPrice)%></span>
                    </div>		    
		            <%}%>
                </div>
            </div>
            <%}%>				
        </div>
        <div class="right">
            <%if(company.FreeShipping > 0){%>
                <div class="freeshipping">
                    <div class="freeshippingtitle">Free Shipping</div>
                    <img src="/Content/images/box.gif" />
                    <div>(on orders of<br /> $<%=string.Format("{0:#,#}", company.FreeShippingMin)%> or more)</div>
                </div>
            <%}%>
            <br />
            <span>Most Popular</span>
            <%  for (int p = 0; p < theme.Searchresultrow && p < theme.Mostpopularcount; p++){
                    ListProduct listProduct = ViewData.Model.productRankings[p];
            %>
            <div>
                <a href="/Product/Index/<%=listProduct.ProductNo%>">
                    <img 
                         class="productimage" 
                        alt="<%=company.Keyword%> - <%=listProduct.Manufacturer%> <%=listProduct.Name%>" 
                        src="/Content/images/products/<%=listProduct.ImageURLSmall%>" />
                </a>
                <br />
                <span class="featuredtitle"><%=listProduct.Manufacturer%>&nbsp;<%=listProduct.Name%></span>
                <span class="strike"><%= string.Format("{0:$#,#.00}", listProduct.ListPrice)%></span>
                <span class="red"><%= string.Format("{0:$#,#.00}", listProduct.OurPrice)%></span>
            </div>                
            <br />
            <%}%>
        </div>
        <div class="indexstripe">
            <span class="title">Recently Viewed Products</span>
        </div>
        <div class="stripe large">
            <%
                i = 0;
                foreach (ListProduct recentlyViewed in ViewData.Model.recentlyViewed)
                {
                    i++;
                    if (i > theme.RecentlyViewedCount)
                        break;
            %>
                    <div class="featuredproduct">
                        <a href="/Product/Index/<%=recentlyViewed.ProductNo%>">
                            <img 
                                alt="<%=company.Keyword%> - <%=recentlyViewed.Manufacturer%> <%=recentlyViewed.Name%>" 
                                src="/Content/images/products/<%=recentlyViewed.ImageURLSmall%>" 
                                 class="productimage"/>
                        </a>
                        <span class="featuredtitle"><%=recentlyViewed.Manufacturer%> <%=recentlyViewed.Name%></span>
		                <span class="strike"><%= string.Format("{0:$#,#.00}", recentlyViewed.ListPrice)%></span>
		                <span class="red"><%= string.Format("{0:$#,#.00}", recentlyViewed.OurPrice)%></span>
                        
                    </div>
            <%
                }
            %>
            </div>
</asp:Content>
