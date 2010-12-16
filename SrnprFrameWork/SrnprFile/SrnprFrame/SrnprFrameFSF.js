/// <reference path="../WebFile/jquery-1.3.2.min-vsdoc.js"/>



(function () {
    var 
    // Will speed up references to window, and allows munging its name.
	window = this,
    // Will speed up references to undefined, and allows munging its name.
	undefined,
    // Map over jQuery in case of overwrite
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


    srnprframe.W =
    {
        Init: function () { }
    }




})();


