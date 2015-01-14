<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CreditCardViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Customer customer = ViewData.Model.customer;
    CreditCard creditCard = ViewData.Model.CreditCard;
    Address address = ViewData.Model.Address;
    List<Address> addresses = ViewData.Model.Addresses;
    Users users = ViewData.Model.user;
%>

<script src="/Scripts/jquery.simplemodal.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    $(document).ready(function() {
        initNavigation("/Account/YourAccount");
        $("#cardholder").focus();
        
        $("#Cancel").click(function(e) {
            window.location = "/Account/CreditCardList/<%=customer.CustomerNo%>";
        });

        $("#continue").click(function(e) {
            if (!ValidateAddress()) {
                e.preventDefault();
                return;
            }
            $("#form").submit();
        });
    });
    

  
</script>

    <form id="form1" action="/Account/CreditCardAdd" method="post" >
    <input name="mode" type="hidden" value="N" />
    <input name="cardid" type="hidden" value="<%=creditCard.CardID%>" />
    <input name="customer" type="hidden" value="<%=creditCard.Customer%>" />
    <input name="addressid" type="hidden" value="<%=creditCard.Address%>" />
    <input name="cardholder" type="hidden" value="<%=creditCard!=null?creditCard.Cardholder:""%>"/>
    <input name="cardnumber" type="hidden" value="<%=creditCard!=null?creditCard.Number:""%>" />
    <input name="expirationmonth" type="hidden" value="<%=creditCard!=null?creditCard.Expiremonth:""%>" />
    <input name="expirationyear" type="hidden" value="<%=creditCard!=null?creditCard.Expireyear:""%>" />
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
        <legend>Update Credit Card add new billing address</legend> 
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <div>
            <div class="stripe">
                <div class="stripecol">
                    <span>Type: <%=creditCard.Type%></span>
                    <span>Card Number: <%=creditCard.Number%></span>
                    <span>Expiration Date: <%=creditCard.Expmonth%>/<%=creditCard.Expyear%></span>
                    <span>Card Holder: <%=creditCard.Cardholder%></span>
                </div>
                <div class="stripecol">
                </div>
            </div>
        </div>
        <br />
        <h3>Enter new billing address</h3> 
        <div>
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
            <a id="continue" href="#" >Continue</a>
        </div>
        </fieldset>
    <div class="actions">
    <ul>
        <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
    </ul>
    </div>
    </div>
    </form>
</asp:Content>
