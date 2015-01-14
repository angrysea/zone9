<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<PricingWizardViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/javascript">
    
        function OnClick() {
            document.form1.submit();
        }

        function OnClickCancel() {
            window.location = "/Home/Index";
        }
        
    </script>

    <form name="form1" action="/Products/PricingWizardUpdate" method="post">
    <div class="frame" id="input-screen">
        <fieldset>
            <legend>Pricing Wizard</legend>
            <br/>
            <span>The pricing wizard will iterate through the products from the manufacturers selected and set the prices to the markup specified in each manufacturer record.</span>
            <br/>
            <br/>
            <font color="red">WARNING: Use this feature with caution.  Prices are affected immediately.</font><br />
            <br/>
            <label for="manufacturer">Manufacturer:</label>
            <select name="manufacturer">
                <option value="">< Select ></option>
                <option 
                value="all"
                    selected="selected">
                    All
                </option>
                <%foreach(Manufacturer manufacturer in ViewData.Model.manufacturers) {%>
                <option 
                    value="<%=manufacturer.Name%>">
                    <%=manufacturer.Name%>
                </option>
                <%}%>
            </select>
            <br/>
            <br/>
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="button" value="Update Pricing" onclick="OnClick()" /></li>
            <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
