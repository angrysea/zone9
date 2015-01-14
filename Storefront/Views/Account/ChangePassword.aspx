<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AccountViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">

<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Users user  = ViewData.Model.User;
%>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            initNavigation("/Account/YourAccount");
            $("#currentpassword").focus();

            $("#Change").click(function(e) {
                password = $("#password");
                if($.trim(password.val()).length < 6) {
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
                return true;
            });

            $("#Cancel").click(function(e) {
                window.location = "/Home/Index";
            });
        });

    </script>

    <form id="form1" action="/Account/ChangePassword" method="post" onsubmit="return(OnSubmitForm());">
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
        <legend>Change Password</legend> 
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <br />
        <label for="currentpassword">Current Password:</label>
        <input name="currentpassword" id="currentpassword" type="password" size="25"/>
        <br />
        <label for="password">Password:</label>
        <input name="password" id="password" type="password" size="25"/>
        <br />
        <label for="password">Confirm:</label>
        <input name="confirmpassword" id="confirmpassword" type="password" size="25" />
        <br />
    </fieldset>
    <div class="actions">
    <ul>
        <li><input class="button" type="submit" value="Change Password" id="Change" /></li>
        <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
    </ul>
    </div>
    </div>
    </form>
</asp:Content>
    