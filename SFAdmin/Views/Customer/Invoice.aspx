<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<InvoiceViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%
        Invoice invoice = ViewData.Model.invoice;
        Customer customer = ViewData.Model.customer;
        Address billing = ViewData.Model.billing;
        CreditCard creditCard = ViewData.Model.creditCard;
        Address shipping = ViewData.Model.shipping;
    %>

    <div class="frame" id="input-screen">
    <table id="headingTable" border="0">
        <tr>
            <td><img src="/Content/images/spacer.gif" width="5" height="5"/></td>
        </tr>
        <tr>
            <td><h2>Invoice</h2></td>
        </tr>
        <tr>
            <td><br /></td>
        </tr>
    </table>

    <table border="0" width="100">
        <tr>
            <td>Invoice ID:</td>
            <td><%=invoice.InvoiceID%></td>
        </tr>
        <tr>
            <td>Date:</td>
            <td><%=string.Format("{0:MM/dd/yyyy}", invoice.CreationDate)%></td>
        </tr>
        <tr>
            <td>Status:</td>
            <td><%=invoice.Status%></td>
        </tr>
        <tr>
            <td>Authorization Code:</td>
            <td><%=invoice.AuthorizationCode%></td>
        </tr>
        <tr>
            <td>Sales Order:</td>
            <td><a href="/Cunstomers/SalesOrder/<%=invoice.SalesOrderID%>"><%=invoice.Salesorder%></a></td>
        </tr>
    </table>
    <br />
    <table border="0" width="550">
        <tr>
            <td colspan="2"><img src="/Content/images/spacer.gif" width="5" height="5"></td>
        </tr>
        <tr>
            <td valign="top">
                <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>Billing Address</td>
                </tr>
                <tr>
                    <td><%=customer.Fullname%></td>
                </tr>
                <tr>
                    <td><%=billing.Address1%></td>
                </tr>
                <%
                if(billing.Address2 != null && billing.Address2.Length > 0)
                {
                %>
                <tr>
                    <td><%=billing.Address2%></td>
                </tr>
                <%
                }
                %>
                <tr>
                    <td><%=billing.City%>, <%=billing.State%> <%=billing.Zip%> </td>
                </tr>
                <tr>
                    <td><%=billing.Country%></td>
                </tr>
                <tr>
                    <td><%=billing.Phone%></td>
                </tr>
                <tr>
                    <td><%=customer.Email1%></td>
                </tr>
                </table>
            </td>
            <td valign="top">
                <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="2">Credit Card Used</td>
                </tr>
                <tr>
                    <td>Number:</td>
                    <td><%=creditCard.Number%></td>
                </tr>
                <tr>
                    <td>Expiration:</td>
                    <td><%=creditCard.Expmonth%>/<%=creditCard.Expyear%></td>
                </tr>
                </table>
            </td>
        </tr>
    </table>
    <br/>

    <table cellspacing="1" cellpadding="2" border="0" width="550">
        <tr>
            <th>Product</th>
            <th></th>
            <th>Unit Price</th>
            <th>Quantity</th>
            <th>Product Total</th>
        </tr>
        <tr>
            <td colspan="5">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td bgcolor="<%=theme.getcolor1()%>"><img src="/Content/images/spacer.gif" width="550" height="2"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <%foreach(InvoiceItem invoiceitem in ViewData.Model.invoiceItems){%>
        <tr>
            <td><%=invoiceitem.Product%></td>
            <td></td>
            <td class="numerictd"><%=string.Format("{0:#,#.00}", invoiceitem.UnitPrice)%></td>
            <td class="numerictd"><%=string.Format("{0:#,#}", invoiceitem.Quantity)%></td>
            <td class="numerictd"><%=string.Format("{0:#,#.00}", invoiceitem.Quantity * invoiceitem.UnitPrice)%></td>
        </tr>
        <%}%>
        <tr>
            <td colspan="5">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="/Content/images/spacer.gif" width="550" height="1"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">Merchandise Total:</td>
            <td class="numerictd"><%=string.Format("{0:#,#.00}", invoice.Totalcost)%></td>
        </tr>
        <%if(invoice.Discount>0.0){%>
        <tr>
            <td colspan="3" align="right"><%=invoice.DiscountDescription%></td>
            <td class="numerictd"><font color="red">- <%=string.Format("{0:#,#.00}", invoice.Discount)%></font></td>
            <td></td>
        </tr>
        <%}%>
        
        <tr>
            <td colspan="1"></td>
            <td colspan="4" align="right">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="/Content/images/spacer.gif" width="250" height="1"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">Shipping Costs:</td>
            <td align="right"><%=string.Format("{0:#,#.00}", invoice.Handling + invoice.ShippingCost)%></td>
        </tr>
        <tr>
            <td colspan="4" align="right"><%=invoice.TaxesDescription%>:</td>
            <td align="right"><%=string.Format("{0:#,#.00}", invoice.Taxes)%></td>
        </tr>
        <tr>
            <td colspan="1"></td>
            <td colspan="4" align="right">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="/Content/images/spacer.gif" width="250" height="1"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">Grand Total:</td>
            <td align="right"><%=string.Format("{0:#,#.00}", invoice.Total)%></td>
        </tr>
    </table>
</div>        
</asp:Content>
