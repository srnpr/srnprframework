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

        S_Name: 'LS',


        F_Init: function(o)
        {
            //alert(o.WidgetType);
        },

        F_Success: function(q, s)
        {
            //alert(q.WidgetType);

            var aHtml = [];

            var sShow = '';

            switch (q.ShowType)
            {
                case 'select':
                    for (var i = 0, j = s.Kvd.length; i < j; i++)
                    {
                        aHtml.push('<option value="' + s.Kvd[i].V + '">' + s.Kvd[i].K + '</option>');
                    }
                    sShow = '<select>' + aHtml.join('') + '</select>';
                    break;
                case 'radio':
                    for (var i = 0, j = s.Kvd.length; i < j; i++)
                    {
                        aHtml.push('<input name="'+q.PId+'" type="radio" value="' + s.Kvd[i].V + '">' + s.Kvd[i].K);
                    }
                    sShow =  aHtml.join('');
                    break;
                case 'checkbox':
                    for (var i = 0, j = s.Kvd.length; i < j; i++)
                    {
                        aHtml.push('<input name="' + q.PId + '" type="checkbox" value="' + s.Kvd[i].V + '">' + s.Kvd[i].K);
                    }
                    sShow = aHtml.join('');
                    break;

            }

            SWW.J('#' + q.SId).html(sShow);


        }



    }



  
}



