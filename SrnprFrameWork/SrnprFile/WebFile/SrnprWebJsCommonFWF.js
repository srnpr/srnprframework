(
function() {

    // Will speed up references to window, and allows munging its name.
    var window = this,
    // Will speed up references to undefined, and allows munging its name.
	undefined,
    // Map over jQuery in case of overwrite
	_SrnprWebJsCommon = window.SrnprWebJsCommon,
    // Map over the $ in case of overwrite
	_SWJC = window.SWJC,
 SrnprWebJsCommon = window.SrnprWebJsCommon = window.SWJC = function(e) {
     if (typeof (e) == "string") {
         e = document.getElementById(e);
     }
     else if (e == undefined) {
         e = document;
     }

     return e;
 }

    SWJC.InitObj = function(oDefault, o) {
        if (o == undefined) {
            o = oDefault;
        }
        else {
            //重新初始化
            for (var p in oDefault) {
                if (o[p] == undefined)
                    o[p] = oDefault[p];
            }
        }
        return o;
    }

    SWJC.AlterMsg = function(m) {
        alert(m);
    }

    SWJC.SubmitElement = function(e) {
        if (typeof (e) == "string") {
            e = document.getElementById(e);
        }
        if (e) {
            e.form.submit();
        }
        else {
            document.forms[0].submit();
        }
    }

    SWJC.AddEvent = function(element, e, fn) {
        if (typeof (element) == "string") {
            element = document.getElementById(element);
        }

        if (element) {
            if (element.addEventListener) {
                element.addEventListener(e, fn, false);
            } else {
                element.attachEvent("on" + e, fn);
            }
        }
    }

    SWJC.AddError = function(e, m) {
        if (!SWJC(e).nextSibling.srnprwebjscommon_vali_error) {
            var o = document.createElement("span");
            o.srnprwebjscommon_vali_error = "true";
            o.innerHTML = "<img src=\"http://f.xgou.com/AtGang/UI/images/vali_0.gif\" />" + m;


            SWJC(e).parentNode.insertBefore(o, SWJC(e).nextSibling);
        }
        SWJC(e).focus();
    }


    var userAgent = navigator.userAgent.toLowerCase();
    SWJC.Browser = {
        version: (userAgent.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [0, '0'])[1],
        safari: /webkit/.test(userAgent),
        opera: /opera/.test(userAgent),
        msie: /msie/.test(userAgent) && !/opera/.test(userAgent),
        mozilla: /mozilla/.test(userAgent) && !/(compatible|webkit)/.test(userAgent)
    }

    SWJC.IsCheckSubmit = function(e) {
        return !SWJC("Jquery_Tooltip_Check_Submit");
    }


    SWJC.UnSubmit = function(e) {
        e = e || window.event;

        if (!this.Browser.msie) {
            e.preventDefault();
        }
        else {
            e.returnValue = false;
        }

    }















}

)();