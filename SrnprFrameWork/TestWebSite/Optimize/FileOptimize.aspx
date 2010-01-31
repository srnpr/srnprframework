<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileOptimize.aspx.cs" Inherits="Optimize_FileOptimize" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="tbFrom" runat="server" Width="503px">S:\AAAProject\AtGang\AtGangStatic\AtGangStatic\Common\js</asp:TextBox>
        <br />
        <br />
        <br />
        <asp:TextBox ID="tbTo"
            runat="server" Width="515px">S:\AAAProject\AtGang\AtGangStaticMINI</asp:TextBox>
        <br />
        <br />
        <input id="Button1" type="submit" value="button" onclick="dSubmit()" /><br />
        <br />
        <asp:Button ID="btnBegin" runat="server" Text="开始优化" onclick="btnBegin_Click" />
    </div>
    </form>
    
      <script type="text/javascript">


          if (document.addEventListener)
          {
               document.addEventListener("click", OnClick, false);
          }
          else
          {
             // document.attachEvent("onclick", OnClick);
          }
          
          
          function dSubmit()
          {
              //document.getElementById("form1").attachEvent("onsubmit", AddSubmit);
              //document.getElementById("form1").attachEvent("onsubmit", AddSubmit2);
              //document.getElementById("form1").attachEvent("onsubmit", AddSubmit);
              

          }

          function OnClick(e)
          {
              e = e ? e : window.event;
              if (e.srcElement && e.srcElement.type && (e.srcElement.type == "submit" || e.srcElement.type == "button"))
              {
                
              }
          }

          function AddSubmit()
          {
              var a = "";
              //return false;
              alert(document.getElementById("form1").onsubmit);
          }

          function AddSubmit2()
          {
              var b = "";
              alert(document.getElementById("form1").onsubmit);
          }
          
    </script>
    
    
</body>
</html>
