<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SrnprSite.Test.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="/Skin/Blue/MasterPageCss.css" />

    <script type="text/javascript" src="/Modules/Master/MasterPage.js"></script>

    <script type="text/javascript" src="/WebFile/jquery-1.4.2.min.js"></script>

   
    
    <script type="text/javascript" src="/WebFile/SrnprWebJsWebWidgetFWF.js"></script>

    <script type="text/javascript" src="/WebFile/SrnprWebJsGridShowFWF.js"></script>

    

    <script type="text/javascript" src="http://f.xgou.com/AtGang/Common/js/SrnprNetJsAllAlpha.js"></script>

    <link href="/WebFile/SrnprWebCSSGridShowFWF.css" type="text/css" rel="Stylesheet" />

     <link href="/WebFile/SrnprWebCSSWebWidgetFWF.css" type="text/css" rel="Stylesheet" />
    <link href="/WebFile/SrnprWebCSSPageShowFWF.css" type="text/css" rel="Stylesheet" />
    <link href="http://f.xgou.com/AtGang/Manage/css/ManageAlpha.css" rel="stylesheet"
        type="text/css" />

        <script type="text/javascript">

            //行变色
            function ChangeColorAll(e)
            {

                if (e.Cell[1].text() == "420100")
                {
                    e.CellTitle["AreaName"].html('<input >');
                }



            }


            //添加绑定函数
            SWW.GS.OnDataRowBind('test.areainfo', ChangeColorAll);
        
        
        </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" paramid="parmareaname"></asp:TextBox>

    
    </div>

    <input class="button_inquire" id="button2" name="button2" onclick="SWW.GS.Query()" type="button" value="确定" />

    <div>
    <sw:GridShowWWW ID="GS" runat="server" XmlConfigName="test.areainfo" />
    </div>
    </form>

    <script>
        document.getElementById("button2").form.submit();
    </script>
</body>
</html>
