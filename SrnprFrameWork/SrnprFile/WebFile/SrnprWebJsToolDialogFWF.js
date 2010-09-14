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
        O_List:
        {
            onlyselect:
            {
                url: ''
            }
        }
        ,


        SetValue: function (sK, sV, sD)
        {

            var u = SWW.F.DOM.Url();

            SWW.W.Dialog.SetValue(u.sww_td_parent_id + '_K', sK);

            SWW.W.Dialog.SetValue(u.sww_td_parent_id, sV ? sV : sK);

            if (sD)
            {
                SWW.W.Dialog.SetValue(u.sww_td_parent_id + "_D", sD);
            }


        },
        GetValue: function (s)
        {


            var u = SWW.F.DOM.Url();

            return SWW.W.Dialog.GetValue(u.sww_td_parent_id + (s ? s : ''));

        }
        ,


        F_Init: function (o)
        {

            if (o && o.url)
            {

                var sUrl = SWW.F.DOM.Url(o.url, { sww_td_parent_id: o.Id });

                var aH = [];
                aH.push('<input paramid="' + o.Id + '" id="' + o.Id + '" type="hidden" >');
                aH.push('<input paramid="' + o.Id + '_K" id="' + o.Id + '_K" type="text" onclick="SWW.W.Dialog.Open({ url:\'' + sUrl + '\' });">');
                aH.push('<span  id="' + o.Id + '_D"  ></span>');
                SWW.F.DOM.Html(o.SId, aH.join(''));
            }
            else
            {
                SWW.F.SYS.Error({ n: 'SWW.TD.F_Init', m: 'o.url is null' });
            }
        }


    }




}





