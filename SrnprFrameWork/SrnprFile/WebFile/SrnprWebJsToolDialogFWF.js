/// <reference path="SrnprWebJsWebWidgetFWF.js"/>

/******************************************************
Description: 扩展控件
Author: Liudpc
Create Date: 2010-8-31 16:50:38
******************************************************/



if (SWW && !SWW.TD)
{

    SWW.TD =
    {
        S_WidgetType: 'TD',


        Obj:
        {

    },

    O_List:
        {
            onlyselect:
            {
                url: ''
            }
        }
        ,

    Close: function () {
        SWW.W.Dialog.Close();
    },



    SetValue: function (sK, sV, sD) {

        var u = SWW.F.DOM.Url();

        SWW.W.Dialog.SetValue(u.sww_td_parent_id + '_K', sK);



        SWW.W.Dialog.SetValue(u.sww_td_parent_id + '_Control_Text', sK);

        SWW.W.Dialog.SetValue(u.sww_td_parent_id + '_Control_Value', sV ? sV : sK);

        if (sD) {
            SWW.W.Dialog.SetValue(u.sww_td_parent_id + "_D", sD);

            SWW.W.Dialog.SetValue(u.sww_td_parent_id + "_Control_Description", sD);
        }


    },
    GetValue: function (s) {


        var u = SWW.F.DOM.Url();

        return SWW.W.Dialog.GetValue(u.sww_td_parent_id + (s ? s : ''));

    }
        ,





    OnBeforeOpen: function (e) {



    },

    OpenDialog: function (e) {


        SWW.F.SYS.ExecAF({ f: 'BeforeOpen', w: 'TD', d: id, e: SWW.GS.Obj[id] });

        SWW.W.Dialog.Open(e.url);

    },


    F_Init: function (o) {

        if (o && o.url) {

            var sUrl = SWW.F.DOM.Url(o.url, { sww_td_parent_id: o.Id });


            this.Obj[o.Guid] = o;

            if (document.getElementById(o.Id)) {

                var the = document.getElementById(o.Id);
                var arrUrlQuery = [];
                for (var i = 0; i < the.attributes.length; i++) {

                    var ch = the.attributes[i].nodeName;

                    if ((ch.charCodeAt(0) >= 65) && (ch.charCodeAt(0) <= 90)) {
                        /*
                        str += "nodeName: " + the.attributes[i].nodeName + " <br> ";
                        str += "nodeType: " + the.attributes[i].nodeType + " <br> ";
                        str += "nodeValue: " + the.attributes[i].nodeValue + " <br> ";
                        str += "name: " + the.attributes[i].name + " <br> ";
                        str += "specified: " + the.attributes[i].specified + " <br> ";
                        str += "expando: " + the.attributes[i].expando + " <br> ";
                        str += " <br> --------------- <br> ";
                        */

                        arrUrlQuery.push(the.attributes[i].nodeName + '=' + the.attributes[i].nodeValue);

                    }
                }


                sUrl += '&' + arrUrlQuery.join('&');


            }


            alert(o.Guid);


            var aH = [];

            aH.push('<input paramid="' + o.Id + '_K" id="' + o.Id + '_K" type="text" value="' + SWW.F.DOM.Auto(o.Id + '_Control_Text') + '" onclick="SWW.TD.OpenDialog({ url:\'' + sUrl + '\' });">');
            aH.push('<img src="http://f.xgou.com/AtGang/UI_C/images/search_helpsearch.gif" onclick="SWW.TD.OpenDialog({ url:\'' + sUrl + '\' });">');
            aH.push('<span  id="' + o.Id + '_D"  >' + SWW.F.DOM.Auto(o.Id + '_Control_Value') + '</span>');
            SWW.F.DOM.Html(o.SId, aH.join(''));
        }
        else {
            SWW.F.SYS.Error({ n: 'SWW.TD.F_Init', m: 'o.url is null' });
        }
    }


}




}




