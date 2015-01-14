<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CatalogViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%
        SiteTheme theme = ViewData.Model.theme;
        Company company = ViewData.Model.company;
        Catalog catalog = ViewData.Model.catalog;
        bool update = ViewData.Model.update;
    %>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {

            $("#Cancel").click(function(e) {
                window.location = "/Company/Catalogs";
            });
            
            $("#Save").click(function(event) {
                if ($("#name").val().length == 0) {
                    alert("Invalid Name!");
                    $("#name").focus();
                    e.preventDefault();
                    return false;
                }

                if ($("#description").val().length == 0) {
                    alert("Invalid Description!");
                    $("#description").focus();
                    e.preventDefault();
                    return false;
                }
                return true;
            });

            $("#Copy").click(function(event) {
                var answer = confirm("Are you sure this will override all data?");
                if(answer)
                {
                    <%if(company.Name!=null&&company.Name.Length>0){%>
                    $("#name").val("<%=company.Name%>");
                    <%} if(company.Description!=null&&company.Description.Length>0){%>
                    $("#description").val("<%=company.Description%>");
                    <%} if(company.Address1!=null&&company.Address1.Length>0){%>
                    $("#address1").val("<%=company.Address1%>");
                    <%} if(company.Address2!=null&&company.Address2.Length>0){%>
                    $("#address2").val("<%=company.Address2%>");
                    <%} if(company.Address3!=null&&company.Address3.Length>0){%>
                    $("#address3").val("<%=company.Address3%>");
                    <%} if(company.City!=null&&company.City.Length>0){%>
                    $("#city").val("<%=company.City%>");
                    <%} if(company.State!=null&&company.State.Length>0){%>
                    $("#state").val("<%=company.State%>");
                    <%} if(company.Zip!=null&&company.Zip.Length>0){%>
                    $("#zipcode").val("<%=company.Zip%>");
                    <%} if(company.Country!=null&&company.Country.Length>0){%>
                    $("#countrycode").val("<%=company.Country%>");
                    <%} if(company.Phone!=null&&company.Phone.Length>0){%>
                    $("#primarytelephone").val("<%=company.Phone%>");	
                    <%}if(company.CustomerService!=null&&company.CustomerService.Length>0){%>
                    $("#customerservice").val("<%=company.CustomerService%>");
                    <%} if(company.Fax!=null&&company.Fax.Length>0){%>
                    $("#fax").val("<%=company.Fax%>");
                    <%} if(company.EMail1!=null&&company.EMail1.Length>0){%>
                    $("#email1").val("<%=company.EMail1%>");
                    <%} if(company.EMail2!=null&&company.EMail2.Length>0){%>
                    $("#email2").val("<%=company.EMail2%>");
                    <%} if(company.EMail3!=null&&company.EMail3.Length>0){%>
                    $("#email3").val("<%=company.EMail3%>");
                    <%} if(company.BaseURL!=null&&company.BaseURL.Length>0){%>
                    $("#baseurl").val("<%=company.BaseURL%>");
                    <%} if(company.BaseSecureURL!=null&&company.BaseSecureURL.Length>0){%>
                    $("#basesecureurl").val("<%=company.BaseSecureURL%>");
                    <%} if(company.CompanyURL!=null && company.CompanyURL.Length>0){%>
                    $("#url").val("<%=company.CompanyURL%>");
                    <%}%>
                    
                    var listBox = document.getElementById("theme");
   			        for(i=0; i<listBox.length; i++) {
    				    if(listBox.options[i].value=="<%=theme.Name%>")
    				    {
    				        listBox.options[i].selected="selected";
    			        }
    			    }
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

    <form id="form1" action="/Company/CatalogAction" method="post">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Catalog</legend>
            <label for="name">Name:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=catalog.Name%>" />
            <input name="name" id="name" type="hidden" value="<%=catalog.Name%>" />
            <%}else{%>    
            <input name="name" id="name" type="text" size="25" value="" />
            <%}%>
            <br />
            <label for="description">Description:</label>
            <textarea name="description" id="description"><%if(update==true)%><%=catalog.Description%></textarea>
            <br />
            <label for="address1">Address 1:</label>
            <input name="address1" id="address1" type="text" size="60" value="<%if(update==true)%><%=catalog.Address1%>"/>
            <br />
            <label for="address2">Address 2:</label>
            <input name="address2" id="address2"  type="text" size="60" value="<%if(update==true)%><%=catalog.Address2%>"/>
            <br />
            <label for="address3">Address 3:</label>
            <input name="address3" id="address3"  type="text" size="60" value="<%if(update==true)%><%=catalog.Address3%>"/>
            <br />
            <label for="city">City:</label>
            <input name="city" id="city"  type="text" size="30" value="<%if(update==true)%><%=catalog.City%>"/>
            <br />
            <label for="state">State:</label>
            <input name="state" id="state"  type="text" size="2" value="<%if(update==true)%><%=catalog.State%>"/>
            <br />
            <label for="zipcode">Zip Code:</label>
            <input name="zipcode" id="zipcode"  type="text" size="8" value="<%if(update==true)%><%=catalog.Zip%>"/>
            <br />
            <label for="countrycode">Country Code:</label>
            <input name="countrycode" id="countrycode"  type="text" size="3" value="<%if(update==true)%><%=catalog.Country%>"/>
            <br />
            <label for="primarytelephone">Primary Telephone:</label>
            <input name="primarytelephone" id="primarytelephone"  type="text" size="20" value="<%if(update==true)%><%=catalog.Phone%>"/>
            <br />
            <label for="">Customer Service:</label>
            <input name="customerservice" id="customerservice"  type="text" size="20" value="<%if(update==true)%><%=catalog.CustomerService%>"/>
            <br />
            <label for="fax">Fax:</label>
            <input name="fax" id="fax"  type="text" size="20" value="<%if(update==true)%><%=catalog.Fax%>"/>
            <br />
            <label for="email1">E-Mail 1:</label>
            <input name="email1" id="email1"  type="text" size="30" value="<%if(update==true)%><%=catalog.Email1%>"/>
            <br />
            <label for="email2">E-Mail 2:</label>
            <input name="email2" id="email2"  type="text" size="30" value="<%if(update==true)%><%=catalog.Email2%>"/>
            <br />
            <label for="email3">E-Mail 3:</label>
            <input name="email3" id="email3"  type="text" size="30" value="<%if(update==true)%><%=catalog.Email3%>"/>
            <br />
            <label for="baseurl">Base URL:</label>
            <input name="baseurl" id="baseurl"  type="text" size="30" value="<%if(update==true)%><%=catalog.Baseurl%>"/>
            <br />
            <label for="basesecureurl">Base Secure URL:</label>
            <input name="basesecureurl" id="basesecureurl"  type="text" size="30" value="<%if(update==true)%><%=catalog.Basesecureurl%>"/>
            <br />
            <label for="theme">Theme:</label>
            <select name="theme" id="theme">
            <%foreach (SiteTheme t in ViewData.Model.themes){%>
                <option value="<%=t.Name %>" 
                    <%if(update==true&&t.Name==catalog.Theme)%>selected="selected">
                        <%=t.Name%> 
                </option>
            <%}%>
            </select>
            <br />
            <label for="url">URL:</label>
            <input name="url" id="url"  type="text" size="80" value="<%if(update==true)%><%=catalog.Url%>" />
            <br />
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
            <li><input class="button" type="button" value="Copy Company" id="Copy"/></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
