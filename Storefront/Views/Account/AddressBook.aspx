<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AddressBookViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Customer customer = ViewData.Model.customer;
    Address address = null;
    CreditCard creditCard = null;
    int count = 0;   
%>

<script src="/Scripts/jquery.simplemodal.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        initNavigation("/Account/YourAccount");

	    <%
            count = 0;   
	        foreach (AddressBookEntries addressEntry in ViewData.Model.AddressBook) {
                address = addressEntry.Address;
                creditCard = addressEntry.CreditCard;
                count++;
              
        %>
        $("#DeleteAddress<%=count%>").click(function(e) {
            e.preventDefault();
           
            confirm("Are you sure you want to delete this Address?", function() {
                window.location.href = "/Account/AddressDelete/<%=address.AddressID%>/<%=address.Customer%>";
            });
        });
        <%}%>
        
        $("#Cancel").click(function(e) {
            window.location = "/Account/YourAccount";
        });

    });
</script>


	<div class="frame">
    <fieldset>
    <legend>Manage Addresses</legend> 
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
	    <%
            count = 0;   
	        foreach (AddressBookEntries addressEntry in ViewData.Model.AddressBook) {
                address = addressEntry.Address;
                creditCard = addressEntry.CreditCard;
                count++;
              
        %>
        <div class="stripe">
            <div class="stripecol">
                <span><%=address.FullName%></span>
                <span><%=address.Address1%></span>
                <%if (address.Address2 != null && address.Address2.Length>0){%>
                <span><%=address.Address2%></span>
                <% } if (address.Address3 != null && address.Address3.Length > 0){%>
                <span><%=address.Address3%></span>
                <%}%>
                <span><%=address.City%>, <%=address.State%>, <%=address.Zip%></span>
                <span><%=address.Country%></span>
                <span>Phone: <%=address.Phone%></span>
            </div>
            <div class="stripecol">
                <span>Prefrences for: <%=address.Description%></span>
                <span>Include in Quick Click: </span>
                <span>Default Shipping Method: <%=address.DefaultShipping%></span>
                <span>Payment Method: <%=creditCard!=null?creditCard.Number:""%></span>
            </div><br />
             <a href="/Account/AddressMaint/<%=address.AddressID%>">Edit</a><a id="DeleteAddress<%=count%>" href="#">Delete</a>
        </div>
        <br />
        <%}%>
        <div class="indexstripe">
            <div class="continue">
                <a href="#" id="Cancel">Cancel</a> <a href="/Account/AddressAdd/<%=customer.CustomerNo%>">Add new address</a>
            </div>
        </div>
    </fieldset>
</div>
<div id="confirm" style="display:none">
    <a href="#" title="Close" class="modalCloseX simplemodal-close">x</a>
    <div class="header"><span>Confirm</span></div>
    <p class="message"></p>
    <div class="buttons">
    <div class="no simplemodal-close">No</div><div class="yes">Yes</div>
    </div>
</div>
</asp:Content>
