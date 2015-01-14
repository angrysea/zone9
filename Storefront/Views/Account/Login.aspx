<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SFViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">

    $(document).ready(function() {
        initNavigation("/Account/Login");
        $("#email").focus();

        $("#Login").click(function(e) {
            if (checkEmail($("#email").val()) == false) {
                $("#email").focus();
                e.preventDefault();
                return;
            }

            if ($("input[@name='customer']:checked").val() == "existing") {
                if ($("#password").val().length == 0) {
                    alert("Password required!");
                    $("#password").focus();
                    e.preventDefault();
                    return;
                }
            }
            else {
                window.location = "/Account/CreateAccount/" + $("#email").val();
                e.preventDefault();
                return;
            }
        });

        $("#Cancel").click(function(e) {
            window.location = "/Home/Index";
        });

    });   
</script>

    <form id="form1" method="post" action="/Account/Login">
    <input type="hidden" name="gotourl" value="<%=ViewData.Model.gotourl%>" />
    
	<div class="login-container">
        <div class="indexstripe">            
            <%IList<string> errors = ViewData.Model.errors; if (errors != null) {%>
            <ul class="error">
            <% foreach (string error in errors) { %>
                <li><%= Html.Encode(error) %></li>
            <% } %>
            </ul>
            <%}%>
        </div>
        <div class="indexstripe">Please enter your username and password below.</div>
        <div class="stripe">            
            <fieldset>
            <label for="email">E-Mail Address:</label>
            <input type="text" class="firstinput" name="email" id="email"/>
            <br />
            <label for="newcustomer">I'm a new customer.</label>
            <input type="radio" class="radioStyle" name="customer" value="new" />
            <br />
            <label for="existingcustomer">I already have an account.</label>
            <input type="radio" class="radioStyle" name="customer" value="existing" checked="checked" />
            <br />
            <label for="password">Password:</label> 
            <input type="password" name="password" id="password"/>
            <br />
            <div class="forgot">
            <a href="" >Forgot your password?</a>
            </div>
            </fieldset>
            <br />
        </div>
        <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="Login" id="Login" /></li>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
	</div>
    </form>
</asp:Content>
