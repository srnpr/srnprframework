

/// <reference path="SrnprWebJsWebWidgetFWF.js"/>



(function () {
    var 
	window = this,
	undefined,

	_srnprframe = window.srnprframe,

    _Z = window.Z;

    srnprframe = window.srnprframe = window.Z = function () {
        return new srnprframe.z.z_init();
    };
    srnprframe.z = srnprframe.prototype =
     {
         z_init: function () { },
         OnInit: function () { }

     };
    srnprframe.z.z_init.prototype = srnprframe.z;


    srnprframe.W = SWW.W;

    srnprframe.F = SWW.F;







    //开始定义扩展GS
    srnprframe.GS = function () {

        return new srnprframe.GS.z.init();

    };

    srnprframe.GS.z = srnprframe.GS.prototype =
     {
         init: function () { },
         OnInit: function () { }
     };


    srnprframe.GS.z.init.prototype = srnprframe.GS.z;


})();



Z.GS().OnInit(function (e) { });

