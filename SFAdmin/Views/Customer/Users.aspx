<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<UsersViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            createDateField(document.getElementById("fromdate"));
            createDateField(document.getElementById("todate"));
            $("#fromdate").focus();
            $("#Go").click(function(event) {
                if ($("#fromdate").val().length == 0) {
                    alert("Invalid From Date!");
                    $("#fromdate").focus();
                    e.preventDefault();
                    return;
                }
                if ($("#todate").val().length == 0) {
                    alert("Invalid To Date!");
                    $("#todate").focus();
                    e.preventDefault();
                    return;
                }
            });
        });
    </script>
    
    <form name="form1" action="/Customer/Users" method="post">
    <div class="shortframe" id="input-screen">
        <fieldset>
            <legend>Users</legend>
            <br />
            <label for="fromdate">From:</label>
            <input name="fromdate" id="fromdate" type="text" value="01/01/2009"/>
            <br />
            <label for="todate">To:</label>
            <input name="todate" id="todate" type="text" value="<%=string.Format("{0:MM/dd/yyyy}", DateTime.Now)%>"/>
            <br />
            <br />
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="go" id="Go" /></li>
        </ul>
        </div>
    </div>
    </form>
    <div class="lists">
        <table class="listtable">
            <tr>
                <th>Email</th>
                <th>Creation Date</th>
                <th>Shopping Cart Products</th>
            </tr>
            <%foreach(ViewUser viewUser in ViewData.Model.users)
            {
                    Users user = viewUser.user;
            %>
            <tr>
                <td><%=user.Email%></td>
                <td><%=string.Format("{0:MM/dd/yyyy}", user.Creationdate)%></td>
                <%if (viewUser.productsInCart > 0)
                  {%>
                <td><a href="/Customer/ShoppingCart/<%=user.Cookie%>"><%=viewUser.productsInCart%></a></td>
                <%}else{ %>
                <td><%=viewUser.productsInCart%></td>
                <%}%>
            </tr>
            <%}%>
            <tr>
                <td><br /></td>
            </tr>
        </table>
        <br/>
    </div>
</asp:Content>
