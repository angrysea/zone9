<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<ProductViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="/Scripts/fileupload.js" type="text/javascript"></script>
    <script src="/Scripts/lytebox.js" type="text/javascript"></script>

    <%
        Product product = ViewData.Model.product;
        Details details = ViewData.Model.details;
        bool updating = ViewData.Model.update;
        string nextProduct = ViewData.Model.nextProduct;
        string prevProduct = ViewData.Model.prevProduct;
    %>


    <script language="javascript" type="text/javascript">
        var currentPage = 1;
        var maxPage = 5;
        
        $(document).ready(function(){
            $("#page1").removeClass("hiddenpage");
            $("#page1").addClass("page");
            initLytebox();
            $("#next").click(function(e) {
                var current = '#page' + currentPage;
                if(currentPage==maxPage)
                    currentPage = 1;
                else
                    currentPage++;
                var next = '#page' + currentPage;
                $(current).removeClass('page');
                $(current).addClass('hiddenpage');
                $(next).removeClass('hiddenpage');
                $(next).addClass('page');
                e.preventDefault();
            });

            $("#prev").click(function(e) {
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
                e.preventDefault();
            });

            $("#buttonUpload").click(function(e) {
                return smallImageUpload();
            });

            $("#Cancel").click(function(e) {
                window.location = "/Home/Index";
            });
    
            $("#Save").click(function(e) {
                <%if(updating==false){%>        
                    if ($("#manufacturer").val() == 0) {
                        alert("A manufacturer must be selected!");
                        $("#manufacturer").focus();
                         e.preventDefault();
                        return false;
                    }

                    if ($("#manufacturerproductNo").val().length == 0) {
                        alert("Invalid Manufacturer Product #!");
                        $("#manufacturerproductNo").focus();
                         e.preventDefault();
                        return false;
                    }
                <%}%>
                
                if ($("#productname").val().length == 0) {
                    alert("Invalid Product Name!");
                    $("#productname").focus();
                     e.preventDefault();
                    return false;
                }

                if ($("#listprice").val().length == 0) {
                    alert("Invalid List Price!");
                    $("#listprice").focus();
                     e.preventDefault();
                    return false;
                }

                if ($("#ourcost").val().length == 0) {
                    alert("Invalid Our Cost!");
                    $("#ourcost").focus();
                     e.preventDefault();
                    return false;
                }

                if ($("#ourprice").val().length == 0) {
                    alert("Invalid Our Price!");
                    $("#ourprice").focus();
                     e.preventDefault();
                    return false;
                }

                if ($("#shippingweight").val().length == 0) {
                    alert("Invalid Shipping Weight!");
                    $("#shippingweight").focus();
                     e.preventDefault();
                    return;
                }

                if ($("#description").val().length == 0) {
                    alert("Invalid Description!");
                    $("#description").focus();
                     e.preventDefault();
                    return;
                }

                if ($("#status").val().length == 0) {
                    alert("A status must be selected!");
                    $("#status").focus();
                     e.preventDefault();
                    return false;
                }
                $("#form1").submit();
            });
        
            <%if(updating==true ){%>        
                $("#Delete").click(function(event) {
                    $("#mode").val("D");
                    $("#form1").submit();
                });
                $("#gtin").focus();
            <%} else {%>
                $("#manufacturer").change(function(e) {
                    OnChangeManufacturerProductNo();
                });

                $("#manufacturerproductNo").change(function(e) {
                    OnChangeManufacturerProductNo();
                });
                $("#manufacturer").focus();
            <%}%>
            
            createNumericField(document.getElementById("shippingweight"), 2);
            createIntegerField(document.getElementById("height"));
            createIntegerField(document.getElementById("length"));
            createIntegerField(document.getElementById("Width"));
            createNumericField(document.getElementById("listprice"), 2);
            createNumericField(document.getElementById("ourcost"), 2);
            createNumericField(document.getElementById("ourprice"), 2);
            createNumericField(document.getElementById("handlingfee"), 2);

        });
        
        function OnChangeManufacturerProductNo() {
            var productno = $("#productno");
            var newValue = null;
            var manufacturer = $("#manufacturer");
            if(manufacturer!=null)
            {
                var manufacturerVal = manufacturer.val();
                <%foreach(Manufacturer manufacturer in ViewData.Model.manufacturers){%>
                if(manufacturerVal == "<%=manufacturer.Name%>") 
                {
                    var productNo = $("#manufacturerproductNo").val();
                    if(productNo!=null&&productNo.length>0)
                        productNo=productNo.replace(/^\s+|\s+$/g, '');
                    var newValue = "<%=manufacturer.Prefix%>" + "-" + productNo;
                    productno.val(newValue);
                }
                <%}%>
            }
            
            if(newValue!=null)
            {
                $("#smallimageurl").attr("src", "/Content/images/products/" + newValue + "small.gif");
                $("#largelink").attr("href", "/Content/images/products/" + newValue + "large.gif");
                
                $("#smallimagetxt").val(newValue + "small.gif");
                $("#mediumimagetxt").val(newValue + "medium.gif");
                $("#largeimagetxt").val(newValue + "large.gif");

                $("#small").val(newValue + "small.gif");
                $("#medium").val(newValue + "medium.gif");
                $("#large").val(newValue + "large.gif");
                
                $("#imageurlsmall").attr("src", "/Content/images/products/" + newValue + "small.gif");
                $("#imageurlmedium").attr("src", "/Content/images/products/" + newValue + "medium.gif");
                $("#imageurllarge").attr("src", "/Content/images/products/" + newValue + "large.gif");
            }
        }
        
    </script>
    <form id="form1" action="/Products/ProductAction" method="post">
    <input name="mode" id="mode" type="hidden" value="<%=(updating==true?"U":"I")%>" />
    <input name="nCategories" type="hidden" value="<%=ViewData.Model.nCategories%>" />
    <input name="nSpecifications" type="hidden" value="<%=ViewData.Model.nSpecifications%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%=updating==true?"Update/Delete " : "Add "%>Product</legend>
            <%if(updating){%>
                <ul class="navigation" id="topbar">
                    <%if(prevProduct != null && prevProduct.Length>0){%>
                    <li class="left">
                        <a href="/Products/ProductMaint/<%=prevProduct%>" >
                            <img src="/Content/images/arrowleft.gif" style="border-style: none" /><%=prevProduct%>
                        </a>
                    </li>
                    <%}%>
                    <%if(nextProduct != null && nextProduct.Length>0){%>
                    <li class="right">
                        <a href="/Products/ProductMaint/<%=nextProduct%>"><%=nextProduct%>
                            <img src="/Content/images/arrowright.gif" style="border-style: none" />
                        </a>
                    </li>
                    <%}%>
                </ul>            
            <%}%>
            <div class="page" id="page1">
                <%if (updating && details.ImageURLSmall != null && details.ImageURLSmall.Length > 0){%>
                <div class="product">
                    <%if (details.ImageURLLarge != null && details.ImageURLLarge.Length > 0){%>
                    <a id="largelink" href="/Content/images/products/<%=details.ImageURLLarge%>" rel="lytebox" title="<%=details.ImageURLLarge%>">
                        <img id="smallimageurl" border="0" alt="" src="/Content/images/products/<%=details.ImageURLSmall%>" />
                    </a>
                    <%}else{%>
                        <img  id="smallimageurl" border="0" alt="" src="/Content/images/products/<%=details.ImageURLSmall%>" />
                    <%}%>
                </div>
                <%}%>
                <label for="manufacturer">Manufacturer</label> 
                <%if (updating) {%>
                <input type="text" name="manufacturer" readonly="readonly" value="<%=product.Manufacturer%>" />
                <input type="hidden" name="manufacturer" value="<%=product.Manufacturer%>" />
                <label for="manufacturerproductNo">Product #</label>
                <input name="manufacturerproductNo" type="text" readonly="readonly" value="<%=details.ManufacturerProduct%>"/>
                <input name="manufacturerproductNo" type="hidden" value="<%=details.ManufacturerProduct%>"/>
                <%} else {%>
                <select name="manufacturer" id="manufacturer">
                    <option value="0">(select)</option>
                    <%foreach (Manufacturer manufacturer in ViewData.Model.manufacturers)
                      {%>
                    <option value="<%=manufacturer.Name%>"
                            <%if(updating && product.Manufacturer==manufacturer.Name){%>
                                selected="selected"
                            <%}%>>
                            <%=manufacturer.Name%>
                    </option>
                    <%}%>
                </select>
                <br/>
                <label for="manufacturerproductNo">Product #</label>
                <input name="manufacturerproductNo" id="manufacturerproductNo" type="text" maxlength="20" value="<%=updating?details.ManufacturerProduct:""%>" />
                <%} %>
                
                <br/>
                <label for="productno">Product No:</label>
                <input readonly="readonly" name="productno" id="productno" type="text" maxlength="10" value="<%=updating?product.ProductNo:""%>" />
                <br/>
                <br/>
                <label for="gtin">GTIN:</label>
                <input name="gtin" id="gtin" type="text" maxlength="14" value="<%=updating?product.GTIN:""%>"/>
                <label for="productname">Product Name:</label>
                <input name="productname" id="productname" type="text" maxlength="200" value="<%=updating?product.Name:""%>"/>
                <label for="listprice">List Price</label>
                <input name="listprice" id="listprice" type="text" maxlength="10" value="<%=updating?string.Format("{0:#,#.00}", product.ListPrice):""%>"/>
                <label for="ourcost">Our Cost</label>
                <input name="ourcost" id="ourcost" type="text" maxlength="10" value="<%=updating?string.Format("{0:#,#.00}", product.OurCost):""%>" />
                <label for="ourprice">Our Price</label>
                <input name="ourprice" id="ourprice" type="text" maxlength="10" value="<%=updating?string.Format("{0:#,#.00}", product.OurPrice):""%>"/>
                <label for="qty">Current Quantity</label>
                <input name="qty" id="qty" type="text" maxlength="10" value="<%=updating?string.Format("{0:#,0}", product.Quantity):""%>"/>
                <br/>
                <br/>
                <label for="description">Description:</label>
                <textarea name="description" id="description"  rows="5" cols="100"><%=updating?details.Description:""%></textarea>
                <br/>
                <br/>
                <label for="status">Status:</label>
                <select id="status" name="status">
                    <option value="0">(select)</option>
                    <%foreach(Availability availability in ViewData.Model.availabilities){%>
                        <option value="<%=availability.Code%>"
                            <%if(updating && availability.Code==product.Availability){%>
                                selected="selected"
                            <%}%>>
                            <%=availability.Description%>
                        </option>
                    <%}%>
                </select>
            </div>

            <div id="page2" class="hiddenpage">
                <label for="">Distributor</label>
                <select name="distributor" id="distributor" >
                    <option value="0">(select)</option>
                    <%foreach(Distributor distributor in ViewData.Model.distributors){%>
                    <option value="<%=distributor.Name%>"
                        <%if(updating && product.Distributor==distributor.Name){%>
                            selected="selected"
                        <%}%>>
                        <%=distributor.Name%>
                    </option>
                    <%}%>
                </select>
                <br/>
                <label for="">Product #</label>
                <input name="distributorproductNo" id="distributorproductNo"  type="text" maxlength="10" value="<%=updating?details.DistributorProduct:""%>"/>
                <br/>
                <br/>
                <label for="shippingweight">Shipping Weight (lbs):</label>
                <input name="shippingweight" id="shippingweight" type="text" maxlength="10" value="<%=updating?string.Format("{0:#,#.00}", details.ShippingWeight):""%>"/>
                <br/>
                <label for="height">Height:</label>
                <input name="height" id="height" type="text" maxlength="10" value="<%=updating?string.Format("{0:#}", details.Height):""%>"/>
                <br/>
                <label for="length">Length:</label>
                <input name="length" id="length" type="text" maxlength="10" value="<%=updating?string.Format("{0:#}", details.Length):""%>"/>
                <br/>
                <label for="width">Width:</label>
                <input name="width" id="width" type="text" maxlength="10" value="<%=updating?string.Format("{0:#}", details.Width):""%>"/>
                <br/>
                <br/>
                <label for="handlingfee">Handling Fee:</label>
                <input name="handlingfee" id="handlingfee" type="text" maxlength="8" value="<%=updating?string.Format("{0:#,#.00}", details.HandlingCharges):""%>"/>
            </div>
                            
            <div id="page3" class="hiddenpage">
                <table border="0">
                    <tr>
                        <td colspan="3"><span class="title">Specifications</span></td>
                    </tr>
                <%
                    foreach(Specification specification in ViewData.Model.specifications){
                %>
                <tr>
                    <td>
                        <label for="specification<%=specification.Name%>"><%=specification.Name%></label>
                        <input name="specification<%=specification.Name%>" type="text" maxlength="80" 
                            value="<%=ViewData.Model.productSpecifications.ContainsKey(specification.Name)?ViewData.Model.productSpecifications[specification.Name]:""%>"/>
                    </td>
                </tr>
                <%}%>
                </table>
            </div>
                       
            <div id="page4" class="hiddenpage">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td colspan="2"><span class="title">Categories</span></td>
                    </tr>
                    <%foreach(ProductGroup group in ViewData.Model.groups){if(group.grouptype!=null){%>                        
                    <tr>
                        <td colspan="2"><img alt="" src="/Content/images/blank.gif" width="10em" height="1em"/></td>
                    </tr>
                    <tr>
                        <td colspan="2"><%=group.grouptype.Name%></td>
                    </tr>
                    <%
                        int cats = 0;
                        int maxcats = 3;
                        foreach (Category category in group.catagories)
                        {
                            cats++;
                    %>
                        <%if (cats == 1){%>
                        <tr>
                            <td><img alt="" src="/Content/images/blank.gif" width="10em" height="1px"/></td>
                        <%}%>
                            <td>
                                <input class="radioStyle" type="checkbox" name="category<%=category.Name%>" 
                                    <%if(updating){
                                        foreach(ProductCategories productCategory in ViewData.Model.productCategories){
                                            if(category.Name == productCategory.Category){%>
                                    checked="checked"
                                    <%break;}}}%> />
                                <%=category.LongName%>
                            </td>
                        <%if (cats >= maxcats){ 
                              cats=0;%>                            
                        </tr>
                        <%}%>
		            <%}%>
                    <%if (cats == maxcats){%>
                    </tr>
                    <%}%>
                <%}}%>
                </table>
                <br/>
            </div>

            <div id="page5" class="hiddenpage">
		        <form name="uploadfiles" action="/Products/UploadFiles" method="post" enctype="multipart/form-data">
                    <input type="hidden" name="smallimagetxt" id="smallimagetxt" value="<%=updating?details.ImageURLSmall:""%>" />
                    <input type="hidden" name="mediumimagetxt" id="mediumimagetxt" value="<%=updating?details.ImageURLMedium:""%>" />
                    <input type="hidden" name="largeimagetxt" id="largeimagetxt" value="<%=updating?details.ImageURLLarge:""%>" />
                    <label for="smallimagetxt">Small Image:</label>
                    <input type="text" readonly="readonly" id="small" value="<%=updating?details.ImageURLSmall:""%>" />
                    <a id="imageurlsmall" href="<%if(updating){%>/Content/images/products/<%=details.ImageURLSmall%>" rel="lytebox" title="<%=details.ImageURLSmall%>"<%}%>>View</a>
                    <br />
                    <label for="filesmall">Upload Small:</label>
                    <input type="file" id="smallimage" name="smallimage"/>
                    <br/>
                    <br/>
                    <label for="mediumimagetxt">Medium Image:</label>
                    <input type="text" readonly="readonly" id="medium" value="<%=updating?details.ImageURLMedium:""%>" />
                    <a id="imageurlmedium"href="<%if(updating){%>/Content/images/products/<%=details.ImageURLMedium%>" rel="lytebox" title="<%=details.ImageURLMedium%>"<%}%> target="">view</a>
                    <br />
                    <label for="filemedium">Upload Medium:</label>
                    <input type="file" name="mediumimage" id="mediumimage"/>
                    <br/>
                    <br/>
                    <label for="largeimagetxt">Large Image:</label>
                    <input type="text" readonly="readonly" id="large" value="<%=updating?details.ImageURLLarge:""%>" />
                    <a id="imageurllarge"href="<%if(updating){%>/Content/images/products/<%=details.ImageURLLarge%>" rel="lytebox" title="<%=details.ImageURLLarge%>"<%}%> target="">View</a>
                    <br/>
                    <label for="filelarge">Upload Large:</label>
                    <input type="file" name="largeimage" id="largeimage"/>
                    <br/>
                    <br/>
                    <div id="results"></div>
                    <br/>
                    <div class="actions">
                        <ul>
                            <li><input class="button" type="button" value="Upload Now" id="buttonUpload" /></li>
		                    <li><img id="loading" alt="" src="/Content/images/loading.gif" style="display:none;"/></li>
                        </ul>
                    </div>
		        </form>
            </div>

            <ul class="navigation">
                <li class="left">
                    <a id="prev" href="#"><img alt="" src="/Content/images/arrowleft.gif" style="border-style: none" />&nbsp;Prev</a>
                </li>
                <li class="right">
                    <a id="next" href="#">Next&nbsp;<img alt="" src="/Content/images/arrowright.gif" style="border-style: none" /></a>
                </li>
            </ul>       
        </fieldset>
        <div class="actions">
        <ul>
        <%if(updating==true){%>
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
