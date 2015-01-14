<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SFAdmin.Models.NotesViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Notes</title>
    <link href="/Content/css/Site.css" rel="stylesheet" type="text/css" />
    <link href="/Content/css/color.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.3.1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {

            $("#Close").click(function(event) {
                self.close();
            });

            $("#Save").click(function(e) {
                if ($("#notetext").val().length == 0) {
                    alert("Invalid Note!");
                    $("#notetext").focus();
                    e.preventDefault();
                }
            });
        });
    </script>        
    
</head>

<body>
    <div class="frame" id="input-screen">
        <form name="form1" action="/Customer/Notes" method="post">
        <input type="hidden" name="referenceno" id="referenceno" value="<%=ViewData.Model.ReferenceNo%>" />
        
        <fieldset>
            <legend>Notes</legend>
            <div>
                <span>Reference Number: <%=ViewData.Model.ReferenceNo%></span>
            </div>    
            
            <table cellspacing="1" cellpadding="2" border="0">
                <tr>
                    <th>Date</th>
                    <th>Note</th>
                </tr>
                <%foreach(Note note in ViewData.Model.Notes){%>
                <tr>
                    <td><%=string.Format("{0:MM/dd/yyyy}", note.Creationdate)%></td>
                    <td><%=note.Text%></td>
                </tr>
                <%}%>
                <tr>
                    <td></td>
                    <td><textarea name="notetext" id="notetext" cols="100" rows="4"></textarea></td>
                </tr>
            </table>
        </fieldset>
        <div class="actions">
        <ul>
            <li><input class="button" type="submit" value="Add Note" id="Save" /></li>
            <li><input class="button" type="button" value="Close Notes" id="Close" /></li>
        </ul>
        </div>
        </form>
    </div>
</body>
</html>