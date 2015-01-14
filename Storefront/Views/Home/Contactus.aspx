<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SFViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <%  Company company = ViewData.Model.company;
    SiteTheme theme = ViewData.Model.theme; 
%>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            initNavigation("/Home/Contactus");
            $("#search").focus();
        });
    </script> 

	<div class="form-container">
        <h3 class="heading">Contact Us</h3>
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
            
        <br />
        <div class="msg"><%=company.Name%></div>
        <br />
        <div class="msg"><%=company.Address1%></div>
        <br />
        <%if(company.Address2 != null && company.Address2.Length > 0){%>
            <div class="msg"><%=company.Address2%></div>
        <br />
        <%} if(company.Address3 != null && company.Address3.Length > 0){%>
            <div class="msg"><%=company.Address3%></div>
        <br />
        <%}%>
        <div class="msg"><%=company.City%>, <%=company.State%> <%=company.Zip%> <%=company.Country%></div>
        <br/>
        <div class="msg"><b>E-Mail:</b></div>
        <div class="msg"><a href="mailto:<%=company.EMail1%>"><%=company.EMail1%></a></div>
        <br/>
        <div class="msg"><b>Telephone</b></div>
        <%if (company.CustomerService != null && company.CustomerService.Length > 0){%>
            <br />
            <div class="msg"><%=company.CustomerService%> phone</div>
        <%} if (company.Fax != null && company.Fax.Length > 0){%>
            <br />
            <div class="msg"><%=company.Fax%> fax</div>
        <%}%>
        <br/>
        <br/>
        <div class="msg">E-mail or call any time to speak with one of our Customer Service representatives.</div>
        <br/>
        <p>
        <%IList<string> errors = ViewData.Model.errors; if (errors != null) {%>
        <ul class="error">
        <% foreach (string error in errors) { %>
            <li><%= Html.Encode(error) %></li>
        <%}%>
        </ul>
        </p>
        <%}%>
    </div>
</asp:Content>
