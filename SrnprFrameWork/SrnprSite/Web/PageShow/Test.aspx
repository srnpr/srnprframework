<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SrnprSite.Web.PageShow.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/WebFile/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/WebFile/json2.js"></script>
     <script type="text/javascript" src="/WebFile/SWW-1.0.0.0.min.js"></script>

    <link href="/WebFile/SWW-CSS-1.0.0.0.min.css" type="text/css" rel="Stylesheet" />
  
   
</head>
<body>
   <form id="form1" runat="server">
    <div>
    <sw:PageShowWWW ID="PageShow1" runat="server" />
    </div>
    </form>
    
    
    <%=sDemo %>
</body>
</html>
