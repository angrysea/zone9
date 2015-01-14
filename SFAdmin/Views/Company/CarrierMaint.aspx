<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CarrierViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%SiteTheme theme = ViewData.Model.theme;%>
    <%Carrier carrier = ViewData.Model.carrier;%>
    <%bool update = ViewData.Model.update;%>
    
    <script language="javascript" type="text/javascript">
    
        function OnClickCancel() {
            window.location="/Company/Carriers";
        }
            
        function OnClick() {
            if (document.form1.code.value.length == 0) {
                alert("Invalid Code!");
                document.form1.code.focus();
                return;
            }

            if (document.form1.name.value.length == 0) {
                alert("Invalid Name!");
                document.form1.name.focus();
                return;
            }

            if (document.form1.description.value.length == 0) {
                alert("Invalid Description!");
                document.form1.description.focus();
                return;
            }

            document.form1.submit();
        }

        function OnClickDelete()
        {
            document.getElementById('mode').value = 'D';
            document.form1.submit();
        }

    </script>
    
    <form name="form1" action="/Company/CarrierAction" method="post">
    <input name="mode" type="hidden" value="<%=update?"U":"I"%>" />
    <div class="frame" id="input-screen">
      
        <fieldset>
            <legend><%=update?"Update/Delete ":"Add "%>Carrier</legend>
            <label for="code">Carrier Code:</label>
            <%if(update){%>
            <input class="input" type="text" readonly="readonly" value="<%=carrier.Code%>" />
            <input name="code" id="code" type="hidden" value="<%=carrier.Code%>" />
            <%}else{%>    
            <input name="code" id="code" type="text" maxlength="10" value="" />
            <%}%>
            <br />
            <label for="name">Name:</label>
            <input name="name" id="name" maxlength="128" type="text" value="<%=update?carrier.Name:""%>" />
            <br />
            <label for="description">Description:</label>
            <textarea name="description" id="Text1" cols="60" rows="10"><%=update?carrier.Description:""%></textarea> 
            <br />
            <label for="">License:</label>
            <input name="license" id="license" type="text" maxlength="80" value="<%=update?carrier.License:""%>" />
            <br />
            <label for="">User ID:</label>
            <input name="userid" id="userid" type="text" maxlength="80" value="<%=update?carrier.UserId:""%>"/>
            <br />
            <label for="">Password:</label>
            <input name="password" id="password" type="password" maxlength="80" value="<%=update?carrier.PassWord:""%>"/>
            <br />
            <label for="">Version:</label>
            <input name="version" id="version" type="text" maxlength="80" value="<%=update?carrier.Version:""%>"/>
            <br />
            <label for="">Pickup Type:</label>
            <input name="pickupType" id="pickupType" type="text" maxlength="80" value="<%=update?carrier.PickupType:""%>"/>
            <br />
            <label for="">URL:</label>
            <input name="url" id="url" type="text" maxlength="1000" value="<%=update?carrier.URL:""%>"/>
            <br />
        </fieldset>
        <div class="actions">
        <ul>
        <%if(update==true){%>
            <li><input class="button" type="button" value="Update" onclick="OnClick()" /></li>
            <li><input class="button" type="button" value="Delete" onclick="OnClickDelete()" /></li>
        <% }else {%>
            <li><input class="button" type="button" value="Insert" onclick="OnClick()" /></li>
        <%}%>
            <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
