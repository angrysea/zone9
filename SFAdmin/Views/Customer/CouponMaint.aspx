<%@ Page MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<CouponViewData>" %>
<%@ Import Namespace="StorefrontModel" %>
<%@ Import Namespace="SFAdmin.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        Coupon coupon = ViewData.Model.coupon;
        bool update = ViewData.Model.update;
    %>
    
    <script language="javascript" type="text/javascript">
    
        $(document).ready(function(){
            createIntegerField(document.getElementById("quantitylimit"));
            createIntegerField(document.getElementById("quantityrequired"), 2);
            createNumericField(document.getElementById("minimumprice"), 2);
            <%if(update){%>
            $("#description").focus();
            <%}else{%>    
            $("#code").focus();
            <%}%>    

            $("#Cancel").click(function(event) {
                window.location = "/Customer/Coupons";
            });
        
            $("#Save").click(function(e) {
                if ($("#code").val().length == 0) {
                    alert("Invalid Code!");
                    $("#code").focus();
                    e.preventDefault();
                    return;
                }

                if ($("#description").val().length == 0) {
                    alert("Invalid Description!");
                    $("#description").focus();
                    e.preventDefault();
                    return;
                }

                if ($("#discounttype").val().length == 0) {
                    alert("Invalid Discount Type!");
                    $("#discounttype").focus();
                    e.preventDefault();
                    return;
                }

                if ($("#discounttype").val().length != 3) {
                    if ($("#discount").val().length == 0) {
                        alert("Invalid Discount!");
                        $("#discount").focus();
                        e.preventDefault();
                        return;
                    }
                }

                if ($("#expirationdate").val().length == 0) {
                    alert("Invalid Expiration Date!");
                    $("#expirationdate").focus();
                     e.preventDefault();
                    return;
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

    <form name="form1" action="/Customer/CouponAction" method="post">
    <input name="mode" id="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <%IList<string> errors = ViewData.Model.errors;
              if (errors != null) {%>
            <div>
                <ul class="error">
                <% foreach (string error in errors) { %>
                    <li><%= Html.Encode(error) %></li>
                <%}%>
                </ul>
            </div>
            <%}%>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Coupon</legend>
            <label for="code">Code:</label>
            <%if(update){%>
            <input type="text" readonly="readonly" value="<%=coupon.Code%>" />
            <input name="code" id="code" type="hidden" value="<%=coupon.Code%>" />
            <%}else{%>    
            <input name="code" id="code" type="text" value="" />
            <%}%>
            <br />
            <label for="description">Description:</label>
            <input name="description" id="description" type="text" value="<%=update?coupon.Description:""%>"/>
            <br />
            <label for="productid">Product ID:</label>
            <input name="productid" id="productid" type="text" value="<%=update?coupon.Product:""%>"/>
            <br />
            <label for="manufacturer">Manufacturer</label> 
            <select name="manufacturer" id="manufacturer">
                <option value="0">(select)</option>
                <%foreach (Manufacturer manufacturer in ViewData.Model.manufacturers){%>
                <option value="<%=manufacturer.Name%>"
                            <%if(update && coupon.Manufacturer==manufacturer.Name){%>
                                selected="selected"
                            <%}%>>
                        <%=manufacturer.Name%>
                </option>
                <%}%>
            </select>
            <br />
            <label for="category">Category</label> 
            <select name="category" id="category">
                <option value="0">(select)</option>
                <%foreach (Category category in ViewData.Model.categories)
                  {%>
                <option value="<%=category.Name%>"
                            <%if(update && coupon.Category==category.Name){%>
                                selected="selected"
                            <%}%>>
                        <%=category.Name%>
                </option>
                <%}%>
            </select>
            <br />
            <label for="quantitylimit">Quantity Limit:</label>
            <input name="quantitylimit" id="quantitylimit" type="text" maxlength="4" value="<%=update?string.Format("{0:#}", coupon.QuantityLimit):""%>"/>
            <br />
            <label for="quantityrequired">Quantity Required:</label>
            <input name="quantityrequired" id="quantityrequired" type="text" maxlength="4" value="<%=update?string.Format("{0:#}", coupon.QuantityRequired):""%>" />
            <br />
            <label for="minimumprice">Minimum Price:</label>
            <input name="minimumprice" id="minimumprice" type="text" maxlength="5" value="<%=update?string.Format("{0:#,#.00}", coupon.PriceMinimum):""%>" />
            <br />
            <label for="discounttype">Discount Type:</label>
            <select name="discounttype" id="discounttype">
                <option value="0" <%=!update||coupon.DiscountType==0?"selected='selected'":""%>>(select)</option>
                <option value="1" <%=update&&coupon.DiscountType==1?"selected='selected'":""%>>Dollar Amount</option>
                <option value="2" <%=update&&coupon.DiscountType==2?"selected='selected'":""%>>Percentage</option>
                <option value="3" <%=update&&coupon.DiscountType==3?"selected='selected'":""%>>Free Shipping</option>
            </select>
            <br />
            <label for="discount">Discount:</label>
            <input name="discount" id="discount" type="text" maxlength="5" value="<%=update?string.Format("{0:#.00}", coupon.Discount):""%>"/>
            <span>(i.e. 0.1 = 10%)</span>
            <br />
            <label for="precludes">Precludes Others:</label>
            <input name="precludes" id="precludes" class="radioStyle" type="checkbox" <%=update&&coupon.Precludes>0?"checked='checked'":""%>/>
            <br />
            <label for="singleuse">Single Use:</label>
            <input name="singleuse" id="singleuse" class="radioStyle" type="checkbox" <%=update&&coupon.SingleUse>0?"checked='checked'":""%>/>
            <br />
            <label for="oneperhousehold">One per Household:</label>
            <input name="oneperhousehold" id="oneperhousehold" class="radioStyle" type="checkbox" <%=update&&coupon.OneperHousehold>0?"checked='checked'":""%>/>
            <br />
            <label for="displayonweb">Display On Web:</label>
            <input name="displayonweb" id="displayonweb" class="radioStyle" type="checkbox" name="displayonweb" <%=update&&coupon.Display>0?"checked='checked'":""%>/>
            <br />
            <label for="imageurl">Image URL:</label>
            <input name="imageurl" id="imageurl" type="text" maxlength="150" value="<%=update?coupon.Imageurl:""%>"/>
            <br />
            <label for="">Expiration Date:</label>
            <input name="expirationdate" id="expirationdate" type="text" maxlength="15" value="<%=update?string.Format("{0:MM/dd/yyyy}", coupon.ExpirationDate):""%>"/>
            <br />
        </fieldset>
        <div class="actions">
        <ul>
        <%if(update==true){%>
            <li><input class="button" type="submit" value="Update" id="Save" /></li>
            <li><input class="button" type="button" value="Delete" id="Delete" /></li>
        <%}else{%>
            <li><input class="button" type="submit" value="Insert" id="Save" /></li>
        <%}%>
            <li><input class="button" type="button" value="Cancel" id="Cancel" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
