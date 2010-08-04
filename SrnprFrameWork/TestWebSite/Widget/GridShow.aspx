<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridShow.aspx.cs" Inherits="Widget_GridShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <!--
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    -->
    <script type="text/javascript" src="http://srnprframeworktest//WebFile/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="http://srnprframeworktest//WebFile/json2.js"></script>
    <script type="text/javascript" src="http://srnprframeworktest//WebFile/SrnprWebJsGridShowFWF.js"></script>
    
    <script type="text/javascript" src="http://f.xgou.com/AtGang/Common/js/SrnprNetJsAllAlpha.js"></script>
    <link href="http://srnprframeworktest//WebFile/SrnprWebCSSGridShowFWF.css" type="text/css" rel="Stylesheet" />
    <link href="http://f.xgou.com/AtGang/Manage/css/ManageAlpha.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <sw:GridShowWWW ID="GSShow" runat="server" XmlConfigName="TestSalesOrderList" />
    
     <sw:GridShowWWW ID="GSShow2" runat="server" XmlConfigName="TestSalesOrderRecord" />
    <textarea id="jsonshow" rows="20" cols="100"></textarea>

    </div>
    </form>
    
    <script type="text/javascript">
    
    
    
    </script>
</body>
</html>
