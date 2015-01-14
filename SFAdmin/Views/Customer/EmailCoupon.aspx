<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<SendCouponViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        Company company = ViewData.Model.company;
        Coupon coupon = ViewData.Model.coupon;
        
        string discountType = "";
        if(coupon.DiscountType == 1)
            discountType = "Dollar Amount";
        else if(coupon.DiscountType == 2)
            discountType = "Percentage";
        else
            discountType = "Free Shipping";
    %>

    <script language="javascript" type="text/javascript">
        function OnClickSendTest() {
            window.location = "/Customer/TestSendCoupons/<%=coupon.Code%>";
        }

        function OnClickSend() {
            window.location = "/Customer/SendCoupons/<%=coupon.Code%>";
        }

        function OnClickCancel() {
            window.location = "/Products/Coupons";
        }
        
    </script>

    <form name="form1" action="/Customer/SendCoupons/<%=coupon.Code%>" method="post">
    <input type="hidden" name="id" value="<%=coupon.Code%>"/>
    <table cellspacing="1" cellpadding="3" border="0">
    <tr>
        <td>Code:</td>
        <td><%=coupon.Code%></td>
    </tr>
    <tr>
        <td>Description:</td>
        <td><%=coupon.Description%></td>
    </tr>
    <tr>
        <td>Product ID:</td>
        <td><%=coupon.Product%></td>
    </tr>
    <tr>
        <td>Quantity Limit:</td>
        <td><%=coupon.QuantityLimit%></td>
    </tr>
    <tr>
        <td>Discount Type:</td>
        <td><%=discountType%></td>
    </tr>
    <tr>
        <td>Discount (i.e. 0.1 = 10%):</td>
        <td><%=string.Format("{0:#.00}", coupon.Discount)%></td>
    </tr>
    <tr>
        <td>Precludes All Other Coupons:</td>
        <td><%=coupon.Precludes==0?"no":"yes"%></td>
    </tr>
    <tr>
        <td>Single Use:</td>
        <td><%=coupon.SingleUse==0?"no":"yes"%></td>
    </tr>
    <tr>
        <td>Expiration Date (mm/dd/yyyy):</td>
        <td><%=string.Format("{0:MM/dd/yyyy}", coupon.ExpirationDate)%></td>
    </tr>
    <tr>
        <td>Redemptions:</td>
        <td><%=coupon.Redemptions%></td>
    </tr>
    </table>
    <br />
    <div>(Test E-Mail Address: <%=company.EMail1%>)</div>
    <br />
    <div>
        <font color="red">WARNING: Use this feature with caution.  The coupon selected will be sent to everyone in the e-mail list.</font>
    </div>
    <div class="actions">
    <ul>
        <li><input class="button" type="button" value="Send Test E-Mail" onclick="OnClickSend()" /></li>
        <li><input class="button" type="button" value="Send to Mail List" onclick="OnClickSend()" /></li>
        <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
    </ul>
    </div>
    </form>
</asp:Content>
