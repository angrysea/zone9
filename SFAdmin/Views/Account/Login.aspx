<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<SFViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">

<% Company company = ViewData.Model.company; %>

    <script type="text/javascript" >
        $(document).ready(function() {
            $("#email").focus();
        });
    </script>

    <form name="form1" method="post" action="/Account/Login">
        <div class="frame" id="input-screen">
                <%IList<string> errors = ViewData.Model.errors;
                  if (errors != null) {%>
                <div>
                    <ul class="error">
                    <% foreach (string error in errors) { %>
                        <li><%= Html.Encode(error) %></li>
                    <%}%>
                    </ul>
                </div>
                <%}%>
                <fieldset>
                <legend>Login</legend>
                    <br />
                    <span>
                        Please enter your email address and password below. If you don't have an account,
                        please <a href="/Account/Register">Register</a>.
                    </span>
                    <br />
                    <br />
                    <label for="email">Email Address:</label>
                    <input type="text" name="email" id="email"/>
                    <br />
                    <label for="password">Password:</label>
                    <input type="password" name="password"/>
                    <br />
                    <label for="password">Remember me?</label>
                    <input type="checkbox" class="radioStyle" name="rememberMe" checked="checked" />
                    <br />
                </fieldset>
                <div class="actions">
                <ul>
                    <li><input class="button" type="submit" value="Login" /></li>
                </ul>
                </div>
        </div>
    </form>
</asp:Content>
