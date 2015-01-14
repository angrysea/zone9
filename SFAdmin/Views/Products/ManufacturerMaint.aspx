<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<ManufacturerViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%
    Manufacturer manufacturer = ViewData.Model.manufacturer;
    bool update = ViewData.Model.update;
%>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {

            $("#Cancel").click(function(e) {
                window.location = "/Products/Manufacturers";
            });
            
            $("#Save").click(function(e) {
                if($("#name").val().length == 0) {
                    alert("Invalid Name!");
                    $("#name").focus();
                    e.preventDefault();
                    return false;
                }

                if($("#longname").val().length == 0)
                {
                    alert("Invalid Long Name!");
                    $("#longname").focus();
                    e.preventDefault();
                    return false;
                }

                if($("#description").val().length == 0)
                {
                    alert("Invalid Description!");
                    $("#description").focus();
                    e.preventDefault();
                    return false;
                }

                if($("#longdescription").val().length == 0)
                {
                    alert("Invalid Long Description!");
                    $("#longdescription").focus();
                    e.preventDefault();
                    return false;
                }

                if($("#prefix").val().length == 0)
                {
                    alert("Invalid Prefix!");
                    $("#prefix").focus();
                    e.preventDefault();
                    return false;
                }

                if($("#markup").val().length == 0)
                {
                    alert("Invalid Markup Percentage!");
                    $("#markup").focus();
                    e.preventDefault();
                    return false;
                }
                return true;
            });

            <%if(update==true ){%>        
            $("#Delete").click(function(event) {
                $("#mode").val("D");
                $("#form1").submit();
            });
            <%}%>
        });
        
    </script>

    <form name="form1" action="/Products/ManufacturerAction" method="post">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <%IList<string> errors = ViewData.Model.errors;
          if (errors != null) {%>
        <div>
            <ul class="error">
            <% foreach (string error in errors) { %>
                <li><%= Html.Encode(error) %></li>
            <%}%>
            </ul>
        </div>
        <%}%>
        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Manufacturer</legend>
            <label for="name">Name:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=manufacturer.Name%>" />
            <input name="name" id="name" type="hidden" value="<%=manufacturer.Name%>" />
            <%}else{%>    
            <input name="name" id="name" type="text" maxlength="25" value="" />
            <%}%>
            <br />
            <label for="active">Active</label>
            <input class="radioStyle" type="checkbox" name="active" id="active" <%=update&&manufacturer.Active!=0?"checked=\"checked\"":""%> />
            <br />
            <label for="longname">Long Name:</label>
            <input name="longname" id="longname" type="text" maxlength="40" value="<%=update?manufacturer.LongName:""%>" />
            <br />
            <label for="description">Description:</label>
            <textarea name="description" id="description" ><%=update?manufacturer.ShortDescription:""%></textarea>
            <br />
            <label for="longdescription">Long Description:</label>
            <textarea name="longdescription" id="longdescription" ><%=update?manufacturer.Description:""%></textarea>
            <br />
            <label for="prefix">Prefix:</label>
            <input name="prefix" id="prefix" type="text" maxlength="4" value="<%=update?manufacturer.Prefix:""%>"/>
            <br />
            <label for="markup">Markup Percent:</label>
            <input name="markup" id="markup" type="text" maxlength="4" value="<%=update?string.Format("{0:#,#}", manufacturer.MarkUp*100):""%>" />&nbsp;(i.e. 40 = 40%)
            <br />
            <label for="logo">Logo:</label>
            <input name="logo" id="logo" type="text" maxlength="60" value="<%=update?manufacturer.Logo:""%>" />
            <br />
            <label for="url">URL:</label>
            <input name="url" id="url" type="text" maxlength="60" value="<%=update?manufacturer.URL:""%>" />
            <br/>
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
