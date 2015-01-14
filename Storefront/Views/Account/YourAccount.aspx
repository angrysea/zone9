<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AccountViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%  
        Company company = ViewData.Model.company; 
        SiteTheme theme = ViewData.Model.theme; 
        Customer customer  = ViewData.Model.customer;
    %>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            initNavigation("/Account/YourAccount");
            $("#search").focus();
        });
    </script>

	<div class="frame">
	    <h1>Your Account</h1>
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
	    <br />
        <div class="signedin">
        You are signed in as <%=customer.Fullname%> <a href="/Account/SignOut/<%=ViewData.Model.user.Email%>">Sign out?</a>
        </div>
        <div class="stripe">
            <div class="col1">
                <h2>Orders</h2>
                <span class="text">See Order history & modify open orders</span>
            </div>
            <div class="col2">
                <ul class="tiles">
                    <li>
                        <div>
                           <h3> View Recent Orders </h3>
                           <dl class="info">
                            <dt>Track a package</dt>
                            <dt>Print an invoice</dt>
                            <dt>Combine orders</dt>
                            <dt>Change orders</dt>
                            <dt>Cancel orders</dt>
                            </dl><br />
                            <a href="/Account/RecentOrders/<%=customer.CustomerNo%>">View recent orders</a>
                        </div>
                    </li>
                    <li>
                        <div>
                           <h3> Past Purchases </h3>
                               <a href="/Account/InvoiceList/<%=customer.CustomerNo%>">View past orders</a><br />
                               <a href="/Account/PreOrderHistory/<%=customer.CustomerNo%>">View filled pre-orders</a><br />

                        </div>
                    </li>
                    <li>
                        <div>
                           <h3> Other Options </h3>
                               <a href="">Return an item</a><br />
                               <a href="">Review an item</a><br />
                               <a href="">Leave feedback</a><br />
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <br />
        <div class="stripe">
            <div class="col1">
                <h2>Settings</h2>
                <span class="text">Addresses, Password &amp; E-mail</span>
            </div>
            <div class="col2">
                <ul class="tiles">
                    <li>
                        <div>
                           <h3> Account Settings </h3>
                               <a href="/Account/Changeemail/<%=customer.CustomerNo%>">Change your e-mail address.</a><br />
                               <a href="/Account/ChangePassword/<%=customer.CustomerNo%>">Change your Password</a><br />
                               <a href="/Account/ForgotPassword/<%=customer.CustomerNo%>">Forgot Your Password?</a><br />
                               <a href="/Account/QuickClick/<%=customer.CustomerNo%>">Quick-Click Settings</a><br />
                        </div>
                    </li>
                    <li>
                        <div>
                           <h3>Address Book</h3>
                               <a href="/Account/AddressBook/<%=customer.CustomerNo%>">Manage Address List</a><br />
                               <a href="/Account/AddressAdd/<%=customer.CustomerNo%>">Add New Address</a><br />

                        </div>
                    </li>
                    <li>
                        <div>
                           <h3> E-mail Notifications </h3>
                               <a href="">E-mail Preferences & Notifications</a><br />
                               <a href="">Product Availability Alerts</a><br />
                               <a href="">Special Occasion Reminders</a><br />
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <br />
        <div class="stripe">
            <div class="col1">
                <h2>Payment</h2>
                <span class="text">Credit Cards, PayPal &amp; Gift Cards</span>
            </div>
            <div class="col2">
                <ul class="tiles">
                    <li>
                        <div>
                           <h3>Payment Method</h3>
                               <a href="/Account/CreditCardList/<%=customer.CustomerNo%>">Manage Payment Options.</a><br />
                               <a href="/Account/CreditCardAdd/<%=customer.CustomerNo%>">Add a Credit Card.</a><br />
                        </div>
                    </li>
                    <li>
                        <div>
                           <h3>Gift Cards</h3>
                               <a href="/Account/GiftCardList/<%=customer.CustomerNo%>">View Gift Certificate/Card Balance.</a><br />
                               <a href="/Account/GiftCardApply/<%=customer.CustomerNo%>">Apply a Gift Certificate/Card to Your Account.</a><br />
                               <a href="/Account/GiftCardPurchase/<%=customer.CustomerNo%>">Purchase a Gift Card.</a><br />
                        </div>
                    </li>
                </ul>
            </div>
        </div>
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
