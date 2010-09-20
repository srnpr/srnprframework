/// <reference path="SrnprWebJsWebWidgetFWF.js"/>




if (SWW && !SWW.DebugLog)
{

    SWW.DebugLog =
    {
        LogMessage:
        {

            sww1001: '开始调用',


            sww2001: '没有找到扩展函数{0}.{1}',
            sww2002: '系统尝试初始化第{0}次',
            sww2003: '开始执行函数{0}.{1}',
            sww2004: '没有找到函数{0}.{1}',
            sww2005: '没有找到扩展函数{0}.{1}',
            sww2006: '开始调用Ajax',
            sww2007: '加载Ajax成功',
            sww2008: '系统初始化成功，调用各脚本文件正常',
            sww2009: '打开对话框：{0}',


            sww0000: '无法找到'
        }
        ,
        ShowDebug: function ()
        {

            var h = [];
            h.push('<div class="SWW_CSS_GS_DIV_MAIN" style="width:95%;"><table><tr><th>时间</th><th>调用接口</th><th>日志内容</th><th>参数</th></tr>');

            for (var i = SWW.O.Log.Debug.length - 1, j = -1; i > j; i--)
            {


                var a = '', b = '';

                if (SWW.O.Log.Debug[i].m)
                {
                    if (this.LogMessage[SWW.O.Log.Debug[i].m])
                    {
                        a = this.LogMessage[SWW.O.Log.Debug[i].m];

                        if (SWW.O.Log.Debug[i].a)
                        {
                            if (typeof (SWW.O.Log.Debug[i].a) != 'object')
                            {
                                SWW.O.Log.Debug[i].a = [SWW.O.Log.Debug[i].a];
                                //SWW.O.Log.Debug[i].a.push();
                            }

                            for (var n = 0, m = SWW.O.Log.Debug[i].a.length; n < m; n++)
                            {
                                a = a.replace('{' + n + '}', SWW.O.Log.Debug[i].a[n]);
                            }

                        }

                    }
                }
                if (SWW.O.Log.Debug[i].c)
                {

                    b = '<a href="javascript:SWW.DebugLog.ShowInfo(' + i + ')">' + typeof (SWW.O.Log.Debug[i].c) + '</a>';

                }

                h.push('<tr><td>' + SWW.O.Log.Debug[i].d + '</td><td>' + SWW.O.Log.Debug[i].t + '</td><td>' + a + '</td><td>' + b + '</td></tr>');
            }
            h.push('</table></div>');
            SWW.W.Dialog.Open({ html: h.join(''), width: 800, title: '页面加载日志' });
        }
            ,

        ShowInfo: function (i)
        {
            SWW.W.Dialog.Open({ html: '<div id="debug"></div>', width: 800 });
            var a = typeof (SWW.O.Log.Debug[i].c) != 'string' ? SWW.F.JSON.StringToJson(SWW.O.Log.Debug[i].c) : SWW.O.Log.Debug[i].c;
            SWW.W.Dialog.Father().SWW.F.DOM.Text('debug', a);

            //SWW.F.DOM.Text('debug', a);
        }

    }

SWW.C.Flag.Debug = true;








SWW.F.JF.Ready(function ()
{
    SWW.J("body").append('<div style="width:100px;height:20px;background-color:#000;position:absolute;z-index:1;top:0px;left:0px;"><a style="color:#fff;" href="javascript:SWW.DebugLog.ShowDebug()">查看日志</a></div>');
});
}








