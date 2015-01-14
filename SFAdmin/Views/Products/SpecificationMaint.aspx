<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<SpecificationViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        Specification specification = ViewData.Model.specification;
        bool update = ViewData.Model.update;
    %>
    
    <script language="javascript" type="text/javascript">
        
        $(document).ready(function(){
            
            $("#Cancel").click(function(e) {
                window.location = "/Products/Specifications";
            });
    
            $("#Save").click(function(e) {
                if ( $("#name").val().length == 0) {
                    alert("Invalid Name!");
                    $("#name").focus();
                    e.preventDefault();
                    return;
                }
            });

            <%if(update==true ){%>        
                $("#description").focus();
                $("#Delete").click(function(event) {
                    $("#mode").val("D");
                    $("#form1").submit();
                });
            <%} else {%>        
                $("#name").focus();
            <%}%>        
        });

    </script>

    <form name="form1" action="/Products/SpecificationAction" method="post" onload="OnLoadSpecification()">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Specification</legend>
            <label for="name">Name:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=specification.Name%>" />
            <input name="name" id="name" type="hidden" value="<%=specification.Name%>" />
            <%}else{%>    
            <input name="name" id="name" type="text" value="" />
            <%}%>
            <br />
            <label for="description">Description:</label>
            <input name="description" id="description" type="text" maxlength="200" value="<%=update?specification.Description:""%>" />
            <br />
            <label for="type">Type:</label>
            <select name="type" id="type">
                <option 
                    value="Amount" 
                    <%if(update==true&&specification.Type=="Amount"){%>
                        selected="selected"
                    <%}%>
                    >Amount</option>
                <option 
                    value="Count" 
                    <%if(update==true&&specification.Type=="Count"){%>
                        selected="selected"
                    <%}%>
                    >Count</option>
                <option 
                    value="Description" 
                    <%if(update==true&&specification.Type=="Description"){%>
                        selected="selected"
                    <%}%>
                    >Description</option>
            </select>
            <br />
            <label for="minValue">Minimum Value:</label>
            <input name="minValue" id="minValue" type="text" maxlength="80" value="<%=update?specification.MinValue:""%>" />
            <br />
            <label for="maxValue">Maximum Value:</label>
            <input name="maxValue" id="maxValue" type="text" maxlength="80" value="<%=update?specification.MaxValue:""%>" />
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
