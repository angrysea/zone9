<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CouponViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company;
    SiteTheme theme = ViewData.Model.theme; 
%>

    <fieldset>
        <legend>Coupons</legend>

    <%if(ViewData.Model.bBreadcrumbs){%>
    <%= Html.BreadCrumbs()%>
    <%}%>

    <%if (ViewData.Model.coupons.Count == 0){%>
        <div class="stripe">
            <br />
            <span class="title">I'm sorry, but no coupons are available at this time.</span>
        </div>
    <%}else{%>
        <% foreach (CouponData couponData in ViewData.Model.coupons){
               Coupon coupon = couponData.coupon;
        %>
        <div class="stripe">
            <div class="stripecol">
                <span>Coupon Code: <%=coupon.Code%></span>
            <%
        if (!string.IsNullOrEmpty(coupon.Product))
        {
            %>
              <span><a href="/Products/Index/<%=coupon.Product%>">
                        <img alt="<%=company.Keyword%>" src="/Content/images/<%=couponData.details.ImageURLSmall%>" />
                    </a></span>
            <%}
        else if (!string.IsNullOrEmpty(coupon.Manufacturer))
        {%>
               <span><a href="/Products/Search/Manufacturer/<%=coupon.Manufacturer%>" >
                        <img alt="<%=company.Keyword%>" src="/Content/images/<%=couponData.manufacturer.Logo%>" />
                    </a></span>      
            <%}%>
            </div>
            <div class="stripecol2">
                <span><%=coupon.Description%></span>
                <span>Expiration Date: <%=string.Format("{0:D}", coupon.ExpirationDate)%></span>
            </div>  
        </div>  
    <%}}%>
    </fieldset>
</asp:Content>
