<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<GroupTypeViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%
        GroupType groupType = ViewData.Model.groupType;
        bool update = ViewData.Model.update;
    %>
    
    <script language="javascript" type="text/javascript">
    
        $(document).ready(function(){
            createIntegerField(document.getElementById("sortorder"));
        })
        
        function OnClickCancel() {
            window.location = "/Products/GroupTypes";
        }
        
        function OnClick() {
            if (document.form1.name.value.length == 0) {
                alert("Invalid Name!");
                document.form1.name.focus();
                return;
            }

            if (document.form1.description.value.length == 0) {
                alert("Invalid Description!");
                document.form1.description.focus();
                return;
            }

            document.form1.submit();
        }

        function OnClickDelete()
        {
            document.getElementById('mode').value = 'D';
            document.form1.submit();
        }

    </script>

    <form name="form1" action="/Products/GroupTypeAction" method="post" onload="OnLoadGroupType()">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Group Type</legend>
            <label for="name">Group Type:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=groupType.Name%>" />
            <input name="name" id="name" type="hidden" value="<%=groupType.Name%>" />
            <%}else{%>    
            <input name="name" id="name" type="text" value="" />
            <%}%>
            <br />
            <label for="type">Type:</label>
            <input name="type" id="type" type="text" maxlength="50" value="<%=update?groupType.Type:""%>" />
            <br />
            <label for="catalog">Catalog:</label>
            <select name="catalog" id="Select1">
            <%foreach (Catalog c in ViewData.Model.catalogs){%>
                <option 
                    value="<%=c.Name%>" 
                    <%if(update&&c.Name==groupType.Catalog)%>selected="selected">
                    <%=c.Name%> 
                </option>
            <%}%>
            </select>
            <br />
            <label for="description" >Description:</label>
            <textarea name="description" rows="10" cols="70"><%=update?groupType.Description:""%></textarea>
            <br />
            <label for="sortorder">Sort Order:</label>
            <input name="sortorder" id="sortorder" type="text" maxlength="25" value="<%=update?groupType.Sortorder.ToString():""%>" />
            <br />
            <label for="image">Image:</label>
            <input name="image" type="text" maxlength="80" value="<%=update?groupType.Image:""%>" />
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
