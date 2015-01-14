<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<InvoicesViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script language="javascript" type="text/javascript">
        
        function OnClickCancel() {
            window.location = "/Home/Index";
        }

        function OnClick() {
        }
        
        function OnSubmitForm()
        {
            if(ValidateDate(document.form1.fromdate) == false)
            {
                return false;
            }

            if(ValidateDate(document.form1.todate) == false)
            {
                return false;
            }

            return true;
        }

        function OnClickOpenInvoices()
        {
            document.form1.submit();
        }
    </script>

    <form name="form1" action="/Customers/Invoices" method="post">
    <div class="shortframe" id="input-screen">
        <fieldset>
            <legend>Invoices</legend>
            <br />
            <label for="fromdate">From:</label>
            <input name="fromdate" type="text" onchange="OnChangeDate()"/>
            <br />
            <label for="todate">To:</label>
            <input name="todate" type="text" onchange="OnChangeDate()"/>
            <br />
            <label for="declinedinvoicesonly">Declined Invoices:</label>
            <input name="declinedinvoicesonly" type="checkbox" <%=declinedinvoicesonly==true?"checked":""%> onclick="OnClickDeclinedInvoices()"/>
            <br />
            <span>Display declined invoices only</span>
            <br />
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="button" value="go" onclick="OnClick()" /></li>
            <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
        </ul>
        </div>
    </div>
    </form>
    <div class="widelists">
        <table class="listtable">
            <tr>
                <th>Invoice Number</th>
                <th>Creation Date</th>
                <th>Customer</th>
                <th class="numericth">Invoice Total</th>
                <th>Status</th>
            </tr>
            <%
            double grandtotal = 0.0;

            foreach (InvoiceView invoiceView in ViewData.Model.invoices)
            {
                Invoice invoice = invoiceView.invoice;
                grandtotal += invoice.Total;

                Customer customer = invoiceView.customer;
            %>
            <tr>
                <td><a href="/Customers/Invoice/<%=invoice.InvoiceID%>"><%=invoice.InvoiceID%></a></td>
                <td><%=string.Format("{0:MM/dd/yyyy}", invoice.CreationDate)%></td>
                <td><a href="/Customer/Customer/<%=customer.Customer%>"><%=customer.Last%>, <%=customer.First%></a></td>
                <td class="numerictd"><%=string.Format("{0:#,#.00}", invoice.Total)%></td>
                <td><%=invoice.Status%></td>
            </tr>
            <%}if(grandtotal>0){%>
            <tr>
                <td colspan="3" />
                <td class="numerictd"><%=string.Format("{0:#,#.00}", grandtotal)%></td>
            </tr>
            <%}else{%>
            <tr>
                <td colspan="5" nowrap="nowrap">There are no Invoices in the system for this time period or status.</td>
            </tr>
            <%}%>
        </table>
        <br/>
    </div>
</asp:Content>
