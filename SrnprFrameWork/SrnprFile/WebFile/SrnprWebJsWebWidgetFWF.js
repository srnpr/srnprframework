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
            M：消息系列
            A：扩展系列
            
            
            参数定义：
            第一标识
            s:字符串(str)
            o:对象(obj)
            i:数字(int)
            f:函数(fun)

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
           //系统加载脚本文件  u：脚本文件名称  n：需要加载的其他脚本  w：window基本名称（全局）  q：服务端类名称（JSON反向解析使用）
           JS:
           {
               Json: 'json2.js',
               JQuery: { u: 'jquery-1.4.2.min.js', w: 'jQuery' },
               C: 'SrnprWebJsConfigFWF.js',
               GS: { u: 'SrnprWebJsGridShowFWF.js', n: ['JQuery', 'Json'], q: 'GridShowRequestWWE' },
               LS: { u: 'SrnprWebJsListShowFWF.js', n: ['JQuery', 'Json'], q: 'ListShowRequestWWE' },
               SWW: 'SrnprWebJsWebWidgetFWF.js'
           },

           //Ajax相关配置
           Ajax:
           {
               Url: '/Asmx/WebWidgetHandler.ashx'
           },

           //基本命名空间
           BaseNamespace: 'http://srnprframework/srnprweb',

           //已经加载的js配置
           JSLoad: {},

           //初始化时配置
           Init:
           {
               //当前加载次数
               N: 0,
               //最大加载次数
               M: 20,
               //加载时间间隔
               T: 100,
               //是否添加ready加载函数
               LoadFlag: false
           },

           Version: '1.0.0.0'
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
            }

        },

       //Jquery适配器  
       J:jQuery,

       //Req提交参数  Res返回参数  AF扩展函数  Guid唯一标识集
       O: { Req: {}, Res: {}, AF: {}, Guid: {} },

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
                    }
                    else
                    {
                        //SWW.F.SYS.Error({ n: 'SWW.F.SYS.ExecFunc', m: SWW.M.SE.FEF, p: [o.t, o.f, this.GetObjPrototype(o.e)] });

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
                        r.push('[' + p + ']:' + o[p]);
                    }

                    return r.join(';');


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
                    }

                }
            }

        },

        //内部调用系列
       Z:
       {

           BasePath: function ()
           {
               ///	<summary>
               ///  返回文件所在路径
               ///	</summary>

               var d = '';
               var e = document.getElementsByTagName('script');
               for (var f = 0; f < e.length; f++)
               {
                   var g = e[f].src.match(/(^|.*[\\\/])SrnprWebJsWebWidgetFWF.js(?:\?.*)?$/i);
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
               ///	<param name="s" type="string">
               ///		响应内容
               ///	</param>



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

                   //判断是否超过最大调用次数限制
                   if (SWW.C.Init.N < SWW.C.Init.M)
                   {

                       //如果所有加载完成
                       if (bFlag)
                       {
                           var sub = [];
                           for (var t in SWW.O.Req)
                           {
                               sub.push(SWW.O.Req[t]);
                           }
                           this.Ajax(sub);
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
               o = SWW.F.SYS.InitReq(o);
               this.O.Req[o.Guid] = o;

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




