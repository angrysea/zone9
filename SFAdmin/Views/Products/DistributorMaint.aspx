<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<DistributorViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%
    SiteTheme theme = ViewData.Model.theme;
    Distributor distributor = ViewData.Model.distributor;
    bool update = ViewData.Model.update;
%>
    
    <script language="javascript" type="text/javascript">

        function OnClickCancel() {
            window.location="/Products/Distributors";
        }

        function OnClick() {
            if (document.form1.name.value.length == 0) {
                alert("Invalid Name!");
                document.form1.name.focus();
                return;
            }

            if (document.form1.description.value.length == 0) {
                alert("Invalid Description!");
                document.form1.description.focus();
                return;
            }

            if (document.form1.email.value.length == 0) {
                alert("Invalid E-Mail!");
                document.form1.email.focus();
                return;
            }

            if (document.form1.zipcode.value.length == 0) {
                alert("Invalid Zip Code!");
                document.form1.zipcode.focus();
                return;
            }

            if (document.form1.country.value.length == 0) {
                alert("Invalid Country Code!");
                document.form1.country.focus();
                return;
            }

            document.form1.submit();
        }

        <% if(update==true ) { %>        
            function OnClickDelete()
            {
                document.getElementById('mode').value = 'D';
                document.form1.submit();
            }
        <% } %>

    </script>

    <form name="form1" action="/Products/DistributorAction" method="post">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Distributor</legend>
            <label for="name">Name:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=distributor.Name%>" />
            <input name="name" id="name" type="hidden" value="<%=distributor.Name%>" />
            <%}else{%>    
            <input name="name" id="name" type="text" maxlength="25" value="" />
            <%}%>
            <br />
            <label for="description">Description:</label>
            <textarea name="description" rows="10" cols="70"><%=update?distributor.Description:""%></textarea>
            <br />
            <label for="dropshipfee">Drop Ship Fee:</label>
            <input name="dropshipfee" type="text" maxlength="10" value="<%=update?string.Format("{0:#,#.00}", distributor.Dropshipfee):""%>" />
            <br />
            <label for="nowrap">E-Mail:</label>
            <input name="email" type="text" maxlength="30" value="<%=update?distributor.Email:""%>" />
            <br />
            <label for="nowrap">Address 1:</label>
            <input name="address1" type="text" maxlength="50" value="<%=update?distributor.Address1:""%>" />
            <br />
            <label for="nowrap">Address 2:</label>
            <input name="address2" type="text" maxlength="50" value="<%=update?distributor.Address2:""%>" />
            <br />
            <label for="nowrap">Address 3:</label>
            <input name="address3" type="text" maxlength="50" value="<%=update?distributor.Address3:""%>" />
            <br />
            <label for="nowrap">City:</label>
            <input name="city" type="text" maxlength="30" value="<%=update?distributor.City:""%>" />
            <br />
            <label for="nowrap">State:</label>
            <input name="state" type="text" maxlength="5" value="<%=update?distributor.State:""%>" />
            <br />
            <label for="nowrap">Zip Code:</label>
            <input name="zipcode" type="text" maxlength="10" value="<%=update?distributor.Zip:""%>" />
            <br />
            <label for="nowrap">Country Code:</label>
            <input name="country" type="text" maxlength="5" value="<%=update?distributor.Country:""%>" />
            <br />
            <label for="nowrap">Telephone:</label>
            <input name="telephone" type="text" maxlength="15" value="<%=update?distributor.Phone:""%>" />
            <br />
        </fieldset>
        <div class="actions">
        <ul>
        <%if(update==true){%>
            <li><input class="button" type="button" value="Update" onclick="OnClick()" /></li>
            <li><input class="button" type="button" value="Delete" onclick="OnClickDelete()" /></li>
        <% }else {%>
            <li><input class="button" type="button" value="Insert" onclick="OnClick()" /></li>
        <%}%>
            <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
