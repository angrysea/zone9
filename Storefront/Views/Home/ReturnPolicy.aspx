<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SFViewData>" %>
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
            initNavigation("/Home/Contactus");
            $("#search").focus();
        });
    </script> 

	<div class="form-container">
        <h3 class="heading">Privacy & Security</h3>
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <div class="stripe">
            <span class="title"><%=company.Name%> offers a 100% money-back guarantee!</span>
        </div>
        <div class="stripe">
            <p>If you are not 100% satisfied with your new knife, just return it to us in original condition for a full refund! 
            No questions asked. Your 100% satisfaction is very important to us, if you are not happy, we're not happy. 
            We strive to provide our customers with the highest level of service possible. From first visit to order delivery, 
            we want you to be completely satisfied with your experience. Our friendly and knowledgeable sales staff is 
            available to help you find the product that best fits your needs.</p>
            <p>You can shop with confidence as every 
            product we ship is covered by a manufacturer's warranty, unless otherwise noted. If the item is damaged 
            or misrepresented, it is either replaced or you get a full refund.</p>
        </div>
        <div class="stripe">
            <span class="title">Receipt of Merchandise:</span>
        </div>
        <div class="stripe">
            <p>Refunds for authorized returns will be issued within 24 - 72 hours of receipt of the merchandise in its original condition. 
            Please contact us for instructions where to return the merchandise at <a href="mailto:<%=company.EMail1%>"><%=company.EMail1%></a>. Thank You.</p>

        </div>

    </div>
</asp:Content>
