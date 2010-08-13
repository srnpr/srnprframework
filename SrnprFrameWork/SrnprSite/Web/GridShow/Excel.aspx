<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Excel.aspx.cs" Inherits="SrnprSite.Web.GridShow.Excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="hidden" name="json" id="json" />
    
   
    <asp:HiddenField  ID="hfJson" runat="server"  ></asp:HiddenField>
    
        <asp:Button ID="btnExcel" runat="server" onclick="btnExcel_Click" 
            Text="导出当前页" OnClientClick="return SubmitCheck()" />
        <asp:Button ID="btnExcelAll" runat="server" Text="导出所有页" 
            onclick="btnExcelAll_Click"  OnClientClick="return SubmitCheck()" />
    </div>
    
    
    <script type="text/javascript">

        function SubmitCheck()
        {
            var id = "<%=sId %>";


            var bReturn = false;


            if (parent && parent.document.getElementById(id))
            {

                var v = parent.document.getElementById(id).value;
                if (v)
                {
                    document.getElementById("<%=hfJson.ClientID %>").value = v;
                    bReturn = true;
                }

            }

            return bReturn;


           
            
        }
    
    </script>
    </form>
</body>
</html>
