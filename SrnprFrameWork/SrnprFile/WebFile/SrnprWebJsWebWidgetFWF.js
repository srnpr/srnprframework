

if (!window.SW)
{
    window.SW =
   {

       basePath: (function()
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

       getWebTable: function()
       {
       }

   }
}







//alert(SW.basePath);

