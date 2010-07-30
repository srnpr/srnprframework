/// <reference path="jquery-1.3.2.min-vsdoc.js"/>
/// <reference path="json2.js"/>




var x = { a: "ddd", b: "dfafdaf" };


$.ajax({ url: "/Asmx/GridShowHander.ashx", type: "POST", data: "json="+JSON.stringify(x) , success: GetJson });






//alert(jQuery.parseJSON(x));


function GetJson(t)
{


    $("#jsonshow").html(t);

    var obj = jQuery.parseJSON(t);
    alert(obj.ListString[0].length);

}













