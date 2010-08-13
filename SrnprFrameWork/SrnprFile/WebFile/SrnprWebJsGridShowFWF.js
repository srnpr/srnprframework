/// <reference path="jquery-1.3.2.min-vsdoc.js"/>
/// <reference path="json2.js"/>


if (!this.SWJGSF)
{

    this.SWJGSF = {};

    
(
    function()
    {


        //定义设置变量
        SWJGSF.SetObj = {};


        //定义基本服务器交互变量
        SWJGSF.Obj = {};



        SWJGSF.Set = function(id, p, v)
        {
            SWJGSF.SetInit(id);
            SWJGSF.SetObj[id][p] = v;
        }

        SWJGSF.SetInit = function(id)
        {

            if (!SWJGSF.SetObj[id])
            {

                var temp =
                {
                    //是否显示统计
                    FlagGroup: true,
                    //是否有全部统计
                    FlagGroupSum: true,

                    //是否显示行序号
                    FlagIndexNumber: true,

                    //分组显示字符串 d表示描述 v表示数量
                    GroupTextTemp: '{gtt:d}【{gtt:v}】',

                    //分组时全部显示的描述信息
                    GroupSumText: '全部',

                    //分页统计字段 i表示当前页 c表示页数总计 r表示条数总计
                    NavPageSum: '{nps:i}/{nps:c}页 共计：{nps:r}条 ',

                    //更改页面字段  i表述页码  s表示每页大小  f表示切换函数
                    NavPageChange: '每页：{npc:i} 跳转到：{npc:s}<a href="javascript:{npc:f}">go</a>',

                    //数字导航样式  f:函数  n数字
                    NavPageNumber: '<a href="javascript:{npn:f}" class="{npn:c}">{npn:n}</a>',

                    //导航 f:首页 p：上一页
                    NavPageLeft: '<a href="javascript:{npl:f}">首页</a><a href="javascript:{npl:p}">上一页</a>',
                    //导航  n下一页  l尾页
                    NavPageRight: '<a href="javascript:{npr:n}">下一页</a><a href="javascript:{npr:l}">尾页</a>',

                    //自定义字段 f函数
                    NavPageUser: '<a href="javascript:{npu:f}">自定义</a>',

                    NavPageExcel: '<a href="javascript:{npe:f}">导出Excel</a>',

                    //导航显示样式
                    NavPageHtml: '<table><tr><td>{nph:nps}</td><td>{nph:npl}{nph:npn}{nph:npr}</td><td>{nph:npu}{nph:npe}</td><td style="text-align:right;">{nph:npc}</td></tr></table>',

                    NavEveryPage: 10

                }

                SWJGSF.SetObj[id] = temp;
            }

            return SWJGSF.SetObj[id];
        }




        //初始化
        SWJGSF.Init = function(s)
        {

            SWJGSF.Obj[s.Id] = s;


            $("document").ready(function()
            {
                SWJGSF.Ajax(s.Id);

            });

            $("form").submit(function() { SWJGSF.SubmitBefore(s.Id); });


        }


        SWJGSF.SubmitBefore = function(id)
        {
            $("#SWJGSF_Hidden_" + SWJGSF.Obj[id].ClientId).val(JSON.stringify(SWJGSF.Obj[id]));
        }


        //提交请求
        SWJGSF.Ajax = function(id)
        {


            $.ajax(
            {
                url: "/Asmx/GridShowHander.ashx",
                type: "POST",
                data: "json=" + JSON.stringify(SWJGSF.Obj[id]),
                success: function(x) { SWJGSF.AjaxSuccess(id, x); },
                error: function(XMLHttpRequest, textStatus) { SWJGSF.AlertMsg(textStatus) }



            });
        }

        SWJGSF.AlertMsg = function(s)
        {
            alert(s);
        }



        //跳转页面
        SWJGSF.PageGoto = function(id, iPage)
        {


            var iPageCount = Math.ceil(SWJGSF.Obj[id].RowsCount / SWJGSF.Obj[id].PageSize);

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


        //分组
        SWJGSF.GroupTable = function(id, k)
        {
            SWJGSF.Obj[id].GroupValue = k;
            SWJGSF.Obj[id].PageIndex = 1;
            SWJGSF.Obj[id].RowsCount = -2;
            SWJGSF.Ajax(id);
        }


        //导航更换
        SWJGSF.ChangeNav = function(id)
        {

            var s = $('#' + SWJGSF.Obj[id].ClientId + '_nav_pagesize').val();
            if (s && !isNaN(s) && s > 0)
            {
                SWJGSF.Obj[id].PageSize = parseInt(s);
            }
            var i = $('#' + SWJGSF.Obj[id].ClientId + '_nav_pageindex').val();

            if (i && !isNaN(i) && i > 0)
            {
                SWJGSF.Obj[id].PageIndex = parseInt(i);
            }

            SWJGSF.PageGoto(id, SWJGSF.Obj[id].PageIndex);

        }




        //执行成功时
        SWJGSF.AjaxSuccess = function(id, o)
        {

            var obj = JSON.parse(o);

            //重新赋上最新版返回参数
            var req = SWJGSF.Obj[id] = obj.Request;

            //开始尝试初始化设置
            var so = SWJGSF.SetInit(id);

            var aHtml = [];

            if (req.GroupKvd && so.FlagGroup)
            {
                var aGroupHtml = [];

                var dSumAll = 0;

                for (var i = 0, j = req.GroupKvd.length; i < j; i++)
                {
                    aGroupHtml.push('<li' + (req.GroupValue == req.GroupKvd[i].K ? ' class="SWCGSF_WEBTAB_UL_LI_HOVER" ' : '') + '><a href="javascript:SWJGSF.GroupTable(\'' + id + '\',\'' + req.GroupKvd[i].K + '\')">' + so.GroupTextTemp.replace('{gtt:d}', req.GroupKvd[i].D).replace('{gtt:v}', req.GroupKvd[i].V) + '</a></li>');

                    dSumAll += parseFloat(req.GroupKvd[i].V);
                }


                var sGroupSumAll = so.FlagGroupSum ? ('<li' + (!req.GroupValue ? ' class="SWCGSF_WEBTAB_UL_LI_HOVER" ' : '') + '><a href="javascript:SWJGSF.GroupTable(\'' + id + '\',\'\')"> ' + so.GroupTextTemp.replace('{gtt:d}', so.GroupSumText).replace('{gtt:v}', dSumAll) + '</a></li>') : '';

                aHtml.push('<div class="SWCGSF_DIV_WEBTAB"><ul>' + sGroupSumAll + aGroupHtml.join("") + '</ul></div>');
            }

            aHtml.push('<div class="SWCGSF_DIV_SHOWINFO">' + obj.HtmlString);

            //开始底部导航
            if (req.ShowColumn)
            {

                var iPageCount = Math.ceil(req.RowsCount / req.PageSize);

                var nav_nps = '', nav_npu = '', nav_npl = '', nav_npr = '', nav_npn = '', nav_npc = '', nav_npe = '';

                nav_npu = so.NavPageUser.replace('{npu:f}', "SWJGSF.ShowDisplay('" + req.Id + "')");

                nav_npe = so.NavPageExcel.replace('{npe:f}', "SWJGSF.Excel('" + req.Id + "')");


                if (req.ProcessType == "" || req.ProcessType == "server")
                {

                    nav_nps = so.NavPageSum.replace('{nps:i}', iPageCount > 0 ? req.PageIndex : 0).replace('{nps:c}', iPageCount).replace('{nps:r}', req.RowsCount);

                    if (req.RowsCount > 0)
                    {
                        nav_npl = so.NavPageLeft.replace('{npl:f}', "SWJGSF.PageGoto('" + req.Id + "',1)").replace('{npl:p}', "SWJGSF.PageGoto('" + req.Id + "','-')");
                        nav_npr = so.NavPageRight.replace('{npr:n}', "SWJGSF.PageGoto('" + req.Id + "','+')").replace('{npr:l}', "SWJGSF.PageGoto('" + req.Id + "','" + iPageCount + "')");



                        var aPageInt = [];

                        var iStep = parseInt(so.NavEveryPage);
                        var iStart = (Math.floor((req.PageIndex - 1) / iStep) * iStep);
                        var iEnd = parseInt(iStart + iStep);

                        if (iEnd > iPageCount) iEnd = iPageCount;

                        for (var iNowIndex = iStart + 1; iNowIndex < iEnd + 1; iNowIndex++)
                        {
                            aPageInt.push(so.NavPageNumber.replace('{npn:n}', iNowIndex).replace('{npn:f}', "SWJGSF.PageGoto('" + req.Id + "','" + iNowIndex + "')").replace('{npn:c}', (iNowIndex == req.PageIndex) ? "SWCGSF_A_FOOT_NAV_HOVER" : ""));
                        }




                        nav_npn = aPageInt.join('');


                    }
                    nav_npc = so.NavPageChange
                    .replace('{npc:i}', '<input type="text" id="' + req.ClientId + '_nav_pagesize" value="' + req.PageSize + '" maxlength="3">')
                    .replace('{npc:s}', '<input type="text" id="' + req.ClientId + '_nav_pageindex" value="' + req.PageIndex + '">')
                    .replace('{npc:f}', 'SWJGSF.ChangeNav(\'' + id + '\')');



                }
                else if (req.ProcessType == "demo")
                {

                }




                aHtml.push(
                '<div class="SWCGSF_DIV_FOOT_NAV">' +
                so.NavPageHtml
                .replace("{nph:nps}", nav_nps)
                .replace("{nph:npu}", nav_npu)
                .replace("{nph:npl}", nav_npl)
                .replace("{nph:npr}", nav_npr)
                .replace("{nph:npn}", nav_npn)
                .replace("{nph:npc}", nav_npc)
                .replace("{nph:npe}", nav_npe)

                + '</div>'
                );
            }



            aHtml.push('</div>');



            $("#SWJGSF_Div_" + req.ClientId).html('<div class="SWCGSF_DIV_ALL">' + aHtml.join('') + '</div>');


            $("#jsonshow").text(o);


            if (so.FlagIndexNumber)
            {
                $('#GS_table_' + req.ClientId + ' tbody').children().each(

                function(index, n)
                {

                    if (index > 0)
                    {
                        $(n).mouseover(function() { $(n).addClass("SWCGSF_TABLE_TR_HOVER"); });
                        $(n).mouseout(function() { $(n).removeClass("SWCGSF_TABLE_TR_HOVER"); });
                    }


                    $($(n).children()[0]).before(index == 0 ? ('<th></th>') : ("<td>" + index + "</td>"));
                }
                );
            }



            //alert(obj.ListString[0].length);
        }




        //导出Excel
        SWJGSF.Excel = function(id)
        {

            SWJGSF.SubmitBefore(id);

            SrnprNetJsAllAlphaShow({ s: "l", m: "请选择显示内容", w: "400",h:"100", u: "/Web/GridShow/Excel.aspx?id=SWJGSF_Hidden_" + SWJGSF.Obj[id].ClientId });

        }
        SWJGSF.ExcelClose = function(id)
        {
            SrnprNetJsAllAlphaShow({ s: "c"});
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

            SrnprNetJsAllAlphaShow({ s: "f", c: s, m: "请选择显示内容", y: "SWJGSF.SetDisplay('" + id + "')", w: "400" });

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

        //开始执行排序操作
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


        //设置显示类型
        SWJGSF.Demo = function()
        {
            for (var p in SWJGSF.Obj)
            {

                SWJGSF.Obj[p].ProcessType = "demo";


            }

        }



        //执行查询 第二参数为空时则遍历所有
        SWJGSF.Query = function(id, sid)
        {
            var a = function() { };

            if (id == undefined)
            {
                id = "[0]";

            }
            if (id.indexOf('[') > -1)
            {

                var sIndex = id.replace('[', '').replace(']', '');

                if (!isNaN(sIndex))
                {

                    var index = 0;
                    for (var p in SWJGSF.Obj)
                    {

                        if (sIndex == index)
                        {
                            id = SWJGSF.Obj[p].Id;
                        }
                        index++;
                    }
                }
            }


            //定义提交参数
            var t = [];

            var vElms = $("#" + sid).children("[paramid][paramid<>'']");

            if (!vElms || vElms.length == 0)
            {
                vElms = $("[paramid][paramid<>'']");
            }


            vElms.each(

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
            SWJGSF.Obj[id].PageIndex = 1;
            SWJGSF.Obj[id].RowsCount = -1;

            SWJGSF.Ajax(id);


        }



        SWJGSF.WebTable = function()
        {

        }





    }

)();
}




  












































