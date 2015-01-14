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
            createIntegerField(document.getElementById("sortorder"));
            createNumericField(document.getElementById("startPrice"), 2);
            createNumericField(document.getElementById("endPrice"), 2);
        })
        
        function OnClickCancel() {
            window.location = "/Products/Coupons";
        }
        
        function OnClick() {
            if (document.form1.code.value.length == 0) {
                alert("Invalid Code!");
                document.form1.code.focus();
                return;
            }

            if (document.form1.description.value.length == 0) {
                alert("Invalid Description!");
                document.form1.description.focus();
                return;
            }

            if (document.form1.discounttype.value == 0) {
                alert("Invalid Discount Type!");
                document.form1.discounttype.focus();
                return;
            }

            if (document.form1.discounttype.value != 3) {
                if (document.form1.discount.value.length == 0) {
                    alert("Invalid Discount!");
                    document.form1.discount.focus();
                    return;
                }
            }

            if (document.form1.expirationdate.value.length == 0) {
                alert("Invalid Expiration Date!");
                document.form1.expirationdate.focus();
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

    <form name="form1" action="/Products/CategoryAction" method="post">
    <input name="mode" type="hidden" value="<%=(update==true?"U":"I")%>" />
    <div class="frame" id="input-screen">
        <fieldset>
            <legend><%= update == true ? "Update/Delete " : "Add "%>Coupon</legend>
            <label for="code">Code:</label>
            <%if(update){%>
            <input id="code" type="text" readonly="readonly" value="<%=coupon.Code%>" />
            <input name="code" type="hidden" value="<%=coupon.Code%>" />
            <%}else{%>    
            <input name="code" id="code" type="text" value="" />
            <%}%>
            <br />
            <label for="description">Description:</label>
            <input name="description" type="text"/>
            <br />
            <label for="manufacturer">Manufacturer</label> 
            <select name="manufacturer" id="manufacturer" onchange="OnChangeManufacturerItemNumber()">
                <option value="0">< select ></option>
                <%foreach (Manufacturer manufacturer in ViewData.Model.manufacturers){%>
                <option value="<%=manufacturer.Name%>"
                        <%=update && coupon.Manufacturer == manufacturer.Name?"selected='selected'":""%>>
                        <%=manufacturer.Name%>
                </option>
                <%}%>
            </select>
            <br />
            <label for="quantitylimit">Quantity Limit:</label>
            <input name="quantitylimit" type="text" maxlength="4" <%=update?string.Format("{0:#}", coupon.QuantityLimit):""%>/>
            <br />
            <label for="quantityrequired">Quantity Required:</label>
            <input name="quantityrequired" type="text" maxlength="4" <%=update?string.Format("{0:#}", coupon.QuantityRequired):""%> />
            <br />
            <label for="minimumprice">Minimum Price:</label>
            <input name="minimumprice" type="text" maxlength="5" <%=update?string.Format("{0:#,#.00}", coupon.PriceMinimum):""%> />
            <br />
            <label for="discounttype">Discount Type:</label>
            <select name="discounttype">
                <option value="0" <%=!update||coupon.DiscountType==0?"selected='selected'":""%>>< select ></option>
                <option value="1" <%=update&&coupon.DiscountType==1?"selected='selected'":""%>>Dollar Amount</option>
                <option value="2" <%=update&&coupon.DiscountType==2?"selected='selected'":""%>>Percentage</option>
                <option value="3" <%=update&&coupon.DiscountType==3?"selected='selected'":""%>>Free Shipping</option>
            </select>
            <br />
            <label for="">Discount:</label>
            <input name="discount" type="text" maxlength="5"/>
            <span>(i.e. 0.1 = 10%)</span>
            <br />
            <label for="">Precludes Others:</label>
            <input class="radioStyle" type="checkbox" name="precludes" <%=update&&coupon.Precludes>0?"checked='checked'":""%>/>
            <br />
            <label for="">Single Use:</label>
            <input class="radioStyle" type="checkbox" name="singleuse" <%=update&&coupon.SingleUse>0?"checked='checked'":""%>/>
            <br />
            <label for="">Display On Web:</label>
            <input class="radioStyle" type="checkbox" name="displayonweb" <%=update&&coupon.Display>0?"checked='checked'":""%>/>
            <br />
            <label for="">Expiration Date:</label>
            <input name="expirationdate" type="text" maxlength="15"/>
            <br />
        </fieldset>
        <div class="actions">
        <ul>
        <%if(update==true){%>
            <li><input class="button" type="button" value="Update" onclick="OnClick()" /></li>
            <li><input class="button" type="button" value="Delete" onclick="OnClickDelete()" /></li>
        <%}else{%>
            <li><input class="button" type="button" value="Insert" onclick="OnClick()" /></li>
        <%}%>
            <li><input class="button" type="button" value="Cancel" onclick="OnClickCancel()" /></li>
        </ul>
        </div>
    </div>
    </form>
</asp:Content>
