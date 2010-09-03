/// <reference path="SrnprWebJsWebWidgetFWF.js"/>


/******************************************************
Description: 数据列表配置文件
Author: Liudpc
Create Date: 2010-8-18 9:34:39
******************************************************/

if (SWW && !SWW.LS)
{

    SWW.LS =
    {
        O_List: {},

        S_WidgetType: 'LS',


        F_Init: function (o)
        {
            //alert(o.WidgetType);
        },

        F_Change: function (g, o)
        {





        },

        F_Success: function (o)
        {
            ///	<summary>
            ///  成功后执行
            ///	</summary>
            ///	<param name="r" type="object">
            ///		成功执行时参数
            ///	</param>

            this.O_List[o.q.Guid] = o;

            var q = o.q;
            var s = o.s;


            var aHtml = [];

            var sShow = '';

            switch (q.ShowType)
            {
                case 'select':
                    for (var i = 0, j = s.Kvd.length; i < j; i++)
                    {
                        aHtml.push('<option value="' + s.Kvd[i].V + '">' + s.Kvd[i].K + '</option>');
                    }
                    sShow = '<select id="' + q.Guid + '" name="' + q.PId + '" onchange="SWW.LS.F_Change(\'' + q.Guid + '\',this)">' + aHtml.join('') + '</select>';
                    break;
                case 'radio':
                    for (var i = 0, j = s.Kvd.length; i < j; i++)
                    {
                        aHtml.push('<input name="' + q.PId + '" type="radio" value="' + s.Kvd[i].V + '">' + s.Kvd[i].K);
                    }
                    sShow = aHtml.join('');
                    break;
                case 'checkbox':
                    for (var i = 0, j = s.Kvd.length; i < j; i++)
                    {
                        aHtml.push('<input name="' + q.PId + '" type="checkbox" value="' + s.Kvd[i].V + '">' + s.Kvd[i].K);
                    }
                    sShow = aHtml.join('');
                    break;

            }

            

            SWW.F.DOM.Html(q.SId, sShow);


        }



    }



  
}



