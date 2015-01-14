<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<EMailListViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    SiteTheme theme = ViewData.Model.theme;
    EMailList emailList = ViewData.Model.emailList;
    bool update = ViewData.Model.update;
%>

    <script language="javascript" type="text/javascript">
    
        $(document).ready(function(){
            <%if(update){%>
                $("#optout").focus();
            <%} else {%>    
                $("#email").focus();
            <%}%>

            $("#Cancel").click(function(event) {
                window.location = "/Company/EMailLists";
            });
        
            $("#Save").click(function(e) {
                if (checkEmail($("#email").val()) == false) {
                    alert("Invalid email!");
                    $("#email").focus();
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
    
    
    <form name="form1" action="/Company/LinkAction" method="post">
    <input name="mode" type="hidden" value="<%=update?"U":"I"%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%=update?"Update/Delete ":"Add "%>EMail</legend>
            <br />
            <label for="email">EMail Address:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=emailList.Email%>"/>
            <input name="email" id="email" type="hidden" value="<%=emailList.Email%>"/>
            <%} else {%>    
            <input name="email" id="email" type="text" size="80" value=""/>
            <%}%>
            <br />
            <label for="optout">Opt Out:</label>
            <input name="optout" id="optout" class="radioStyle" type="checkbox" name="optout" <%=update&&emailList.Optout>0?"checked='checked'":""%>/>
            <br />
        </fieldset>
        <div class="actions">
        <ul>
        <%if(update==true){%>
            <li><input class="button" type="submit" value="Update" id="Save" /></li>
            <li><input class="button" type="button" value="Delete" id="Delete" /></li>
        <%}else{%>
            <li><input class="button" type="submit" value="Insert" id="Save" /></li>
        <%}%>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </div>
    </form>

</asp:Content>
