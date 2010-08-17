

if (!window.SWW)
{
    window.SWW =
   {
       SWW_BasePath: (function()
       {
           var d = '';
           var e = document.getElementsByTagName('script');
           for (var f = 0; f < e.length; f++)
           {
               var g = e[f].src.match(/(^|.*[\\\/])SrnprWebJSWWebWidgetFWF.js(?:\?.*)?$/i);
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
               d = location.href.match(/^[^\?]*\/(?:)/)[0] + d;
           }

           return d;
       }
       )
       (),

       SWW_JS:
       {
           C:'SrnprWebJsConfigFWF.js',
           Json:'json2.js',
           JQuery:'jquery-1.4.2.min.js',
           GS: { u: 'SrnprWebJsGridShowFWF.js', n: ['IS','JQuery','Json'] },
           IS: 'SrnprWebJsItemShowFWF.js'
           
       },

       SWW_JSLoad: {}
       ,

       SWW_Alert: function(m)
       {
           ///	<summary>
           ///  弹出提示信息
           ///	</summary>
           ///	<param name="u" type="string">
           ///		提示信息
           ///	</param>
        
           alert(m);
       },

       SWW_AddScript: function(u)
       {
           ///	<summary>
           ///  添加脚本文件
           ///	</summary>
           ///	
           ///	<param name="u" type="string">
           ///		脚本文件路径
           ///	</param>
           document.write('<script type="text/javascript" src="' + u + '"></script>');
       },
       
       G: function(t)
       {
           ///	<summary>
           ///  根据名称初始化对象并返回对象
           ///	</summary>
           ///	<returns type="obj" />
           ///	<param name="t" type="string">
           ///		返回对象
           ///	</param>
           
           if (!this[t] && this.SWW_JS[t] && !this.SWW_JSLoad[t])
           {
               var u = '';
               if ("string" == typeof (this.SWW_JS[t]))
               {
                   u = this.SWW_BasePath + this.SWW_JS[t];
               }
               else
               {
                   if (this.SWW_JS[t].n)
                   {
                       for (var i = 0; i < this.SWW_JS[t].n.length; i++)
                       {
                           this.G(this.SWW_JS[t].n[i]);
                       }
                   }
                   u = this.SWW_BasePath + this.SWW_JS[t].u;
               }

               this.SWW_JSLoad[t] = Date().toString();
               this.SWW_AddScript(u);

           }

           return this[t];
       }

   }

}





