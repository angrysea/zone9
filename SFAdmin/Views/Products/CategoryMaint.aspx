<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CategoryViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%
        Category category = ViewData.Model.category;
        bool update = ViewData.Model.update;
    %>
    
    <script language="javascript" type="text/javascript">
    
        $(document).ready(function(){
            createIntegerField(document.getElementById("sortorder"));
            createNumericField(document.getElementById("startPrice"), 2);
            createNumericField(document.getElementById("endPrice"), 2);
            $("#Cancel").click(function(e) {
                window.location = "/Products/Categories";
            });
            
            $("#Save").click(function(e) {
                var name = $("#name");
                if(name.val().length == 0) {
                    alert("Invalid Name!");
                    name.focus();
                    e.preventDefault();
                    return;
                }
                var longName = $("#longName");
                if (longName.val().length == 0) {
                    alert("Invalid long name!");
                    longName.focus();
                    e.preventDefault();
                    return;
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

    <form name="form1" action="/Products/CategoryAction" method="post">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Category</legend>
            <label for="name">Name:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=category.Name%>" />
            <input id="name" name="name" type="hidden" value="<%=category.Name%>" />
            <%}else{%>    
            <input name="name" id="name" type="text" value="" />
            <%}%>
            <br />
            <label for="groupType">Group Type:</label>
            <select name="groupType" id="groupType">
                <%foreach (GroupType g in ViewData.Model.groupTypes){%>
                    <option 
                        value="<%=g.Name %>" 
                        <%if(update==true&&g.Name==category.GroupType){%>
                            selected="selected"
                        <%}%>
                        >
                        <%=g.Name%> 
                    </option>
                <%}%>
            </select>
            <br />
            <label for="sortOrder">Sort Order:</label>
            <input name="sortOrder" id="sortorder" type="text" value="<%=update?category.SortOrder.ToString():""%>" />
            <br />
            <label for="url">URL:</label>
            <input name="url" type="text" value="<%=update?category.URL:""%>" />
            <br />
            <label for="active">Active:</label>
            <input class="radioStyle" name="active" type="checkbox" <%if(update&&category.Active>0){%>checked="checked"<%}%> />
            <br />
            <label for="longName">Long Name:</label>
            <input name="longName" id="longName" type="text" value="<%=update?category.LongName:""%>" />
            <br />
            <label for="startPrice">Start Price:</label>
            <input name="startPrice" id="startPrice" type="text" value="<%=update?string.Format("{0:#,#.00}", category.StartPrice):""%>"/>
            <br />
            <label for="endPrice">End Price:</label>
            <input name="endPrice" id="endPrice" type="text" value="<%=update?string.Format("{0:#,#.00}", category.EndPrice):""%>"/>
            <br />
            <label for="description">Description:</label>
            <textarea name="description" id="description" rows="10" cols="70"><%=update?category.Description:""%></textarea>
            <br />
            <label for="parent">Parent Category:</label>
            <select name="parent" id="parent">
                    <option />
                <%foreach (Category c in ViewData.Model.categories){%>
                    <option 
                        value="<%=c.Name %>" 
                        <%if(update==true&&c.Name==category.Parent){%>
                            selected="selected"
                        <%}%>
                        >
                        <%=c.Name%> 
                    </option>
                <%}%>
            </select>
        </fieldset>
        <br />
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
