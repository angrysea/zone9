<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<AccountViewData>" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            $("#email").focus();

            $("#Register").click(function(e) {
                var email = $("#email");
                if (email.val().length == 0) {
                    alert("Invalid Email!");
                    email.focus();
                    e.preventDefault();
                    return false;
                }

                var password = $("#password");
                if (password.val().length == 0) {
                    alert("Invalid Password!");
                    password.focus();
                    e.preventDefault();
                    return false;
                }

                var confirmPassword = $("#confirmPassword");
                if (confirmPassword.val().length == 0) {
                    alert("Invalid Confirm Password!");
                    confirmPassword.focus();
                    e.preventDefault();
                    return false;
                }

                if (password.val() != confirmPassword.val()) {
                    alert("Invalid Password does not match confirmation!");
                    password.focus();
                    e.preventDefault();
                    return false;
                }
                return true;
            });
        });
    </script>

    <form name="form1" method="post" action="/Account/Register">
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
    <div class="frame" id="input-screen">
        <fieldset>
            <legend>Account Creation</legend>
            <p>
                Use the form below to create a new account. 
            </p>
            <p>
                Passwords are required to be a minimum of <%=Html.Encode(ViewData.Model.PasswordLength)%> characters in length.
            </p>
            <br />
            <label for="email">Email:</label>
            <input type="text" name="email" id="email"/>
            <br />
            <label for="password">Password:</label>
            <input type="password" name="password" id="password"/>
            <br />
            <label for="confirmPassword">Confirm Password:</label>
            <input type="password" name="confirmPassword" id="confirmPassword"/>
            <br />
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="Register" id="Register"/></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
