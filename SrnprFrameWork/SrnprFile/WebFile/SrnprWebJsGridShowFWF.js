/// <reference path="jquery-1.3.2.min-vsdoc.js"/>
/// <reference path="json2.js"/>


if (!this.SWJGSF)
{

    this.SWJGSF = {};
}

    
(
    function()
    {
        SWJGSF.ObjTemp = {
            Id: "",
            PageIndex: 1,
            PageSize: 10,
            RowsCount: -1,
            ProcessType: ""
        };



        SWJGSF.Ajax = function(o)
        {
            $.ajax({ url: "/Asmx/GridShowHander.ashx", type: "POST", data: "json=" + JSON.stringify(o), success: SWJGSF.AjaxSuccess });
        }




        //执行成功时
        SWJGSF.AjaxSuccess = function(o)
        {


            var obj = JSON.parse(o);


            var sShowHtml = obj.HtmlString;
            
            

            sShowHtml += "<div>"+obj.Request.RowsCount+"</div>";


            $("#GSShow").html(sShowHtml);

            $("#jsonshow").text(sShowHtml);


            //alert(obj.ListString[0].length);
        }





    }

)();





    $("document").ready(function() { SWJGSF.Ajax(SWJGSF.ObjTemp) });





















var x = { a: "ddd", b: "dfafdaf" };









//alert(jQuery.parseJSON(x));


function GetJson(t)
{


   

}













