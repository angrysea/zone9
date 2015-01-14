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
                <th>Item</th>
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
                        <td><a href="/Products/CouponMaint/<%=coupon.Code%>"><%=coupon.Code%></a></td>
                        <td><%=coupon.Item%></td>
                        <%if(coupon.DiscountType == 1){%>
                        <td><%=string.Format("{0:#.00}", coupon.Discount)%></td>
                        <%}else{%>
                        <td><%=string.Format("{0:#.00}", coupon.Discount*100.0)%>%</td>
                        <%}%>
                        <td><%=coupon.SingleUse%></td>
                        <td><%=string.Format("dd/MM/yyyy",coupon.ExpirationDate)%></td>
                        <td><%=coupon.Display%></td>
                        <td><%=coupon.Redemptions%></td>
                    </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Products/CouponMaint">Add a new Coupon</a>
        </div>
    </div>
</asp:Content>
