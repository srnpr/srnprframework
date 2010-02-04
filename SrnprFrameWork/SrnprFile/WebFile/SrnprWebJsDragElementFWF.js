/// <reference path="SrnprWebJsCommonFWF.js"/>




SWJC.AddEvent(window, "load", SrnprWebJsDragElementFWFPreLoad);
var SrnprWebJsDragElementFWFObj = {}

var SrnprWebJsDragElementFWFElementArray = new Array()

function SrnprWebJsDragElementFWFPreLoad()
{
    dragobj = SrnprWebJsDragElementFWFObj;
    dragArray = SrnprWebJsDragElementFWFElementArray;
    var o = document.getElementsByTagName("div")
    for (var i = 0; i < o.length; i++)
    {

        if (o[i].className == "Menubox" && o[i].parentNode && o[i].parentNode.id.indexOf("srnprwebjsdragelementfwfid") > -1)
        {

            if (o[i].parentNode.id.split('-').length == 2)
            {
                o[i].onmousedown = function(e)
                {

                    if (dragobj.o != null)
                        return false
                    e = e || event

                    dragobj.o = this.parentNode
                    dragobj.xy = SWJC.GetPoint(dragobj.o)
                    dragobj.xx = new Array((e.x - dragobj.xy[1]), (e.y - dragobj.xy[0]))
                    dragobj.o.style.width = (dragobj.xy[2] - 10) + "px"
                    dragobj.o.style.height = dragobj.xy[3] + "px"
                    dragobj.o.style.left = (e.clientX - dragobj.xx[0]) + "px"
                    dragobj.o.style.top = (e.clientY - dragobj.xx[1]) + "px"
                    dragobj.o.style.position = "absolute"
                    var om = document.createElement("div")
                    dragobj.otemp = om
                    om.style.width = dragobj.xy[2] + "px"
                    om.style.height = dragobj.xy[3] + "px"
                    dragobj.o.parentNode.insertBefore(om, dragobj.o)
                    return false
                }

                dragArray.push(o[i].parentNode);
            }




        }
    }


    SWJC.AddEvent(document, "selectstart", function() { return false });

    SWJC.AddEvent(document, "mouseup",
    function()
    {

        if (dragobj.o != null)
        {
            dragobj.o.style.width = "auto"
            dragobj.o.style.height = "auto"
            dragobj.otemp.parentNode.insertBefore(dragobj.o, dragobj.otemp)
            dragobj.o.style.position = ""
            SWJC.DelElement(dragobj.otemp)
            dragobj = {}




            var eDrag = document.getElementById("dev_workspace_show_drag_e").childNodes;
            var sDrag = "";
            for (var i = 0, j = eDrag.length; i < j; i++)
            {
                var s = eDrag[i].id.split("-")[1];
                if (sDrag == "")
                {
                    sDrag = s;
                }
                else
                {
                    sDrag = sDrag + "," + s;
                }

            }
            var t = SWJC.Ajax({ u: "/Ajax/WorkSpaceAjax.aspx?id=" + sDrag });



        }
    }
    );


    SWJC.AddEvent(document, "mousemove",
    function(e)
    {
        e = e || event;
        if (dragobj.o != null)
        {
            dragobj.o.style.left = (e.x - dragobj.xx[0]) + "px";
            dragobj.o.style.top = (e.y - dragobj.xx[1]) + "px";


            for (var i = 0, j = dragArray.length; i < j; i++)
            {
                if (dragArray[i].id.split('-').length == 2)
                {
                    if (dragArray[i] == dragobj.o)
                        continue

                    var b;

                    var a = SWJC.GetPoint(dragArray[i])

                    if (e.x > a[1] && e.x < (a[1] + a[2]) && e.y > a[0] && e.y < (a[0] + a[3]))
                    {
                        if (e.y < (a[0] + a[3] / 2))
                            b = 1;
                        else
                            b = 2;
                    } else
                        b = 0;




                    if (b == 0)
                        continue
                    dragobj.otemp.style.width = dragArray[i].offsetWidth
                    if (b == 1)
                    {
                        dragArray[i].parentNode.insertBefore(dragobj.otemp, dragArray[i])
                    } else
                    {
                        if (dragArray[i].nextSibling == null)
                        {
                            dragArray[i].parentNode.appendChild(dragobj.otemp)
                        } else
                        {
                            dragArray[i].parentNode.insertBefore(dragobj.otemp, dragArray[i].nextSibling)
                        }
                    }
                    return



                }


            }





            /*

            for (var i = 0; i < 12; i++)
            {
            if (SWJC("m" + i))
            {
            if (SWJC("m" + i) == dragobj.o)
            continue

                    var b;

                    var a = SWJC.GetPoint(SWJC("m" + i))

                    if (e.x > a[1] && e.x < (a[1] + a[2]) && e.y > a[0] && e.y < (a[0] + a[3]))
            {
            if (e.y < (a[0] + a[3] / 2))
            b = 1;
            else
            b = 2;
            } else
            b = 0;




                    if (b == 0)
            continue
            dragobj.otemp.style.width = SWJC("m" + i).offsetWidth
            if (b == 1)
            {
            SWJC("m" + i).parentNode.insertBefore(dragobj.otemp, SWJC("m" + i))
            } else
            {
            if (SWJC("m" + i).nextSibling == null)
            {
            SWJC("m" + i).parentNode.appendChild(dragobj.otemp)
            } else
            {
            SWJC("m" + i).parentNode.insertBefore(dragobj.otemp, SWJC("m" + i).nextSibling)
            }
            }
            return
            }
            }
            for (var j = 0; j < 3; j++)
            {
            if (SWJC("dom" + j))
            {
            if (SWJC("dom" + j).innerHTML.indexOf("div") > -1 || SWJC("dom" + j).innerHTML.indexOf("DIV") > -1)
            continue
            var op = SWJC.GetPoint(SWJC("dom" + j))
            if (e.x > (op[1] + 10) && e.x < (op[1] + op[2] - 10))
            {
            SWJC("dom" + j).appendChild(dragobj.otemp)
            dragobj.otemp.style.width = (op[2] - 10) + "px"
            }
            }
            }
            */
        }
    }
    );

}















