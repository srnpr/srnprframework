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
               
               
               GS: { u: 'SrnprWebJsGridShowFWF.js', n: [ 'JQuery', 'Json'] },
               LS: 'SrnprWebJsListShowFWF.js'

           },
           JSLoad:{}
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
           if (o)
           {
               if (!this.O[t])
               {
                   this.O[t] = [];
               }

               this.O[t].push(o);
           }
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
                           this.G(this.Z.JS[t].n[i]);
                       }
                   }
                   u = this.Z.JS[t].u;
               }
               this.Z.JSLoad[t] = Date().toString();
               this.Z.AddScript(u);
           }

       }


   }

}




