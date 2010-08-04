/// <reference path="jquery-1.3.2.min-vsdoc.js"/>
/// <reference path="json2.js"/>


if (!this.SWJGSF)
{

    this.SWJGSF = {};
}

    
(
    function()
    {


        SWJGSF.Obj = {};


        //初始化
        SWJGSF.Init = function(s)
        {
            $("document").ready(function()
            {

                SWJGSF.Obj[s.Id] = s;
                SWJGSF.Ajax(s.Id);

            })
        }


        //提交请求
        SWJGSF.Ajax = function(id)
        {
            $.ajax({ url: "/Asmx/GridShowHander.ashx", type: "POST", data: "json=" + JSON.stringify(SWJGSF.Obj[id]), success: function(x) { SWJGSF.AjaxSuccess(id, x) } });
        }

        //跳转页面
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


            sShowHtml += "<div class=\"SWCGSF_DIV_FOOT_NAV\">" + obj.Request.PageIndex + "/" + iPageCount + "页  共计：" + obj.Request.RowsCount + "条"
            + "<a href=\"javascript:SWJGSF.PageGoto('" + obj.Request.Id + "',1)\">首页</a><a href=\"javascript:SWJGSF.PageGoto('" + obj.Request.Id + "','-')\">上一页</a><a href=\"javascript:SWJGSF.PageGoto('" + obj.Request.Id + "','+')\">下一页</a><a href=\"javascript:SWJGSF.PageGoto('" + obj.Request.Id + "','" + iPageCount + "')\">尾页</a>"
            + "<a href=\"javascript:SWJGSF.ShowDisplay('" + obj.Request.Id + "')\">自定义</a></div>";


            $("#SWJGSF_Div_" + obj.Request.ClientId).html(sShowHtml);





            $("#jsonshow").text(o);


            //alert(obj.ListString[0].length);
        }

        //自定义显示字段
        SWJGSF.ShowDisplay = function(id)
        {

            var s = "<ul>";
            for (var i = 0, j = SWJGSF.Obj[id].ShowColumn.length; i < j; i++)
            {
                s += "<li><input type=\"checkbox\" id=\"" + SWJGSF.Obj[id].ClientId + "_showcolumn_ckb_" + SWJGSF.Obj[id].ShowColumn[i].Guid + "\" " + (SWJGSF.Obj[id].ShowColumn[i].ShowDisplay == "n" ? "" : "checked=\"checked\"") + " />" + SWJGSF.Obj[id].ShowColumn[i].HeaderText + "</li>";
            }

            s += "</ul>";

            SrnprNetJsAllAlphaShow({ s: "f", c: s, m: "请选择显示内容", y: "SWJGSF.SetDisplay('" + id + "')", w: "400" })
        }

        //设置显示字段
        SWJGSF.SetDisplay = function(id)
        {

            for (var i = 0, j = SWJGSF.Obj[id].ShowColumn.length; i < j; i++)
            {
                if ($("#" + SWJGSF.Obj[id].ClientId + "_showcolumn_ckb_" + SWJGSF.Obj[id].ShowColumn[i].Guid).attr("checked") == true)
                {
                    SWJGSF.Obj[id].ShowColumn[i].ShowDisplay = "d";
                }
                else
                {
                    SWJGSF.Obj[id].ShowColumn[i].ShowDisplay = "n";
                }
            }

            SWJGSF.Ajax(id);
            SrnprNetJsAllAlphaShow({ s: "c" });
        }


        SWJGSF.Sort = function(id, gid)
        {
            for (var i = 0, j = SWJGSF.Obj[id].ShowColumn.length; i < j; i++)
            {
                if (SWJGSF.Obj[id].ShowColumn[i].Guid == gid)
                {
                    SWJGSF.Obj[id].ShowColumn[i].OrderType = SWJGSF.Obj[id].ShowColumn[i].OrderType == "a" ? "e" : "a";
                }
                else
                {
                    if (SWJGSF.Obj[id].ShowColumn[i].OrderType != "n")
                    {
                        SWJGSF.Obj[id].ShowColumn[i].OrderType = "d";
                    }
                }
            }

            SWJGSF.Ajax(id);
        }



        SWJGSF.Query = function(id, sid)
        {
            var t = [];


            $("#" + sid).children("[paramid][paramid<>'']").each(

             function(index, n)
             {
                 var el = $(n);
                 var vl = el.val();
                 if (el.is("input") && (n.type == "checkbox" || n.type == "radio") && !n.checked)
                 {
                     vl = "";
                 }

                 if (vl != "")
                 {
                     var jLength = t.length;

                     for (var i = 0, j = t.length; i < j; i++)
                     {
                         if (t[i].Key == n.paramid)
                         {
                             t[i].Value = t[i].Value + "," + vl;
                             jLength = -1;
                         }
                     }

                     if (jLength > -1)
                     {
                         t.push({ Key: n.paramid, Value: vl });
                     }

                 }

             }
            );

             SWJGSF.Obj[id].QueryDict = t;
            
            SWJGSF.Ajax(id);


        }




    }

)();





  












































