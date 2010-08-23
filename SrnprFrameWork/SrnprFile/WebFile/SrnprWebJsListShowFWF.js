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


        F_GetObjArray: function()
        {
            return SWW.O[this.S_Name];
        },




        F_Init: function(o)
        {
            var oList = this.F_GetObjArray();
            if (oList)
            {
                for (var i = 0, j = oList.length; i < j; i++)
                {

                    alert(oList[i].a);
                }
            }
        }
    }



    if (SWW.LS.F_GetObjArray())
    {
        SWW.J_Ready(function() { SWW.LS.F_Init() });
    }
}



