<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SFAdmin.Models.ShipPackagesViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            
            $("#Cancel").click(function(event) {
                event.preventDefault();
                window.location.href = "/Customer/SalesOrders/";
            });

            $("#Ship").click(function(event) {
                if( AreItemsSelected() == false ||
                    AreTrackingNumbersEntered() == false)
                {
                    event.preventDefault();
                    return false;
                }
                return true;
            });
        });

        function AreItemsSelected()
        {
            <%
                foreach (ShipPackagesData shipPackagesData in ViewData.Model.ShipPackagesData)
                {
                    Packingslip packingslip = shipPackagesData.Packingslip.Packingslip;
            %>
                    if($("#packingslip<%=packingslip.PackingSlipId%>").attr('checked')) 
                    {
                        return true;
                    }
            <%
                }
            %>
            alert("At least one item must be selected to create a package.");
            return false;
        }

        function AreTrackingNumbersEntered()
        {
            <%
                foreach (ShipPackagesData shipPackagesData in ViewData.Model.ShipPackagesData)
                {
                    Packingslip packingslip = shipPackagesData.Packingslip.Packingslip;
            %>
                    if($("#packingslip<%=packingslip.PackingSlipId%>").attr('checked') &&
                            ($("#trackingnumber<%=packingslip.PackingSlipId%>").val() == null ||
                            $("#trackingnumber<%=packingslip.PackingSlipId%>").val().length == 0)) 
                    {
                        alert("A tracking number was not entered.");
                        $("#trackingnumber<%=packingslip.PackingSlipId%>").focus();
                        return false;
                    }
            <%
                }
            %>
            return true;
        }
    </script>
    
    <div class="wideframe" id="input-screen">
    <form name="form1" action="/Logistics/ShipPackages" method="post">
        <fieldset>
            <legend>Ship Packages</legend>

            <%
                if(ViewData.Model.ShipPackagesData.Count>0)
                {
                    foreach (ShipPackagesData shipPackagesData in ViewData.Model.ShipPackagesData)
                    {
                        Packingslip packingslip = shipPackagesData.Packingslip.Packingslip;
                        Customer customer = shipPackagesData.Customer;
                        Address shipping = shipPackagesData.Shipping;
                        ShippingMethod shippingMethod = shipPackagesData.ShippingMethod;
            %>
                        <table class="listtable">
                            <tr>
                                <th class="center">Select</th>
                                <th>Ship To</th>
                                <th>Shipping Method</th>
                                <th>Tracking #</th>
                            </tr>
                            <tr>
                                <td valign="top"><input type="checkbox" class="radioStyle" name="packingslip<%=packingslip.PackingSlipId%>" id="packingslip<%=packingslip.PackingSlipId%>"/></td>
                                <td>
                                <table>
                                    <tr><td><%=customer.Fullname%></td></tr>
                                    <tr><td><%=shipping.Address1%></td></tr>
                                    <%
                                    if (!string.IsNullOrEmpty(shipping.Address2))
                                    {
                                    %>
                                        <tr><td><%=shipping.Address2%></td></tr>
                                    <%
                                    }

                                    if (!string.IsNullOrEmpty(shipping.Address3))
                                    {
                                    %>
                                        <tr><td><%=shipping.Address3%></td></tr>
                                    <%
                                    }
                                    %>
                                    <tr><td><%=shipping.City%>, <%=shipping.State%> <%=shipping.Zip%></td></tr>
                                    <tr><td><%=shipping.Country%></td></tr>
                                </table>
                                </td>
                                <td valign="top"><%=shippingMethod.Carrier%></td>
                                <td valign="top"><input name="trackingnumber<%=packingslip.PackingSlipId%>" id="trackingnumber<%=packingslip.PackingSlipId%>" type="text" size="25" /></td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                <table>
                                    <tr>
                                        <th>Item ID</th>
                                        <th>Quantity</th>
                                        <th>Product Name</th>
                                        <th>Sales Order ID</th>
                                    </tr>
                                    <%
                                        foreach (PackingslipItem packingslipItem in shipPackagesData.Packingslip.Items)
                                        {
                                    %>
                                            <tr>
                                                <td><%=packingslipItem.ProductNo%></td>
                                                <td><%=packingslipItem.Quantity%></td>
                                                <td><%=packingslipItem.ProductName%></td>
                                                <td><a href="/Customer/SalesOrder/<%=packingslipItem.SalesOrder%>"><%=packingslipItem.SalesOrder%></a></td>
                                            </tr>
                                    <%
                                        }
                                    %>
                                </table>
                                </td>
                            </tr>                            
                        </table>
            <%
                    }
                }
                else
                {
            %>
                    <table>
                        <tr>
                            <td>No packages are available to ship.  Packing slips must be created first.</td>
                        </tr>
                    </table>
            <%
                }
            %>
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="Ship" id="Ship" /></li>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </form>
    </div>
</asp:Content>
