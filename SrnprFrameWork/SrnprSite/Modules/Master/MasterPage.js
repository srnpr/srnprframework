


(function()
{
    window.MasterPage = {};


    MasterPage.Message = function(m)
    {

        document.getElementById("srdmainmessage").className = "srdmainmessage";


        document.getElementById("srdmainmessage").innerHTML = '<div class="srdmainmessagetitle"><a href="javascript:MasterPage.MessageClose()">关闭</a></div><div class="srdmainmessagecontent">' + m + '</div>';


    }
    MasterPage.MessageClose = function()
    {
        document.getElementById("srdmainmessage").style.display = "none";
    }



}
)();
