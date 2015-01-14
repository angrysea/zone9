<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CompanyViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<%
    Company company = ViewData.Model.company; 
    bool update = ViewData.Model.update;
%>
    <script type="text/javascript" >
        var currentPage = 1;
        var maxPage = 3;
        $(document).ready(function() {

            $("#next").click(function(event) {
                var current = '#page' + currentPage;
                if (currentPage == maxPage)
                    currentPage = 1;
                else
                    currentPage++;
                var next = '#page' + currentPage;
                $(current).removeClass('page');
                $(current).addClass('hiddenpage');
                $(next).removeClass('hiddenpage');
                $(next).addClass('page');
                event.preventDefault();
            });

            $("#prev").click(function(event) {
                var current = '#page' + currentPage;
                if (currentPage == 1)
                    currentPage = maxPage;
                else
                    currentPage--;
                var prev = '#page' + currentPage;
                $(current).removeClass('page');
                $(current).addClass('hiddenpage');
                $(prev).removeClass('hiddenpage');
                $(prev).addClass('page');
                event.preventDefault();
            });

            $("#Cancel").click(function(e) {
                window.location = "/Home/Index";
            });

        });

    </script>
        
    <form method="post" action="/Company/CompanyAction">
        <input name="mode" type="hidden" value="<%=update?"U":"I"%>" />
        <div class="frame" id="input-screen">
        <fieldset>
            <legend>Company Details</legend>
            <label for="name">Company Name:</label>
            <%if(update){%>
            <input id="name" class="input" type="text" readonly="readonly" value="<%=company.Name%>" />
            <input name="name" type="hidden" value="<%=company.Name%>" />
            <%}else{%>    
            <input name="name" id="name" type="text" maxlength="10" value="" />
            <%}%> 
            <div id="page1" class="page">
                <br/>
                <label for="name">Description:</label>
                <textarea name="description" rows="10" cols="60"><%=company.Description%></textarea>
                <br/>
                <label for="address1">Address 1:</label>
                <input name="address1" type="text" size="60" value="<%=company.Address1%>"/>
                <br/>
                <label for="address2">Address 2:</label>
                <input name="address2" type="text" size="60" value="<%=company.Address2%>"/>
                <br/>
                <label for="address3">Address 3:</label>
                <input name="address3" type="text" size="60" value="<%=company.Address3%>"/>
                <br/>
                <label for="city">City:</label>
                <input name="city" type="text" size="30" value="<%=company.City%>"/>
                <br/>
                <label for="state">State:</label>
                <input name="state" type="text" size="2" value="<%=company.State%>"/>
                <br/>
                <label for="zipcode">Zip Code:</label>
                <input name="zipcode" type="text" size="8" value="<%=company.Zip%>"/>
                <br/>
                <label for="countrycode">Country Code:</label>
                <input name="countrycode" type="text" size="3" value="<%=company.Country%>"/>
                <br/>
                <label for="primarytelephone">Telephone:</label>
                <input name="primarytelephone" type="text" size="20" value="<%=company.Phone%>"/>
                <br/>
                <label for="customerservice">Customer Service:</label>
		        <input name="customerservice" type="text" size="20" value="<%=company.CustomerService%>"/>
                <br/>
                <label for="fax">Fax:</label>
                <input name="fax" type="text" size="20" value="<%=company.Fax%>"/>
                <br/>
            </div>

            <div id="page2" class="hiddenpage">
                <br/>
                <label for="email1">E-Mail 1:</label>
                <input name="email1" type="text" size="30" value="<%=company.EMail1%>"/>
                <br/>
                <label for="email2">E-Mail 2:</label>
                <input name="email2" type="text" size="30" value="<%=company.EMail2%>"/>
                <br/>
                <label for="email3">E-Mail 3:</label>
                <input name="email3" type="text" size="30" value="<%=company.EMail3%>"/>
                <br/>
                <label for="baseurl">Base URL:</label>
                <input name="baseurl" type="text" size="30" value="<%=company.BaseURL%>"/>
                <br/>
                <label for="basesecureurl">Base Secure URL:</label>
                <input name="basesecureurl" type="text" size="30" value="<%=company.BaseSecureURL%>"/>
                <br/>
                <label for="visualurl">Visual URL:</label>
                <input name="visualurl" type="text" size="30" value="<%=company.CompanyURL%>"/>
                <br/>
                <label for="impersonatepassword">Impersonate Pw:</label>
                <input name="impersonatepassword" type="text" size="20" value="<%=company.PassWord%>"/>
                <br/>
                <label for="defaultshipping">Carrier:</label>
                <select name="defaultshipping" id="defaultshipping">
                <%foreach (ShippingMethod s in ViewData.Model.shippingMethods)
                  {%>
                    <option 
                        <%if(update==true&&s.Code==company.DefaultShipping){%>selected="selected"<%}%>
                        value="<%=s.Code%>" ><%=s.Code%></option> 
                <%}%>
                </select>
                <br />
            </div>   
                         
            <div id="page3" class="hiddenpage">            
                <br/>
                <label for="keyword">Keyword:</label>
                <input name="keyword" type="text" size="60" value="<%=company.Keyword%>" />
                <br/>
                <label for="keyword1">Keyword1:</label>
                <input name="keyword1" type="text" size="60" value="<%=company.Keyword1%>"/>
                <br/>
                <label for="keyword2">Keyword2:</label>
                <input name="keyword2" type="text" size="60" value="<%=company.Keyword2%>"/>
                <br/>
                <label for="keyword3">Keyword3:</label>
                <input name="keyword3" type="text" size="60" value="<%=company.Keyword3%>"/>
                <br/>
                <label for="keyword4">Keyword4:</label>
                <input name="keyword4" type="text" size="60" value="<%=company.Keyword4%>"/>
                <br/>
                <label for="keyword5">Keyword5:</label>
                <input name="keyword5" type="text" size="60" value="<%=company.Keyword5%>"/>
                <br/>
                <label for="salesordercoupon">Sales Order Coupon:</label>
                <input name="salesordercoupon" type="text" size="60" value="<%=company.SalesOrderCoupon%>"/>
                <br/>
                <label for="instockonly">In Stock Only:</label>
                <input class="radioStyle" name="instockonly" type="checkbox" value="<%=company.InStockOnly%>"/>
                <br/>
                <label for="theme">Theme:</label>
                <select name="theme" id="theme">
                        <%foreach( SiteTheme theme in ViewData.Model.themes) {%>
                        <option 
                            value="<%=theme.Name%>" 
                                <%=theme.Name==company.Theme?"selected=\"selected\"":"" %>;
                            > 
                            <%=theme.Name %> 
                        </option>
                        <%}%>
                    </select>
                <br/>
            </div>
            <br />
            <ul class="navigation">
                <li class="left">
                    <a id="prev" href=""><img src="/Content/images/arrowleft.gif" style="border-style: none" />&nbsp;Prev</a>
                </li>
                <li class="right">
                    <a id="next" href="">Next&nbsp;<img src="/Content/images/arrowright.gif" style="border-style: none" /></a>
                </li>
            </ul>            
        </fieldset>
        <div class="actions">
            <ul>
                <li><input class="button" type="submit" value="Update Company" /></li>
                <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
            </ul>
        </div>
    </div>
    </form>
   
</asp:Content>
