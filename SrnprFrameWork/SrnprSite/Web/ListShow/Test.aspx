<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SrnprSite.Web.ListShow.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">



<input type="text" value="abc" />
<input type="button" value="提交" />
<div id="ddddd"></div>


<div id="dshow"></div>




<textarea id="tshow" rows="30" cols="100"></textarea>

<script>




    var t = {};
    t.__type = "ListShowRequestWWE:http:\/\/srnprframework\/srnprweb";
    t.MyString = "aa";
    t.Guid = "ff";
    t.WidgetType = "LS";
    t.Id = "abc";
    t.SId = "ddddd";
    t.ShowType = "select";
    SWW.I("LS", t);
    //SWW.I("LS", { a: 'dddd' });



   
    

    SWW.A('LS', 'Success', t.Id,
      function(rq, rs, s)
    {

        SWW.J("#dshow").text(s);
        SWW.J("#tshow").val(s);
    }
    )


</script>

</asp:Content>
