﻿/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function(config)
{
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.skin = 'office2003';


    config.extraPlugins = 'srnpr_ck_ct_list,srnpr_ck_forms,srnpr_ck_gridshow';

    config.toolbar =
   [['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'], ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'], ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],

   '/', ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'], ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'], ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], ['Link', 'Unlink', 'Anchor'], ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
   '/', ['srnpr_ck_ct_list', 'srnpr_ck_gridshow'],
     ['TextField', 'Form', 'Checkbox', 'Radio', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
   '/', ['Styles', 'Format', 'Font', 'FontSize'], ['TextColor', 'BGColor'], ['Maximize', 'ShowBlocks', '-', 'About']];



    //移除掉系统插件
    config.removePlugins = "forms";



    config.srnprck =
   {
        //参数
       param:
       {
           id: 'paramid',
           title: '参数名称',
           emptymsg: '参数名称不能为空！'
       },

       cktype: 'srnpr_srnpr_ck_control_type_id',
       eventbase: 'srnprck_eventbase_',
       
       cklist:
       {
          gridshow:
          {
              id: 'gridshow',
              xmltype:'srnpr_ck_gridshow_xmlid',
             imgsrc:'/srnpr_ckeditor/plugins/srnpr_ck_gridshow/gridshow.png',
            toolbar: '数据显示',
            title: '插入数据显示控件',
            
            
            xmlid: '数据显示编号', 
            xmlemptymsg: '数据显示编号不能为空'
          }
          ,
          ct_list:
          {
              id: 'ct_list'
          }
       },
       
       
       
       
       //控件编号
       srnpr_srnpr_ck_control_type_id:
       {
            gridshow:'gridshow',
            ct_list:'ct_list'
       },
       
       ct_list:
       {



        },
        
        
        version:"1.0.0.0"
}

};


