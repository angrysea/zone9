<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<SFViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<% 
    Company company = (Company)ViewData.Model.company; 
%>
    <div style="vertical-align: middle; text-indent: 6em;">
        <h1><%= Html.Encode(company.Name) %></h1>
    </div>
</asp:Content>
