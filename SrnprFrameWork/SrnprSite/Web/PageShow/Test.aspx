<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SrnprSite.Web.PageShow.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/WebFile/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/WebFile/json2.js"></script>



    <script type="text/javascript" src="/WebFile/SrnprWebJsWebWidgetFWF.js"></script>
    
    <link href="/WebFile/SrnprWebCSSGridShowFWF.css" type="text/css" rel="Stylesheet" />

     <link href="/WebFile/SrnprWebCSSWebWidgetFWF.css" type="text/css" rel="Stylesheet" />
    <link href="/WebFile/SrnprWebCSSPageShowFWF.css" type="text/css" rel="Stylesheet" />
    <!--
     <script type="text/javascript" src="/WebFile/SWW-1.0.0.0.min.js"></script>

    <link href="/WebFile/SWW-CSS-1.0.0.0.min.css" type="text/css" rel="Stylesheet" />
  -->
   
</head>
<body>
   <form id="form1" runat="server">
   <input type="button" onclick="ShowDebug()" />

    <div>
    <sw:PageShowWWW ID="PageShow1" runat="server" />
    </div>

    
    </form>
    
    
    <%=sDemo %>

    
    <script>


       // alert(SWW.F.STR.Format('1{0}2{1}',['aa','bb']));

        SWW.C.Flag.Debug = true;

        

        function ShowDebug()
        {
           

            var h = [];
            h.push('<div class="SWW_CSS_GS_DIV_MAIN"><table><tr><th>时间</th><th>调用接口</th><th>日志内容</th></tr>');

            for (var i =  SWW.O.Log.Debug.length-1,j=-1; i >j;i--)
            {
                var a = typeof (SWW.O.Log.Debug[i].c) != 'string' ? JSON.stringify(SWW.O.Log.Debug[i].c) : SWW.O.Log.Debug[i].c;

                if (a.length > 90)
                {
                    a ='<a href="javascript:ShowInfo('+i+')">详细信息</a>&nbsp;&nbsp;'+ SWW.F.STR.HtmlEncode( a.substr(0, 90)) ;
                }

                h.push('<tr><td>' + SWW.O.Log.Debug[i].d + '</td><td>'+ SWW.O.Log.Debug[i].t+'</td><td>'+a+'</td></tr>');


            }
            h.push('</table></div>');

            SWW.W.Dialog.Open({ html: h.join('') ,  width: 1000 });

            

        }

        function ShowInfo(i)
        {
            SWW.W.Dialog.Open({ html: '<div id="debug"></div>', width: 1000 });
            var a = typeof (SWW.O.Log.Debug[i].c) != 'string' ? JSON.stringify(SWW.O.Log.Debug[i].c) : SWW.O.Log.Debug[i].c;
            SWW.W.Dialog.Father().SWW.F.DOM.Text('debug', a);

            //SWW.F.DOM.Text('debug', a);
        }
    
    </script>

</body>
</html>
