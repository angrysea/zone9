<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AccountViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            initNavigation("/Account/YourAccount");
            $("#currentemailaddress").focus();

            $("#Cancel").click(function(e) {
                window.location = "/Home/Index";
            });

            $("#Change").click(function(e) {
                var currentemailaddress = $("#currentemailaddress");
                if (checkEmail(currentemailaddress.val()) == false) {
                    currentemailaddress.focus();
                    e.preventDefault();
                    return false;
                }
                var emailaddress = $("#newmailaddress");
                if (checkEmail(newmailaddress.val()) == false) {
                    newmailaddress.focus();
                    e.preventDefault();
                    return false;
                }
                return true;
            });
        });

    </script>
    
    <form name="form1" action="/Account/UpdateEmail" method="post" onsubmit="return(OnSubmitForm());">
        <input name="user" type="hidden" value="<%=ViewData.Model.user.Email%>"/>
        <div class="frame" id="input-screen">
        <p>
        <%IList<string> errors = ViewData.Model.errors;
          if (errors != null) {%>
                <ul class="error">
                <% foreach (string error in errors) { %>
                    <li><%= Html.Encode(error) %></li>
                <% } %>
                </ul>
            <%
        }
        %>
        </p>
        <fieldset>
        <legend>Change your e-mail address</legend>
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <label for="currentemailaddress">Current E-Mail:</label>
        <input name="currentemailaddress" type="text" size="25" readonly="readonly" value="<%=user.getemail()%>"/>
        <br />
        <label for="newemailaddress">New e-mail address:</label>
        <input name="newemailaddress" type="text" size="25"/>
        </fieldset>
        <br/>
        <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="Change Email" id="Change" /></li>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </div>
</form>

</asp:Content>
