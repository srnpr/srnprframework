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
            Id: "ceshi",
            PageIndex: 1,
            PageSize: 10,
            RowsCount: -1,
            ProcessType: ""
        };


        SWJGSF.Obj = {};



        SWJGSF.Ajax = function(id)
        {
            $.ajax({ url: "/Asmx/GridShowHander.ashx", type: "POST", data: "json=" + JSON.stringify(SWJGSF.Obj[id]), success: function(x) { SWJGSF.AjaxSuccess(id, x) } });
        }

        SWJGSF.PageGoto = function(id, iPage)
        {


            var iPageCount = Math.floor(SWJGSF.Obj[id].RowsCount / SWJGSF.Obj[id].PageSize);

            if (iPage == "-")
            {
                iPage = SWJGSF.Obj[id].PageIndex - 1;
            }
            else if (iPage == "+")
            {
                iPage = SWJGSF.Obj[id].PageIndex + 1;
            }

            iPage = parseInt(iPage);

            if (iPage < 1)
            {
                iPage = 1;
            }
            else if (iPage > iPageCount)
            {
                iPage = iPageCount;
            }



            SWJGSF.Obj[id].PageIndex = iPage;

            SWJGSF.Ajax(id);
        }


        //执行成功时
        SWJGSF.AjaxSuccess = function(id, o)
        {


            var obj = JSON.parse(o);
            
            //重新赋上最新版返回参数
            SWJGSF.Obj[id] = obj.Request;

            var sShowHtml = obj.HtmlString;




            var iPageCount = Math.floor(obj.Request.RowsCount / obj.Request.PageSize);


            sShowHtml += "<div>" + obj.Request.PageIndex + "/" + iPageCount + "页  共计：" + obj.Request.RowsCount + "条<a href=\"javascript:SWJGSF.PageGoto('" + obj.Request.Id + "',1)\">首页</a><a href=\"javascript:SWJGSF.PageGoto('" + obj.Request.Id + "','-')\">上一页</a><a href=\"javascript:SWJGSF.PageGoto('" + obj.Request.Id + "','+')\">下一页</a><a href=\"javascript:SWJGSF.PageGoto('" + obj.Request.Id + "','" + iPageCount + "')\">尾页</a></div>";


            $("#GSShow").html(sShowHtml);

            $("#jsonshow").text(sShowHtml);


            //alert(obj.ListString[0].length);
        }





    }

)();





    $("document").ready(function()
    {
        var sId = "ceshi";

        SWJGSF.Obj[sId] = SWJGSF.ObjTemp;
        SWJGSF.Obj[sId].Id = sId;

        SWJGSF.Ajax(sId);

    });





















var x = { a: "ddd", b: "dfafdaf" };









//alert(jQuery.parseJSON(x));


function GetJson(t)
{


   

}













