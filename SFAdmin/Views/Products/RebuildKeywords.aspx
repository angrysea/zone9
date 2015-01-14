<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<RebuildKeywordsViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%
    string manufacturername = ViewData.Model.manufacturer;
%>

    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            $("#Cancel").click(function(e) {
                window.location = "/Home/Index";
            });

        });
    </script>


    <form name="form1" action="/Products/RebuildKeywords" method="post">
    <div class="frame" id="input-screen">
        <fieldset>
            <legend>Rebuild Product Keywords</legend>
            <div>This feature will iterate through the products from the manufacturer(s) selected and regenerate the keywords for each product.  The keywords are generated from the product name, specifications, details, etc.</div>
            <br />
            <div><font color="red">WARNING: Use this feature with caution.  Keywords are affected immediately and will affect customer search results.</font></div>
            <br />
            <label for="name">Select Manufacturer:</label>
            <select name="manufacturer">
                <option value="0">all</option>
                <%  foreach(Manufacturer manufacturer in ViewData.Model.manufacturers)
                    {
                %>
                        <option value="<%=manufacturer.Name%>">
                        <%=manufacturer.Name%>
                        </option>
                <%
                    }
                %>
            </select>
            <br />
            <%if (ViewData.Model.rebuildcount > 0){%>
            <div>
                <h3>Rebuild Keywords Results</h3>
                <br />
                <span><%=ViewData.Model.rebuildcount%> product(s) keywords were rebuilt successfully for <%=manufacturername%>.</span>
            </div>
            <br />
            <%}%>
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="Rebuild" /></li>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
