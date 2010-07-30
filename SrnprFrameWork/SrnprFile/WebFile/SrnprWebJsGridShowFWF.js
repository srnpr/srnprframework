/// <reference path="jquery-1.3.2.min-vsdoc.js"/>
/// <reference path="json2.js"/>






if (!this.SWJGSF)
{

    this.SWJGSF = {};
    }

        (
        function()
        {
            var t = {

            PI: 0,
            PS: 0, 
            PC: 0


        };    
        
        
        
    


        


    }

    )
    ();
    


























var x = { a: "ddd", b: "dfafdaf" };


$.ajax({ url: "/Asmx/GridShowHander.ashx", type: "POST", data: "json="+JSON.stringify(x) , success: GetJson });






//alert(jQuery.parseJSON(x));


function GetJson(t)
{


    $("#jsonshow").html(t);

    var obj = jQuery.parseJSON(t);
    alert(obj.ListString[0].length);

}













