<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CustomerViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%
        Customer customer = ViewData.Model.customer;
        Users user = ViewData.Model.user;
        CreditCard creditCard = ViewData.Model.creditCard;
        Address billingaddress = ViewData.Model.billingaddress;
        Address shippingaddress = ViewData.Model.shippingaddress;
    %>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("#Cancel").click(function(e) {
                window.location = "/Customer/Customers";
            });

            $("#Delete").click(function(event) {
                var answer = confirm("Are you sure you want to delete this customer?");
                if (answer) {
                    window.location = "Customer/DeleteCustomer/<%=customer.CustomerNo%>";
                }
            });
        });
    </script>

    <div>

    <table cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td valign="top" width="20">
                    <img alt="" src="/Content/images/blank.gif" border="0" />
                </td>
                <td valign="top">
                    <table border="0">
                        <tr>
                            <td><img alt="" src="/Content/images/blank.gif" width="5" height="5"/></td>
                        </tr>
                        <tr>
                            <td class="producttitle">Customer Information</td>
                        </tr>
                        <tr>
                            <td><br /></td>
                        </tr>
                    </table>

                    <table border="0" width="250">
                        <tr>
                            <td align="left" nowrap="nowrap"><b>Name:</b></td>
                            <td align="left" nowrap="nowrap"><%=customer.Fullname%></td>
                        </tr>
                        <tr>
                            <td align="left" nowrap="nowrap"><b>Created:</b></td>
                            <td align="left" nowrap="nowrap"><%=user.Creationdate%></td>
                        </tr>
                    <%
                        if(user.Lastlogindate != null)
                        {
                    %>
                        <tr>
                            <td align="left" nowrap="nowrap"><b>Last Login:</b></td>
                            <td align="left" nowrap="nowrap"><%=user.Lastlogindate%></td>
                        </tr>
                    <%
                        }
                    %>
                    </table>
                    <table border="0" width="550">
                        <tr>
                            <td colspan="2"><img src="/Content/images/blank.gif" width="5" height="5" alt=""/></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td nowrap="nowrap"><b>Billing Address</b></td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap"><%=customer.Fullname%></td>
                                </tr>
                                <%
                                    if(billingaddress!=null){
                                    if (billingaddress.Address1 != null && billingaddress.Address1.Length > 0)
                                    {
                                %>
                                <tr>
                                    <td nowrap="nowrap"><%=billingaddress.Address1%></td>
                                </tr>
                                <%
                                    } if (billingaddress.Address2 != null && billingaddress.Address2.Length > 0)
                                {
                                %>
                                    <tr>
                                        <td nowrap="nowrap"><%=billingaddress.Address2%></td>
                                    </tr>
                                <%
                                }
                                %>
                                <%
                                    if (billingaddress.Address3 != null && billingaddress.Address3.Length > 0)
                                {
                                %>
                                    <tr>
                                        <td nowrap="nowrap"><%=billingaddress.Address3%></td>
                                    </tr>
                                <%
                                }
                                %>
                                <tr>
                                    <td nowrap="nowrap"><%=billingaddress.City%>, <%=billingaddress.State%> <%=billingaddress.Zip%> </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap"><%=billingaddress.Phone%></td>
                                </tr>
                                <%
                                }
                                %>
                                <tr>
                                    <td nowrap="nowrap"><%=customer.Email1%></td>
                                </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td nowrap="nowrap"><b>Shipping Address</b></td>
                                </tr>
                                <%
                                if(shippingaddress == null)
                                {
                                %>
                                    <tr>
                                        <td nowrap="nowrap">none</td>
                                    </tr>
                                <%
                                }
                                else
                                {
                                %>
                                    <tr>
                                        <td nowrap="nowrap"><%=shippingaddress.FullName%></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap"><%=shippingaddress.Address1%></td>
                                    </tr>
                                    <%
                                    if(shippingaddress.Address2 != null && shippingaddress.Address2.Length > 0)
                                    {
                                    %>
                                        <tr>
                                            <td nowrap="nowrap"><%=shippingaddress.Address2%></td>
                                        </tr>
                                    <%
                                    }
                                    %>
                                    <%
                                    if(shippingaddress.Address3 != null && shippingaddress.Address3.Length > 0)
                                    {
                                    %>
                                        <tr>
                                            <td nowrap="nowrap"><%=shippingaddress.Address3%></td>
                                        </tr>
                                    <%
                                    }
                                    %>
                                    <tr>
                                        <td nowrap="nowrap"><%=shippingaddress.City%>, <%=shippingaddress.State%> <%=shippingaddress.Zip%> </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap"><%=shippingaddress.Phone%></td>
                                    </tr>
                                <%
                                }
                                %>
                                    </table>
                                </td>
                            <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td colspan="2" nowrap="nowrap"><b>Current Credit Card</b></td>
                                </tr>
                                <%
                                    if (creditCard == null || creditCard.Number == null || creditCard.Number.Length == 0)
                                {
                                %>
                                <tr>
                                    <td nowrap="nowrap">none</td>
                                </tr>

                                <%
                                }
                                else
                                {
                                %>
                                <tr>
                                    <td nowrap="nowrap">Number:</td>
                                    <td><%=creditCard.Number%></td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">Expiration:</td>
                                    <td nowrap="nowrap"><%=creditCard.Expmonth%>/<%=creditCard.Expyear%></td>
                                </tr>
                                <%
                                }
                                %>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="1" cellpadding="3" width="550" border="0">
                        <tr>
                            <td colspan="2"><img src="/Content/images/blank.gif" alt="" width="1" height="15" /></td>
                        </tr>
                        <tr>
                            <td align="left"><b>Sales Order Number</b></td>
                            <td align="left"><b>Order Date</b></td>
                            <td align="right"><b>Amount</b></td>
                            <td align="right"><b>Status</b></td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td><img src="/Content/images/blank.gif" alt="" width="550" height="1" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%
                            double orderTotal = 0;
                            bool orders = false;
                            foreach (SalesOrder salesOrder in ViewData.Model.salesOrders)
                            {
                                orders = true;
                                orderTotal += (double)salesOrder.Total;
                        %>
                        <tr>
                            <td><a href="/Customer/salesorder/<%=salesOrder.SalesOrderID%>"></a></td>
                            <td><%=salesOrder.Creationdate%></td>
                            <td nowrap="nowrap" align="right"><%=salesOrder.Total%></td>
                            <td nowrap="nowrap" align="right"><%=salesOrder.Status%></td>
                        </tr>
                        <%
                            }

                            if(orders == false)
                            {
                        %>
                        <tr>
                            <td colspan="5" nowrap="nowrap">There are no orders under this customer account.</td>
                        </tr>
                        <%
                            }
                            else
                            {
                        %>
                        <tr>
                            <td colspan="5">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td"><img src="/Content/images/blank.gif" alt="" width="550" height="1" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td align="right"><%=orderTotal%></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <%
                            }
                        %>
                        <tr>
                            <td><br /></td>
                        </tr>
                    </table>
                    <table border="0">
                        <tr>
                            <td nowrap="nowrap"><a href="/Customer/shoppingcart/<%=customer.CustomerNo%>">Current Shopping Cart ( <%=ViewData.Model.productsInCart%> )</a></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>        
    <div class="actions">
    <ul>
        <li><input class="button" type="button" value="Delete" id="Delete" /></li>
        <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
    </ul>
    </div>
</asp:Content>
