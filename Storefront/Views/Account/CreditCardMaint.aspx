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
        
        <%if(creditCard!=null) {%>
            $("#expirationmonth option[value='<%=creditCard.Expmonth%>']").attr('selected', 'selected');
            $("#expirationyear option[value='<%=creditCard.Expyear%>']").attr('selected', 'selected');
        <%}%>
        
        $("#Cancel").click(function(e) {
            window.location = "/Account/CreditCardList/<%=customer.CustomerNo%>";
        });
        
        $("#addnewaddress").click(function(e) {
            if(ValidateCreditCard())
            {
                $("#form1").attr(action="/Account/CreditCardNewAddr");
                $("#form1").submit();
            }
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
    <input name="mode" type="hidden" value="U" />
    <input name="cardid" type="hidden" value="<%=creditCard.CardID%>" />
    <input name="customer" type="hidden" value="<%=creditCard.Customer%>" />
    <input name="addressid" type="hidden" value="<%=creditCard.Address%>" />
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
        <legend>Update Credit Card</legend> 
        <%if(ViewData.Model.bBreadcrumbs){%>
        <%= Html.BreadCrumbs()%>
        <%}%>
        <div class="indexstripe">
        <br />
            <div class="cc">
            <label for="ccimage">We Accept:</label>
            <img name="ccimage" alt="visa mastercard american express discover" src="/Content/images/visamcamexdisc.gif" />
            </div><br />
            <label for="cardholder">Card Holder:</label>
            <input type="text" name="cardholder" id="cardholder" value="<%=creditCard!=null?creditCard.Cardholder:""%>"/>
            <br />
            <label for="cardnumber">Card Number:</label>
            <input type="text" name="cardnumber" id="cardnumber" value="<%=creditCard!=null?creditCard.Number:""%>" />
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
            <br />
            <div class="continue">
                <a id="addnewaddress" href="#" >Add new address</a>
                <a href="#" id="DeleteCreditCard">Delete Credit Card</a>
            </div>
        </div>
        <br />
        <br />
        <div class="stripe">
            <div class="stripecol">
                <span><%=address.FullName%></span>
                <span><%=address.Address1%></span>
                <%if (address.Address2 != null && address.Address2.Length > 0)
                  {%>
                <span><%=address.Address2%></span>
                <% } if (address.Address3 != null && address.Address3.Length > 0)
                  {%>
                <span><%=address.Address3%></span>
                <%}%>
                <span><%=address.City%>, <%=address.State%>, <%=address.Zip%></span>
                <span><%=address.Country%></span>
                <span>Phone: <%=address.Phone%></span>
                <a href="#" onclick="UseThisAddress('<%=address.AddressID%>');">Use The Current Address</a>              
            </div>
        <%
            count = 1;
            foreach (Address addressEntry in ViewData.Model.Addresses)
            {
                if (addressEntry.AddressID == address.AddressID)
                {
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
                    %>
                 <br />
                <%
                    count = 0;
                }
            }
        }%>
        </div>
        <div class="indexstripe">
            <div class="continue">
                <a id="Cancel" href="#" >Cancel</a>
            </div>
        </div>
    </fieldset>
    </div>
    </form>
</asp:Content>
