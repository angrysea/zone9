<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AccountViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Customer customer  = ViewData.Model.Customer;
    Users users = ViewData.Model.user;
    bool update = ViewData.Model.update;
%>

<script language="javascript" type="text/javascript">
    var currentPage = 1;
    var maxPage = 4;

    $(document).ready(function() {
        initNavigation("/Account/YourAccount");
        $("#fullname").focus();

        $("#Cancel").click(function(e) {
            window.location = "/Home/Index";
        });

        $("#Create").click(function(e) {
            var email = $("#email");
            if (checkEmail(email.val()) == false) {
                email.focus();
                e.preventDefault();
                return false;
            }

            var confirmemail = $("#confirmemail");
            if (confirmemail.val() != email.val()) {
                alert("Email address does not match confirm email.");
                confirmemail.val("");
                email.focus();
                e.preventDefault();
                return false;
            }

            password = $("#password");
            if ($.trim(password.val()).length < 6) {
                alert("Password must be at least 6 characters");
                password.focus();
                e.preventDefault();
                return false;
            }

            var confirmpassword = $("#confirmpassword");
            if ($.trim(confirmpassword.val()).length < 6) {
                alert("Password must be at least 6 characters");
                confirmpassword.focus();
                e.preventDefault();
                return false;
            }

            if (password.val() != confirmpassword.val()) {
                alert("The confirmed password does not match!");
                confirmpassword.focus();
                e.preventDefault();
                return false;
            }

            fullname = $("#fullname");
            if ($.trim(fullname.val()).length == 0) {
                alert("Name is required!");
                fullname.focus();
                e.preventDefault();
                return false;
            }
        });
    });
</script>

    <form id="form1" action="/Account/CreateAccountAction" method="post">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <input type="hidden" name="gotourl" value="<%=ViewData.Model.gotourl%>" />
    <div class="frame" id="input-screen">
            <%IList<string> errors = ViewData.Model.errors; if (errors != null) {%>
            <ul class="error">
            <% foreach (string error in errors) { %>
                <li><%= Html.Encode(error) %></li>
            <%}%>
            </ul>
            <br/>
            <%}%>
        <fieldset>
        <legend>Create New Account</legend> 
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <br />
        <div class="page" id="page1">
            <label for="fullname">Full Name:</label>
            <input name="fullname" id="fullname" type="text" size="120" value="<%=update?customer.Fullname:""%>"/>
            <br />
            <label for="email">E-Mail Address:</label>
            <input name="email" id="email" type="text" size="25" value="<%=update?users.Email:ViewData.Model.EMail%>"/>
            <br />
            <label for="confirmemail">Confirm E-Mail:</label>
            <input name="confirmemail" id="confirmemail" type="text" size="25" />
            <br />
            <br />
            <div class="msg">Protect your data with a Password.</div><br />
            <label for="password">Password:</label>
            <input name="password" id="password" type="password" size="25" value="<%=update?users.Password:""%>"/>
            <br />
            <label for="password">Confirm:</label>
            <input name="confirmpassword" id="confirmpassword" type="password" size="25" value="<%=update?users.Password:""%>"/>
        </div>
        </fieldset>
    <div class="actions">
    <ul>
        <li><input class="button" type="submit" value="Create Account" id="Create" /></li>
        <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
    </ul>
    </div>
    </div>
    </form>
</asp:Content>
