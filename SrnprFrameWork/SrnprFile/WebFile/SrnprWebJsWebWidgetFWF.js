﻿/// <reference path="jquery-1.3.2.min-vsdoc.js"/>

/******************************************************
Description: 核心类文件 所有widget使用的初始化及加载文件
            目前使用Jquery框架 所有引用调用J
            
            公开属性：
            J：jQuery
            O：表示对象系列
            C：配置文件
            I：初始化
            F：外部调用函数
            Z：基类使用自身操作
            M：消息系列配置
            A：扩展系列
            W：调用控件系列


            
            

            参数定义：
            第一标识
            s:字符串(str)
            o:对象(obj)
            i:数字(int)
            f:函数(fun)
            b:布尔型(bool)
            a:数组(arr)

            第二标识：
            [y]:必填参数
            n:非必填参数

            
Author: Liudpc
Create Date: 2010-8-18 9:27:36
******************************************************/

if (!window.SWW)
{
    window.SWW =
   {
       //配置
       C:
       {
           //系统加载脚本文件  u：脚本文件名称  n：需要加载的其他脚本  w：window基本名称（全局）  q：服务端类名称（JSON反向解析使用,如果不存在则表明不需初始化） 
           JS:
           {
               Json: { u: 'json2.js', w: 'JSON' },
               JQuery: { u: 'jquery-1.4.2.min.js', w: 'jQuery' },
               GS: { u: 'SrnprWebJsGridShowFWF.js', n: ['JQuery'], q: 'GridShowRequestWWE' },
               LS: { u: 'SrnprWebJsListShowFWF.js', n: ['JQuery'], q: 'ListShowRequestWWE' },
               TD: { u: 'SrnprWebJsToolDialogFWF.js', n: ['JQuery'] },
               SWW: 'SrnprWebJsWebWidgetFWF.js'
           },

           //Ajax相关配置
           Ajax:
           {
               Url: '/Ashx/WebWidgetHandler.ashx'
           },

           //基本命名空间
           BaseNamespace: 'http://srnprframework/srnprweb',

           Flag: { Debug: false },


           //已经加载的js配置
           JSLoad: {},

           //初始化时配置
           Init:
           {
               //当前加载次数
               N: 0,
               //最大加载次数
               M: 30,
               //加载时间间隔
               T: 200,
               //是否添加ready加载函数
               LoadFlag: false
           },

           //基本信息
           BaseInfo:
           {
               //版本编号
               Version: '1.0.0.0'
           }
       },

       //消息系列
       M:
        {
            //系统错误时提示消息
            SE:
            {
                ET: '【系统消息】：系统出现异常错误，请联系管理员',
                EN: '\n【错误标识】：',
                EM: '\n【错误内容】：',
                IM: '系统尝试初始化失败！',
                AS: '无法加载类型',
                FEF: '加载类型{0}函数名{1}时错误，参数为：{2}'
            },
            ME:
            {
                Load: '正在加载中……',
                Query: '【提示消息】：系统正在加载数据，请稍后点击该按钮！'
            }

        },

       //Jquery适配器  
       J: jQuery,

       //Req提交参数  Res返回参数  AF扩展函数  Guid唯一标识集 系统自动检测全局唯一编号  Log日志系列
       O: { Req: {}, Res: {}, AF: {}, Guid: {}, Log: { Debug: []} },

       //扩展函数系列
       A: function (sType, sFunc, sId, fExec) {
           ///	<summary>
           ///  扩展调用接口
           ///	</summary>
           ///	<param name="sType" type="str">
           ///		类型
           ///	</param>
           ///	<param name="sFunc" type="str">
           ///		函数操作 目前支持：Success(调用Ajax成功后)
           ///	</param>
           ///	<param name="sId" type="str">
           ///		编号
           ///	</param>
           ///	<param name="fExec" type="fun">
           ///		函数  扩展当操作执行时的执行函数 不同函数所需参数不一致
           ///	</param>
           if (!SWW.O.AF[sType]) {
               SWW.O.AF[sType] = {};
           }
           if (!SWW.O.AF[sType][sFunc]) {
               SWW.O.AF[sType][sFunc] = {};
           }

           if (!SWW.O.AF[sType][sFunc][sId]) {
               SWW.O.AF[sType][sFunc][sId] = [];
           }


           SWW.O.AF[sType][sFunc][sId].push(fExec);
       },

       //函数系列
       F:
       {

           DOM:
           {


               Cookie: function (sName, snValue, snOptions) {

                   if (typeof snValue != 'undefined') {
                       snOptions = snOptions || {};
                       if (snValue === null) {
                           snValue = '';
                           snOptions = $.extend({}, snOptions);
                           snOptions.expires = -1;
                       }
                       var expires = '';
                       if (snOptions.expires && (typeof snOptions.expires == 'number' || snOptions.expires.toUTCString)) {
                           var date;
                           if (typeof snOptions.expires == 'number') {
                               date = new Date();
                               date.setTime(date.getTime() + (snOptions.expires * 24 * 60 * 60 * 1000));
                           } else {
                               date = snOptions.expires;
                           }
                           expires = '; expires=' + date.toUTCString();
                       }
                       var path = snOptions.path ? '; path=' + (snOptions.path) : '';
                       var domain = snOptions.domain ? '; domain=' + (snOptions.domain) : '';
                       var secure = snOptions.secure ? '; secure' : '';
                       document.cookie = [sName, '=', encodeURIComponent(snValue), expires, path, domain, secure].join('');
                   } else { // only sName given, get cookie
                       var cookieValue = null;
                       if (document.cookie && document.cookie != '') {
                           var cookies = document.cookie.split(';');
                           for (var i = 0; i < cookies.length; i++) {
                               var cookie = jQuery.trim(cookies[i]);

                               if (cookie.substring(0, sName.length + 1) == (sName + '=')) {
                                   cookieValue = decodeURIComponent(cookie.substring(sName.length + 1));
                                   break;
                               }
                           }
                       }
                       return cookieValue;
                   }



               },



               Html: function (sElement, snHtml) {
                   ///	<summary>
                   ///  设置或返回元素的html内容
                   ///	</summary>
                   ///	<param name="sElement" type="str">
                   ///		元素名称
                   ///	</param>
                   ///	<param name="snHtml" type="str">
                   ///		元素的html
                   ///	</param>
                   if (snHtml) {
                       SWW.J('#' + sElement).html(snHtml);
                   }
                   else {
                       return SWW.J('#' + sElement).html() ? SWW.J('#' + sElement).html() : '';
                   }
               },
               Text: function (sElement, snText) {
                   ///	<summary>
                   ///  设置或返回元素的Text
                   ///	</summary>
                   ///	<param name="sElement" type="str">
                   ///		元素名称
                   ///	</param>
                   ///	<param name="snText" type="str">
                   ///		元素的Text
                   ///	</param>
                   if (snText) {
                       SWW.J('#' + sElement).text(snText);
                   }
                   else {
                       return SWW.J('#' + sElement).text();
                   }
               },
               Value: function (sElement, snValue) {
                   ///	<summary>
                   ///  设置或返回元素的值
                   ///	</summary>
                   ///	<param name="sElement" type="str">
                   ///		元素名称
                   ///	</param>
                   ///	<param name="snText" type="str">
                   ///		元素的值
                   ///	</param>
                   if (snValue) {
                       SWW.J('#' + sElement).val(snValue);
                   }
                   else {
                       return SWW.J('#' + sElement).val() ? SWW.J('#' + sElement).val() : '';
                   }
               },
               Get: function (sn) {
                   ///	<summary>
                   ///  返回元素
                   ///	</summary>
                   ///	<param name="sn" type="str">
                   ///		元素名称
                   ///	</param>
                   return sn ? document.getElementById(sn) : document;
               },


               Auto: function (sE, sV) {

                   sE = '#' + sE;
                   var bV = SWW.J(sE).is("input|textarea");


                   if (sV != undefined) {
                       bV ? SWW.J(sE).val(sV) : SWW.J(sE).html(sV);
                   }
                   else {
                       return bV ? SWW.J(sE).val() : SWW.J(sE).html();
                   }

               },

               Display: function (s, bn) {
                   ///	<summary>
                   ///  显示
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		元素id
                   ///	</param>
                   ///	<param name="bn" type="bool">
                   ///		是否显示 默认不显示 如果传true标记为显示
                   ///	</param>

                   if (this.Get(s))
                       this.Get(s).style.display = (!bn ? 'none' : '');


               },
               Url: function (s, on) {
                   ///	<summary>
                   ///  返回url参数
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		url地址
                   ///	</param>
                   ///	<param name="on" type="obj">
                   ///		链接地址上添加的对象   如果有该参数则返回的是拼接链接的字符串
                   ///	</param>
                   var r;
                   if (!s) {
                       s = location.href;
                   }

                   var iIndex = s.indexOf('?');
                   if (!on) {
                       if (iIndex > -1) {
                           r = {};

                           var u = s.substr(iIndex + 1);
                           var a = u.split('&');
                           for (var i = 0, j = a.length; i < j; i++) {
                               var iN = a[i].indexOf('=');

                               r[a[i].substr(0, iN)] = a[i].substr(iN + 1);
                           }
                       }

                       return r;
                   }
                   else {
                       return s + (iIndex > -1 ? '&' : '?') + SWW.F.SYS.GetObjPrototype(on);
                   }

               }
           },


           JF:
           {
               Ready: function (f) {
                   SWW.J().ready(f);
               }
           }
           ,

           //字符串相关函数
           STR:
           {

               Pad: function (sB, sP, iM) {
                   ///	<summary>
                   ///  用字符补齐字符串长度
                   ///	</summary>
                   ///	<param name="sB" type="str">
                   ///		原始字符
                   ///	</param>
                   ///	<param name="sP" type="str">
                   ///		添加的字符
                   ///	</param>
                   ///	<param name="iM" type="int">
                   ///		补全后的长度
                   ///	</param>

                   var a = '';

                   sB = sB.toString();
                   while (sB.length < Math.abs(iM)) {
                       sB = iM > 0 ? (sP + sB) : (sB + sP);
                   }
                   return sB;
               },
               HtmlEncode: function (s) {
                   ///	<summary>
                   ///  html编码字符串
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		原始字符串
                   ///	</param>

                   if (s.length == 0) return "";
                   s = s.replace(/&/g, "&amp;");
                   s = s.replace(/</g, "&lt;");
                   s = s.replace(/>/g, "&gt;");
                   s = s.replace(/\'/g, "&#39;");
                   s = s.replace(/\"/g, "&quot;");
                   return s;
               },
               HtmlDecode: function (s) {
                   ///	<summary>
                   ///  html解码字符串
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		待解码的字符串
                   ///	</param>

                   if (s.length == 0) return "";
                   s = s.replace(/&amp;/g, "&");
                   s = s.replace(/&lt;/g, "<");
                   s = s.replace(/&gt;/g, ">");
                   s = s.replace(/&#39;/g, "\'");
                   s = s.replace(/&quot;/g, "\"");
                   return s;
               },


               StringToDate: function (sn) {
                   ///	<summary>
                   ///  字符串转换为日期
                   ///	</summary>
                   ///	<param name="sn" type="str">
                   ///		日期的字符串表示 例如：2010-01-01 01:01:01
                   ///	</param>


                   if (!sn) {
                       sn = new Date();
                   }
                   else if (typeof (sn) == 'string') {
                       sn = new Date(Date.parse(sn.replace(/-/g, "/")));

                   }
                   return sn;
               },

               DateTime: function (snD, snR) {
                   ///	<summary>
                   ///  日期格式化
                   ///	</summary>
                   ///	<param name="snD" type="str">
                   ///		要替换的日期格式  如果传空表示当前时间
                   ///	</param>
                   ///	<param name="snR" type="str">
                   ///		转换的格式 支持：yyyy(年) MM(月) dd(日) hh(时) mm(分) ss(秒) ms(毫秒) 默认为：yyyy-MM-dd hh:mm:ss
                   ///	</param>

                   var d = this.StringToDate(snD);

                   if (!snR) {
                       snR = "yyyy-MM-dd hh:mm:ss";
                   }

                   return snR.replace('yyyy', d.getYear()).replace('MM', SWW.F.STR.Pad(d.getMonth(), '0', 2)).replace('dd', SWW.F.STR.Pad(d.getDate(), '0', 2)).replace('hh', SWW.F.STR.Pad(d.getHours(), '0', 2)).replace('mm', SWW.F.STR.Pad(d.getMinutes(), '0', 2)).replace('ss', SWW.F.STR.Pad(d.getSeconds(), '0', 2)).replace('ms', d.getMilliseconds());


               },

               Trim: function (s, sn) {
                   ///	<summary>
                   ///  格式化字符串
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		原字符串
                   ///	</param>
                   ///	<param name="sn" type="str">
                   ///		替换掉的字符串
                   ///	</param>
                   if (!sn) {
                       sn = '\\s';
                   }

                   var r = new RegExp("(^" + sn + "*)|(" + sn + "*$)", "g");
                   return s.replace(r, '');





               },


               Format: function (s, a) {
                   ///	<summary>
                   ///  格式替换函数
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		原始字符串
                   ///	</param>
                   ///	<param name="a" type="arr">
                   ///		替换的数组
                   ///	</param>

                   if (a != undefined) {
                       if (typeof (a) != 'object') {
                           a = [a];
                       }

                       for (var i = 0, j = a.length; i < j; i++) {

                           var r = new RegExp("\\u007B" + i + "\\u007D", "g");

                           s = s.replace(r, a[i]);
                       }
                   }
                   return s;
               }

           },



           SYS:
            {
                ///	<summary>
                ///  扩展调用接口
                ///	</summary>

                Alert: function (s) {
                    ///	<summary>
                    ///  弹出提示信息
                    ///	</summary>
                    ///	<param name="s" type="str">
                    ///		提示信息
                    ///	</param>
                    alert(s);
                },




                Error: function (o) {
                    ///	<summary>
                    ///  出现严重错误时提示
                    ///	</summary>
                    ///	<param name="o" type="obj">
                    ///  错误内容  o{n:错误标识,m:错误内容,[p]:array 替换参数}
                    ///	</param>

                    if (o.p && o.m) {
                        for (var i = 0, j = o.p.length; i < j; i++) {
                            o.m = o.m.replace('{' + i + '}', o.p[i]);
                        }
                    }

                    this.Alert(SWW.M.SE.ET + (o.n ? SWW.M.SE.EN + o.n : '') + (o.m ? SWW.M.SE.EM + o.m : ''));
                },

                Run: function (o) {
                    ///	<summary>
                    ///  执行提交函数
                    ///	</summary>
                    ///	<param name="o" type="obj">
                    ///		request
                    ///	</param>

                    return SWW.Z.Ajax(o);
                },

                GetGuid: function (r) {
                    ///	<summary>
                    ///  生成Guid
                    ///	</summary>
                    ///	<param name="r" type="string">
                    ///		生成模板 example:8-12-16-20
                    ///	</param>
                    var a = (r ? r : '8-12-16-20').split('-');
                    var al = a.length;
                    var guid = "guid";
                    for (var i = 1 + guid.length; i <= 32; i++) {
                        var g = Math.floor(Math.random() * 16.0).toString(16);
                        guid += g;
                        for (var n = 0; n < al; n++) {
                            if (i == a[n]) {
                                guid += '-';
                            }
                        }
                    }

                    //判断是否重复
                    if (SWW.O.Guid[guid]) {
                        guid = this.GetGuid();
                    }
                    else {
                        SWW.O.Guid[guid] = guid;
                    }

                    return guid;

                },

                ItemBase: function () {
                    ///	<summary>
                    ///  返回基本对象
                    ///	</summary>

                    return { __type: '' };
                },


                InitReq: function (e) {
                    ///	<summary>
                    ///  重新初始化对象
                    ///	</summary>
                    ///	<param name="e" type="obj">
                    ///		request
                    ///	</param>

                    if (e && e.WidgetType && (!e.__type || !e.Guid)) {
                        var o = this.ItemBase();
                        if (!e.__type && SWW.C.JS[e.WidgetType] && SWW.C.JS[e.WidgetType].q) {
                            o.__type = SWW.C.JS[e.WidgetType].q + ':' + SWW.C.BaseNamespace;

                        }
                        if (!e.Guid) {
                            e.Guid = this.GetGuid();
                        }
                        for (var p in e) {
                            o[p] = e[p];
                        }
                        e = o;
                    }
                    return e;
                },

                ExecFunc: function (o) {
                    ///	<summary>
                    ///  执行函数
                    ///	</summary>
                    ///	<param name="o" type="object">
                    ///	对象{t:类型,f:函数名称,e:参数}
                    ///  目前支持函数：
                    ///  F_Success({q:提交对象,s:返回对象})
                    ///	</param>

                    if (SWW[o.t] && SWW[o.t][o.f]) {
                        SWW[o.t][o.f](o.e);
                        if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.f.sys.execfunc', o, 'sww2003', [o.t, o.f]);
                    }
                    else {
                        //SWW.F.SYS.Error({ n: 'SWW.F.SYS.ExecFunc', m: SWW.M.SE.FEF, p: [o.t, o.f, this.GetObjPrototype(o.e)] });
                        if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.f.sys.execfunc', o, 'sww2004', [o.t, o.f]);
                    }
                },


                GetObjPrototype: function (o) {
                    ///	<summary>
                    ///  得到一个对象的属性字符串
                    ///	</summary>
                    ///	<param name="o" type="obj">
                    ///		对象
                    ///	</param>


                    var r = [];

                    for (var p in o) {
                        r.push('' + p + '=' + o[p]);
                    }

                    return r.join('&');


                },

                ExecAF: function (o) {
                    ///	<summary>
                    ///  执行扩展函数
                    ///	</summary>
                    ///	<param name="o" type="obj">
                    ///		对象{f:函数名称,w:扩展类型,d:编号,e:参数}
                    ///	</param>

                    if (SWW.O.AF[o.w] && SWW.O.AF[o.w][o.f] && SWW.O.AF[o.w][o.f][o.d]) {
                        //SWW.O.AF[o.w][o.f][o.d](o.e);

                        for (var i = 0, j = SWW.O.AF[o.w][o.f][o.d].length; i < j; i++) {
                            SWW.O.AF[o.w][o.f][o.d][i](o.e);
                        }



                        if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.f.sys.execaf', o, 'sww2005', [o.w, o.f]);
                    }
                    else {
                        //if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.f.sys.execaf', o, 'sww2001', [o.w, o.f]);
                    }

                }
            },

           OBJ:
            {
                Clone: function (o) {
                    ///	<summary>
                    ///  浅度克隆一个对象
                    ///	</summary>
                    ///	<param name="o" type="obj">
                    ///		对象
                    ///	</param>
                    var r = {};
                    for (var p in o) {
                        r[p] = o[p];
                    }
                    return r;
                },
                CloneAll: function (o) {
                    ///	<summary>
                    ///  深度克隆一个对象
                    ///	</summary>
                    ///	<param name="o" type="obj">
                    ///		对象
                    ///	</param>
                    return SWW.F.JSON.JsonFromString(SWW.F.JSON.StringFromJson(o));
                },

                TypeName: function (o) {
                    return typeof (o);
                },

                IsObj: function (o) {
                    return this.TypeName(o) == 'object';
                },
                IsStr: function (o) {
                    return this.TypeName(o) == "string";
                }
            },

           JSON:
            {

                Obj_Json:
            {
                cx: /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
                escapable: /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
                gap: null,
                indent: null,
                meta: {    // table of character substitutions
                    '\b': '\\b',
                    '\t': '\\t',
                    '\n': '\\n',
                    '\f': '\\f',
                    '\r': '\\r',
                    '"': '\\"',
                    '\\': '\\\\'
                },
                rep: null


            },

                Fun_Quote: function (string) {

                    var meta = this.Obj_Json.meta;

                    this.Obj_Json.escapable.lastIndex = 0;
                    return this.Obj_Json.escapable.test(string) ?
            '"' + string.replace(this.Obj_Json.escapable, function (a) {

                var c = meta[a];
                return typeof c === 'string' ? c :
                    '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
            }) + '"' :
            '"' + string + '"';
                },

                Fun_Str: function (key, holder) {



                    var i,
                    k,
                    v,
                    length,
                    mind = this.Obj_Json.gap,
                    partial,
                    value = holder[key];


                    if (value && typeof value === 'object' && typeof value.toJSON === 'function') {
                        value = value.toJSON(key);
                    }


                    if (typeof this.Obj_Json.rep === 'function') {
                        value = this.Obj_Json.rep.call(holder, key, value);
                    }


                    switch (typeof value) {
                        case 'string':
                            return this.Fun_Quote(value);

                        case 'number':


                            return isFinite(value) ? String(value) : 'null';

                        case 'boolean':
                        case 'null':


                            return String(value);


                        case 'object':


                            if (!value) {
                                return 'null';
                            }


                            this.Obj_Json.gap += this.Obj_Json.indent;
                            partial = [];


                            if (Object.prototype.toString.apply(value) === '[object Array]') {


                                length = value.length;
                                for (i = 0; i < length; i += 1) {
                                    partial[i] = this.Fun_Str(i, value) || 'null';
                                }



                                v = partial.length === 0 ? '[]' :
                    this.Obj_Json.gap ? '[\n' + this.Obj_Json.gap +
                            partial.join(',\n' + this.Obj_Json.gap) + '\n' +
                                mind + ']' :
                          '[' + partial.join(',') + ']';
                                this.Obj_Json.gap = mind;
                                return v;
                            }


                            if (this.Obj_Json.rep && typeof this.Obj_Json.rep === 'object') {
                                length = this.Obj_Json.rep.length;
                                for (i = 0; i < length; i += 1) {
                                    k = this.Obj_Json.rep[i];
                                    if (typeof k === 'string') {
                                        v = this.Fun_Str(k, value);
                                        if (v) {
                                            partial.push(this.Fun_Quote(k) + (this.Obj_Json.gap ? ': ' : ':') + v);
                                        }
                                    }
                                }
                            } else {


                                for (k in value) {
                                    if (Object.hasOwnProperty.call(value, k)) {
                                        v = this.Fun_Str(k, value);
                                        if (v) {
                                            partial.push(this.Fun_Quote(k) + (this.Obj_Json.gap ? ': ' : ':') + v);
                                        }
                                    }
                                }
                            }



                            v = partial.length === 0 ? '{}' :
                this.Obj_Json.gap ? '{\n' + this.Obj_Json.gap + partial.join(',\n' + this.Obj_Json.gap) + '\n' +
                        mind + '}' : '{' + partial.join(',') + '}';
                            this.Obj_Json.gap = mind;
                            return v;
                    }
                },

                StringFromJson: function (value, replacer, space) {


                    var i;
                    this.Obj_Json.gap = '';
                    this.Obj_Json.indent = '';
                    if (typeof space === 'number') {
                        for (i = 0; i < space; i += 1) {
                            this.Obj_Json.indent += ' ';
                        }

                    } else if (typeof space === 'string') {
                        this.Obj_Json.indent = space;
                    }
                    this.Obj_Json.rep = replacer;
                    if (replacer && typeof replacer !== 'function' &&
                    (typeof replacer !== 'object' ||
                     typeof replacer.length !== 'number')) {
                        throw new Error('JSON.StringFromJson');
                    }

                    return this.Fun_Str('', { '': value });
                },

                Fun_Walk: function (holder, key, reviver) {
                    var k, v, value = holder[key];
                    if (value && typeof value === 'object') {
                        for (k in value) {
                            if (Object.hasOwnProperty.call(value, k)) {
                                v = this.Fun_Walk(value, k, reviver);
                                if (v !== undefined) {
                                    value[k] = v;
                                } else {
                                    delete value[k];
                                }
                            }
                        }
                    }
                    return reviver.call(holder, key, value);
                },

                Fun_Cx: function (a) {
                    return '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
                },


                JsonFromString: function (text, reviver) {
                    var j;
                    text = String(text);

                    this.Obj_Json.cx.lastIndex = 0;

                    if (this.Obj_Json.cx.test(text)) {
                        text = text.replace(this.Obj_Json.cx, this.Fun_Cx);
                    }


                    var temp = text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, '');

                    if (/^[\],:{}\s]*$/.test(temp)) {
                        j = eval('(' + text + ')');
                        return typeof reviver === 'function' ? this.Fun_Walk({ '': j }, '', reviver) : j;
                    }

                    return "";
                }

            }

       },

       //内部调用系列
       Z:
       {


           DebugLog: function (s, o, snMsg, an) {
               ///	<summary>
               ///  添加日志文件
               ///	</summary>
               ///	<param name="s" type="str">
               ///		日志发生的函数路径
               ///	</param>
               ///	<param name="o" type="obj">
               ///		日志调用时的对象
               ///	</param>
               ///	<param name="snMsg" type="str">
               ///		日志附加的描述编号
               ///	</param>
               ///	<param name="an" type="arr">
               ///		日志描述的替换参数
               ///	</param>


               if (SWW.C.Flag.Debug) {
                   SWW.O.Log.Debug.push({ d: SWW.F.STR.DateTime(null, 'yyyy-MM-dd hh:mm:ss.ms'), t: s, c: o, m: snMsg, a: an });
               }

           },

           BasePath: function () {
               ///	<summary>
               ///  返回文件所在路径
               ///	</summary>

               var d = '';
               var e = document.getElementsByTagName('script');
               for (var f = 0; f < e.length; f++) {
                   var g = e[f].src.match(/(^|.*[\\\/])SrnprWebJsWebWidgetFWF.js(?:\?.*)?/i);
                   if (g) {
                       d = g[1];
                       break;
                   }
               }

               if (d.indexOf('://') == -1) {
                   if (d.indexOf('/') === 0) d = location.href.match(/^.*?:\/\/[^\/]*/)[0] + d;
               }
               else {
                   //d = location.href.match(/^[^\?]*\/(?:)/)[0] + d;
               }

               return d;
           },
           AddScript: function (u) {
               ///	<summary>
               ///  添加脚本文件
               ///	</summary>
               ///	<param name="u" type="string">
               ///		地址
               ///	</param>

               SWW.J.getScript(this.BasePath() + u);

           },

           Ajax: function (e) {
               ///	<summary>
               ///  提交请求
               ///	</summary>
               ///	<param name="e" type="string">
               ///		调用的元素数组
               ///	</param>


               var t = {};
               if (e.length) {
                   t.RQ = e;
               } else {
                   t.RQ = [];
                   t.RQ.push(e);
               }


               //开始检测是否定义了正确的类型并是否需要重新初始化
               for (var i = 0, j = t.RQ.length; i < j; i++) {
                   t.RQ[i] = SWW.F.SYS.InitReq(t.RQ[i]);





               }

               if (SWW.C.Flag.Debug) {
                   SWW.Z.DebugLog('sww.z.ajax', { url: SWW.C.Ajax.Url, parm: t }, 'sww2006');
               }

               //开始提交数据
               SWW.J.ajax(
                {
                    url: SWW.C.Ajax.Url,
                    type: "POST",
                    data: "json=" + SWW.F.JSON.StringFromJson(t),
                    success: function (s) { SWW.Z.AjaxSuccess(s); },
                    error: function (XMLHttpRequest, textStatus) {
                        if (SWW.C.Flag.Debug) {
                            SWW.Z.DebugLog('sww.z.ajax', t, 'sww2011', this.url);
                        }
                        SWW.F.SYS.Error({ n: 'SWW.Z.Ajax', m: textStatus })
                    }
                });
           },

           AjaxSuccess: function (s) {
               ///	<summary>
               ///  执行成功时调用
               ///	</summary>
               ///	<param name="s" type="str">
               ///		响应内容
               ///	</param>

               if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.z.ajaxsuccess', s, 'sww2007');

               var json = SWW.F.JSON.JsonFromString(s);

               for (var i = 0, j = json.RS.length; i < j; i++) {


                   if (json.RS[i].WidgetType && SWW[json.RS[i].WidgetType]) {

                       SWW.O.Res[json.RQ[i].Guid] = json.RS[i];

                       //执行标准函数
                       SWW.F.SYS.ExecFunc({ t: json.RS[i].WidgetType, f: 'F_Success', e: { q: json.RQ[i], s: json.RS[i]} });

                       //执行扩展函数
                       SWW.F.SYS.ExecAF({ f: 'Success', w: json.RQ[i].WidgetType, d: json.RQ[i].Id, e: { s: json.RS[i]} });

                   }
                   else {
                       SWW.F.SYS.Error({ n: 'SWW.Z.AjaxSuccess', m: x });
                   }

               }
           },

           CheckInit: function () {
               ///	<summary>
               ///  判断系统初始化加载加载
               ///	</summary>


               if (!SWW.C.Init.LoadFlag) {
                   SWW.J().ready(function () { SWW.Z.Init(); });
                   SWW.C.Init.LoadFlag = true;
               }
           },


           Init: function () {
               ///	<summary>
               ///  系统初始化时调用
               ///	</summary>


               if (SWW.O) {
                   //定义是否加载完成
                   var bFlag = true;

                   //开始判断是否所有加载完成
                   if (bFlag) {
                       for (var p in SWW.O.Req) {
                           if (!SWW[SWW.O.Req[p].WidgetType]) {
                               bFlag = false;
                               break;
                           }
                       }
                   }


                   //累加当前调用次数
                   SWW.C.Init.N++;

                   if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.z.init', SWW.C.Init.N, 'sww2002', SWW.C.Init.N);


                   //判断是否超过最大调用次数限制
                   if (SWW.C.Init.N < SWW.C.Init.M) {

                       //如果所有加载完成
                       if (bFlag) {
                           this.InitSuccess();
                       }
                       else {
                           //重新调用执行
                           setTimeout("SWW.Z.Init()", SWW.C.Init.T);
                       }
                   }
                   else {
                       //提示错误信息
                       SWW.F.SYS.Error({ n: 'SWW.Z.Init', m: SWW.M.SE.IM });
                   }
               }



           }
           ,
           InitSuccess: function () {

               if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.z.initsuccess', '', 'sww2008');
               var sub = [];
               for (var t in SWW.O.Req) {

                   SWW.F.SYS.ExecFunc({ t: SWW.O.Req[t].WidgetType, f: "F_Init", e: SWW.O.Req[t] });

                   if (SWW.O.Req[t].__type) {
                       sub.push(SWW.O.Req[t]);
                   }
               }
               if (sub.length > 0) {
                   this.Ajax(sub);
               }
           }
       },


       W:
       {

           Dialog:
           {
               Init:
               {

                   Temp:
                   {
                       //控件高度
                       height: 400,
                       //宽度
                       width: 400,
                       //顶部距离
                       top: 100,
                       //左边距 默认-1表示系统自动居中
                       left: -1,
                       //弹出框表头
                       title: '',
                       //弹出框html内容
                       html: '',
                       //弹出框编号
                       id: '',
                       //背景透明度
                       opacity: 50,
                       //弹出框iframe链接
                       url: '',
                       //按钮
                       button: [],
                       //是否自动状态保存 0表示不自动保存状态 2表示自动保存状态
                       save: 0,
                       //是否自动跟随滚动 1为滚动 2为不滚动
                       scroll: 1,
                       //自身所加载的window对象
                       self: null,
                       //当前加载的序号
                       current: 0,
                       //Guid编号
                       guid: '',
                       //样式编号
                       cssid: 'SWW_CSS_W_Dialog_'
                   }
                   ,
                   Config:
                   {

                       BgId: 'SWW_SWW_F_BOX_INIT_CONFIG_BGID',
                       DefaultId: 'SWW_SWW_F_BOX_INIT_CONFIG_DEFAULTID',

                       Opactity: 50,
                       BgColor: '#000',
                       CountDialog: 0

                   },

                   ObjArray: [],

                   Father: function () {
                       return top;
                   },

                   AddBg: function () {

                       if (!SWW.F.DOM.Get(this.Config.BgId)) {

                           var e = document.createElement("div");
                           e.id = this.Config.BgId;
                           if (SWW.J.browser.msie && SWW.J.browser.version == '6.0')
                               e.innerHTML = '<iframe style="width:100%;height:100%;border:none;filter:alpha(opacity=0);opacity:0;"></iframe>';



                           var co = this.Config;

                           with (e.style) {
                               height = Math.max(window.screen.height, document.body.offsetHeight + 50) + "px";
                               position = 'absolute';
                               zIndex = 555;
                               filter = " alpha(opacity = " + co.Opactity + ")";
                               opacity = co.Opactity / 100;
                               width = document.documentElement.scrollWidth + "px";
                               top = "0px";
                               backgroundColor = co.BgColor;
                           }

                           document.body.appendChild(e);
                       }
                       else {
                           SWW.F.DOM.Display(this.Config.BgId, true);
                       }
                   },

                   DefaultPosition: function (i) {

                       return isNaN(i) ? i : (i + "px");

                   },


                   Clear: function () {
                       this.ObjArray.pop();

                       if (this.Config.CountDialog <= 0) {

                           SWW.F.DOM.Display(this.Config.BgId);
                           this.Config.CountDialog = 0;
                       }
                       else {

                           SWW.F.DOM.Display(this.ObjArray[this.Config.CountDialog - 1].guid, true);

                       }



                   },
                   Create: function (o) {

                       if (!o) {
                           o = {};
                       }
                       for (var p in this.Temp) {
                           if (!o[p]) {
                               o[p] = this.Temp[p];
                           }
                       }

                       if (!o.id) {
                           o.id = this.Config.DefaultId;
                       }

                       if (!o.guid) {
                           o.guid = SWW.F.SYS.GetGuid();
                       }



                       o.top = Math.max(document.body.scrollTop, document.documentElement.scrollTop) + o.top;
                       if (o.left == -1) o.left = (document.body.offsetWidth - parseInt(o.width)) / 2;
                       this.AddBg();





                       if (o.button && o.button.length > 0) {
                           //o.height += 30;
                       }


                       var aH = [];


                       var boxover = '<div id="' + o.guid + '" class="' + o.cssid + 'Box_Over" style=" top:' + this.DefaultPosition(o.top) + ';left:' + this.DefaultPosition(o.left) + ';">';


                       aH.push(boxover);

                       aH.push('<div class="' + o.cssid + 'Box_Back" >');

                       aH.push('<div class="' + o.cssid + 'Box" style="width:' + this.DefaultPosition(o.width) + ';">');


                       //开始添加抬头

                       aH.push('<div class="' + o.cssid + 'Title" id="' + o.guid + '_title"><span class="' + o.cssid + 'Title_Left">' + (o.title ? ('&nbsp;&nbsp;' + o.title + '&nbsp;&nbsp;&nbsp;&nbsp;') : '') + '</span><span  class="' + o.cssid + 'Title_Right" onclick="SWW.W.Dialog.Close(\'' + o.guid + '\',' + o.save + ')">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>');




                       aH.push('<div class="' + o.cssid + 'Content" style="width:' + o.width + ';height:' + o.height + ';">');


                       //判断内容模型
                       if (o.url) {
                           aH.push('<div id="' + o.guid + '_iframe_load">' + SWW.M.ME.Load + '</div>');
                           aH.push('<div id="' + o.guid + '_iframe_show" style="display:none;">');
                           aH.push('<iframe id="' + o.guid + '_iframe" onload="SWW.F.DOM.Display(\'' + o.guid + '_iframe_load\');SWW.F.DOM.Display(\'' + o.guid + '_iframe_show\',true);" style="width:' + (o.width) + 'px;height:' + (o.height) + 'px" src="' + o.url + '" frameborder="0"></iframe>');
                           aH.push('<div>');
                       }
                       else if (o.html) {
                           aH.push('<div style="margin:5px;">');
                           aH.push(o.html);

                           aH.push('</div>');

                       }

                       aH.push('</div>');

                       if (o.button && o.button.length > 0) {

                           aH.push('<div class="' + o.cssid + 'Button_Back">');
                           for (var i = 0, j = o.button.length; i < j; i++) {
                               var iIndex = o.button[i].indexOf(':');

                               if (iIndex > -1) {
                                   aH.push('<input type="button" class="' + o.cssid + 'Button" onclick="SWW.W.Dialog.Source().' + o.button[i].substr(iIndex + 1) + '" value="' + o.button[i].substr(0, iIndex) + '"/>');
                               }
                           }
                           aH.push('</div>');
                       }

                       aH.push('</div></div></div>');
                       SWW.J('body').append(aH.join(''));

                       o.current = this.Config.CountDialog;

                       if (this.Config.CountDialog > 0) {
                           SWW.F.DOM.Display(this.ObjArray[this.Config.CountDialog - 1].guid);
                       }

                       this.Config.CountDialog++;

                       //开始加载拖动代码
                       SWW.W.Drag.DragElement(SWW.J('#' + o.guid + '_title'), SWW.J('#' + o.guid));

                       if (o.scroll == 1) {
                           SWW.J(window).scroll(function () {
                               SWW.J('#' + o.guid).css("top", (document.documentElement.scrollTop + 120) + "px");

                           });
                       }
                       this.ObjArray.push(o);


                       if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.w.dialog.init.create', o.html || o.url, 'sww2009', o.title);

                   }
               },


               Father: function () {
                   ///	<summary>
                   ///  返回打开对话框的
                   ///	</summary>
                   return top;
               },

               Open: function (o) {
                   ///	<summary>
                   ///  创建对话框
                   ///	</summary>
                   ///	<param name="o" type="obj">
                   ///  弹出框对象 属性参照SWW.W.Dialog.Init.Temp
                   ///	</param>



                   if (!o) o = {};

                   if (!o.self) {
                       if (this.Father() != self) {
                           o.self = self;
                           this.Father().SWW.W.Dialog.Open(o);
                           return;
                       }
                       else {
                           o.self = window;
                       }
                   }




                   SWW.F.JF.Ready(function () { SWW.W.Dialog.Init.Create(o) });



               },

               Close: function (sn, iSave) {

                   ///	<summary>
                   ///  关闭对话框
                   ///	</summary>
                   ///	<param name="sn" type="obj">
                   ///		对话框编号 如果留空时表示关闭最近一次打开的对话框
                   ///	</param>
                   ///	<param name="iSave" type="int">
                   ///		关闭类型 0为直接清除 1为保存状态
                   ///	</param>


                   if (this.Father() != self && this.Father().SWW) {
                       this.Father().SWW.W.Dialog.Close(sn, iSave);
                       return;
                   }

                   if (!sn) {
                       sn = this.Init.ObjArray[this.Init.ObjArray.length - 1].guid;
                   }

                   if (!iSave) {
                       iSave = this.Init.Temp.save;
                   }



                   if (iSave == 0) {
                       document.body.removeChild(SWW.F.DOM.Get(sn));
                   }
                   else {
                       SWW.J('#' + sn).hide();
                   }
                   this.Init.Config.CountDialog--;



                   //清除背景
                   this.Init.Clear();




               },

               Source: function () {
                   ///	<summary>
                   ///  得到当前对话框的来源页面
                   ///	</summary>

                   var o = this.Father().SWW.W.Dialog.Init.ObjArray[this.Father().SWW.W.Dialog.Init.ObjArray.length - 1];
                   if (o) {
                       return o.self;
                   }
                   else {
                       return self;
                   }
               },

               GetValue: function (s) {
                   ///	<summary>
                   ///  得到来源页面的元素的值
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		元素编号
                   ///	</param>

                   return this.Source().SWW.J('#' + s).val();

               },
               SetValue: function (s, v) {
                   ///	<summary>
                   ///  设置来源页面的元素的值
                   ///	</summary>
                   ///	<param name="s" type="str">
                   ///		元素编号
                   ///	</param>


                   // this.Source().SWW.J('#' + s).val(v);

                   this.Source().SWW.F.DOM.Auto(s, v);


               }

           }
           ,

           //拖拽扩展
           Drag:
           {
               GetPos: function (e) {
                   var D = document.documentElement;
                   if (e.pageX)
                       return { x: e.pageX, y: e.pageY };
                   else
                       return { x: (e.clientX + D.scrollLeft - D.clientLeft), y: (e.clientY + D.scrollTop - D.clientTop) }
               },

               DragElement: function (el, handle, fDragIng, end) {

                   SWW.J(el).mousedown(function (e) {
                       e = e || window.event;
                       var pos = SWW.W.Drag.GetPos(e);

                       var father = handle ? SWW.J(handle) : SWW.J(el);

                       var hPos = father.offset();
                       var diffx = pos.x - hPos.left;
                       var diffy = pos.y - hPos.top;

                       SWW.W.Drag.DragIng(father, diffx, diffy, fDragIng);



                       SWW.W.Drag.Drop(end);


                   });

                   return false;
               },

               DragIng: function (handle, diffx, diffy, fDragIng) {

                   SWW.J(document).bind('mousemove', function (e) {
                       var movePos = SWW.W.Drag.GetPos(e);
                       SWW.J(handle).css({ left: (movePos.x - diffx), top: (movePos.y - diffy) });

                       if (fDragIng) fDragIng();

                   });
               },

               Drop: function (fEnd) {
                   SWW.J(document).mouseup(function (e) {
                       SWW.J(document).unbind('mousemove');
                       if (fEnd) fEnd();
                   });

               }
           }

       },




       //初始化
       I: function (t, o) {
           ///	<summary>
           ///  根据名称初始化对象
           ///	</summary>
           ///	<param name="t" type="string">
           ///		操作类型
           ///	</param>
           ///	<param name="o" type="obj">
           ///		操作的对象
           ///	</param>



           //开始自动判断参数传入类型
           if (typeof (t) != "string" && t.WidgetType) {
               o = t;
               t = o.WidgetType;
           }


           //判断是否存在参数并且重新初始化
           if (o) {
               if (o.WidgetType) {
                   o = SWW.F.SYS.InitReq(o);
                   this.O.Req[o.Guid] = o;
               }

           }

           //判断是否已经存在加载的对象
           if (!this[t] && this.C.JS[t] && !this.C.JSLoad[t] && (!this.C.JS[t].w || !window[this.C.JS[t].w])) {
               //定义文件
               var u = '';
               if ("string" == typeof (this.C.JS[t])) {
                   u = this.C.JS[t];
               }
               else {
                   //判断是否有需要加载的内容
                   if (this.C.JS[t].n) {
                       for (var i = 0; i < this.C.JS[t].n.length; i++) {
                           this.I(this.C.JS[t].n[i]);
                       }
                   }
                   u = this.C.JS[t].u;
               }
               this.C.JSLoad[t] = Date().toString();

               this.Z.AddScript(u);

               if (SWW.C.Flag.Debug) SWW.Z.DebugLog('sww.i', u, 'sww2010', t);


           }





           //开始加载初始化函数
           this.Z.CheckInit();




       }




   }



}




