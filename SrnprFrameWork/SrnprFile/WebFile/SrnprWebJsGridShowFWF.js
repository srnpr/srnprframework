/// <reference path="jquery-1.3.2.min-vsdoc.js"/>
/// <reference path="json2.js"/>
/// <reference path="SrnprWebJsWebWidgetFWF.js"/>







if (SWW && !SWW.GS)
{

    SWW.GS =
    {
        //定义设置变量
        SetObj: {},


        //定义基本服务器交互变量
        Obj: {},



        Set: function (id, p, v)
        {
            SWW.GS.SetInit(id);
            SWW.GS.SetObj[id][p] = v;
        },

        SetInit: function (id)
        {

            if (!SWW.GS.SetObj[id])
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

                };

                SWW.GS.SetObj[id] = temp;
            };

            return SWW.GS.SetObj[id];
        }
        ,



        //初始化
        Init: function (s)
        {





            s.WidgetType = "GS";
            SWW.I(s);

            SWW.J("form").submit(function () { SWW.GS.SubmitBefore(s.Guid); });


        }
        ,

        SubmitBefore: function (id)
        {
            SWW.J("#SWJGSF_Hidden_" + SWW.GS.Obj[id].ClientId).val(JSON.stringify(SWW.GS.Obj[id]));
        },


        //提交请求
        Ajax: function (id)
        {





            SWW.F.SYS.Run(SWW.GS.Obj[id]);
        },

        AlertMsg: function (s)
        {
            alert(s);
        }
        ,


        //跳转页面
        PageGoto: function (id, iPage)
        {


            var iPageCount = Math.ceil(SWW.GS.Obj[id].RowsCount / SWW.GS.Obj[id].PageSize);

            if (iPage == "-")
            {
                iPage = SWW.GS.Obj[id].PageIndex - 1;
            }
            else if (iPage == "+")
            {
                iPage = SWW.GS.Obj[id].PageIndex + 1;
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



            SWW.GS.Obj[id].PageIndex = iPage;

            SWW.GS.Ajax(id);
        },


        //分组
        GroupTable: function (id, k)
        {
            SWW.GS.Obj[id].GroupValue = k;
            SWW.GS.Obj[id].PageIndex = 1;
            SWW.GS.Obj[id].RowsCount = -2;
            SWW.GS.Ajax(id);
        }
        ,

        //导航更换
        ChangeNav: function (id)
        {

            var s = SWW.J('#' + SWW.GS.Obj[id].ClientId + '_nav_pagesize').val();
            if (s && !isNaN(s) && s > 0)
            {
                SWW.GS.Obj[id].PageSize = parseInt(s);
            }
            var i = SWW.J('#' + SWW.GS.Obj[id].ClientId + '_nav_pageindex').val();

            if (i && !isNaN(i) && i > 0)
            {
                SWW.GS.Obj[id].PageIndex = parseInt(i);
            }

            SWW.GS.PageGoto(id, SWW.GS.Obj[id].PageIndex);

        },




        //执行成功时
        AjaxSuccess: function (id, o)
        {


            var obj = typeof (o) == "string" ? JSON.parse(o) : o;

            //重新赋上最新版返回参数
            var req = SWW.GS.Obj[id] = obj.Request;


            //开始尝试初始化设置
            var so = SWW.GS.SetInit(id);

            var aHtml = [];

            if (req.GroupKvd && so.FlagGroup)
            {
                var aGroupHtml = [];

                var dSumAll = 0;

                for (var i = 0, j = req.GroupKvd.length; i < j; i++)
                {
                    aGroupHtml.push('<li' + (req.GroupValue == req.GroupKvd[i].K ? ' class="SWW_GS_CSS_WEBTAB_UL_LI_HOVER" ' : '') + '><a href="javascript:SWW.GS.GroupTable(\'' + id + '\',\'' + req.GroupKvd[i].K + '\')">' + so.GroupTextTemp.replace('{gtt:d}', req.GroupKvd[i].D).replace('{gtt:v}', req.GroupKvd[i].V) + '</a></li>');

                    dSumAll += parseFloat(req.GroupKvd[i].V);
                }


                var sGroupSumAll = so.FlagGroupSum ? ('<li' + (!req.GroupValue ? ' class="SWW_GS_CSS_WEBTAB_UL_LI_HOVER" ' : '') + '><a href="javascript:SWW.GS.GroupTable(\'' + id + '\',\'\')"> ' + so.GroupTextTemp.replace('{gtt:d}', so.GroupSumText).replace('{gtt:v}', dSumAll) + '</a></li>') : '';

                aHtml.push('<div class="SWW_GS_CSS_DIV_WEBTAB"><ul>' + sGroupSumAll + aGroupHtml.join("") + '</ul></div>');
            }

            aHtml.push('<div class="SWW_GS_CSS_DIV_SHOWINFO">' + obj.HtmlString);

            //开始底部导航
            if (req.ShowColumn)
            {

                var iPageCount = Math.ceil(req.RowsCount / req.PageSize);

                var nav_nps = '', nav_npu = '', nav_npl = '', nav_npr = '', nav_npn = '', nav_npc = '', nav_npe = '';

                nav_npu = so.NavPageUser.replace('{npu:f}', "SWW.GS.ShowDisplay('" + req.Guid + "')");

                nav_npe = so.NavPageExcel.replace('{npe:f}', "SWW.GS.Excel('" + req.Guid + "')");


                if (req.ProcessType == "" || req.ProcessType == "server")
                {

                    nav_nps = so.NavPageSum.replace('{nps:i}', iPageCount > 0 ? req.PageIndex : 0).replace('{nps:c}', iPageCount).replace('{nps:r}', req.RowsCount);

                    if (req.RowsCount > 0)
                    {
                        nav_npl = so.NavPageLeft.replace('{npl:f}', "SWW.GS.PageGoto('" + req.Guid + "',1)").replace('{npl:p}', "SWW.GS.PageGoto('" + req.Guid + "','-')");
                        nav_npr = so.NavPageRight.replace('{npr:n}', "SWW.GS.PageGoto('" + req.Guid + "','+')").replace('{npr:l}', "SWW.GS.PageGoto('" + req.Guid + "','" + iPageCount + "')");



                        var aPageInt = [];

                        var iStep = parseInt(so.NavEveryPage);
                        var iStart = (Math.floor((req.PageIndex - 1) / iStep) * iStep);
                        var iEnd = parseInt(iStart + iStep);

                        if (iEnd > iPageCount) iEnd = iPageCount;

                        for (var iNowIndex = iStart + 1; iNowIndex < iEnd + 1; iNowIndex++)
                        {
                            aPageInt.push(so.NavPageNumber.replace('{npn:n}', iNowIndex).replace('{npn:f}', "SWW.GS.PageGoto('" + req.Guid + "','" + iNowIndex + "')").replace('{npn:c}', (iNowIndex == req.PageIndex) ? "SWW_GS_CSS_A_FOOT_NAV_HOVER" : ""));
                        }




                        nav_npn = aPageInt.join('');


                    }
                    nav_npc = so.NavPageChange
                    .replace('{npc:i}', '<input type="text" id="' + req.ClientId + '_nav_pagesize" value="' + req.PageSize + '" maxlength="3">')
                    .replace('{npc:s}', '<input type="text" id="' + req.ClientId + '_nav_pageindex" value="' + req.PageIndex + '">')
                    .replace('{npc:f}', 'SWW.GS.ChangeNav(\'' + id + '\')');



                }
                else if (req.ProcessType == "demo")
                {

                }




                aHtml.push(
                '<div class="SWW_GS_CSS_DIV_FOOT_NAV">' +
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



            SWW.J("#SWJGSF_Div_" + req.ClientId).html('<div class="SWW_GS_CSS_DIV_ALL">' + aHtml.join('') + '</div>');


            SWW.J("#jsonshow").text(o);


            if (so.FlagIndexNumber)
            {
                SWW.J('#GS_table_' + req.ClientId + ' tbody').children().each(

                function (index, n)
                {

                    if (index > 0)
                    {
                        SWW.J(n).mouseover(function () { SWW.J(n).addClass("SWW_GS_CSS_TABLE_TR_HOVER"); });
                        SWW.J(n).mouseout(function () { SWW.J(n).removeClass("SWW_GS_CSS_TABLE_TR_HOVER"); });
                    }


                    SWW.J(SWW.J(n).children()[0]).before(index == 0 ? ('<th></th>') : ("<td>" + index + "</td>"));
                }
                );
            }



            //alert(obj.ListString[0].length);
        }

        ,


        //导出Excel
        Excel: function (id)
        {

            SWW.GS.SubmitBefore(id);

            SWW.W.Dialog.Open({ title: '请选择显示内容', url: "/Web/GridShow/Excel.aspx?id=SWJGSF_Hidden_" + SWW.GS.Obj[id].ClientId, width: 400, height: 100 });

            //SrnprNetJsAllAlphaShow({ s: "l", m: "请选择显示内容", w: "400", h: "100", u: "/Web/GridShow/Excel.aspx?id=SWJGSF_Hidden_" + SWW.GS.Obj[id].ClientId });

        },
        ExcelClose: function (id)
        {
            //SrnprNetJsAllAlphaShow({ s: "c" });
            SWW.W.Dialog.Close();
        },






        //自定义显示字段
        ShowDisplay: function (id)
        {

            var s = "<ul>";
            for (var i = 0, j = SWW.GS.Obj[id].ShowColumn.length; i < j; i++)
            {
                s += "<li><input type=\"checkbox\" id=\"" + SWW.GS.Obj[id].ClientId + "_showcolumn_ckb_" + SWW.GS.Obj[id].ShowColumn[i].Guid + "\" " + (SWW.GS.Obj[id].ShowColumn[i].ShowDisplay == "n" ? "" : "checked=\"checked\"") + " />" + SWW.GS.Obj[id].ShowColumn[i].HeaderText + "</li>";
            }

            s += "</ul>";

            //SrnprNetJsAllAlphaShow({ s: "f", c: s, m: "请选择显示内容", y: "SWW.GS.SetDisplay('" + id + "')", w: "400" });

            SWW.W.Dialog.Open({ title: '请选择显示内容', width: 400, html: s, button: ["确定:SWW.GS.SetDisplay('" + id + "')"] });



        }
        ,
        //设置显示字段
        SetDisplay: function (id)
        {

            var source = SWW.W.Dialog.Father();

            for (var i = 0, j = SWW.GS.Obj[id].ShowColumn.length; i < j; i++)
            {
                if (source.SWW.J("#" + SWW.GS.Obj[id].ClientId + "_showcolumn_ckb_" + SWW.GS.Obj[id].ShowColumn[i].Guid).attr("checked") == true)
                {
                    SWW.GS.Obj[id].ShowColumn[i].ShowDisplay = "d";
                }
                else
                {
                    SWW.GS.Obj[id].ShowColumn[i].ShowDisplay = "n";
                }
            }

            SWW.GS.Ajax(id);
            //SrnprNetJsAllAlphaShow({ s: "c" });
            SWW.W.Dialog.Close();
        },

        //开始执行排序操作
        Sort: function (id, gid)
        {
            for (var i = 0, j = SWW.GS.Obj[id].ShowColumn.length; i < j; i++)
            {
                if (SWW.GS.Obj[id].ShowColumn[i].Guid == gid)
                {
                    SWW.GS.Obj[id].ShowColumn[i].OrderType = SWW.GS.Obj[id].ShowColumn[i].OrderType == "a" ? "e" : "a";
                }
                else
                {
                    if (SWW.GS.Obj[id].ShowColumn[i].OrderType != "n")
                    {
                        SWW.GS.Obj[id].ShowColumn[i].OrderType = "d";
                    }
                }
            }

            SWW.GS.Ajax(id);
        },

        F_Success: function (o)
        {
            SWW.GS.Obj[o.q.Guid] = o.q;
            SWW.GS.AjaxSuccess(o.q.Guid, o.s);
        },


        //设置显示类型
        Demo: function ()
        {
            for (var p in SWW.GS.Obj)
            {
                SWW.GS.Obj[p].ProcessType = "demo";
            }

        }
        ,


        //执行查询 第二参数为空时则遍历所有
        Query: function (id, sid)
        {
            var a = function () { };

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
                    for (var p in SWW.GS.Obj)
                    {

                        if (sIndex == index)
                        {
                            id = SWW.GS.Obj[p].Guid;
                        }
                        index++;
                    }
                }
            }


            //定义提交参数
            var t = [];

            var vElms = SWW.J("#" + sid).children("[paramid][paramid<>'']");

            if (!vElms || vElms.length == 0)
            {
                vElms = SWW.J("[paramid][paramid<>'']");
            }


            vElms.each(

             function (index, n)
             {

                 var el = SWW.J(n);
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



            SWW.GS.Obj[id].QueryDict = t;
            SWW.GS.Obj[id].PageIndex = 1;
            SWW.GS.Obj[id].RowsCount = -1;

            SWW.GS.Ajax(id);


        }
        ,


        WebTable: function ()
        {

        }
    }
}



  


