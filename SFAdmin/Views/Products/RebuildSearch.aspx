<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<RebuildSearchViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            $("#Cancel").click(function(e) {
                window.location = "/Home/Index";
            });

            $("#manufacturer").change(function(e) {
                $('#type').val("manufacturer");
            });

            $("#category").change(function(e) {
                $('#type').val("category");
            });
        });
        
    </script>

    <form name="form1" action="/Products/RebuildSearch" method="post">
    <input type="hidden" name="type" id="type" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend>Rebuild Landing Page Searches</legend>
            <div>This feature will iterate through the products from the manufacturer(s) or category (s) selected and regenerate the landing page search.</div>
            <br />
            <div><font color="red">WARNING: Use this feature with caution.  Landing page searches are affected immediately and will affect customer search results.</font></div>
            <br />
            <label for="name">Select Manufacturer:</label>
            <select name="manufacturer" id="manufacturer">
                <option value="0">all</option>
                <%  foreach(Manufacturer manufacturer in ViewData.Model.manufacturers)
                    {
                %>
                        <option value="<%=manufacturer.Name%>">
                            <%=manufacturer.LongName%>
                        </option>
                <%
                    }
                %>
            </select>
            <br />
            <label for="name">Select Category:</label>
            <select name="category" id="category" >
                <option value="0">all</option>
                <%  foreach (Category category in ViewData.Model.categories)
                    {
                %>
                        <option value="<%=category.Name%>">
                            <%=category.LongName%>
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
                <span><%=ViewData.Model.rebuildcount%> product(s) for Landing Page Search <%=ViewData.Model.searchname%> rebuilt successfully.</span>
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
