
function initNavigation(href) {
    $("div.navigation li a").each(function(i) {
        if (this.href.indexOf(href) > 0) {
            $(this).parent().addClass('active');
        }
    });
    
    $("div.navigation li a").click(function(e) {
        $("div.navigation li.active").removeClass('active');
        $(this).parent().addClass('active');
    });
}

function checkEmail(emailaddress) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(emailaddress))
    {
        return (true)
    }
    alert("Invalid E-mail Address! Please re-enter.")
    return (false)
}

function isValid(string,allowed) {
    for (var i=0; i< string.length; i++)
    {
        if (allowed.indexOf(string.charAt(i)) == -1)
            return false;
    }
    return true;
}

//Test Amex - 373273418892180
var g_cards = new Array();
g_cards[0] = { cardName: "Visa", lengths: "13,16", prefixes: "4", checkdigit: true };
g_cards[1] = { cardName: "MasterCard", lengths: "16", prefixes: "51,52,53,54,55", checkdigit: true };
g_cards[2] = { cardName: "AmEx", lengths: "15", prefixes: "34,37", checkdigit: true };
g_cards[3] = { cardName: "Discover", lengths: "16", prefixes: "6011,650", checkdigit: true };
g_cards[4] = { cardName: "DinersClub", lengths: "14,16", prefixes: "300,301,302,303,304,305,36,38,55", checkdigit: true };
//g_cards[5] = { cardName: "CarteBlanche", lengths: "14", prefixes: "300,301,302,303,304,305,36,38", checkdigit: true };
//g_cards[6] = { cardName: "JCB", lengths: "15,16", prefixes: "3,1800,2131", checkdigit: true };
//g_cards[7] = { cardName: "enRoute", lengths: "15", prefixes: "2014,2149", checkdigit: true };
//g_cards[8] = { cardName: "Solo", lengths: "16,18,19", prefixes: "6334, 6767", checkdigit: true };
//g_cards[9] = { cardName: "Switch", lengths: "16,18,19", prefixes: "4903,4905,4911,4936,564182,633110,6333,6759", checkdigit: true };
//g_cards[10] = { cardName: "Maestro", lengths: "16,18", prefixes: "5020,6", checkdigit: true };
//g_cards[11] = { cardName: "VisaElectron", lengths: "16", prefixes: "417500,4917,4913", checkdigit: true };


function ValidateCC(cardNo, cardName) {

    var cardType = -1;
    for (var i=0; i<g_cards.length; i++) {
        if (cardName.toLowerCase() == g_cards[i].cardName.toLowerCase()) {
	        cardType = i;
	        break;
        }
    }
    
    if (cardType == -1) {
        return false; 
    }

    cardNo = cardNo.replace(/[\s-]/g, "");
    if (cardNo.length == 0) {
        return false; 
    }

    var cardexp = /^[0-9]{13,19}$/;
    if (!cardexp.exec(cardNo)) {
        return false; 
    }
	
    cardNo = cardNo.replace(/\D/g, "");

    if (g_cards[cardType].checkdigit) {
        if (!checkDigit(cardNo)) {
            return false;
        }
    }  

    var lengthValid = false;
    var prefixValid = false; 
    var prefix = new Array ();
    var lengths = new Array ();

    prefix = g_cards[cardType].prefixes.split(",");
    for (i=0; i<prefix.length; i++) {
        var exp = new RegExp ("^" + prefix[i]);
        if (exp.test (cardNo)) prefixValid = true;
    }
    if (!prefixValid) {
        return false; 
    }

    lengths = g_cards[cardType].lengths.split(",");
    for (j=0; j<lengths.length; j++) {
        if (cardNo.length == lengths[j]) lengthValid = true;
    }
    
    if (!lengthValid) {
        return false; 
    }

    return true;
}

function getCCType(cardNo) {
    
    var type = null;
    cardNo = cardNo.replace(/[\s-]/g, "");
    if (cardNo.length > 0) {
        var cardexp = /^[0-9]{13,19}$/;
        if (cardexp.exec(cardNo)) {
            cardNo = cardNo.replace(/\D/g, "");
            for (var h = 0; h < g_cards.length; h++) {
                var prefix = g_cards[h].prefixes.split(",");
                for (i = 0; i < prefix.length; i++) {
                    var exp = new RegExp("^" + prefix[i]);
                    if (exp.test(cardNo)) {
                        var lengths = g_cards[h].lengths.split(",");
                        for (j = 0; j < lengths.length; j++) {
                            if (cardNo.length == lengths[j]) {
                                type = g_cards[h].cardName;
                                break;
                            }
                        }
                        break;
                    }
                }
                if (type != null)
                    break;
            }
        }
    }
    return type;
}

function checkDigit(cardNo) {
    var checksum = 0;
    var mychar = "";
    var j = 1;

    var calc;
    for (i = cardNo.length - 1; i >= 0; i--) {
        calc = Number(cardNo.charAt(i)) * j;
        if (calc > 9) {
            checksum = checksum + 1;
            calc = calc - 10;
        }
        checksum = checksum + calc;
        if (j == 1) { j = 2 } else { j = 1 };
    }

    if (checksum % 10 != 0) {
        return false;
    }
    return true;
}

function confirm(message, callback) {
    $('#confirm').modal({
        close: false,
        position: ["20%", ],
        overlayId: 'confirmModalOverlay',
        containerId: 'confirmModalContainer',
        onShow: function(dialog) {
            dialog.data.find('.message').append(message);
            dialog.data.find('.yes').click(function() {
                if ($.isFunction(callback)) {
                    callback.apply();
                }
                $.modal.close();
            });
        }
    });
}

function ValidateCreditCard() {
    if ($.trim($("#cardholder").val()).length == 0) {
        alert("Card Holder is required!");
        $("#cardholder").focus();
        return false;
    }

    if ($.trim($("#cardnumber").val()).length == 0) {
        alert("Card Number is required!");
        $("#cardnumber").focus();
        return false;
    }

    if ($.trim($("#cctype").val()).length == 0) {
        alert("Invalid Card Number!");
        $("#cardnumber").focus();
        return false;
    }

    if ($("#expirationmonth").val() == "0") {
        alert("Expiration Month is required!");
        $("#expirationmonth").focus();
        return false;
    }

    if ($("#expirationyear").val() == "0") {
        alert("Expiration Year is required!");
        $("#expirationyear").focus();
        return false;
    }
    return true;
}

function ValidateAddress() {

    fullname = $("#fullname");
    if ($.trim(fullname.val()).length == 0) {
        alert("Name is required!");
        fullname.focus();
        return false;
    }

    address1 = $("#address1");
    if ($.trim(address1.val()).length == 0) {
        alert("Address 1 is required!");
        address1.focus();
        return false;
    }

    city = $("#city");
    if ($.trim(city.val()).length == 0) {
        alert("City is required!");
        city.focus();
        return false;
    }

    country = $("#country");
    if (country.val() == "US") {
        state = $("#state");
        if ($.trim(state.val()).length == 0) {
            alert("A state must be selected!");
            state.focus();
            return false;
        }
    }

    zipcode = $("#zipcode");
    if ($.trim(zipcode.val()).length == 0) {
        alert("Zip/Postal Code is required!");
        zipcode.focus();
        return false;
    }

    phone = $("#phone");
    if ($.trim(phone.val()).length < 10) {
        alert("A valid phone number is required!");
        phone.focus();
        return false;
    }
    return true;
}    
    
