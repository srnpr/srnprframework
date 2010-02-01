﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Drag.aspx.cs" Inherits="Widget_Drag" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        
        
body {margin:0px;padding:0px;font-size:12px;text-align:center;}
body > div {text-align:center; margin-right:auto; margin-left:auto;} 
.content{width:900px;}
.content .left{
 float:left;
 width:20%;
 border:1px solid #FF0000;
 margin:3px;
}
.content .center{float:left;border:1px solid #FF0000;margin:3px;width:57%}
.content .right{float:right;width:20%;border:1px solid #FF0000;margin:3px}
.mo{height:auto;border:1px solid #CCC;margin:3px;background:#FFF}
.mo h1{background:#ECF9FF;height:18px;padding:3px;cursor:move}
.mo .nr{height:80px;border:1px solid #F3F3F3;margin:2px}
h1{margin:0px;padding:0px;text-align:left;font-size:12px}
</style>
<script>
var dragobj={}
window.onerror=function(){return false}
function on_ini(){
 String.prototype.inc=function(s){return this.indexOf(s)>-1?true:false}
 var agent=navigator.userAgent
 window.isOpr=agent.inc("Opera")
 window.isIE=agent.inc("IE")&&!isOpr
 window.isMoz=agent.inc("Mozilla")&&!isOpr&&!isIE
 if(isMoz){
  Event.prototype.__defineGetter__("x",function(){return this.clientX+2})
  Event.prototype.__defineGetter__("y",function(){return this.clientY+2})
 }
 basic_ini()
}
function basic_ini(){
 window.$=function(obj){return typeof(obj)=="string"?document.getElementById(obj):obj}
 window.oDel=function(obj){if($(obj)!=null){$(obj).parentNode.removeChild($(obj))}}
}
window.onload=function(){
 on_ini()
 var o=document.getElementsByTagName("h1")
 for(var i=0;i<o.length;i++){
  o[i].onmousedown=function(e){
   if(dragobj.o!=null)
    return false
   e=e||event
   dragobj.o=this.parentNode
   dragobj.xy=getxy(dragobj.o)
   dragobj.xx=new Array((e.x-dragobj.xy[1]),(e.y-dragobj.xy[0]))
   dragobj.o.style.width=dragobj.xy[2]+"px"
   dragobj.o.style.height=dragobj.xy[3]+"px"
   dragobj.o.style.left=(e.x-dragobj.xx[0])+"px"
   dragobj.o.style.top=(e.y-dragobj.xx[1])+"px"   
   dragobj.o.style.position="absolute"
   var om=document.createElement("div")
   dragobj.otemp=om
   om.style.width=dragobj.xy[2]+"px"
   om.style.height=dragobj.xy[3]+"px"
   dragobj.o.parentNode.insertBefore(om,dragobj.o)
   return false
  }
 }
}
document.onselectstart=function(){return false}

document.onmouseup=function(){
 if(dragobj.o!=null){
  dragobj.o.style.width="auto"
  dragobj.o.style.height="auto"
  dragobj.otemp.parentNode.insertBefore(dragobj.o,dragobj.otemp)
  dragobj.o.style.position=""
  oDel(dragobj.otemp)
  dragobj={}
 }
}
document.onmousemove=function(e){
 e=e||event
 if(dragobj.o!=null){
  dragobj.o.style.left=(e.x-dragobj.xx[0])+"px"
  dragobj.o.style.top=(e.y-dragobj.xx[1])+"px"
  createtmpl(e)
 }
}
function getxy(e){
 var a=new Array()
 var t=e.offsetTop;
 var l=e.offsetLeft;
 var w=e.offsetWidth;
 var h=e.offsetHeight;
 while(e=e.offsetParent){
  t+=e.offsetTop;
  l+=e.offsetLeft;
 }
 a[0]=t;a[1]=l;a[2]=w;a[3]=h
  return a;
}
function inner(o,e){
 var a=getxy(o)
 if(e.x>a[1]&&e.x<(a[1]+a[2])&&e.y>a[0]&&e.y<(a[0]+a[3])){
  if(e.y<(a[0]+a[3]/2))
   return 1;
  else
   return 2;
 }else
  return 0;
}
function createtmpl(e){
 for(var i=0;i<12;i++){
  if($("m"+i)==dragobj.o)
   continue
  var b=inner($("m"+i),e)
  if(b==0)
   continue
  dragobj.otemp.style.width=$("m"+i).offsetWidth
  if(b==1){
   $("m"+i).parentNode.insertBefore(dragobj.otemp,$("m"+i))
  }else{
   if($("m"+i).nextSibling==null){
    $("m"+i).parentNode.appendChild(dragobj.otemp)
   }else{
    $("m"+i).parentNode.insertBefore(dragobj.otemp,$("m"+i).nextSibling)
   }
  }
  return
 }
 for(var j=0;j<3;j++){
  if($("dom"+j).innerHTML.inc("div")||$("dom"+j).innerHTML.inc("DIV"))
   continue
  var op=getxy($("dom"+j))
  if(e.x>(op[1]+10)&&e.x<(op[1]+op[2]-10)){
   $("dom"+j).appendChild(dragobj.otemp)
   dragobj.otemp.style.width=(op[2]-10)+"px"
  }
 }
}
</script>
</head>
<body>
<div class=content>
 <div class=left id=dom0>
  <div class=mo id=m0>
   <h1>dom0</h1>
   <div class="nr"></div>
  </div>
  <div class=mo id=m1>
   <h1>dom1</h1><div class="nr"></div>
  </div>
  <div class=mo id=m2><h1>dom2</h1><div class="nr"></div></div>
  <div class=mo id=m3><h1>dom3</h1><div class="nr"></div></div>
 </div>
 <div class=center id=dom1>
  <div class=mo id=m4><h1>dom4</h1><div class="nr"><iframe src=""></iframe></div></div>
  <div class=mo id=m5><h1>dom5</h1><div class="nr"></div></div>
  <div class=mo id=m6><h1>dom6</h1><div class="nr"></div></div>
  <div class=mo id=m7><h1>dom7</h1><div class="nr"></div></div>
 </div>
 <div class=right id=dom2>
  <div class=mo id=m8><h1>dom8</h1><div class="nr"></div></div>
  <div class=mo id=m9><h1>dom9</h1><div class="nr"></div></div>
  <div class=mo id=m10><h1>dom10</h1><div class="nr"></div></div>
  <div class=mo id=m11><h1>dom11</h1><div class="nr"></div></div>
 </div>
</div>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
