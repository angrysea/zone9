<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CustomersViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="JavaScript" type="text/javascript">
        $(document).ready(function() {
            $("#Cancel").click(function(e) {
                window.location = "/Home/Index";
            });

            $("#sortby").change(function(e) {
                $("#form1").submit();
            });
        });
        
    </script>
    
    <div class="lists">
        <h1>Customers</h1>
        <form name="form1" action="/Customer/Customers" method="get">
        <fieldset>
            <legend>Sort Order</legend>
            <label for="sortby">Sort By:</label>
            <select name="sortby" id="sortby">
                <option value="Last">Name</option>
                <option value="Creationdate">Created</option>
            </select>
        </fieldset>
        </form>
        <table class="listtable">
            <tr>
                <th>Name</th>
                <th>E-Mail</th>
                <th>Created</th>
                <th>Last Login</th>
            </tr>
            <%foreach (ViewCustomer customer in ViewData.Model.customers){%>
            <tr>
                <td><a href="/Customer/Customer/<%=customer.customer.CustomerNo%>"><%=customer.customer.Fullname%></a></td>
                <td><%=customer.customer.Email1%></td>
                <td><%=string.Format("{0:f}", customer.user.Creationdate)%></td>
                <td><%=string.Format("{0:f}", customer.user.Lastlogindate)%></td>
            </tr>
            <%}%>
        </table>
        <br/>
        <br/>
        <div class="actions">
            <ul>
                <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
            </ul>
        </div>    
    </div>
</asp:Content>


