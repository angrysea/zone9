<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<AvailabilityViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%
    Availability availability = ViewData.Model.availability;
    bool update = ViewData.Model.update;
%>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {

            $("#Cancel").click(function(e) {
                window.location = "/Company/Availabilities";
            });
            
            $("#Save").click(function(event) {
                if($("#description").val().length == 0)
                {
                    alert("Invalid description!");
                    $("#description").focus();
                    e.preventDefault();
                    return false;
                }

                if($("#priority").val().length == 0)
                {
                    alert("Invalid priority!");
                    $("#priority").focus();
                    e.preventDefault();
                    return false;
                }

            });

            <%if(update==true ){%>        
            $("#Delete").click(function(event) {
                $("#mode").val("D");
                $("#form1").submit();
            });
            <%}%>
        });
        
    </script>

    <form name="form1" action="/Company/AvailabilityAction" method="post">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Status Code</legend>
            <label for="code">Status Code:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=availability.Code%>" />
            <input name="code" id="code" type="hidden" value="<%=availability.Code%>" />
            <%}else{%>    
            <input name="code" id="code" type="text" size="25" value="" />
            <%}%>
            <br />
            <label for="description">Description:</label>
            <input name="description" id="description" type="text" maxlength="80" value="<%=update?availability.Description:""%>" />
            <br />
            <label for="expectedwait">Expected Wait:</label>
            <input name="expectedwait" id="expectedwait" type="text" size="25"  value="<%=update?availability.ExpectedWait:0%>" />
            <br />
            <label for="priority">Priority:</label>
            <input name="priority" id="priority" type="text" size="25" value="<%=update?availability.Priority:""%>"/>
        </fieldset>
        <div class="actions">
        <ul>
        <%if(update==true){%>
            <li><input class="button" type="submit" value="Update" id="Save" /></li>
            <li><input class="button" type="button" value="Delete" id="Delete" /></li>
        <% }else {%>
            <li><input class="button" type="submit" value="Insert" id="Save" /></li>
        <%}%>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
