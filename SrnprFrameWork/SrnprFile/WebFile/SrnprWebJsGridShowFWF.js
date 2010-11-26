/// <reference path="jquery-1.3.2.min-vsdoc.js"/>
/// <reference path="SrnprWebJsWebWidgetFWF.js"/>







if (SWW && !SWW.GS)
{

    SWW.GS =
    {
        //定义设置变量
        SetObj: {},


        //定义基本服务器交互变量
        Obj: {},

        //扩充列对象
        Obj_Extend: {},



        DemoFlag: false,


        Set: function (id, p, v) {
            SWW.GS.SetInit(id);
            SWW.GS.SetObj[id][p] = v;
        },

        SetInit: function (id) {

            if (!SWW.GS.SetObj[id]) {

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
        Init: function (s) {





            s.WidgetType = "GS";
            SWW.I(s);

            SWW.J("form").submit(function () { SWW.GS.SubmitBefore(s.Guid); });


        }
        ,

        SubmitBefore: function (id) {

            SWW.J("#SWJGSF_Hidden_" + SWW.GS.Obj[id].ClientId).val(SWW.F.JSON.StringFromJson(SWW.GS.Obj[id]));

        },


        //提交请求
        Ajax: function (id) {





            SWW.F.SYS.Run(SWW.GS.Obj[id]);
        },

        AlertMsg: function (s) {
            alert(s);
        }
        ,


        //跳转页面
        PageGoto: function (id, iPage) {


            var iPageCount = Math.ceil(SWW.GS.Obj[id].RowsCount / SWW.GS.Obj[id].PageSize);

            if (iPage == "-") {
                iPage = SWW.GS.Obj[id].PageIndex - 1;
            }
            else if (iPage == "+") {
                iPage = SWW.GS.Obj[id].PageIndex + 1;
            }

            iPage = parseInt(iPage);

            if (iPage < 1) {
                iPage = 1;
            }
            else if (iPage > iPageCount) {
                iPage = iPageCount;
            }



            SWW.GS.Obj[id].PageIndex = iPage;

            SWW.GS.Ajax(id);
        },


        //分组
        GroupTable: function (id, k) {
            SWW.GS.Obj[id].GroupValue = k;
            SWW.GS.Obj[id].PageIndex = 1;
            SWW.GS.Obj[id].RowsCount = -2;
            SWW.GS.Ajax(id);
        }
        ,

        //导航更换
        ChangeNav: function (id) {

            var s = SWW.J('#' + SWW.GS.Obj[id].ClientId + '_nav_pagesize').val();
            if (s && !isNaN(s) && s > 0) {
                SWW.GS.Obj[id].PageSize = parseInt(s);
            }
            var i = SWW.J('#' + SWW.GS.Obj[id].ClientId + '_nav_pageindex').val();

            if (i && !isNaN(i) && i > 0) {
                SWW.GS.Obj[id].PageIndex = parseInt(i);
            }

            SWW.GS.PageGoto(id, SWW.GS.Obj[id].PageIndex);

        },




        //执行成功时
        AjaxSuccess: function (id, o) {


            var obj = typeof (o) == "string" ? SWW.F.JSON.JsonFromString(o) : o;

            //重新赋上最新版返回参数
            var req = SWW.GS.Obj[id] = obj.Request;


            //开始尝试初始化设置
            var so = SWW.GS.SetInit(id);


            var aHtml = [];


            //开始加载分组统计信息
            if (req.GroupKvd && so.FlagGroup) {
                var aGroupHtml = [];

                var dSumAll = 0;

                for (var i = 0, j = req.GroupKvd.length; i < j; i++) {
                    aGroupHtml.push('<li' + (req.GroupValue == req.GroupKvd[i].K ? ' class="SWW_CSS_GS_WEBTAB_UL_LI_HOVER" ' : '') + '><a href="javascript:SWW.GS.GroupTable(\'' + id + '\',\'' + req.GroupKvd[i].K + '\')">' + so.GroupTextTemp.replace('{gtt:d}', req.GroupKvd[i].D).replace('{gtt:v}', req.GroupKvd[i].V) + '</a></li>');

                    dSumAll += parseFloat(req.GroupKvd[i].V);
                }


                var sGroupSumAll = so.FlagGroupSum ? ('<li' + (!req.GroupValue ? ' class="SWW_CSS_GS_WEBTAB_UL_LI_HOVER" ' : '') + '><a href="javascript:SWW.GS.GroupTable(\'' + id + '\',\'\')"> ' + so.GroupTextTemp.replace('{gtt:d}', so.GroupSumText).replace('{gtt:v}', dSumAll) + '</a></li>') : '';

                aHtml.push('<div class="SWW_CSS_GS_DIV_WEBTAB"><ul>' + sGroupSumAll + aGroupHtml.join("") + '</ul></div>');
            }


            aHtml.push('<div class="SWW_CSS_GS_DIV_ALL"><div class="SWW_CSS_GS_DIV_SHOWINFO">');

            //开始加载显示的Table内容
            if (true) {
                var sShowDivBox = '<div class="SWW_CSS_GS_DIV_MAIN"><table id="GS_table_' + req.ClientId + '" class="SWW_CSS_GS_TABLE_SHOW">';

                //开始自适应宽度

                var iAutoWidth_SumTitle = 0;
                for (var i = 0, j = req.ShowColumn.length; i < j; i++) {
                    if (req.ShowColumn[i].ShowDisplay == 'd')
                        iAutoWidth_SumTitle += req.ShowColumn[i].HeaderText.length;
                }
                var iAutoWidth_DivWidth = $('#SWJGSF_Div_' + req.ClientId).width();
                if (!iAutoWidth_DivWidth) {
                    //iAutoWidth_DivWidth = $('body').width();


                    var eAutoWidth_Check_Father = $('#SWJGSF_Div_' + req.ClientId);
                    while (iAutoWidth_DivWidth <= 0) {

                        iAutoWidth_DivWidth = eAutoWidth_Check_Father.width();
                        eAutoWidth_Check_Father = eAutoWidth_Check_Father.parent();
                    }
                }

                if (iAutoWidth_SumTitle && iAutoWidth_DivWidth) {
                    if (Math.floor(iAutoWidth_DivWidth / 18) < iAutoWidth_SumTitle) {
                        sShowDivBox = sShowDivBox.replace('<div class="', '<div style="width:' + (iAutoWidth_DivWidth - 14) + 'px" class="SWW_CSS_GS_DIV_MAIN_Scroll ').replace('<table ', '<table style="width:' + (iAutoWidth_SumTitle * 16) + 'px" ');
                    }
                }


                aHtml.push(sShowDivBox);
                aHtml.push(obj.HtmlString);
                aHtml.push('</table></div>');
            }


            //开始底部导航
            if (req.ShowColumn) {

                var iPageCount = Math.ceil(req.RowsCount / req.PageSize);

                var nav_nps = '', nav_npu = '', nav_npl = '', nav_npr = '', nav_npn = '', nav_npc = '', nav_npe = '';

                nav_npu = so.NavPageUser.replace('{npu:f}', "SWW.GS.ShowDisplay('" + req.Guid + "')");

                nav_npe = so.NavPageExcel.replace('{npe:f}', "SWW.GS.Excel('" + req.Guid + "')");


                if (req.ProcessType == "" || req.ProcessType == "server") {

                    nav_nps = so.NavPageSum.replace('{nps:i}', iPageCount > 0 ? req.PageIndex : 0).replace('{nps:c}', iPageCount).replace('{nps:r}', req.RowsCount);

                    if (req.RowsCount > 0) {
                        nav_npl = so.NavPageLeft.replace('{npl:f}', "SWW.GS.PageGoto('" + req.Guid + "',1)").replace('{npl:p}', "SWW.GS.PageGoto('" + req.Guid + "','-')");
                        nav_npr = so.NavPageRight.replace('{npr:n}', "SWW.GS.PageGoto('" + req.Guid + "','+')").replace('{npr:l}', "SWW.GS.PageGoto('" + req.Guid + "','" + iPageCount + "')");



                        var aPageInt = [];

                        var iStep = parseInt(so.NavEveryPage);
                        var iStart = (Math.floor((req.PageIndex - 1) / iStep) * iStep);
                        var iEnd = parseInt(iStart + iStep);

                        if (iEnd > iPageCount) iEnd = iPageCount;

                        for (var iNowIndex = iStart + 1; iNowIndex < iEnd + 1; iNowIndex++) {
                            aPageInt.push(so.NavPageNumber.replace('{npn:n}', iNowIndex).replace('{npn:f}', "SWW.GS.PageGoto('" + req.Guid + "','" + iNowIndex + "')").replace('{npn:c}', (iNowIndex == req.PageIndex) ? "SWW_CSS_GS_A_FOOT_NAV_HOVER" : ""));
                        }




                        nav_npn = aPageInt.join('');


                    }
                    nav_npc = so.NavPageChange
                    .replace('{npc:i}', '<input type="text" id="' + req.ClientId + '_nav_pagesize" value="' + req.PageSize + '" maxlength="3">')
                    .replace('{npc:s}', '<input type="text" id="' + req.ClientId + '_nav_pageindex" value="' + req.PageIndex + '">')
                    .replace('{npc:f}', 'SWW.GS.ChangeNav(\'' + id + '\')');



                }
                else if (req.ProcessType == "demo") {

                }




                aHtml.push(
                '<div class="SWW_CSS_GS_DIV_FOOT_NAV">' +
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



            aHtml.push('</div></div>');



            SWW.J("#SWJGSF_Div_" + req.ClientId).html(aHtml.join(''));


            //SWW.J("#jsonshow").text(o);


            //开始添加数字序列
            if (so.FlagIndexNumber) {
                SWW.J('#GS_table_' + req.ClientId + ' tbody').children().each(

                function (index, n) {

                    if (index > 0) {
                        SWW.J(n).mouseover(function () { SWW.J(n).addClass("SWW_CSS_GS_TABLE_TR_HOVER"); });
                        SWW.J(n).mouseout(function () { SWW.J(n).removeClass("SWW_CSS_GS_TABLE_TR_HOVER"); });
                    }


                    SWW.J(SWW.J(n).children()[0]).before(index == 0 ? ('<th></th>') : ("<td>" + index + "</td>"));
                }
                );
            }


        }

        ,


        //导出Excel
        Excel: function (id) {

            SWW.GS.SubmitBefore(id);

            SWW.W.Dialog.Open({ title: '请选择显示内容', url: "/Ashx/Excel.aspx?id=SWJGSF_Hidden_" + SWW.GS.Obj[id].ClientId, width: 400, height: 100 });

            //SrnprNetJsAllAlphaShow({ s: "l", m: "请选择显示内容", w: "400", h: "100", u: "/Web/GridShow/Excel.aspx?id=SWJGSF_Hidden_" + SWW.GS.Obj[id].ClientId });

        },
        ExcelClose: function (id) {
            //SrnprNetJsAllAlphaShow({ s: "c" });
            SWW.W.Dialog.Close();
        },






        //自定义显示字段
        ShowDisplay: function (id) {



            var aHtml = [];
            /*
            aHtml.push('<div class="SWW_CSS_GS_DIV_ShowDisplay"><ul>');

            for (var i = 0, j = SWW.GS.Obj[id].ShowColumn.length; i < j; i++)
            {
            if (SWW.GS.Obj[id].ShowColumn[i].ShowDisplay != 'h')
            {
            aHtml.push('<li><input type="checkbox" value="' + i + '" id="' + SWW.GS.Obj[id].ClientId + '_showcolumn_ckb_' + SWW.GS.Obj[id].ShowColumn[i].Guid + '" ' + (SWW.GS.Obj[id].ShowColumn[i].ShowDisplay == 'n'? '' : 'checked="checked"') + ' />' + SWW.GS.Obj[id].ShowColumn[i].HeaderText + '</li>');
            }
            }

            aHtml.push('</ul></div>');
            */

            aHtml.push('<div style="width:350px;" id="SWW_GS_DIV_ShowDisplay_' + id + '"><div class="SWW_CSS_GS_DIV_SHOWINFO"><table class="SWW_CSS_GS_TABLE_SHOW">');
            aHtml.push('<tr><th style="width:80px;"><input type="checkbox" onclick="SWW.GS.ShowDisplaySelectAll(\'' + id + '\',this)">全选</th><th>字段</th></tr>');
            for (var i = 0, j = SWW.GS.Obj[id].ShowColumn.length; i < j; i++) {
                if (SWW.GS.Obj[id].ShowColumn[i].ShowDisplay != 'h') {
                    aHtml.push('<tr><td><input type="checkbox" value="' + i + '" id="' + SWW.GS.Obj[id].ClientId + '_showcolumn_ckb_' + SWW.GS.Obj[id].ShowColumn[i].Guid + '" ' + (SWW.GS.Obj[id].ShowColumn[i].ShowDisplay == 'n' ? '' : 'checked="checked"') + ' /></td><td>' + SWW.GS.Obj[id].ShowColumn[i].HeaderText + '</td></tr>');
                }
            }

            aHtml.push('</table></div></div>');
            SWW.W.Dialog.Open({ title: '请选择显示内容', width: 400, html: aHtml.join(''), button: ["确定:SWW.GS.SetDisplay('" + id + "')"] });



        }
        ,

        ShowDisplaySelectAll: function (id, chk) {
            var c = chk.checked;
            SWW.W.Dialog.Father().SWW.J('#SWW_GS_DIV_ShowDisplay_' + id + ' :checkbox').each(function (i) {
                $(this).attr("checked", c);
            }
            );
        },


        //设置显示字段
        SetDisplay: function (id) {

            var source = SWW.W.Dialog.Father();



            source.SWW.J('#SWW_GS_DIV_ShowDisplay_' + id + ' :checkbox').each(function (i) {
                var vCheckedIndex = $(this).val();
                if (vCheckedIndex != undefined && vCheckedIndex != null && vCheckedIndex != '' && !isNaN(vCheckedIndex)) {
                    if ($(this).attr('checked') == true) {
                        SWW.GS.Obj[id].ShowColumn[vCheckedIndex].ShowDisplay = "d";
                    }
                    else {
                        if (SWW.GS.Obj[id].ShowColumn[vCheckedIndex].ShowDisplay == 'd')
                            SWW.GS.Obj[id].ShowColumn[vCheckedIndex].ShowDisplay = "n";
                    }
                }


            });


            /*
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
            */
            SWW.GS.Ajax(id);
            //SrnprNetJsAllAlphaShow({ s: "c" });
            SWW.W.Dialog.Close();
        },

        //开始执行排序操作
        Sort: function (id, gid) {
            for (var i = 0, j = SWW.GS.Obj[id].ShowColumn.length; i < j; i++) {
                if (SWW.GS.Obj[id].ShowColumn[i].Guid == gid) {
                    SWW.GS.Obj[id].ShowColumn[i].OrderType = SWW.GS.Obj[id].ShowColumn[i].OrderType == "a" ? "e" : "a";
                }
                else {
                    if (SWW.GS.Obj[id].ShowColumn[i].OrderType != "n") {
                        SWW.GS.Obj[id].ShowColumn[i].OrderType = "d";
                    }
                }
            }

            SWW.GS.Ajax(id);
        },


        F_Init: function (o) {

            if (this.DemoFlag) {
                o.ProcessType = "demo";
            }


            this.SetQueryParam(o);


        },


        F_Success: function (o) {
            SWW.GS.Obj[o.q.Guid] = o.q;
            SWW.GS.AjaxSuccess(o.q.Guid, o.s);
        },


        //设置显示类型
        Demo: function () {

            this.DemoFlag = true;



        }
        ,
        //数据绑定
        OnDataRowBind: function (s, f) {
            ///	<summary>
            ///  绑定列函数操作
            ///	</summary>
            ///	<param name="s" type="str">
            ///		操作GridShow的Id
            ///	</param>
            ///	<param name="f" type="fun">
            ///		绑定的操作函数不加括号  函数匿名传递一个参数  参数支持属性 RowIndex:当前行序号,从表头开始计数  CellCount行的单元格统计  Cell单元格对象集合  CellTitle根据表头的单元格对象集合
            ///	</param>

            SWW.A('GS', 'Success', s, function (e) { SWW.GS.EventRowBind(e.s.Request.Guid, f); });
        },


        OnDataSuccess: function (s, f) {



            SWW.A('GS', 'Success', s, function (e) { f(e.s.Request); });

        },




        EventRowBind: function (g, f) {
            var c = 'GS_table_' + SWW.GS.Obj[g].ClientId;


            var aTitle = [];



            SWW.J('#' + c).children().eq(0).children().eq(0).children().each(function (e) { aTitle.push(SWW.F.STR.Trim(SWW.J(this).text().replace('↑', '').replace('↓', ''))); });


            if (document.getElementById(c)) {
                for (var i = 0, j = document.getElementById(c).rows.length; i < j; i++) {
                    var row = {};
                    row.RowIndex = i;
                    row.CellCount = aTitle.length;
                    row.Cell = [];
                    row.CellTitle = {};
                    row.TableId = c;
                    row.Row = SWW.J('#' + c).children().eq(0).children().eq(i);
                    row.BaseGuid = g;
                    row.Guid = null;
                    row.ExtendFunc = null;

                    SWW.J('#' + c).children().eq(0).children().eq(i).children().each(function (e) { row.Cell.push(SWW.J(this)); row.CellTitle[aTitle[e]] = SWW.J(this); });



                    f(row);
                }
            }

        },
        ///	<summary>
        ///  绑定列扩展内容事件
        ///	</summary>
        ///	<param name="oRow" type="obj">
        ///		绑定扩展的列对象
        ///	</param>
        ///	<param name="f" type="fun">
        ///		点击时执行的函数
        ///	</param>
        ///	<param name="onTarget" type="obj">
        ///		绑定扩展的列对象
        ///	</param>
        ExtendFunction: function (oRow, f, onTarget) {
            ///	<summary>
            ///  绑定列扩展内容事件
            ///	</summary>






            if (oRow.RowIndex != 0) {
                oRow = SWW.F.OBJ.Clone(oRow);

                if (!this.Obj_Extend[oRow.BaseGuid]) {
                    this.Obj_Extend[oRow.BaseGuid] = { sum: 0 };

                }

                if (!oRow.Guid) {
                    oRow.Guid = SWW.F.SYS.GetGuid();
                }

                if (!onTarget) {
                    onTarget = oRow.Row;

                    onTarget.children().eq(0).prepend('<span class="SWW_CSS_GS_TABLE_TD_SPAN_Extend"><div id="swwgs_extend_Flag_' + oRow.Guid + '">+</div></span>');
                }
                else {
                    oRow.Guid = SWW.F.SYS.GetGuid();
                }



                if (!this.Obj_Extend[oRow.BaseGuid][oRow.Guid]) {
                    this.Obj_Extend[oRow.BaseGuid][oRow.Guid] = oRow;
                }


                if (!onTarget.attr('swwgs_extend_rowindex'))
                    onTarget.click(SWW.GS.ExtendClickEvent);
                onTarget.attr('swwgs_extend_rowindex', oRow.Guid);
                onTarget.attr('swwgs_extend_baseguid', oRow.BaseGuid);

                this.Obj_Extend[oRow.BaseGuid][oRow.Guid].ExtendFunc = f;


            }

        },

        ExtendSetHtml: function (e, s) {

            if (SWW.F.OBJ.IsObj(e))
                e = e.Guid;

            SWW.J('#swwgs_extend_td_' + e).html(s);
        },

        ExtendClickEvent: function () {



            var iIndex = SWW.J(this).attr('swwgs_extend_rowindex');
            var BaseGuid = SWW.J(this).attr('swwgs_extend_baseguid');

            if (!SWW.GS.Obj_Extend[BaseGuid][iIndex].show) {
                SWW.GS.Obj_Extend[BaseGuid][iIndex].show = 1;
                //var tr = SWW.J(this).is('tr') ? SWW.J(this) : SWW.J(this).parents('tr');
                // var tr = SWW.J('#' + SWW.GS.Obj_Extend[BaseGuid][iIndex].TableId).children().eq(0).children().eq(SWW.GS.Obj_Extend[BaseGuid][iIndex].RowIndex + SWW.GS.Obj_Extend[BaseGuid].sum);

                //SWW.GS.Obj_Extend[BaseGuid].sum++;
                var tr = SWW.GS.Obj_Extend[BaseGuid][iIndex].Row;

                tr.after('<tr id="' + iIndex + '"><td style="" colspan="100" id="swwgs_extend_td_' + iIndex + '"></td></tr>');

                SWW.GS.Obj_Extend[BaseGuid][iIndex].ExtendFunc(SWW.GS.Obj_Extend[BaseGuid][iIndex]);

                if (SWW.J('#swwgs_extend_Flag_' + iIndex)) { SWW.J('#swwgs_extend_Flag_' + iIndex).html('-') };

            }
            else if (SWW.GS.Obj_Extend[BaseGuid][iIndex].show == '1') {
                SWW.GS.Obj_Extend[BaseGuid][iIndex].show = 0;
                //tr.next().css("display", 'none');

                SWW.J('#' + iIndex).css('display', 'none');
                if (SWW.J('#swwgs_extend_Flag_' + iIndex)) { SWW.J('#swwgs_extend_Flag_' + iIndex).html('+') };
            }
            else {
                SWW.GS.Obj_Extend[BaseGuid][iIndex].show = 1;
                SWW.J('#' + iIndex).css('display', '');
                if (SWW.J('#swwgs_extend_Flag_' + iIndex)) { SWW.J('#swwgs_extend_Flag_' + iIndex).html('-') };
            }

        },


        OnQueryBefore: function (id, f) {

            SWW.A("GS", "BeforeQuery", this.ZZZ_ReLoadGuid(id), f);

        },


        Display: function (id, b) {

            SWW.F.DOM.Display('SWW_GS_DIV_ShowDisplay_' + this.ZZZ_ReLoadGuid(id), b);

        },



        ZZZ_ReLoadGuid: function (id) {
            if (id == undefined || !id) {
                id = "[0]";

            }
            var iIndex = -1;
            if (id.indexOf('[') > -1) {

                var sIndex = id.replace('[', '').replace(']', '');
                if (!isNaN(sIndex)) {
                    iIndex = parseInt(Math.abs(sIndex));
                }
            }


            var iNow = 0;
            for (var p in SWW.GS.Obj) {

                if (iIndex > -1) {
                    if (iNow == iIndex) {
                        id = SWW.GS.Obj[p].Guid;
                    }
                }
                else if (SWW.GS.Obj[p].Id == id) {
                    id = SWW.GS.Obj[p].Guid;
                }
            }

            return id;
        },







        //执行查询 第二参数为空时则遍历所有
        Query: function (id, sid) {


            id = this.ZZZ_ReLoadGuid(id);



            if (id && SWW.GS.Obj[id]) {

                this.SetQueryParam(SWW.GS.Obj[id], sid);

                SWW.GS.Obj[id].PageIndex = 1;
                SWW.GS.Obj[id].RowsCount = -1;



                SWW.GS.Ajax(id);
            }
            else {
                SWW.F.SYS.Alert(SWW.M.ME.Query);
            }

        }
        ,




        SetQueryParam: function (oGS, sid) {

            //定义提交参数
            var t = [];

            var vElms = SWW.J("#" + sid).children("[paramid][paramid<>'']");

            if (!vElms || vElms.length == 0) {
                vElms = SWW.J("[paramid][paramid<>'']");
            }


            vElms.each(

             function (index, n) {

                 var el = SWW.J(n);
                 var vl = el.val();
                 if (el.is("input") && (n.type == "checkbox" || n.type == "radio") && !n.checked) {
                     vl = "";
                 }

                 if (vl != "") {
                     var jLength = t.length;

                     for (var i = 0, j = t.length; i < j; i++) {
                         if (t[i].Key == n.paramid) {
                             t[i].Value = t[i].Value + "," + vl;
                             jLength = -1;
                         }
                     }

                     if (jLength > -1) {
                         t.push({ Key: n.paramid, Value: vl });

                     }

                 }

             }
            );



            oGS.QueryDict = t;



            //执行扩展函数
            SWW.F.SYS.ExecAF({ f: 'BeforeQuery', w: 'GS', d: oGS.Guid, e: oGS });

        },



        WebTable: function () {

        }
    }
}



  


