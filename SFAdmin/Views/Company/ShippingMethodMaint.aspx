<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<ShippingMethodViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%
    ShippingMethod shippingMethod = ViewData.Model.shippingMethod;
    bool update = ViewData.Model.update;
%>
    
    <script language="javascript" type="text/javascript">
        function OnClickCancel() {
            window.location="/Company/ShippingMethods";
        }

        function OnClick() {
            if (document.form1.code.value.length == 0) {
                alert("Invalid code!");
                document.form1.code.focus();
                return;
            }

            if (document.form1.description.value.length == 0) {
                alert("Invalid Description!");
                document.form1.description.focus();
                return;
            }

            document.form1.submit();
        }

        <% if(update==true ) { %>        
            function OnClickDelete()
            {
                document.getElementById('mode').value = 'D';
                document.form1.submit();
            }
        <% } %>

    </script>
    
    <form name="form1" action="/Company/ShippingMethodAction" method="post">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">

        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Shipping Method</legend>
            <label for="code">Code:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=shippingMethod.Code%>" />
            <input name="code" id="code" type="hidden" value="<%=shippingMethod.Code%>" />
            <%}else{%>    
            <input name="code" id="code" type="text" size="25" value="" />
            <%}%>
            <br />
            <label for="carrier">Carrier:</label>
            <select name="carrier" id="carrier">
            <%foreach (Carrier c in ViewData.Model.carriers){%>
                <option 
                    <%if(update==true&&c.Code==shippingMethod.Carrier){%>selected="selected"<%}%>
                    value="<%=c.Code%>" ><%=c.Code%></option> 
            <%}%>
            </select>
            <br />
            <label for="country">Country:</label>
            <input name="country" type="text" maxlength="50" value="<%=update?shippingMethod.Country:""%>"/>
            <br />
            <label for="fixedPrice">Fixed Price:</label>
            <input name="fixedPrice" type="text" maxlength="50" value="<%=update?string.Format("{0:#,#.00}", shippingMethod.FixedPrice):""%>"/>
            <br />
            <label for="freeShippingAmount">Free Ship Amount:</label>
            <input name="freeShippingAmount" type="text" maxlength="50" value="<%=update?string.Format("{0:#,#.00}", shippingMethod.FreeShippingAmount):""%>"/>
            <br />
            <label for="description">Description:</label>
            <textarea name="description"><%=update?shippingMethod.Description:""%></textarea>
            <br />
            <label for="notes">Notes:</label>
            <textarea name="notes" rows="5" cols="70"><%=update?shippingMethod.Notes : ""%></textarea>
            <br />
        </fieldset>
        <div class="actions">
        <ul>
        <%if(update==true){%>
            <li><input class="button" type="button" value="Update" onclick="OnClick()" /></li>
            <li><input class="button" type="button" value="Delete" onclick="OnClickDelete()" /></li>
        <% }else {%>
            <li><input class="button" type="button" value="Insert" onclick="OnClick()" /></li>
        <%}%>
            <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
