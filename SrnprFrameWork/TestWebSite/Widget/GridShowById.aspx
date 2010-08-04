<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridShowById.aspx.cs" Inherits="Widget_GridShowById" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script type="text/javascript" src="/WebFile/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/WebFile/json2.js"></script>
    <script type="text/javascript" src="/WebFile/SrnprWebJsGridShowFWF.js"></script>
    
    <script type="text/javascript" src="http://f.xgou.com/AtGang/Common/js/SrnprNetJsAllAlpha.js"></script>
    <link href="/WebFile/SrnprWebCSSGridShowFWF.css" type="text/css" rel="Stylesheet" />
    <link href="http://f.xgou.com/AtGang/Manage/css/ManageAlpha.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
   <form id="form1" runat="server">
    <div>
    
    
    
    
    <sw:GridShowWWW ID="GSShow" runat="server" />
    
     <br /><br /><br /><br />
     
     <a href="../Config/ConfigGridShowList.aspx">返回列表</a>
     <br /><br />
     
     <a href="javascript:ShowList()">显示返回Json内容</a>
     <div id="show" style="display:none;">
    <textarea id="jsonshow" rows="20" cols="100"></textarea>
</div>


    </div>
    </form>
    
    <script type="text/javascript">
        function ShowList()
        {
            document.getElementById("show").style.display = "";
        }
    
    </script>
    
</body>
</html>
