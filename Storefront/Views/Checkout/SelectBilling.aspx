<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CheckoutViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Customer customer = ViewData.Model.customer;
    CreditCard creditCard = ViewData.Model.CreditCard;
    List<Address> addresses = ViewData.Model.Addresses;
    Users users = ViewData.Model.user;
    int count = 0;
%>

<script src="/Scripts/jquery.simplemodal.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    $(document).ready(function() {
        initNavigation("/Checkout/Index");
        $("#cardholder").focus();
        
        $("#Cancel").click(function(e) {
            window.location = "/Checkout/CancelCheckout/<%=customer.CustomerNo%>";
        });

        $("#usenewaddress").click(function(e) {
            if(ValidateAddress()==false) {
                e.preventDefault();
                return;
            }
            $("#form1").submit();
        });
    });
    
    function UseThisAddress(newID) {
        $("#addressid").val(newID);
        $("#form1").submit();
    }

</script>

    <form id="form1" action="/Checkout/SelectBilling" method="post" >
    <input name="addressid" id="addressid" type="hidden" value="" />
    <input name="customer" id="customer" type="hidden" value="<%=customer.CustomerNo%>" />
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
        <legend>Select Bill to Address</legend> 
        <div class="stripe">
        <%
            count = 0;
            foreach (Address addressEntry in ViewData.Model.Addresses) {
                count++;%>
                <div class="stripecol">
                    <span><%=addressEntry.FullName%></span>
                    <span><%=addressEntry.Address1%></span>
                    <%if (addressEntry.Address2 != null && addressEntry.Address2.Length > 0)
                      {%>
                    <span><%=addressEntry.Address2%></span>
                    <% } if (addressEntry.Address3 != null && addressEntry.Address3.Length > 0)
                      {%>
                    <span><%=addressEntry.Address3%></span>
                    <%}%>
                    <span><%=addressEntry.City%>, <%=addressEntry.State%>, <%=addressEntry.Zip%></span>
                    <span><%=addressEntry.Country%></span>
                    <span>Phone: <%=addressEntry.Phone%></span>
                    <a href="#" onclick="UseThisAddress('<%=addressEntry.AddressID%>');" >Use This Address</a>
                </div>
            <%
            if (count == 2)
            {
                %><br /><%
                count = 0;
            }
        }%>
        </div>
        <h3>Or enter a new address</h3> 
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
            <br />
            <div class="continue">
                <a id="usenewaddress" href="#" >New Address</a>
            </div>
        </div>
        </fieldset>
    </div>
    </form>
</asp:Content>
