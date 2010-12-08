<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="BasePage.aspx.cs" Inherits="SrnprSite.Demo.Dialog.BasePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">





<span id="aa_span_662babb2-7390-40cd-a453-a86731e35edb" ></span>
<script>

    SWW.I({ WidgetType: "TD", Id: "aa", url: "/Demo/Dialog/DialogFirst.aspx", SId: "aa_span_662babb2-7390-40cd-a453-a86731e35edb" });
</script>

<br /><br />

<input type="button" value="打开一号对话框" onclick="SWW.W.Dialog.Open({url:'/Demo/Dialog/DialogFirst.aspx?sww_td_parent_id=aa',title:'一号对话框',width:900,height:450,top:200})" />






<sw:ToolDialogWWW ID="TD" runat="server" Control_Url="/Demo/Dialog/DialogFirst.aspx" Controldd="ff" Controldd2="ff"   />

<br />

    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />



    <script>

        SWW.TD.OnBeforeOpen('TD', BeforeOpen);

        function BeforeOpen(e) {
            //添加Url参数
            e.UrlParam['aa'] = 'xxx';
        }



        function Test() {

            alert(SWW.TD.GetPageValue('TD', 'Control_Value'));
        }


        SWW.F.DOM.Cookie('a', 'cookiev');

        //alert(SWW.F.DOM.Cookie('a'));

      
    </script>


</asp:Content>
