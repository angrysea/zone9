<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AddressViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Customer customer = ViewData.Model.customer;
%>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        initNavigation("/Account/YourAccount");
        $("#fullname").focus();


        $("#Cancel").click(function(e) {
            window.location = "/Account/YourAccount";
        });

        $("#Continue").click(function(e) {
            if (!ValidateAddress()) {
                e.preventDefault();
                return;
            }
        });
    });
</script>
 
    <form id="form1" action="/Account/AddressAdd" method="post">
    <input name="id" type="hidden" value="<%=customer.CustomerNo%>" />
    <div class="frame" id="input-screen">
        <fieldset>
        <legend>Add Address</legend> 
        <br />
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <div class="indexstripe">
            <label for="fullname">Full Name:</label>
            <input name="fullname" id="fullname" type="text" size="25" />
            <br />
            <label for="address1">Address:</label>
            <input name="address1" id="address1" type="text" size="35" />
            <br />
            <label for="address2"></label>
            <input name="address2" id="address2" type="text" size="35" />
            <br />
            <label for="address3"></label>
            <input name="address3" id="address3" type="text" size="35" />
            <br />
            <label for="city">City:</label>
            <input name="city" id="city" type="text" size="20" />
            <br />
            <label for="city">State/Province:</label>
            <select name="state" id="state">
                <%foreach (StateCode state in ViewData.Model.States){%>
                    <option value="">(Select)</option>
                    <option value="<%=state.Code%>"> 
                        <%=state.Name%></option>
                <%}%>
            </select>
            <br />
            <label for="city">Zip/Postal:</label>
            <input name="zipcode" id="zipcode" type="text" size="10" />
            <br />
            <label for="country">Country:</label>
            <select name="country" id="country">
            <%foreach (CountryCode country in ViewData.Model.Countries){%>
            <option <%=country.Code=="us"?"selected='selected'":""%>
                value="<%=country.Code%>">
                <%=country.Name%>
            </option>
            <%}%>
            </select>
            <br />
            <label for="Phone">Phone:</label>
            <input name="phone" id="phone" type="text" size="20" />
            <p>Is this also your billing address (as it <br /> appears on credit card or bank statement)?</p>
            <label for="defaultbilling">Yes:</label>
            <input type="radio" class="radioStyle" value="1" name="defaultbilling" checked="checked" />
            <label for="defaultbilling">No:</label>
            <input type="radio" class="radioStyle" value="0" name="defaultbilling"/>
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
