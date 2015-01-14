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
            <p><%=company.Name%> keeps your personal information private and secure. When you make a purchase from our site, 
            you provide your name, email address, credit card information, address, phone number and a password. 
            We use this information to process your orders, to keep you updated on your orders and to personalize your shopping experience. 
            We do not sell, trade or rent this information to other parties. 
            Our secure servers protect the information using advanced encryption and firewall technology. 
            Your personal information cannot be read as it travels to our ordering system. 
            In order to most efficiently serve you, credit card transactions and order fulfillment are 
            handled by reputable third-party banking and distribution institutions. 
            They receive the information needed to verify and authorize your credit card and to process your order. 
            They are under strict obligation to keep your personal information private.</p>
        </div>
    </div>
</asp:Content>
