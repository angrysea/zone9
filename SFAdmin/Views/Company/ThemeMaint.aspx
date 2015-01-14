<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<ThemeViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<%
    SiteTheme theme = ViewData.Model.theme;
    bool update = ViewData.Model.update;
%>
    
    <script type="text/javascript" >
    
        var currentPage = 1;
        $(document).ready(function() {
            $("#next").click(function(event) {
                if (currentPage == 1) {
                    $('#page1').removeClass('page');
                    $('#page1').addClass('hiddenpage');
                    $('#page2').removeClass('hiddenpage');
                    $('#page2').addClass('page');
                    currentPage = 2;
                }
                else if (currentPage == 2) {
                    $('#page2').removeClass('page');
                    $('#page2').addClass('hiddenpage');
                    $('#page3').removeClass('hiddenpage');
                    $('#page3').addClass('page');
                    currentPage = 3;
                }
                else {
                    $('#page3').removeClass('page');
                    $('#page3').addClass('hiddenpage');
                    $('#page1').removeClass('hiddenpage');
                    $('#page1').addClass('page');
                    currentPage = 1;
                }
                event.preventDefault();
            });
            
            $("#prev").click(function(event) {
                if (currentPage == 1) {
                    $('#page1').removeClass('page');
                    $('#page1').addClass('hiddenpage');
                    $('#page3').removeClass('hiddenpage');
                    $('#page3').addClass('page');
                    currentPage = 3;
                }
                else if (currentPage == 2) {
                    $('#page2').removeClass('page');
                    $('#page2').addClass('hiddenpage');
                    $('#page1').removeClass('hiddenpage');
                    $('#page1').addClass('page');
                    currentPage = 1;
                }
                else {
                    $('#page3').removeClass('page');
                    $('#page3').addClass('hiddenpage');
                    $('#page2').removeClass('hiddenpage');
                    $('#page2').addClass('page');
                    currentPage = 2;
                }
                event.preventDefault();
            });

            $("#Cancel").click(function(e) {
                window.location = "/Company/Themes";
            });
            
            $("#Save").click(function(event) {
    	       return true;
            });

            <% if(update==true ) { %>        
                $("#Delete").click(function(event) {
                    $("#mode").val("D");
                    $("#form1").submit();
                });
            <% } %>
        });

    </script>
    
    <form id="form1" action="/Company/ThemeAction" method="post">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Theme</legend>
            <label for="themename">Theme Name:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=theme.Name%>" />
            <input name="themename" id="themename" type="hidden" value="<%=theme.Name%>" />
            <%}else{%>    
            <input name="themename" id="themename" type="text" maxlength="25" value="" />
            <%}%>
            <div id="page1" class="page">
                <br/>
                <label for="css1">CSS 1:</label>
                <input name="css1" id="css1" type="text" maxlength="200" maxlength="60" value="<%=update?theme.CSS1:""%>"/>
                <br />
                <label for="css2">CSS 2:</label>
                <input name="css2"  id="css2" type="text" maxlength="60" value="<%=update?theme.CSS2:""%>"/>
                <br />
                <label for="css3">CSS 3:</label>
                <input name="css3"  id="css3" type="text" maxlength="60" value="<%=update?theme.CSS3:""%>"/>
                <br />
                <label for="css4">CSS 4:</label>
                <input name="css4"  id="css4" type="text" maxlength="60" value="<%=update?theme.CSS4:""%>"/>
                <br />
                <label for="css5">CSS 5:</label>
                <input name="css5"  id="css5" type="text" maxlength="60" value="<%=update?theme.CSS5:""%>"/>
                <br />
                <label for="image1">Image 1:</label>
                <input name="image1" type="text" maxlength="40" value="<%=update?theme.Image1:""%>"/>
                <br />
                <label for="image2">Image 2:</label>
                <input name="image2" type="text" maxlength="40" value="<%=update?theme.Image2:""%>"/>
                <br />
                <label for="image3">Image 3:</label>
                <input name="image3" type="text" maxlength="40" value="<%=update?theme.Image3:""%>"/>
                <br />
                <label for="image4">Image 4:</label>
                <input name="image4" type="text" maxlength="40" value="<%=update?theme.Image4:""%>"/>
                <br />
                <label for="image5">Image 5:</label>
                <input name="image5" type="text" maxlength="40" value="<%=update?theme.Image5:""%>"/>
                <br />
                <label for="heading1">Heading 1:</label>
                <input name="heading1" type="text" maxlength="40" value="<%=update?theme.Heading1:""%>"/>
                <br />
                <label for="heading2">Heading 2:</label>
                <input name="heading2" type="text" maxlength="40" value="<%=update?theme.Heading2:""%>"/>
                <br />
                <label for="heading3">Heading 3:</label>
                <input name="heading3" type="text" maxlength="40" value="<%=update?theme.Heading3:""%>"/>
                <br />
                <label for="heading4">Heading 4:</label>
                <input name="heading4" type="text" maxlength="40" value="<%=update?theme.Heading4:""%>"/>
                <br />
                <label for="heading5">Heading 5:</label>
                <input name="heading5" type="text" maxlength="40" value="<%=update?theme.Heading5:""%>"/>
            </div>

            <div id="page2" class="hiddenpage">
                <br/>
                <label for="titleinfo">TitleInfo:</label>
                <textarea name="titleinfo" rows="5" cols="70"><%=update?theme.Titleinfo:""%></textarea>
                <br />
                <label for="metacontenttype">MetaContentType:</label>
                <textarea name="metacontenttype" rows="5" cols="70"><%=update?theme.Metacontenttype:""%></textarea>
                <br />
                <label for="metakeywords">MetaKeywords:</label>
                <textarea name="metakeywords" rows="5" cols="70"><%=update?theme.Metakeywords:""%></textarea>
                <br />
                <label for="metadescription">MetaDescription:</label>
                <textarea name="metadescription" rows="10" cols="70"><%=update?theme.Metadescription:""%></textarea>
                <br />
            </div>
            
            <div id="page3" class="hiddenpage">
                <label for="mostpopularcount">Most Popular Count:</label>
                <input name="mostpopularcount" type="text" maxlength="5" value="<%=update?theme.Mostpopularcount.ToString():""%>"/>
                <br /><span class="smallnote">(Number of products displayed on the 'Most Popular' pane)</span>
                <br />
                <label for="searchresultcol">Result Columns:</label>
                <input name="searchresultcol" type="text" maxlength="5" value="<%=update?theme.Searchresultcol.ToString():""%>"/>
                <br /><span class="smallnote">(Number of Columns of products displayed on a single line for the search results page)</span>
                <br />
                <label for="nowrapsearchresultrow">Search Result Rows:</label>
                <input name="searchresultrow" type="text" maxlength="5" value="<%=update?theme.Searchresultrow.ToString():""%>"/>
                <br /><span class="smallnote">(Number of Rows of products displayed on a single line for the search results page)</span>
                <br />
                <label for="featuredproductcount">Featured Product Count:</label>
                <input name="featuredproductcount" type="text" maxlength="5" value="<%=update?theme.Featuredproductcount.ToString():""%>"/>
                <br /><span class="smallnote">(Number of products displayed on a line in a 'Featured Product' grouping)</span>
                <br />
            </div>   
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
        <%if(update==true){%>
            <li><input class="button" type="submit" value="Update" id="Save" /></li>
            <li><input class="button" type="button" value="Delete" id="Delete" /></li>
        <% }else {%>
            <li><input class="button" type="button" value="Insert" id="Save" /></li>
        <%}%>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
