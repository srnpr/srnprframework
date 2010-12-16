/// <reference path="SrnprFrameFSF.js"/>

(function () {



    srnprframe.GS = function () {

        return new srnprframe.GS.z.init();

    };

    srnprframe.GS.z = srnprframe.GS.prototype =
     {

         init: function () { },

         get: function () { },

         OnInit: function () { }

     };


     srnprframe.GS.z.init.prototype = srnprframe.GS.z;


})();


 srnprframe.GS().OnLoad(function (e) { });



