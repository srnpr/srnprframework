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
               GS: { u: 'SrnprWebJsGridShowFWF.js', n: ['JQuery', 'Json'] },
               LS: { u: 'SrnprWebJsListShowFWF.js', n: ['JQuery', 'Json'] },
               SWW: 'SrnprWebJsWebWidgetFWF.js'
           },
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
               T: 100
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
                IM: '系统尝试初始化失败！'
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
       O: { Req: {}, Res: {} },
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
               var t = {};
               t.RQ = e;
               alert(JSON.stringify(t));
               SWW.J.ajax(
                {
                    url: "/Asmx/GridShowHander.ashx",
                    type: "POST",
                    data: "json=" + JSON.stringify(t),
                    success: function(x) { SWJGSF.AjaxSuccess(t, x); },
                    error: function(XMLHttpRequest, textStatus) { SWW.F.Error('sww.z.ajax', textStatus) }
                });
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

                   SWW.C.Init.N++;

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

       }


   }


}




SWW.J_Ready(function() { SWW.Z.Init();SWW.F.Error('dd') });

