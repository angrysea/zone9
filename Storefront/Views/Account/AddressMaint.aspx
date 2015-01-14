<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AddressViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>
<%@ Import Namespace="Storefront.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  
    Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Customer customer = ViewData.Model.customer;
    Address address = ViewData.Model.Address;
    CreditCard creditCard = ViewData.Model.CreditCard;
    Users users = ViewData.Model.user;    
%>

<script src="/Scripts/jquery.simplemodal.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    $(document).ready(function() {
        initNavigation("/Account/YourAccount/<%=customer.CustomerNo%>");
        $("#fullname").focus();

        $("#DeleteAddress").click(function(e) {
            e.preventDefault();

            confirm("Are you sure you want to delete this Address?", function() {
                window.location.href = "/Account/AddressDelete/<%=address.AddressID%>/<%=address.Customer%>";
            });
        });

        $("#Cancel").click(function(e) {
            window.location = "/Account/AddressBook/<%=customer.CustomerNo%>";
        });

        $("#Continue").click(function(e) {
            if (!ValidateAddress()) {
                e.preventDefault();
                return;
            }
            if ($.trim($("#cardholder").val()).length > 0) {
                if (ValidateCreditCard() == false) {
                    e.preventDefault();
                    return false;
                } 
            }
            return true;
        });
    });
    
</script>

    <form id="form1" action="/Account/AddressMaint" method="post" >
    <input name="addressid" type="hidden" value="<%=address.AddressID%>" />
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
        <legend>Update Address</legend> 
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <div class="indexstripe">
            <label for="fullname">Full Name:</label>
            <input name="fullname" id="fullname" type="text" size="25" value="<%=address.FullName%>"/>
            <br />
            <label for="address1">Address:</label>
            <input name="address1" id="address1" type="text" size="35" value="<%=address.Address1%>"/>
            <br />
            <label for="address2"></label>
            <input name="address2" id="address2" type="text" size="35" value="<%=address.Address2%>"/>
            <br />
            <label for="address3"></label>
            <input name="address3" id="address3" type="text" size="35" value="<%=address.Address3%>"/>
            <br />
            <label for="city">City:</label>
            <input name="city" id="city" type="text" size="20" value="<%=address.City%>"/>
            <br />
            <label for="city">State/Province:</label>
            <select name="state" id="state">
                <%foreach (StateCode state in ViewData.Model.States){%>
                    <option value="<%=state.Code%>"
                        <%=state.Code==address.State?"selected='selected'":""%>> 
                        <%=state.Name%></option>
                <%}%>
            </select>
            <br />
            <label for="city">Zip/Postal:</label>
            <input name="zipcode" id="zipcode" type="text" size="10" value="<%=address.Zip%>"/>
            <br />
            <label for="country">Country:</label>
            <select name="country" id="country">
            <%foreach (CountryCode country in ViewData.Model.Countries){%>
            <option 
                value="<%=country.Code%>"
                <%=country.Code==address.Country?"selected='selected'":""%>>
                <%=country.Name%>
            </option>
            <%}%>
            </select>
            <br />
            <label for="Phone">Phone:</label>
            <input name="phone" id="phone" type="text" size="20" value="<%=address.Phone%>"/><br />
            <p>Would you like this to be your default shipping address?</p><br/>
            <label for="defaultshipping">Yes:</label>
            <input type="radio"  class="radioStyle" value="1" name="defaultshipping" <%=address.DefaultShipping!=0?"checked='checked'":"" %> />
            <label for="defaultshipping">No:</label>
            <input type="radio"  class="radioStyle" value="0" name="defaultshipping"  <%=address.DefaultShipping==0?"checked='checked'":"" %>/>    
            <br />
            <div class="continue">
            <a id = "DeleteAddress" href='#'>Delete Address</a>
            </div>
            <h3>Credit Card Information (Optional)</h3>
            <div class="cc">
            <label for="ccimage">We Accept:</label>
            <img name="ccimage" alt="visa mastercard american express discover" src="/Content/images/visamcamexdisc.gif" />
            </div><br />
            <label for="cardholder">Card Holder:</label>
            <input type="text" name="cardholder" id="cardholder" />
            <br />
            <label for="cardnumber">Card Number:</label>
            <input type="text" name="cardnumber" id="cardnumber" />
            <br />
            <label for="expirationmonth">Expiration Date:</label>
            <select class="thin" name="expirationmonth" id="expirationmonth">
                <option value="0"> Month</option>
                <option value="01">January</option>
                <option value="02">February</option>
                <option value="03">March</option>
                <option value="04">April</option>
                <option value="05">May</option>
                <option value="06">June</option>
                <option value="07">July</option>
                <option value="08">August</option>
                <option value="09">Septempber</option>
                <option value="10">October</option>
                <option value="11">November</option>
                <option value="12">December</option>
            </select>
            <select class="thin" name="expirationyear" id="expirationyear">
                <option value="0">Year</option>
                <%  int year = DateTime.Now.Year;
                    for(int i=0 ; i<20 ; i++) {
                        var syear = string.Format("{0:####}", i+year);
                %>
                <option value="<%=syear%>"><%=syear%></option>
                <%}%>
            </select>
        </div>
        </fieldset>
        <br />
    <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="Continue" id="Continue" /></li>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
    </div>
    <div class="info">
        <h3>Address Accuracy</h3>       
        It is important to make sure your address is entered correctly. 
        If not your package could be returned as undeliverable. You would then have to 
        place a new order. Save time and avoid frustration by entering the address 
        information in the appropriate boxes and double-checking for typos and other errors. 
        </div>
    </div>
    </form>
    <div id="confirm" style="display:none">
	    <a href="#" title="Close" class="modalCloseX simplemodal-close">x</a>
	    <div class="header"><span>Confirm</span></div>
	    <p class="message"></p>
	    <div class="buttons">
	    <div class="no simplemodal-close">No</div><div class="yes">Yes</div>
	    </div>
    </div>
</asp:Content>
