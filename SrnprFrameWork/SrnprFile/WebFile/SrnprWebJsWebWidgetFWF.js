

if (!window.SW)
{
    window.SW =
   {
       SW_BasePath: (function()
       {
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
               d = location.href.match(/^[^\?]*\/(?:)/)[0] + d;
           }

           return d;
       }
       )
       (),

       SW_JS:
       {
           GS: { u: 'SrnprWebJsGridShowFWF.js', n: ['IS'] },
           IS: 'SrnprWebJsItemShowFWF.js'
       },

       SW_JSLoad: {}
       ,

       SW_Alert: function(m)
       {
           alert(m);
       },

       SW_AddScript: function(u)
       {
           document.write('<script type="text/javascript" src="' + u + '"></script>');
           alert(u);
       },
       SW_Init: function(t)
       {
           if (!SW[t] && SW.SW_JS[t] && !SW.SW_JSLoad[t])
           {
               var u = '';
               if ("string" == typeof (SW.SW_JS[t]))
               {
                   u = SW.SW_BasePath + SW.SW_JS[t];
               }
               else
               {
                   if (SW.SW_JS[t].n)
                   {
                       for (var i = 0; i < SW.SW_JS[t].n.length; i++)
                       {
                           SW.SW_Init(SW.SW_JS[t].n[i]);
                       }
                   }
                   u = SW.SW_BasePath + SW.SW_JS[t].u;
               }

               SW.SW_JSLoad[t] = Date().toString();
              
               SW.SW_AddScript(u);

           }
       }

   }
}







//alert(SW.basePath);

