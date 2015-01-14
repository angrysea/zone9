<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CheckoutViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="Storefront.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  Company company = ViewData.Model.company; 
    SiteTheme theme = ViewData.Model.theme; 
    Customer customer = ViewData.Model.customer;
%>

<script src="/Scripts/jquery.simplemodal.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        initNavigation("/Checkout/Index");
        $("#cardholder").focus();

        $("#Cancel").click(function(e) {
            window.location = "/Checkout/CancelCheckout/<%=customer.CustomerNo%>";
        });

        $("#cardnumber").blur(function(e) {
            var ccType = getCCType($("#cardnumber").val());
            if (ccType != null) {
                $("#cctype").val(ccType);
            }
            else {
                alert("Invalid Credit Card Number!");
            }
        });

        $("#usenewcard").click(function(e) {
            if (ValidateCreditCard() == false) {
                e.preventDefault();
                return;
            }
            $("#form1").submit();
        });
    });

    function UseThisCard(newID) {
        $("#cardid").val(newID);
        $("#form1").submit();
    }

</script>

    <form id="form1" action="/Checkout/SelectPayment" method="post" >
    <input name="cardid" id="cardid" type="hidden" value="" />
    <input name="cctype" id="cctype" type="hidden" value="" />
    <input name="customer" id="customer" type="hidden" value="<%=customer.CustomerNo%>" />
    <div class="frame">
        <div class="form-container">
            <fieldset>
                <legend>Select payment method</legend> 
                <div class="stripe">
                    <% int count = 0;
                    foreach (CreditCard creditCard in ViewData.Model.creditCards)
                    {
                            count++;  
                    %>
                        <div class="stripecol">
                            <span>Type: <%=creditCard.Type%></span>
                            <span>Card Number: <%=creditCard.Number%></span>
                            <span>Expiration Date: <%=creditCard.Expmonth%>/<%=creditCard.Expyear%></span>
                            <span>Card Holder: <%=creditCard.Cardholder%></span>
                            <br />
                            <a href="#" onclick="UseThisCard('<%=creditCard.CardID%>');" >Use This Credit Card</a>
                        </div>
                    <%
                        if (count == 3)
                        {
                            %><br /><%
                            count = 0;
                        }
                    }%>
                </div>
                <h3>Pay with new card</h3> 
                <div class="indexstripe">

                    <img class="left" alt="visa mastercard american express discover" src="/Content/images/visamcamexdisc.gif" />
                    <br />
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
                    <br />
                    <div class="continue">
                        <a id="usenewcard" href="#" >New Card</a>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</form>
</asp:Content>
