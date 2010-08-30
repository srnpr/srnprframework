<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SrnprSite.Web.ListShow.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">



<input type="text" value="abc" />
<input type="button" value="提交" />
<div id="ddddd"></div>
<div id="ddddd2"></div>
<div id="ddddd3"></div>
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
    t.PId = "t";
    
    
    
    SWW.I("LS", t);

    var t2 = {};
    t2.__type = "ListShowRequestWWE:http:\/\/srnprframework\/srnprweb";
    t2.MyString = "aa";
    t2.Guid = "abcdeff";
    t2.WidgetType = "LS";
    t2.Id = "abc";
    t2.SId = "ddddd2";
    t2.ShowType = "radio";
    t2.PId = "t2";
    SWW.I("LS", t2);

    var t3 = {};
    t3.__type = "ListShowRequestWWE:http:\/\/srnprframework\/srnprweb";
    t3.MyString = "aa";
    t3.Guid = "abcdeff";
    t3.WidgetType = "LS";
    t3.Id = "abc";
    t3.SId = "ddddd3";
    t3.ShowType = "checkbox";
    t3.PId = "t3";
    SWW.I("LS", t3);
    
   
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
