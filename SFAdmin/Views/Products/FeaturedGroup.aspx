<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<FeaturedProductViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        bool updating = ViewData.Model.update;
        FeaturedGroup featuredGroup = ViewData.Model.featuredGroup;
    %>
    
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            createIntegerField(document.getElementById("sortorder"));

            $("#manufacturer").change(function() {
                var selBox = $("#manufacturer")[0];
                var selection = selBox.value;
                $("#products").load("/Products/GetListProducts", { p: selection });
            });

            $("#Cancel").click(function(e) {
                window.location = "/Products/FeaturedGroups";
            });
            
            $("#Save").click(function(e) {
            
                <%if(!updating){%>
                    if($("#name").val().length == 0) {
                        alert("A Name must be entered!");
                        $("#name").focus();
                        e.preventDefault();
                        return;
                    }
                <%}%>    

                if($("#heading").val().length == 0) {
                    alert("A heading must be entered!");
                    $("#heading").focus();
                    e.preventDefault();
                    return;
                }

                if($("#comments").val().length == 0) {
                    alert("Comments must be entered!");
                    $("#comments").focus();
                    e.preventDefault();
                    return;
                }

                var valid = '0123456789';
                if($("#sortorder").val().length == 0) {
                    alert("An invalid sortorder was entered.");
                    $("#sortorder").focus();
                    e.preventDefault();
                    return;
                }
            });
            
            <%if(updating){%>
                $("#heading").focus()
            <%}else{%>    
                $("#name").focus()
            <%}%>

        });
    
    </script>

    <form name="form1" action="/Products/FeaturedProductAction" method="post" onload="OnLoadSpecification()">
    <input name="mode" type="hidden" value="<%=updating?"U":"I"%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%= updating == true ? "Update/Delete " : "Add "%>Featured Product</legend>
            <br />
            <label for="name">Name:</label>
            <%if (updating){%>
                <input type="text" readonly="readonly" value="<%=featuredGroup.Name%>" />
                <input id="name" name="name" type="hidden" value="<%=featuredGroup.Name%>" />
            <%}else{%>    
                <input name="name" id="name" type="text" value="" />
            <%}%>
            <label for="heading">Heading:</label>
            <input name="heading" id="heading" type="text" size="40" value="<%=updating?featuredGroup.Heading:""%>"/>
            <br />
            <label for="comments">Comments:</label>
            <textarea name="comments" id="comments" rows="10" cols="70"><%=updating?featuredGroup.Comments:""%></textarea>
            <br />
            <label for="sortorder">Sort Order:</label>
            <input name="sortorder" id="sortorder" type="text" size="5" value="<%=updating?string.Format("{0:#,#}", featuredGroup.Sortorder):""%>"/>
            <br />
            <label for="active">Active:</label>
            <input name="active" id="active" type="checkbox" class="radioStyle" <%if(!updating||featuredGroup.Active>0){%>checked="checked"<%}%> />
            <br />
            <br />
            <table class="lists">
            <%if (updating == true && ViewData.Model.featuredProducts.Count > 0) {
                  foreach (ListProduct featured in ViewData.Model.featuredProducts){%>
                <tr>
                    <th>Delete</th>
                    <th>Product No</th>
                    <th>Product Name</th>
                </tr>
                <tr>
                <td>
                    <input class="radioStyle" type="checkbox" name="featured<%=featured.ProductNo%>"/>
                </td>
                <td><%=featured.ProductNo%></td>
                <td> <%=featured.Name%> </td>
                <td>
                    <img src="/Content/images/products/<%=featured.ImageURLSmall%>" border="0"/>
                </td>
                </tr>
            <%}
              }%>
            </table>
            
            <label for="manufacturer">Manufacturer:</label>
            <select name="manufacturer" id="manufacturer">
                <option value="">< Select ></option>
                <option 
                value="all"
                    <%if (ViewData.Model.manufacturer == "all"){%>selected="selected"<%}%>>
                    All
                </option>
                <%foreach(Manufacturer manufacturer in ViewData.Model.manufacturers) {%>
                <option 
                    <%if (ViewData.Model.manufacturer == manufacturer.Name){%>
                    selected="selected"
                    <%}%>
                    value="<%=manufacturer.Name%>">
                    <%=manufacturer.Name%>
                </option>
                <%}%>
            </select>
            <br />
            <div id="products">
            </div>
        </fieldset>
        <div class="actions">
        <ul>
        <%if (updating == true){%>
            <li><input class="button" type="submit" value="Update" id="Save" /></li>
            <li><input class="button" type="button" value="Delete" id="Delete" /></li>
        <% }else {%>
            <li><input class="button" type="submit" value="Insert" id="Save" /></li>
        <%}%>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
