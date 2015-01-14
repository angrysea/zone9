  var isIE = (navigator.userAgent.toLowerCase().indexOf("msie") > 0) ? true : false;

  var dateTip = "mm/dd/yyyy";

  //Reqular expression to test the entry of each character
  var reValidDateChars = /\d|\/|\.|\-/;
  var reValidAlphaChars = /[a-zA-Z]/;
  var reValidAlphaNumericChars = /[a-zA-Z0-9\ \,\.\&\-]/;
  var reValidNumericChars = /\.|\d|\,/i;
  var reValidFractionChars = /\.|\d|\s|\,|\\|\//i;
  var reValidIntegerChars = /\d|\,/i;
  var reValidLargeIntegerChars = /m|\d|\,|\./i;
  var reShortcutChars = /m/i;
  var reDecoratorChars = /\$|\,|\//g;
  var reKeyboardChars = /[\x00\x03\x08\x0D\x16\x18\x1A]/;
  var reClipboardChars = /[cvxz]/i;

  //Reqular expression to test the validity during entry
  var reInteger = /(^-?\d[\d\,]*$)/;
  var reLargeInteger = /(^-?\d[\d\,]*m{0,2}[\d\,]*$)|(^-?\d\d*\.\d*m{0,2}$)|(^-?\.\d*m{0,2}$)/i;
  var reAlpha = /^[a-zA-Z]*$/;
  var reAlphaNumeric = /^[a-zA-Z0-9][a-zA-Z0-9\ \,\.\&\-]*$/;
  var reNumeric  =  /(^-?\d[\d\,]*m{0,2}\.\d*$)|(^-?\d[\d\,]*\.\d*m{0,2}$)|(^-?\d[\d\,]*m{0,2}$)|(^-?\.\d*m{0,2}$)/i;
  var reFraction  =  /(^-?\d[\d\,]*m{0,2}\.\d*$)|(^-?\d[\d\,]*m{0,2}\s$)|(^-?\d[\d\,]*m{0,2}\s\d+$)|(^-?\d[\d\,]*m{0,2}\s\d+(\/|\\)$)|(^-?\d[\d\,]*m{0,2}\s\d+(\/|\\)\d+$)|(^-?d+(\/|\\)\d+$)|(^-?\d\d*(\/|\\)$)|(^-?\d\d*(\/|\\)\d\d*$)|(^-?\d[\d\,]*m{0,2}$)|(^-?\.\d*$)/i;
  var reDate = /(^\d{0,2}[\-\/\.]\d{0,2}[\-\/\.]\d{0,4}$)|(^\d{0,2}[\-\/\.]\d{0,2}$)|(^\d{1,2}$)/;

  // For future use
  var reStrictDate = /(^\d{1,2}[\-\/\.]\d{1,2}[\-\/\.]\d{0,4}$)|(^\d{1,2}[\-\/\.]\d{1,2}[\-\/\.](1|2)$)|(^(0[1-9]|1[012])[\-\/\.](0[1-9]|[12][0-9]|3[01])[\-\/\.]$)|(^(0[1-9]|1[012])[\-\/\.](0[1-9]|[12][0-9]|3[01])$)|(^(0[1-9]|1[012])[\-\/\.](0|1|2|3)$)|(^(0[1-9]|1[012])[\-\/\.]$)|(^(0[1-9]|1[012])$)|(^(0|1)|(\d{0,8})$)/;
  var reCurrency = /^-?[\$][0-9\.\,]+$/;
  var reZipcode = /(^\d{5}$)|(^\d{5}-\d{4}$)/
  var rePhoneNumber = /^\([1-9]\d{2}\)\s?\d{3}\-\d{4}$/;

  //Reqular expression to test the validity once entry is complete
  var reValidInteger = /(^-?\d[\d\,]*$)/;
  var reValidLargeInteger = /(^-?\d[\d\,]*m{0,2}[\d\,]*$)|(^-?\d\d*\.\d*m{1,2}$)|(^-?\.\d*m{1,2}$)/i;
  var reValidNumeric  =  /(^-?\d[\d\,]*m{0,2}[\d\,]*\.\d*$)|(^-?\d[\d\,]*[\d\,]*\.\d*m{0,2}$)|(^-?\d[\d\,]*m{0,2}[\d\,]*$)|(^-?\.\d\d*m{0,2}$)/i;
  var reValidFraction  =  /(^-?\d[\d\,]*m{0,2}\.\d*$)|(^-?\d[\d\,]*m{0,2}\s\d+(\/|\\)\d+$)|(\d+(\/|\\)\d+$)|(^-?\d[\d\,]*m{0,2}$)|(^-?\.\d\d*$)/i;
  var reValidDate = /(^0?[1-9]\/0?[1-9]\/\d{2}$)|(^0?[1-9]\/0?[1-9]\/\d{4}$)|(^0?[1-9]\/[1-3]\d\/\d{2}$)|(^0?[1-9]\/[1-3]\d\/\d{4}$)|(^1[0-2][\/]0?[1-9][\/]\d{2}$)|(^1[0-2][\/]0?[1-9][\/]\d{4}$)|(^1[0-2][\/][1-3]\d[\/]\d{2}$)|(^1[0-2][\/][1-3]\d[\/]\d{4}$)/;

  // For future use
  var reStrictValidDate = /(^(0[1-9]|1[012])[\-\/\.](0[1-9]|[12][0-9]|3[01])[\-\/\.](19|20)\d{0,2}$)|(^(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])(19|20)\d{0,2}$)/;
  var reValidCurrency = /^-?[\$][0-9\.\,]+$/;
  var reValidZipcode = /(^\d{5}$)|(^\d{5}-\d{4}$)/
  var reValidPhoneNumber = /^\([1-9]\d{2}\)\s?\d{3}\-\d{4}$/;

  function setFieldMaskAlpha(obj) {

    obj.validChars = reValidAlphaChars;
    obj.fieldMask = reAlpha;
    obj.validFieldMask = reAlpha;
    obj.validValue = '';
  }
    
  function createAlphaField(obj, min) {

	if(min==null)
		min=2;
	setFieldMaskAlpha(obj);
    createMaskedField(obj);
  }
  
  function setFieldMaskAlphaNumeric(obj) {

    obj.validChars = reValidAlphaNumericChars;
    obj.fieldMask = reAlphaNumeric;
    obj.validFieldMask = reAlphaNumeric;
    obj.validValue = '';
  }
    
  function createAlphaNumericField(obj, min) {

	if(min==null)
		min=2;
	setFieldMaskAlphaNumeric(obj);
    createMaskedField(obj);
  }

  function setFieldMaskNumeric(obj) {

    obj.validChars = reValidNumericChars;
    obj.fieldMask = reNumeric;
    obj.validFieldMask = reValidNumeric;
    obj.validValue = '';
  }
    
  function createNumericField(obj, min) {

	if(min==null)
		min=2;
	setFieldMaskNumeric(obj);
    createMaskedField(obj);

    if(obj.onblur!=null) {
      var oldHandler = obj.onblur;
      obj.onblur = function() { if(formatDecimal(event, min)==true)  return oldHandler(event); else return false; };
    }
    else {
      obj.onblur = function() { return formatDecimal(event, min); };
    }
  }

  function setFieldMaskFraction(obj) {

    obj.validChars = reValidFractionChars;
    obj.fieldMask = reFraction;
    obj.validFieldMask = reValidFraction;
    obj.validValue = '';
  }

  function createFractionField(obj) {

	setFieldMaskFraction(obj);
    createMaskedField(obj);

    if(obj.onblur!=null) {
      var oldHandler = obj.onblur;
      obj.onblur = function() { if(formatFraction(event)==true) return oldHandler(event); else return false; };
    }
    else {
      obj.onblur = function() { return formatFraction(event); };
    }
  }

  function setFieldMaskInteger(obj) {

    obj.validChars = reValidIntegerChars;
    obj.fieldMask = reInteger;
    obj.validFieldMask = reValidInteger;
    obj.validValue = '';
  }

  function createIntegerField(obj) {

	setFieldMaskInteger(obj);
    createMaskedField(obj);

    if(obj.onblur!=null) {
      var oldHandler = obj.onblur;
      obj.onblur = function() { if(formatDecimal(event,0)==true) return oldHandler(event); else return false; };
    }
    else {
      obj.onblur = function() { return formatDecimal(event,0); };
    }
  }

  function setFieldMaskLargeInteger(obj) {

    obj.validChars = reValidLargeIntegerChars;
    obj.fieldMask = reLargeInteger;
    obj.validFieldMask = reValidLargeInteger;
    obj.validValue = '';
  }
    
  function createLargeIntegerField(obj) {

    setFieldMaskLargeInteger(obj);
    createMaskedField(obj);

    if(obj.onblur!=null) {
      var oldHandler = obj.onblur;
      obj.onblur = function() { if(formatDecimal(event,0)==true) return oldHandler(event); else return false; };
    }
    else {
      obj.onblur = function() { return formatDecimal(event,0); };
    }
  }

  function setFieldMaskDate(obj) {
    obj.validChars = reValidDateChars;
    obj.fieldMask = reDate;
    obj.validFieldMask = reValidDate;
    obj.validValue = '';
  }

  function createDateField(obj) {
	setFieldMaskDate(obj);
    createMaskedField(obj);

    if(obj.onblur!=null) {
      var oldHandler = obj.onblur;
      obj.onblur = function() { if(formatDate(event)==true)  return oldHandler(event); else return false; };
    }
    else {
      obj.onblur = function() { return formatDate(event); };
    }
    
    if(obj.onfocus!=null) {
      var oldHandler = obj.onfocus;
      obj.onfocus = function() { if(onFocusDateEvent(event)==true) return oldHandler(event); else return false; };
    }
    else {
      obj.onfocus = function() { return onFocusDateEvent(event); };
    }       
  }

  function setFieldMaskMonthYear(obj) {
    obj.validChars = reValidDateChars;
    obj.fieldMask = reDate;
    obj.validFieldMask = reValidDate;
    obj.validValue = '';
  }

  function createMonthYearField(obj) {
	setFieldMaskMonthYear(obj);
    createMaskedField(obj);

    if(obj.onblur!=null) {
      var oldHandler = obj.onblur;
      obj.onblur = function() { if(formatDate(event)==true)  return oldHandler(event); else return false; };
    }
    else {
      obj.onblur = function() { return formatDate(event); };
    }
    
    if(obj.onfocus!=null) {
      var oldHandler = obj.onfocus;
      obj.onfocus = function() { if(onFocusDateEvent(event)==true) return oldHandler(event); else return false; };
    }
    else {
      obj.onfocus = function() { return onFocusDateEvent(event); };
    }    
  }

  function setFormReadOnly() {
    var inputElts = document.getElementsByTagName('input');
    for ( i = 0; i < inputElts.length; i++ ) {
      var type = inputElts[i].getAttribute('type');

      if(type=='text') {
        setReadOnlyText(inputElts[i]);
      }
      else if(type=='checkbox') {
        setReadOnlyCheckbox(inputElts[i]);
      }
      else if(type=='radio') {
        setReadOnlyRadio(inputElts[i]);
      }
      else if(type=='select') {
        setReadOnlySelect(inputElts[i]);
      }
    }

    inputElts = document.getElementsByTagName('select');
    for ( i = 0; i < inputElts.length; i++ ) {
      setReadOnlySelect(inputElts[i]);
    }
  }

  function setReadOnlyText(obj) {
    obj.onchange = function() { return false; };
    obj.onpaste = function() { return false; };
    obj.onkeydown = function() { return readOnlyKeyDown(event); };
    obj.onkeypress = function() { return readOnlyKeyPress(event); };
    obj.onkeyup = function() { return false; };
    obj.onfocus = function() { return false; };
    obj.onblur = function() { return false; };
  }

  function setReadOnlyCheckbox(obj) {
    obj.onchange = function() { return false; };
    obj.onclick = function() { return false; };
    obj.onkeydown = function() { return readOnlyKeyDown(event); };
    obj.onkeypress = function() { return readOnlyKeyPress(event); };
    obj.onkeyup = function() { return false; };
  }

  function setReadOnlyRadio(obj) {
    obj.onchange = function() { return false; };
    obj.onclick = function() { setDefaultRadio(event); event.cancelBubble=true; return false; };
    obj.onkeydown = function() { return readOnlyKeyDown(event); };
    obj.onkeypress = function() { return readOnlyKeyPress(event); };
    obj.onkeyup = function() { return false; };
  }

  function setReadOnlySelect(obj) {
    obj.validValue = obj.value;
    obj.onchange = function() { obj.value = obj.validValue; return false; };
    obj.onkeydown = function() { return readOnlyKeyDown(event); };
    obj.onkeypress = function() { return readOnlyKeyPress(event); };
    obj.onkeyup = function() { return false; };
  }

  function setDefaultRadio(objEvent) {
    var obj;

    if (isIE) {
      obj = objEvent.srcElement;
    }
    else {
      obj = objEvent.target;
    }

    var radioElts = document.getElementsByName(obj.name);
    for ( i = 0; i < radioElts.length; i++ ) {
      radioElts[i].checked = radioElts[i].defaultChecked;
    }
  }

  function createMaskedField(obj) {

    if(obj.onkeypress!=null) {
      var oldHandler = obj.onkeypress;
      obj.onkeypress = function() { if(maskedKeyPress(event)==true) return oldHandler(event); else return false; };
    }
    else {
      obj.onkeypress = function() { return maskedKeyPress(event); };
    }

    if(obj.onkeyup!=null) {
      var oldHandler = obj.onkeyup;
      obj.onkeyup = function() { if(maskedKeyUp(event)==true) return oldHandler(event); else return false; };
    }
    else {
      obj.onkeyup = function() { return maskedKeyUp(event); };
    }

    if(obj.onchange!=null) {
      var oldHandler = obj.onchange;
      obj.onchange = function() { if(maskedFieldChange(event)==true) return oldHandler(event); else return false; };
    }
    else {
      obj.onchange = function() { return maskedFieldChange(event); };
    }

    if(obj.onpaste!=null) {
      var oldHandler = obj.onpaste;
      obj.onpaste = function() { if(maskedPaste(event)==true) return oldHandler(event); else return false; };
    }
    else {
      obj.onpaste = function() { return maskedPaste(event); };
    }
  }

  function maskedKeyPress(objEvent) {
    var iKeyCode, strKey, objInput;

    if (isIE) {
      iKeyCode = objEvent.keyCode;
      objInput = objEvent.srcElement;
    }
    else {
      iKeyCode = objEvent.which;
      objInput = objEvent.target;
    }
    strKey = String.fromCharCode(iKeyCode);
    if(reKeyboardChars.test(strKey) || checkClipboardCode(objEvent, strKey)) {
      return true;
    }

    if (!objInput.validChars.test(strKey) ) {
      return false;
    }

    return true;
  }

  function maskedKeyUp(objEvent) {
    var objInput;

    if (isIE) {
      objInput = objEvent.srcElement;
    }
    else {
      objInput = objEvent.target;
    }

    if (isValidValue(objInput.value, objInput.fieldMask)) {
      objInput.validValue = objInput.value;
    }
    else {
      objInput.value = objInput.validValue;
      return false;
    }
    return true;
  }

  function readOnlyKeyDown(objEvent) {
    var iKeyCode, strKey, objInput;

    if (isIE) {
      iKeyCode = objEvent.keyCode;
    }
    else {
      iKeyCode = objEvent.which;
    }

    if(iKeyCode==8 || iKeyCode==46) {
      return false;
    }
    return true;
  }

  function readOnlyKeyPress(objEvent) {
    var iKeyCode, strKey, objInput;

    if (isIE) {
      iKeyCode = objEvent.keyCode;
      objInput = objEvent.srcElement;
    }
    else {
      iKeyCode = objEvent.which;
      objInput = objEvent.target;
    }

    strKey = String.fromCharCode(iKeyCode);
    if(reKeyboardChars.test(strKey)) {
      return true;
    }

    return false;
  }

  function isValidValue(strValue, keyMask) {
    return keyMask.test(strValue) || strValue.length == 0;
  }

  function maskedFieldChange(objEvent) {
    var objInput;

    if (isIE) {
      objInput = objEvent.srcElement;
    }
    else {
      objInput = objEvent.target;
    }

    if (!isValidValue(objInput.value, objInput.validFieldMask)) {
      objInput.value = objInput.validValue || "";
      objInput.focus();
      objInput.select();
      return false;
    }
    else {
      objInput.validValue = objInput.value;
    }
    return true;
  }

  function maskedPaste(objEvent) {
    var strPasteData = window.clipboardData.getData("Text");
    var objInput = objEvent.srcElement;
    if (!isValidValue(strPasteData, objInput.fieldMask)) {
      objInput.focus();
      return false;
    }
  }

  function formatDecimal(objEvent, min, max) {
    var objInput;
    if (isIE) {
      objInput = objEvent.srcElement;
    }
    else {
      objInput = objEvent.target;
    }

    if (!isValidValue(objInput.value, objInput.validFieldMask)) {
      //objInput.value = objInput.validValue || "";
      //objInput.focus();
      //objInput.select();
      return false;
    }

    if(objInput.value.length>0) {
      objInput.value = convertDecimalValue(objInput.value, min, max);
      if(objInput.value.length>objInput.maxLength) {
        objInput.value = objInput.value.substring(0,objInput.maxLength);
        objInput.value = convertDecimalValue(objInput.value, min);
      }
    }
    return true;
  }

  function formatFraction(objEvent, min, max) {
    var objInput;

    if (isIE) {
      objInput = objEvent.srcElement;
    }
    else {
      objInput = objEvent.target;
    }

    if (!isValidValue(objInput.value, objInput.validFieldMask)) {
      //objInput.value = objInput.validValue || "";
      //objInput.focus();
      //objInput.select();
      return false;
    }

    if(objInput.value.length>0) {
      objInput.value = convertFractionValue(objInput.value, min);
      if(objInput.value.length>objInput.maxLength) {
        objInput.value = objInput.value.substring(0,objInput.maxLength);
        objInput.value = convertFractionValue(objInput.value,2);
      }
    }
    return true;
  }

  function formatCurrency(objEvent, min) {
    var objInput;

    if (isIE) {
      objInput = objEvent.srcElement;
    }
    else {
      objInput = objEvent.target;
    }

    if (!isValidValue(objInput.value, objInput.validFieldMask)) {
      //objInput.value = objInput.validValue || "";
      //objInput.focus();
      //objInput.select();
      return false;
    }

    if(objInput.value.length>0) {
      objInput.value = convertCurrencyValue(objInput.value, min);
      if(objInput.value.length>objInput.maxLength) {
        objInput.value = objInput.value.substring(0,objInput.maxLength);
        objInput.value = convertCurrencyValue(objInput.value, min);
      }
    }
    return true;
  }

  function formatDate(objEvent) {
    var objInput;
    if (isIE) {
      objInput = objEvent.srcElement;
    }
    else {
      objInput = objEvent.target;
    }

	if (objInput.value == dateTip) {
		return true;
	}
	else if (objInput.value == "") {
		objInput.className = "formattip";
		objInput.value = dateTip;
		return true;
	}	
    else if (!isValidValue(objInput.value, objInput.validFieldMask)) {
      //objInput.value = objInput.validValue || "";
      //objInput.focus();
      //objInput.select();
      return false;
    }

    return convertDate(objInput);
  }

function convertDate(objInput) {
	var dateFormat = "mm/dd/yyyy";
	var yearIndex = 2;
	var dateMinLength = 6;
	var dateMaxLength = 10;
	
	if ( objInput.value != "" ) {
		var dateString = objInput.value;
		var dateEltsArray = objInput.value.split(/[\/]/);
		
		if ( dateEltsArray.length < 3 ) {
			return false;
		} else {
			dateEltsArray = determineLengthOfMonthDayOrYear(dateEltsArray);
		}
		
		if ( dateEltsArray[yearIndex] == "0000" ){
			return false;
		}
		
		var month = dateEltsArray[0];
		var day = dateEltsArray[1];
		var year = dateEltsArray[yearIndex];
		
		if ( dateString.length >= dateMinLength &&
			dateString.length <= dateMaxLength )
		{
			if ( month > 12 || month < 1 ) {
				return false;
			}
			
			var maxDay = determineDaysInMonth(month, year);
					
			if ( day > maxDay || day < 1 ){
				return false;
			}
		} else {
			return false;
		}
		
		objInput.value = month + "/" + day + "/" + year;
	}
	return true;
}

function determineLengthOfMonthDayOrYear(dateArray){
	for(i = 0; i < dateArray.length; i++){
		if ( dateArray[i].length < 2 ){
			dateArray[i] = "0" + dateArray[i];
		}
	}
	
	var yearIndex = dateArray.length == 3 ? 2 : 1;
	if ( dateArray[yearIndex].length <= 2 ){
		if ( dateArray[yearIndex] < 70 ){
			dateArray[yearIndex] = "20" + dateArray[yearIndex];
		}else{
			dateArray[yearIndex] = "19" + dateArray[yearIndex];
		}
	}
	return dateArray;
}

function determineDaysInMonth(monthValue, yearValue){
	maxDay = 31;
	if ( monthValue == 4 || monthValue == 6 || monthValue == 9 || monthValue == 11 ) {
		maxDay = 30;
	} else if ( monthValue == 2 ) {
		// determine if it's a leap year
		if ( (yearValue % 4) == 0 ) {
			// potential leap year -- check for new
			// century
			if ( (yearValue % 100) != 0 ) {
				// not a new century -- definitely
				// a leap year
				maxDay = 29;
			} else if ( (yearValue % 400) == 0 ) {
				// new century divisible by 400 is
				// a leap year
				maxDay = 29;
			} else {
				// new century and not a leap year
				maxDay = 28;
			}
		} else { // definitely not a leap year
			maxDay = 28;
		}
	}
	return maxDay;
}



  function removeDecorators(num) {
    num = num.toString().replace(reDecoratorChars,'');
    return num;
  }

  function expandShortcuts(num) {

    var factor = 1;
    if(num.length>1) {
      var i=0;
      while (i < num.length) {
        if (reShortcutChars.test(num.charAt(i))) {
          factor*=1000;
          num = num.substring(0,i) + num.substring(i+1, num.length);
          continue;
        }
        i++;
      }
      if(factor>1) {
        newValue = parseFloat(num)*factor;
        num = newValue.toString();
      }
    }
    return num;
  }

  function coreDecimalconversion(num, min) {
    var fraction;
    var max=0;
    var i = 0;
    var whole = '' + num;
    var dec = whole.indexOf('.');

    if(dec>-1) {
      fraction = whole.substring(dec+1, num.length);
      whole = whole.substring(0, dec);
    }
	
	if ( whole.length > 3 ) whole = addCommas( whole );

  	if(min>0) {
  		if(fraction==null) {
  			fraction="";
  		}
        if(fraction.length<min) {
          for (i = fraction.length; i < min; i++) {
            fraction=fraction+'0';
          }
        }
   	}
    if(fraction!=null) {
      if(max>0) {
        if(fraction.length>max) {
          fraction=substring(0,max);
        }
      }

      num = whole + "." + fraction;
    }
    else {
      num = whole;
    }
    return num;
  }

  function addCommas( strValue ) {
    var objRegExp  = new RegExp('(-?[0-9]+)([0-9]{3})');
    while(objRegExp.test(strValue)) {
      strValue = strValue.replace(objRegExp, '$1,$2');
    }
    return strValue;
  }

  function convertFractionValue(num, min) {

    //Make sure that the value contains a / character which represents a fraction
    if(num != null && num.length > 2) {

      if(num.indexOf('/') != -1) {
        num=coreFractionConversion(num, '/', min);
      }
      else if(num.indexOf("\\") != -1) {
        num=coreFractionConversion(num, "\\", min);
      }
    }
	
	num = removeDecorators( num );

    if(min!=null) {
      num = coreDecimalconversion(num, min);
    }
    else {
      num = coreDecimalconversion(num, 2);
    }

    return num;
  }

  function coreFractionConversion(num, fractchar, min) {
    var arr1, arr2;
    var fraction;
    var newVal=0;

    arr1 = num.split(' ');

    if(arr1 != null) {
      if(arr1.length==1) {
        arr2 = arr1[0].split(fractchar);
      }
      else {
        newVal = parseInt(removeDecorators(arr1[0]));
        arr2 = arr1[1].split(fractchar);
      }

      if(arr2 != null && arr2.length == 2) {
        fraction = arr2[0] / arr2[1];
        newVal += parseFloat(fraction.toFixed(3));
        num = convertDecimalValue(newVal,min);
      }
    }
    return num;
  }


  function convertDecimalValue(num, min) {
    var sign;
    num = removeDecorators(num);
    num = expandShortcuts(num);
    sign = (num == (num = Math.abs(num)));
    if(min!=null) {
      num = coreDecimalconversion(num, min);
    }
    else {
      num = coreDecimalconversion(num, 2);
    }
    return (((sign)?'':'-') + num);
  }

  function convertCurrencyValue(num, min) {
    num = removeDecorators(num);
    num = expandShortcuts(num);
    var sign = (num == (num = Math.abs(num)));
    if(min!=null) {
      num = coreDecimalconversion(num, min);
    }
    else {
      num = coreDecimalconversion(num, 2);
    }
    return (((sign)?'':'-') + '$' + num + '.' + cents);
  }

  function checkClipboardCode(objEvent, strKey) {
    if (isIE) {
      return false;
    }
    return objEvent.ctrlKey && reClipboardChars.test(strKey);
  }

function onFocusDateEvent(objEvent) {
    var objInput;
    if (isIE) {
      objInput = objEvent.srcElement;
    }
    else {
      objInput = objEvent.target;
    }

	if (objInput.value == dateTip) {
		objInput.className = "";
		objInput.value = "";
      	objInput.focus();
      	objInput.select();
	}
}

function onFocusDate(objInput) {
	if (objInput.value == dateTip) {
		objInput.className = "";
		objInput.value = "";
      	objInput.focus();
      	objInput.select();
	}
}

function onBlurDate(objInput) {
	if (objInput.value == "") {
		objInput.className = "formattip";
		objInput.value = dateTip;
	}
}
	
  