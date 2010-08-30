/// <reference path="jquery-1.3.2.min-vsdoc.js"/>

/******************************************************
Description: 核心类文件 所有widget使用的初始化及加载文件
            目前使用Jquery框架 所有引用调用J
            
            公开属性：
            J：jQuery
            O：表示对象系列
            C：配置文件
            I：初始化
            F：函数
            Z：基类使用自身操作
            M：消息系列
            A：扩展系列
            
            
            
            
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
           //系统加载脚本文件  u：脚本文件名称  n：需要加载的其他脚本  w：window基本名称（全局）
           JS:
           {
               Json: 'json2.js',
               JQuery: { u: 'jquery-1.4.2.min.js', w: 'jQuery' },
               C: 'SrnprWebJsConfigFWF.js',
               GS: { u: 'SrnprWebJsGridShowFWF.js', n: ['JQuery', 'Json'], q: 'GridShowRequestWWE' },
               LS: { u: 'SrnprWebJsListShowFWF.js', n: ['JQuery', 'Json'], q: 'ListShowRequestWWE' },
               SWW: 'SrnprWebJsWebWidgetFWF.js'
           },

           //Ajax相关
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
                AS: '无法加载类型'
            }

        },

       //Jquery适配器  
       J: jQuery,

       J_Ready: function(f)
       {
           ///	<summary>
           ///  脚本文件加载完成后调用函数
           ///	</summary>
           ///	<param name="f" type="string">
           ///		函数
           ///	</param>
           this.J().ready(f);
       },
       O: { Req: {}, Res: {}, AF: {}, Base: { __type: ''} },
       A: function(t, f, id, fu)
       {
           ///	<summary>
           ///  扩展调用接口
           ///	</summary>
           ///	<param name="t" type="string">
           ///		类型
           ///	</param>
           ///	<param name="f" type="string">
           ///		函数操作 目前支持：Success(返回值成功时)
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
       F:
       {
           Alert: function(m)
           {
               ///	<summary>
               ///  弹出提示信息
               ///	</summary>
               ///	<param name="m" type="string">
               ///		提示信息
               ///	</param>
               alert(m);
           },
           Error: function(n, m, e)
           {
               ///	<summary>
               ///  出现严重错误时提示
               ///	</summary>
               ///	<param name="n" type="string">
               ///		错误标识
               ///	</param>
               ///	<param name="m" type="string">
               ///		错误内容
               ///	</param>
               ///	<param name="e" type="string">
               ///		解决方案
               ///	</param>

               this.Alert(SWW.M.SE.ET + (n ? SWW.M.SE.EN + n : '') + (m ? SWW.M.SE.EM + m : ''));
           }
       },
       Z:
       {

           BasePath: function()
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
           AddScript: function(u)
           {
               ///	<summary>
               ///  添加脚本文件
               ///	</summary>
               ///	<param name="u" type="string">
               ///		地址
               ///	</param>

               SWW.J.getScript(this.BasePath() + u);
           },

           Ajax: function(e)
           {
               ///	<summary>
               ///  提交请求
               ///	</summary>
               ///	<param name="e" type="string">
               ///		调用的元素数组
               ///	</param>


               var t = {};
               t.RQ = e;
               SWW.J.ajax(
                {
                    url: SWW.C.Ajax.Url,
                    type: "POST",
                    data: "json=" + JSON.stringify(t),
                    success: function(s) { SWW.Z.AjaxSuccess(s); },
                    error: function(XMLHttpRequest, textStatus) { SWW.F.Error('sww.z.ajax', textStatus) }
                });
           },

           AjaxSuccess: function(s)
           {
               ///	<summary>
               ///  执行成功时调用
               ///	</summary>
               ///	<param name="s" type="string">
               ///		响应内容
               ///	</param>

               SWW.O.Res = JSON.parse(s);

               for (var i = 0, j = SWW.O.Res.RS.length; i < j; i++)
               {
                   if (SWW.O.Res.RS[i].WidgetType && SWW[SWW.O.Res.RS[i].WidgetType])
                   {
                       SWW[SWW.O.Res.RS[i].WidgetType].F_Success(SWW.O.Res.RQ[i], SWW.O.Res.RS[i]);



                       //加载成功时调用
                       if (SWW.O.AF[SWW.O.Res.RS[i].WidgetType] && SWW.O.AF[SWW.O.Res.RS[i].WidgetType].Success[SWW.O.Res.RQ[i].Id])
                       {
                           SWW.O.AF[SWW.O.Res.RS[i].WidgetType].Success[SWW.O.Res.RQ[i].Id](SWW.O.Res.RQ[i], SWW.O.Res.RS[i], s);
                       }


                   }
                   else
                   {
                       SWW.F.Error('sww.z.ajaxsuccess', x);
                   }

               }
           },

           CheckInit: function()
           {
               ///	<summary>
               ///  判断系统初始化加载加载
               ///	</summary>


               if (!SWW.C.Init.LoadFlag)
               {
                   SWW.J_Ready(function() { SWW.Z.Init(); });
                   SWW.C.Init.LoadFlag = true;
               }
           },


           Init: function()
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
                           for (var i = 0, j = SWW.O.Req[p].length; i < j; i++)
                           {
                               if (!SWW[p])
                               {
                                   bFlag = false;
                                   i = j;
                               }
                           }
                       }
                   }

                   //累加当前调用次数
                   SWW.C.Init.N++;

                   //判断是否超过最大限制
                   if (SWW.C.Init.N < SWW.C.Init.M)
                   {

                       //如果所有加载完成
                       if (bFlag)
                       {

                           var sub = [];
                           for (var t in SWW.O.Req)
                           {
                               for (var n = 0, m = SWW.O.Req[t].length; n < m; n++)
                               {
                                   //SWW[t].F_Init(SWW.O.Req[t][n]);

                                   if (SWW.C.JS[t] && SWW.C.JS[t].q && !SWW.O.Req[t][n].__type)
                                   {
                                       var oe = SWW.O.Base;


                                       oe.__type = SWW.C.JS[t].q + ':' + SWW.C.BaseNamespace;

                                       for (var p in SWW.O.Req[t][n])
                                       {
                                           oe[p] = SWW.O.Req[t][n][p];
                                       }



                                       //SWW.O.Req[t][n].__type = SWW.C.JS[t].q + ':' + SWW.C.BaseNamespace;

                                       SWW.O.Req[t][n] = oe;


                                   }
                                   sub.push(SWW.O.Req[t][n]);
                               }
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
                       SWW.F.Error('sww.z.init', SWW.M.SE.IM);
                   }
               }



           }
       },
       I: function(t, o)
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




           //判断是否存在参数
           if (o)
           {
               if (!this.O.Req[t])
               {
                   this.O.Req[t] = [];
               }

               this.O.Req[t].push(o);
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




