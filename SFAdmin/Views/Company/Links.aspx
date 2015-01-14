<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<LinksViewData>" %>
<%@ Import Namespace="SFAdmin.Models" %>
<%@ Import Namespace="StorefrontModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="lists">
        <h1>Links</h1>
        <table class="listtable">
            <tr>
                <th>URL</th>
                <th>Link Back</th>
                <th>Last E-Mail Date</th>
            </tr>
            <%
                foreach (Link link in ViewData.Model.links)
                {
            %>
                <tr>
                    <td>
                        <a href="/Company/LinkMaint/<%=link.Url%>"><%=link.Url%></a>
                    </td>
                    <td><%=link.Linkback==1?"true":"false"%></td>
                    <td><%=string.Format("{0:MM/dd/yyyy}",link.Emailssentdate)%></td>
                    <td>
                    <%
                        if (link.Email!=null&&link.Email.Length>0&&link.Linkback==0)
                        {
                    %>
                            <a href="/Company/LinkSend/<%=link.Url%>">send e-mail</a>
                    <%
                        }
                    %>
                        </td>
                </tr>
            <%
                }
            %>
        </table>
        <br/>
        <div><img src="/Content/images/blank.gif" alt="" width="350" height="1" /></div>
        <div><span style="width: 20em; text-align: right">Link Count: </span><%=ViewData.Model.links.Count%></div>
        <br/>
        <div class="actions">
            <a href="/Company/LinkMaint/">Add a new Link</a>
        </div>    
    </div>

</asp:Content>
