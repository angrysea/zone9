<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AddLinkViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme;
    Link link = ViewData.Model.Link;
%>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
    initNavigation("/Home/index");
    $("#sitename").focus();

    $("#Cancel").click(function(e) {
        window.location = "/Links/Links";
    });

    $("#Create").click(function(e) {
        var email = $("#email");
        if (checkEmail(email.val()) == false) {
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

        fullname = $("#sitename");
        if ($.trim(fullname.val()).length == 0) {
            alert("Name is required!");
            fullname.focus();
            e.preventDefault();
            return false;
        }
    });
});
</script>
 
    <form id="form1" action="/Links/Signup" method="post">
    <div class="frame">
        <fieldset>
        <legend>Links</legend> 

        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>


        <h3 class="heading">Submit Your Site</h3>

        <div class="indexstripe">
            <span class="title">Webmasters: Complete the form below to submit your link request to our site. 
            You will be contacted via email when your site is approved.</span>
        </div>
        <div class="indexstripe">
            <p>NOTE: All fields are required. If you already have an active listing on our site, 
            you can click here to edit your current listing. An email will be sent to you after 
            you submit this link request. You must click the activation link within the mailer in 
            order to verify your email address. Your link request will be discarded if you do not 
            verify your email address.</p>
        </div>
        <div class="indexstripe">
            <label for="sitename">Site Name:</label>
            <input name="sitename" id="sitename" type="text" size="100" />
            <br />
            <label for="URL">URL:</label>
            <input name="URL" id="URL" type="text" size="200" />
            <br />
            <label for="description">Description:</label>
            <textarea name="description" id="description" rows="4" cols="80"></textarea>
            <br />
            <label for="reciprocate">Reciprocate URL</label>
            <input name="reciprocate" id="reciprocate" type="text" size="200" />
            <br />
            <label for="webmaster">Your Name:</label>
            <input name="webmaster" id="webmaster" type="text" size="120" />
            <br />
            <label for="webmasteremail">Your EMail:</label>
            <input name="webmasteremail" id="webmasteremail" type="text" size="120" />
            <br />
            <div class="msg">Protect your data with a Password.</div><br /><br />
            <label for="password">Password:</label>
            <input name="password" id="password" type="password" size="25" />
            <br />
            <label for="password">Confirm:</label>
            <input name="confirmpassword" id="confirmpassword" type="password" size="25"/>
            <br />
            <label for="city">Category:</label>
            <select name="category" id="category">
                <option value="">(Select)</option>
                <%foreach (LinkCategory category in ViewData.Model.categories) {%>
                    <option value="<%=category.Name%>"> 
                        <%=category.LongName%></option>
                <%}%>
            </select>
            <br />
            </div>
        </fieldset>
    <div class="actions">
    <ul>
        <li><input class="button" type="submit" value="Continue" id="Continue" /></li>
        <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
    </ul>
    </div>
    </div>
    </form>
</asp:Content>
