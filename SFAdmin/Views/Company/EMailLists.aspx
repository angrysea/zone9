<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<EMailListsViewData>" %>
<%@ Import Namespace="SFAdmin.Models" %>
<%@ Import Namespace="StorefrontModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
                        
    <div class="lists">
        <h1>Email List</h1>
        <table class="listtable">
            <tr>
                <th>EMail</th>
                <th>Opt Out</th>
                <th>Creation Date</th>
            </tr>
        <%
            foreach (EMailList emailList in ViewData.Model.emailLists)
            {
        %>
                <tr>
                    <td>
                        <a href="/Company/EMailListMaint/<%=emailList.Email%>"><%=emailList.Email%></a>
                    </td>
                    <td><%=emailList.Optout%></td>
                    <td><%=string.Format("{0:MM/dd/yyyy}",emailList.Creationdate)%></td>
                </tr>
        <%
            }
        %>
        </table>
        <br/>
        <div class="actions">
            <a href="/Company/EMailListMaint/">Add a new Email</a>
        </div>    
    </div>

</asp:Content>
