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
        <h3 class="heading">Shipping Rates</h3>
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <div class="stripe">
            <span class="title">Domestic Shipping - Purchase of $249 or greater:</span><br />
            <p><%=company.Name%> is proud to offer FREE UPS Ground shipping on all orders totaling at least 
            $249 or greater to all U.S. destinations except Alaska and Hawaii; shipments to Alaska and Hawaii 
            can only be shipped by 2nd day or Next Day service.</p>
        </div>
        <br />
        <br />
        <div class="stripe">
            <span class="title">Domestic Shipping - Purchases of less than $249</span><br />
            <p><%=company.Name%> will ship all orders less than $249 in the United States by UPS. 
            Before your order is placed, our systems electronically interact with the UPS computers to 
            determine the actual shipping costs from our warehouse to your doorstep. 
            This way, you will know the actual shipping costs before your order is placed. 
            Note: We do not markup UPS shipping costs.</p>
            <p>All orders are shipped using UPS and are fully insured in transit. Please note that we cannot ship to P.O. Boxes via UPS.</p>
        </div>
        <br />
        <br />
        <div class="stripe">
            <span class="title">International Shipping & US Military</span><br />
            <p>We now ship internationally via the US Postal Service to select countries. 
            International orders are shipped via the USPS Global Express Mail service, providing 
            trackable shipments to the following countries:</p>
        </div>
        <br />
        <br />
        <div class="stripe">
            <table>
            <tr>
                <td>Australia (AU)</td>
                <td>$31.00</td>
            </tr>
            <tr>
                <td>Belgium (BE)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Canada (CA)</td>
                <td>$26.25</td>
            </tr>
            <tr>
                <td>Finland (FI)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>France (FR)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Germany (DE)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Iceland (IS)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Ireland (IE)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Italy (IT)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Luxembourg (LU)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Netherlands (NL)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Norway (NO)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Poland (PL)</td>
                <td>$30.50</td>
            </tr>
            <tr>
                <td>Portugal (PT)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Scotland (SF)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Spain (ES)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Sweden (SE)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>Switzerland (CH)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>United Kingdom (GB)</td>
                <td>$33.50</td>
            </tr>
            <tr>
                <td>US Military (APO)</td>
                <td>$12.00</td>
            </table>
        </div>
    </div>
</asp:Content>
