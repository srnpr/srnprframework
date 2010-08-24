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
            
            
            
            
Author: Liudpc
Create Date: 2010-8-18 9:27:36
******************************************************/

if (!window.SWW)
{
    window.SWW =
   {

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
       O: {},
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
           SystemError: function(n, m, e)
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
               this.Alert('【系统消息】：系统出现异常错误，请联系管理员' + (n ? '\n【错误标识】：' + n : '') + (m ? '\n【错误内容】：' + m : ''));
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
           //系统加载脚本文件  u：脚本文件名称  n：需要加载的其他脚本  w：window基本名称（全局）
           JS:
           {

               SWW: 'SrnprWebJsWebWidgetFWF.js',
               Json: 'json2.js',

               JQuery: { u: 'jquery-1.4.2.min.js', w: 'jQuery' },

               C: 'SrnprWebJsConfigFWF.js',


               GS: { u: 'SrnprWebJsGridShowFWF.js', n: ['JQuery', 'Json'] },
               LS: { u: 'SrnprWebJsListShowFWF.js', n: ['JQuery', 'Json'] },

               Temp: { u: '文件地址', n: '需要加载的其他项目', w: 'Window属性判断(如果有则不加载)' }

           },
           Init: function()
           {
               ///	<summary>
               ///  系统初始化时调用
               ///	</summary>


               if (SWW.O)
               {
                   var bFlag = true;
                   //开始判断是否所有加载完成
                   if (bFlag)
                   {
                       for (var p in SWW.O)
                       {
                           var t = p.toString();

                           for (var i = 0, j = SWW.O[t].length; i < j; i++)
                           {
                               if (!SWW[p])
                               {
                                   bFlag = false;
                                   i = j;
                               }
                           }
                       }
                   }

                   //如果所有加载完成
                   if (bFlag)
                   {
                       for (var p in SWW.O)
                       {
                           var t = p.toString();

                           for (var i = 0, j = SWW.O[t].length; i < j; i++)
                           {
                               SWW[t].F_Init(SWW.O[t][i]);
                           }
                       }
                   }
                   else
                   {
                       //重新调用执行
                       setTimeout("SWW.Z.Init()", 100);
                   }
               }



           },



           JSLoad: {}
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
               if (!this.O[t])
               {
                   this.O[t] = [];
               }

               this.O[t].push(o);
           }


           //判断是否已经存在加载的对象
           if (!this[t] && this.Z.JS[t] && !this.Z.JSLoad[t] && (!this.Z.JS[t].w || !window[this.Z.JS[t].w]))
           {
               var u = '';
               if ("string" == typeof (this.Z.JS[t]))
               {
                   u = this.Z.JS[t];
               }
               else
               {
                   if (this.Z.JS[t].n)
                   {
                       for (var i = 0; i < this.Z.JS[t].n.length; i++)
                       {
                           this.I(this.Z.JS[t].n[i]);
                       }
                   }
                   u = this.Z.JS[t].u;
               }
               this.Z.JSLoad[t] = Date().toString();
               this.Z.AddScript(u);
           }

       },
       C:
       {
            
            Version:'1.0.0.0'
        }


}



   


    //系统初始化时加载
   SWW.J_Ready(function()
   {
       SWW.Z.Init();
   }
   );
  

}




