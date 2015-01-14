<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<LinkViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%
    SiteTheme theme = ViewData.Model.theme;
    Link link = ViewData.Model.link;
    bool update = ViewData.Model.update;
%>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function(){
            <%if(update){%>
                $("#header").focus();
            <%} else {%>    
                $("#url").focus();
            <%}%>
            
            $("#Cancel").click(function(e) {
                window.location = "/Company/Links";
            });
        
            $("#Save").click(function(e) {
                if($("#url").val().length == 0)
                {
                    alert("Invalid URL!");
                    $("#url").focus();
                    e.preventDefault();
                    return;
                }

                if($("#header").val().length == 0)
                {
                    alert("Invalid Heading!");
                    $("#header").focus();
                    e.preventDefault();
                    return;
                }

                if ($("#description").val().length == 0) {
                    alert("Invalid Description!");
                    $("#description").focus();
                    e.preventDefault();
                    return;
                }

                if ($("#email").val().length == 0) {
                    alert("Invalid email!");
                    $("#email").focus();
                    e.preventDefault();
                    return;
                }
            });
    
            <%if(update==true ){%>        
            $("#Delete").click(function(e) {
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
            <legend><%=update?"Update/Delete ":"Add "%>Link</legend>
            <br />
            <label for="url">Link URL:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=link.Url%>"/>
            <input name="url" id="url" type="hidden" value="<%=link.Url%>"/>
            <%} else {%>    
            <input name="url" type="text" size="40" value=""/>
            <%}%>
            <br />
            <label for="header">Heading:</label>
            <input name="header" id="header" type="text" size="40" value="<%=update?link.Header:""%>" />
            <label for="description" >Description:</label>
            <textarea name="description" id="description" rows="10" cols="70"><%=update?link.Description:""%></textarea>
            <br />
            <label for="email">EMail Address:</label>
            <input name="email" id="email" type="text" size="40" value="<%=update?link.Email:""%>" />
            <br />
            <label for="emailssent">EMail Sent:</label>
            <input name="emailssent" id="emailssent" class="radioStyle" type="checkbox" disabled="disabled"  <%=update&&link.Emailssent>0?"checked='checked'":""%>/>
            <br />
            <label for="linkback">Linked Back:</label>
            <input name="linkback" id="linkback" class="radioStyle" type="checkbox" <%=update&&link.Linkback>0?"checked='checked'":""%>/>
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
