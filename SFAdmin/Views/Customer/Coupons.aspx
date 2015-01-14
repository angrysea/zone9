<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CouponsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="lists">
        <h1>Coupons</h1>
        <table class="listtable">
            <tr>
                <th>Code</th>
                <th>Mfgr</th>
                <th>Product</th>
                <th>Discount</th>
                <th>Single Use</th>
                <th>Expiration Date</th>
                <th>Display on Web</th>
                <th>Redemptions</th>
            </tr>
            <%
                foreach(Coupon coupon in ViewData.Model.coupons)
                {
            %>
                    <tr>
                        <td><a href="/Customer/CouponMaint/<%=coupon.Code%>"><%=coupon.Code%></a></td>
                        <td><%=coupon.Manufacturer%></td>
                        <td><%=coupon.Product%></td>
                        <%if(coupon.DiscountType == 1){%>
                        <td><%=string.Format("{0:#.00}", coupon.Discount)%></td>
                        <%}else{%>
                        <td><%=string.Format("{0:#.00}", coupon.Discount*100.0)%>%</td>
                        <%}%>
                        <td><%=coupon.SingleUse == 1?"Y":"N"%></td>
                        <td><%=string.Format("{0:MM/dd/yyyy}", coupon.ExpirationDate)%></td>
                        <td><%=coupon.Display == 1 ? "Y" : "N"%></td>
                        <td><%=coupon.Redemptions%></td>
                        <td><a href="/Customers/EmailCoupon/<%=coupon.Code%>">E-Mail</a></td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Customer/CouponMaint">Add a new Coupon</a>
        </div>
    </div>
</asp:Content>
