/// <reference path="jquery-1.3.2.min-vsdoc.js"/>

/******************************************************
Description: 核心类文件 所有widget使用的初始化及加载文件
            目前使用Jquery框架 所有引用调用J
            
            公开属性：
            J：jQuery
            O：表示对象系列
            C：配置文件
            I：初始化
            F：外部调用函数
            Z：基类使用自身操作
            M：消息系列配置
            A：扩展系列
            W：调用控件系列
            
            
            参数定义：
            第一标识
            s:字符串(str)
            o:对象(obj)
            i:数字(int)
            f:函数(fun)
            b:布尔型(bool)
            a:数组(arr)

            第二标识：
            [y]:必填参数
            n:非必填参数

            
Author: Liudpc
Create Date: 2010-8-18 9:27:36
******************************************************/

if (!window.SWW)
{
    window.SWW =
   {
       //配置
       C:
       {
           //系统加载脚本文件  u：脚本文件名称  n：需要加载的其他脚本  w：window基本名称（全局）  q：服务端类名称（JSON反向解析使用,如果不存在则表明不需初始化） 
           JS:
           {
               Json: { u: 'json2.js', w: 'JSON' },
               JQuery: { u: 'jquery-1.4.2.min.js', w: 'jQuery' },
               GS: { u: 'SrnprWebJsGridShowFWF.js', n: ['JQuery', 'Json'], q: 'GridShowRequestWWE' },
               LS: { u: 'SrnprWebJsListShowFWF.js', n: ['JQuery', 'Json'], q: 'ListShowRequestWWE' },
               TD: { u: 'SrnprWebJsToolDialogFWF.js', n: ['JQuery', 'Json'] },
               SWW: 'SrnprWebJsWebWidgetFWF.js'
           },

           //Ajax相关配置
           Ajax:
           {
               Url: '/Asmx/WebWidgetHandler.ashx'
           },

           //基本命名空间
           BaseNamespace: 'http://srnprframework/srnprweb',

           Flag: { Debug: false },


           //已经加载的js配置
           JSLoad: {},

           //初始化时配置
           Init:
           {
               //当前加载次数
               N: 0,
               //最大加载次数
               M: 30,
               //加载时间间隔
               T: 200,
               //是否添加ready加载函数
               LoadFlag: false
           },

           //基本信息
           BaseInfo:
           {
               //版本编号
               Version: '1.0.0.0'
           }
       },

       //消息系列
       M:
        {
            //系统错误时提示消息
            SE:
            {
                ET: '【系统消息】：系统出现异常错误，请联系管理员',
                EN: '\n【错误标识】：',
                EM: '\n【错误内容】：',
                IM: '系统尝试初始化失败！',
                AS: '无法加载类型',
                FEF: '加载类型{0}函数名{1}时错误，参数为：{2}'
            },
            ME:
            {
                Load: '正在加载中……'
            }

        },

       //Jquery适配器  
       J: jQuery,

       //Req提交参数  Res返回参数  AF扩展函数  Guid唯一标识集 系统自动检测全局唯一编号
       O: { Req: {}, Res: {}, AF: {}, Guid: {}, Log: { Debug: []} },

       //扩展函数系列
       A: function (t, f, id, fu)
       {
           ///	<summary>
           ///  扩展调用接口
           ///	</summary>
           ///	<param name="t" type="string">
           ///		类型
           ///	</param>
           ///	<param name="f" type="string">
           ///		函数操作 目前支持：Success(调用Ajax成功后)
           ///	</param>
           ///	<param name="id" type="string">
           ///		编号
           ///	</param>
           ///	<param name="fu" type="string">
           ///		函数  扩展当操作执行时的执行函数 不同函数所需参数不一致
           ///	</param>
           if (!SWW.O.AF[t])
           {
               SWW.O.AF[t] = {};
           }
           if (!SWW.O.AF[t][f])
           {
               SWW.O.AF[t][f] = {};
           }
           SWW.O.AF[t][f][id] = fu;
       },

       //函数系列
       F:
       {

           DOM:
           {
               Html: function (sElement, snHtml)
               {
                   ///	<summary>
                   ///  设置或返回元素的html内容
                   ///	</summary>
                   ///	<param name="sElement" type="str">
                   ///		元素名称
                   ///	</param>
                   ///	<param name="snHtml" type="str">
                   ///		元素的html
                   ///	</param>
                   if (snHtml)
                   {
                       SWW.J('#' + sElement).html(snHtml);
                   }
                   else
                   {
                       return SWW.J('#' + sElement).html();
                   }
               },
               Text: function (sElement, snText)
               {
                   ///	<summary>
                   ///  设置或返回元素的Text
                   ///	</summary>
                   ///	<param name="sElement" type="str">
                   ///		元素名称
                   ///	</param>
                   ///	<param name="snText" type="str">
                   ///		元素的Text
                   ///	</param>
                   if (snText)
                   {
                       SWW.J('#' + sElement).text(snText);
                   }
                   else
                   {
                       return SWW.J('#' + sElement).text();
                   }
               },
               Value: function (sElement, snValue)
               {
                   ///	<summary>
                   ///  设置或返回元素的值
                   ///	</summary>
                   ///	<param name="sElement" type="str">
                   ///		元素名称
                   ///	</param>
                   ///	<param name="snText" type="str">
                   ///		元素的值
                   ///	</param>
                   if (snValue)
                   {
                       SWW.J('#' + sElement).val(snValue);
                   }
                   else
                   {
                       return SWW.J('#' + sElement).val();
                   }
               },
               Get: function (sn)
               {
                   ///	<summary>
                   ///  返回元素
                   ///	</summary>
                   ///	<param name="sn" type="str">
                   ///		元素名称
                   ///	</param>
                   return sn ? document.getElementById(sn) : document;
               },


               Auto: function (sE, sV)
               {

                   sE = '#' + sE;
                   var bV = SWW.J(sE).is("input|textarea");


                   if (sV)
                   {
                       bV ? SWW.J(sE).val(sV) : SWW.J(sE).html(sV);
                   }
                   else
                   {
                       return bV ? SWW.J(sE).val() : SWW.J(sE).html();
                   }

               },

               Display: function (s, bn)
               {
                   ///	<summary>
                   ///  显示
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		元素名称
                   ///	</param>
                   ///	<param name="bn" type="bool">
                   ///		是否显示 默认不显示
                   ///	</param>


                   this.Get(s).style.display = (!bn ? 'none' : '');


               },
               Url: function (s, on)
               {
                   ///	<summary>
                   ///  返回url参数
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		url地址
                   ///	</param>
                   ///	<param name="on" type="obj">
                   ///		链接地址上添加的对象   如果有该参数则返回的是拼接链接的字符串
                   ///	</param>
                   var r;
                   if (!s)
                   {
                       s = location.href;
                   }

                   var iIndex = s.indexOf('?');
                   if (!on)
                   {
                       if (iIndex > -1)
                       {
                           r = {};

                           var u = s.substr(iIndex + 1);
                           var a = u.split('&');
                           for (var i = 0, j = a.length; i < j; i++)
                           {
                               var iN = a[i].indexOf('=');

                               r[a[i].substr(0, iN)] = a[i].substr(iN + 1);
                           }
                       }

                       return r;
                   }
                   else
                   {
                       return s + (iIndex > -1 ? '&' : '?') + SWW.F.SYS.GetObjPrototype(on);
                   }

               }
           },


           JF:
           {
               Ready: function (f)
               {
                   SWW.J().ready(f);
               }
           }
           ,


           STR:
           {

               Pad: function (sB, sP, iM)
               {
                   var a = '';

                   sB = sB.toString();
                   while (sB.length < Math.abs(iM))
                   {
                       sB = iM > 0 ? (sP + sB) : (sB + sP);
                   }
                   return sB;
               },
               HtmlEncode: function (s)
               {

                   if (s.length == 0) return "";
                   s = s.replace(/&/g, "&amp;");
                   s = s.replace(/</g, "&lt;");
                   s = s.replace(/>/g, "&gt;");
                   s = s.replace(/\'/g, "&#39;");
                   s = s.replace(/\"/g, "&quot;");
                   return s;
               },
               HtmlDecode: function (str)
               {
                   if (s.length == 0) return "";
                   s = s.replace(/&amp;/g, "&");
                   s = s.replace(/&lt;/g, "<");
                   s = s.replace(/&gt;/g, ">");
                   s = s.replace(/&#39;/g, "\'");
                   s = s.replace(/&quot;/g, "\"");
                   return s;
               },


               StringToDate: function (s)
               {
                   if (!s)
                   {
                       s = new Date();
                   }
                   else if (typeof (s) == 'string')
                   {
                       s = new Date(Date.parse(s.replace(/-/g, "/")));

                   }
                   return s;
               },

               DateTime: function (d, s)
               {
                   d = this.StringToDate(d);

                   if (!s)
                   {
                       s = "yyyy-MM-dd hh:mm:ss";
                   }

                   return s.replace('yyyy', d.getYear()).replace('MM', SWW.F.STR.Pad(d.getMonth(), '0', 2)).replace('dd', SWW.F.STR.Pad(d.getDate(), '0', 2)).replace('hh', SWW.F.STR.Pad(d.getHours(), '0', 2)).replace('mm', SWW.F.STR.Pad(d.getMinutes(), '0', 2)).replace('ss', SWW.F.STR.Pad(d.getSeconds(), '0', 2)).replace('ms', d.getMilliseconds());


               },
               Format: function (s, a)
               {
                   for (var i = 0, j = a.length; i < j; i++)
                   {
                       var r = new RegExp("\{" + i + "\}","g");
                      
                       
                       s = s.replace(r, a[i]);
                   }
                   return s;
               }

           },



           SYS:
            {
                ///	<summary>
                ///  扩展调用接口
                ///	</summary>

                Alert: function (s)
                {
                    ///	<summary>
                    ///  弹出提示信息
                    ///	</summary>
                    ///	<param name="s" type="str">
                    ///		提示信息
                    ///	</param>
                    alert(s);
                },




                Error: function (o)
                {
                    ///	<summary>
                    ///  出现严重错误时提示
                    ///	</summary>
                    ///	<param name="o" type="obj">
                    ///  错误内容  o{n:错误标识,m:错误内容,[p]:array 替换参数}
                    ///	</param>

                    if (o.p && o.m)
                    {
                        for (var i = 0, j = o.p.length; i < j; i++)
                        {
                            o.m = o.m.replace('{' + i + '}', o.p[i]);
                        }
                    }

                    this.Alert(SWW.M.SE.ET + (o.n ? SWW.M.SE.EN + o.n : '') + (o.m ? SWW.M.SE.EM + o.m : ''));
                },

                Run: function (o)
                {
                    ///	<summary>
                    ///  执行提交函数
                    ///	</summary>
                    ///	<param name="o" type="obj">
                    ///		request
                    ///	</param>

                    return SWW.Z.Ajax(o);
                },

                GetGuid: function (r)
                {
                    ///	<summary>
                    ///  生成Guid
                    ///	</summary>
                    ///	<param name="r" type="string">
                    ///		生成模板 example:8-12-16-20
                    ///	</param>
                    var a = (r ? r : '8-12-16-20').split('-');
                    var al = a.length;
                    var guid = "guid";
                    for (var i = 1 + guid.length; i <= 32; i++)
                    {
                        var g = Math.floor(Math.random() * 16.0).toString(16);
                        guid += g;
                        for (var n = 0; n < al; n++)
                        {
                            if (i == a[n])
                            {
                                guid += '-';
                            }
                        }
                    }

                    //判断是否重复
                    if (SWW.O.Guid[guid])
                    {
                        guid = this.GetGuid();
                    }
                    else
                    {
                        SWW.O.Guid[guid] = guid;
                    }

                    return guid;

                },

                ItemBase: function ()
                {
                    ///	<summary>
                    ///  返回基本对象
                    ///	</summary>

                    return { __type: '' };
                },


                InitReq: function (e)
                {
                    ///	<summary>
                    ///  重新初始化对象
                    ///	</summary>
                    ///	<param name="e" type="obj">
                    ///		request
                    ///	</param>

                    if (e && e.WidgetType && (!e.__type || !e.Guid))
                    {
                        var o = this.ItemBase();
                        if (!e.__type && SWW.C.JS[e.WidgetType] && SWW.C.JS[e.WidgetType].q)
                        {
                            o.__type = SWW.C.JS[e.WidgetType].q + ':' + SWW.C.BaseNamespace;

                        }
                        if (!e.Guid)
                        {
                            e.Guid = this.GetGuid();
                        }
                        for (var p in e)
                        {
                            o[p] = e[p];
                        }
                        e = o;
                    }
                    return e;
                },

                ExecFunc: function (o)
                {
                    ///	<summary>
                    ///  执行函数
                    ///	</summary>
                    ///	<param name="o" type="object">
                    ///	对象{t:类型,f:函数名称,e:参数}
                    ///  目前支持函数：
                    ///  F_Success({q:提交对象,s:返回对象})
                    ///	</param>

                    if (SWW[o.t] && SWW[o.t][o.f])
                    {
                        SWW[o.t][o.f](o.e);
                        if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.f.sys.execfunc.exec', o);
                    }
                    else
                    {
                        //SWW.F.SYS.Error({ n: 'SWW.F.SYS.ExecFunc', m: SWW.M.SE.FEF, p: [o.t, o.f, this.GetObjPrototype(o.e)] });
                        if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.f.sys.execfunc.notfunction', o);
                    }
                },


                GetObjPrototype: function (o)
                {
                    ///	<summary>
                    ///  得到一个对象的属性字符串
                    ///	</summary>
                    ///	<param name="o" type="obj">
                    ///		对象
                    ///	</param>


                    var r = [];

                    for (var p in o)
                    {
                        r.push('' + p + '=' + o[p]);
                    }

                    return r.join('&');


                },

                ExecAF: function (o)
                {
                    ///	<summary>
                    ///  执行扩展函数
                    ///	</summary>
                    ///	<param name="t" type="obj">
                    ///		对象{f:函数名称,q:request,e:参数}
                    ///	</param>

                    if (SWW.O.AF[o.q.WidgetType] && SWW.O.AF[o.q.WidgetType][o.f][o.q.Id])
                    {
                        SWW.O.AF[o.q.WidgetType][o.f][o.q.Id](o.e);

                        if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.f.sys.execaf.exec', o);
                    }
                    else
                    {
                        if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.f.sys.execaf.notfunction', o);
                    }

                }
            }

       },

       //内部调用系列
       Z:
       {


           DebugLog: function (s, o)
           {

               if (SWW.C.Flag.Debug)
               {
                   SWW.O.Log.Debug.push({ d: SWW.F.STR.DateTime(null, 'yyyy-MM-dd hh:mm:ss.ms'), t: s, c: o });
               }

           },

           BasePath: function ()
           {
               ///	<summary>
               ///  返回文件所在路径
               ///	</summary>

               var d = '';
               var e = document.getElementsByTagName('script');
               for (var f = 0; f < e.length; f++)
               {
                   var g = e[f].src.match(/(^|.*[\\\/])SrnprWebJsWebWidgetFWF.js(?:\?.*)?/i);
                   if (g)
                   {
                       d = g[1];
                       break;
                   }
               }

               if (d.indexOf('://') == -1)
               {
                   if (d.indexOf('/') === 0) d = location.href.match(/^.*?:\/\/[^\/]*/)[0] + d;
               }
               else
               {
                   //d = location.href.match(/^[^\?]*\/(?:)/)[0] + d;
               }

               return d;
           },
           AddScript: function (u)
           {
               ///	<summary>
               ///  添加脚本文件
               ///	</summary>
               ///	<param name="u" type="string">
               ///		地址
               ///	</param>

               SWW.J.getScript(this.BasePath() + u);

           },

           Ajax: function (e)
           {
               ///	<summary>
               ///  提交请求
               ///	</summary>
               ///	<param name="e" type="string">
               ///		调用的元素数组
               ///	</param>


               var t = {};
               if (e.length)
               {
                   t.RQ = e;
               } else
               {
                   t.RQ = [];
                   t.RQ.push(e);
               }


               //开始检测是否定义了正确的类型并是否需要重新初始化
               for (var i = 0, j = t.RQ.length; i < j; i++)
               {
                   t.RQ[i] = SWW.F.SYS.InitReq(t.RQ[i]);

               }

               if (SWW.C.Flag.Debug)
               {
                   SWW.Z.DebugLog('sww.z.ajax.onbefore', t);
               }

               //开始提交数据
               SWW.J.ajax(
                {
                    url: SWW.C.Ajax.Url,
                    type: "POST",
                    data: "json=" + JSON.stringify(t),
                    success: function (s) { SWW.Z.AjaxSuccess(s); },
                    error: function (XMLHttpRequest, textStatus) { SWW.F.SYS.Error({ n: 'SWW.Z.Ajax', m: textStatus }) }
                });
           },

           AjaxSuccess: function (s)
           {
               ///	<summary>
               ///  执行成功时调用
               ///	</summary>
               ///	<param name="s" type="str">
               ///		响应内容
               ///	</param>

               if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.z.ajaxsuccess.onsuccess', s);

               var json = JSON.parse(s);

               for (var i = 0, j = json.RS.length; i < j; i++)
               {


                   if (json.RS[i].WidgetType && SWW[json.RS[i].WidgetType])
                   {

                       SWW.O.Res[json.RQ[i].Guid] = json.RS[i];

                       //执行标准函数
                       SWW.F.SYS.ExecFunc({ t: json.RS[i].WidgetType, f: 'F_Success', e: { q: json.RQ[i], s: json.RS[i]} });

                       //执行扩展函数
                       SWW.F.SYS.ExecAF({ f: 'Success', q: json.RQ[i], e: { s: s} });

                   }
                   else
                   {
                       SWW.F.SYS.Error({ n: 'SWW.Z.AjaxSuccess', m: x });
                   }

               }
           },

           CheckInit: function ()
           {
               ///	<summary>
               ///  判断系统初始化加载加载
               ///	</summary>


               if (!SWW.C.Init.LoadFlag)
               {
                   SWW.J().ready(function () { SWW.Z.Init(); });
                   SWW.C.Init.LoadFlag = true;
               }
           },


           Init: function ()
           {
               ///	<summary>
               ///  系统初始化时调用
               ///	</summary>


               if (SWW.O)
               {
                   //定义是否加载完成
                   var bFlag = true;

                   //开始判断是否所有加载完成
                   if (bFlag)
                   {
                       for (var p in SWW.O.Req)
                       {
                           if (!SWW[SWW.O.Req[p].WidgetType])
                           {
                               bFlag = false;
                               break;
                           }
                       }
                   }


                   //累加当前调用次数
                   SWW.C.Init.N++;

                   if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.z.init.initnow', SWW.C.Init.N);


                   //判断是否超过最大调用次数限制
                   if (SWW.C.Init.N < SWW.C.Init.M)
                   {

                       //如果所有加载完成
                       if (bFlag)
                       {
                           this.InitSuccess();
                       }
                       else
                       {
                           //重新调用执行
                           setTimeout("SWW.Z.Init()", SWW.C.Init.T);
                       }
                   }
                   else
                   {
                       //提示错误信息
                       SWW.F.SYS.Error({ n: 'SWW.Z.Init', m: SWW.M.SE.IM });
                   }
               }



           }
           ,
           InitSuccess: function ()
           {

               if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.z.initsuccess.success', '');
               var sub = [];
               for (var t in SWW.O.Req)
               {

                   SWW.F.SYS.ExecFunc({ t: SWW.O.Req[t].WidgetType, f: "F_Init", e: SWW.O.Req[t] });

                   if (SWW.O.Req[t].__type)
                   {
                       sub.push(SWW.O.Req[t]);
                   }
               }
               if (sub.length > 0)
               {
                   this.Ajax(sub);
               }
           }
       },


       W:
       {

           Dialog:
           {
               Init:
               {

                   Temp:
                   {
                       //控件高度
                       height: 400,
                       //宽度
                       width: 400,
                       //顶部距离
                       top: 100,
                       //左边距 默认-1表示系统自动居中
                       left: -1,
                       //弹出框表头
                       title: '',
                       //弹出框html内容
                       html: '',
                       //弹出框编号
                       id: '',
                       //背景透明度
                       opacity: 50,
                       //弹出框iframe链接
                       url: '',
                       //按钮
                       button: [],
                       //是否自动状态保存 0表示不自动保存状态 2表示自动保存状态
                       save: 0,
                       //是否自动跟随滚动 1为滚动 2为不滚动
                       scroll: 1,
                       //自身所加载的window对象
                       self: null,
                       //当前加载的序号
                       current: 0,
                       //Guid编号
                       guid: '',
                       //样式编号
                       cssid: 'SWW_CSS_W_Dialog_'
                   }
                   ,
                   Config:
                   {

                       BgId: 'SWW_SWW_F_BOX_INIT_CONFIG_BGID',
                       DefaultId: 'SWW_SWW_F_BOX_INIT_CONFIG_DEFAULTID',

                       Opactity: 50,
                       BgColor: '#000',
                       CountDialog: 0

                   },

                   ObjArray: [],

                   Father: function ()
                   {
                       return top;
                   },

                   AddBg: function ()
                   {

                       if (!SWW.F.DOM.Get(this.Config.BgId))
                       {

                           var e = document.createElement("div");
                           e.id = this.Config.BgId;
                           if (SWW.J.browser.msie && SWW.J.browser.version == '6.0')
                               e.innerHTML = '<iframe style="width:100%;height:100%;border:none;filter:alpha(opacity=0);opacity:0;"></iframe>';



                           var co = this.Config;

                           with (e.style)
                           {
                               height = Math.max(window.screen.height, document.body.offsetHeight + 50) + "px";
                               position = 'absolute';
                               zIndex = 555;
                               filter = " alpha(opacity = " + co.Opactity + ")";
                               opacity = co.Opactity / 100;
                               width = document.documentElement.scrollWidth + "px";
                               top = "0px";
                               backgroundColor = co.BgColor;
                           }

                           document.body.appendChild(e);
                       }
                       else
                       {
                           SWW.F.DOM.Display(this.Config.BgId, true);
                       }
                   },
                   Clear: function ()
                   {
                       this.ObjArray.pop();

                       if (this.Config.CountDialog <= 0)
                       {

                           SWW.F.DOM.Display(this.Config.BgId);
                           this.Config.CountDialog = 0;
                       }
                       else
                       {

                           SWW.F.DOM.Display(this.ObjArray[this.Config.CountDialog - 1].guid, true);

                       }



                   },
                   Create: function (o)
                   {

                       if (!o)
                       {
                           o = {};
                       }
                       for (var p in this.Temp)
                       {
                           if (!o[p])
                           {
                               o[p] = this.Temp[p];
                           }
                       }

                       if (!o.id)
                       {
                           o.id = this.Config.DefaultId;
                       }

                       if (!o.guid)
                       {
                           o.guid = SWW.F.SYS.GetGuid();
                       }



                       o.top = Math.max(document.body.scrollTop, document.documentElement.scrollTop) + o.top;
                       if (o.left == -1) o.left = (document.body.offsetWidth - parseInt(o.width)) / 2;
                       this.AddBg();





                       var aH = [];

                       aH.push('<div id="' + o.guid + '" class="' + o.cssid + 'Box_Over" style="width:' + (o.width + 4) + 'px;height:' + (o.height + 4) + 'px; top:' + o.top + 'px;left:' + o.left + 'px;">');
                       aH.push('<div class="' + o.cssid + 'Box_Back" style="width:' + (o.width + 3) + 'px;height:' + (o.height + 3) + 'px;">');

                       aH.push('<div class="' + o.cssid + 'Box" style="width:' + o.width + 'px;height:' + o.height + 'px;">');


                       //开始添加抬头

                       aH.push('<div class="' + o.cssid + 'Title" id="' + o.guid + '_title"><span class="' + o.cssid + 'Title_Left">' + o.title + '</span><span  class="' + o.cssid + 'Title_Right" onclick="SWW.W.Dialog.Close(\'' + o.guid + '\',' + o.save + ')">关闭</span></div>');




                       aH.push('<div class="' + o.cssid + 'Content" style="width:' + o.width + 'px;height:' + o.height + 'px;">');
                       //判断内容模型
                       if (o.url)
                       {
                           aH.push('<div id="' + o.guid + '_iframe_load">' + SWW.M.ME.Load + '</div>');
                           aH.push('<div id="' + o.guid + '_iframe_show" style="display:none;">');
                           aH.push('<iframe id="' + o.guid + '_iframe" onload="SWW.F.DOM.Display(\'' + o.guid + '_iframe_load\');SWW.F.DOM.Display(\'' + o.guid + '_iframe_show\',true);" style="width:' + (o.width ) + 'px;height:' + (o.height) + 'px" src="' + o.url + '" frameborder="0"></iframe>');
                           aH.push('<div>');
                       }
                       else if (o.html)
                       {
                           aH.push('<div style="margin:5px;">');
                           aH.push(o.html);

                           aH.push('</div>');

                       }


                       if (o.button && o.button.length > 0)
                       {
                           var s = '';
                           aH.push('<div style="text-align:center;">');
                           for (var i = 0, j = o.button.length; i < j; i++)
                           {
                               var iIndex = o.button[i].indexOf(':');

                               if (iIndex > -1)
                               {
                                   aH.push('<input type="button" onclick="SWW.W.Dialog.Source().' + o.button[i].substr(iIndex + 1) + '" value="' + o.button[i].substr(0, iIndex) + '"/>');
                               }
                           }
                           aH.push('</div>');
                       }

                       aH.push('</div></div></div></div>');
                       SWW.J('body').append(aH.join(''));

                       o.current = this.Config.CountDialog;

                       if (this.Config.CountDialog > 0)
                       {
                           SWW.F.DOM.Display(this.ObjArray[this.Config.CountDialog - 1].guid);
                       }

                       this.Config.CountDialog++;

                       //开始加载拖动代码
                       SWW.W.Drag.DragElement(SWW.J('#' + o.guid + '_title'), SWW.J('#' + o.guid));

                       if (o.scroll == 1)
                       {
                           SWW.J(window).scroll(function ()
                           {
                               SWW.J('#' + o.guid).css("top", (document.documentElement.scrollTop + 120) + "px");

                           });
                       }
                       this.ObjArray.push(o);
                   }
               },


               Father: function ()
               {
                   return top;
               },

               Open: function (o)
               {
                   ///	<summary>
                   ///  创建对话框
                   ///	</summary>
                   ///	<param name="o" type="obj">
                   ///		对话框
                   ///	</param>



                   if (!o) o = {};

                   if (!o.self)
                   {
                       if (this.Father() != self)
                       {
                           o.self = self;
                           this.Father().SWW.W.Dialog.Open(o);
                           return;
                       }
                       else
                       {
                           o.self = window;
                       }
                   }

                   SWW.F.JF.Ready(function () { SWW.W.Dialog.Init.Create(o) });

               },

               Close: function (sn, iSave)
               {

                   ///	<summary>
                   ///  关闭对话框
                   ///	</summary>
                   ///	<param name="sn" type="obj">
                   ///		对话框编号 如果留空时表示关闭最近一次打开的对话框
                   ///	</param>
                   ///	<param name="iSave" type="int">
                   ///		关闭类型 0为直接清除 1为保存状态
                   ///	</param>


                   if (this.Father() != self && this.Father().SWW)
                   {
                       this.Father().SWW.W.Dialog.Close(sn, iSave);
                       return;
                   }

                   if (!sn)
                   {
                       sn = this.Init.ObjArray[this.Init.ObjArray.length - 1].guid;
                   }

                   if (!iSave)
                   {
                       iSave = this.Init.Temp.save;
                   }



                   if (iSave == 0)
                   {
                       document.body.removeChild(SWW.F.DOM.Get(sn));
                   }
                   else
                   {
                       SWW.J('#' + sn).hide();
                   }
                   this.Init.Config.CountDialog--;



                   //清除背景
                   this.Init.Clear();
               },

               Source: function ()
               {
                   ///	<summary>
                   ///  得到当前对话框的来源页面
                   ///	</summary>

                   var o = this.Father().SWW.W.Dialog.Init.ObjArray[this.Father().SWW.W.Dialog.Init.ObjArray.length - 1];
                   if (o)
                   {
                       return o.self;
                   }
                   else
                   {
                       return self;
                   }
               },

               GetValue: function (s)
               {
                   ///	<summary>
                   ///  得到来源页面的元素的值
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		元素编号
                   ///	</param>

                   return this.Source().SWW.J('#' + s).val();

               },
               SetValue: function (s, v)
               {
                   ///	<summary>
                   ///  设置来源页面的元素的值
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		元素编号
                   ///	</param>


                   // this.Source().SWW.J('#' + s).val(v);

                   this.Source().SWW.F.DOM.Auto(s, v);


               }

           }
           ,
           Drag:
           {
               GetPos: function (e)
               {
                   var D = document.documentElement;
                   if (e.pageX)
                       return { x: e.pageX, y: e.pageY };
                   else
                       return { x: (e.clientX + D.scrollLeft - D.clientLeft), y: (e.clientY + D.scrollTop - D.clientTop) }
               },

               DragElement: function (el, handle, fDragIng, end)
               {

                   SWW.J(el).mousedown(function (e)
                   {
                       e = e || window.event;
                       var pos = SWW.W.Drag.GetPos(e);

                       var father = handle ? SWW.J(handle) : SWW.J(el);

                       var hPos = father.offset();
                       var diffx = pos.x - hPos.left;
                       var diffy = pos.y - hPos.top;

                       SWW.W.Drag.DragIng(father, diffx, diffy, fDragIng);



                       SWW.W.Drag.Drop(end);


                   });

                   return false;
               },

               DragIng: function (handle, diffx, diffy, fDragIng)
               {

                   SWW.J(document).bind('mousemove', function (e)
                   {
                       var movePos = SWW.W.Drag.GetPos(e);
                       SWW.J(handle).css({ left: (movePos.x - diffx), top: (movePos.y - diffy) });

                       if (fDragIng) fDragIng();

                   });
               },

               Drop: function (fEnd)
               {
                   SWW.J(document).mouseup(function (e)
                   {
                       SWW.J(document).unbind('mousemove');
                       if (fEnd) fEnd();
                   });

               }
           }

       },




       //初始化
       I: function (t, o)
       {
           ///	<summary>
           ///  根据名称初始化对象
           ///	</summary>
           ///	<param name="t" type="string">
           ///		操作类型
           ///	</param>
           ///	<param name="o" type="obj">
           ///		操作的对象
           ///	</param>



           //开始自动判断参数传入类型
           if (typeof (t) != "string" && t.WidgetType)
           {
               o = t;
               t = o.WidgetType;
           }


           //判断是否存在参数并且重新初始化
           if (o)
           {
               if (o.WidgetType)
               {
                   o = SWW.F.SYS.InitReq(o);
                   this.O.Req[o.Guid] = o;
               }

           }

           //判断是否已经存在加载的对象
           if (!this[t] && this.C.JS[t] && !this.C.JSLoad[t] && (!this.C.JS[t].w || !window[this.C.JS[t].w]))
           {
               //定义文件
               var u = '';
               if ("string" == typeof (this.C.JS[t]))
               {
                   u = this.C.JS[t];
               }
               else
               {
                   //判断是否有需要加载的内容
                   if (this.C.JS[t].n)
                   {
                       for (var i = 0; i < this.C.JS[t].n.length; i++)
                       {
                           this.I(this.C.JS[t].n[i]);
                       }
                   }
                   u = this.C.JS[t].u;
               }
               this.C.JSLoad[t] = Date().toString();

               this.Z.AddScript(u);




           }





           //开始加载初始化函数
           this.Z.CheckInit();




       }




   }



}




