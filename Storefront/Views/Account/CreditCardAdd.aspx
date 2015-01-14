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
    int count = 0;
%>

<script src="/Scripts/jquery.simplemodal.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    $(document).ready(function() {
        initNavigation("/Account/YourAccount");
        $("#cardholder").focus();

        $("#cardnumber").blur(function(e) {
            var ccType = getCCType($("#cardnumber"));
            if (ccType != null) {
                $("#cctype").val(ccType);
            }
            else {
                alert("Invalid Credit Card Number!");
            }
        });

        $("#Cancel").click(function(e) {
            window.location = "/Account/CreditCardList/<%=customer.CustomerNo%>";
        });

        $("#usenewaddress").click(function(e) {
            if (ValidateCreditCard() == false) {
                e.preventDefault();
                return;
            }
            else if (ValidateAddress() == false) {
                e.preventDefault();
                return;
            }
            $("#form1").submit();
        });
    });
    
    function UseThisAddress(newID) {
        $("#addressid").val(newID);
        if(ValidateCreditCard())
        {
            $("#form1").submit();
        }
    }

</script>

    <form id="form1" action="/Account/CreditCardAdd" method="post" >
    <input name="addressid" id="addressid" type="hidden" value="" />
    <input name="cctype" id="cctype" type="hidden" value="" />
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
        <legend>Add a Credit Card</legend> 
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <div class="indexstripe">
            <div class="cc">
            <label for="ccimage">We Accept:</label>
            <img name="ccimage" alt="visa mastercard american express discover" src="/Content/images/visamcamexdisc.gif" />
            </div>
            <br />
            <label for="cardholder">Card Holder:</label>
            <input type="text" name="cardholder" id="cardholder"/>
            <br />
            <label for="cardnumber">Card Number:</label>
            <input type="text" name="cardnumber" id="cardnumber"/>
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
        <br />
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
                    <br />
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
        <br />
        <div class="indexstripe">
        <h3>Or enter a new billing address</h3> 
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
            <div class="continue">
                <a id="Cancel" href="#" >Cancel</a>
                <a id="usenewaddress" href="#" >Continue</a>
            </div>
        </div>
        </fieldset>
        <br />
    </div>
    </form>
</asp:Content>
