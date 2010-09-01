/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config)
{
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.skin = 'office2003';


    config.extraPlugins = 'srnpr_ck_ct_list,srnpr_ck_forms,srnpr_ck_gridshow,srnpr_ck_listshow';

    config.toolbar =
   [['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'], ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'], ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],

   '/', ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'], ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'], ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], ['Link', 'Unlink', 'Anchor'], ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
   '/', ['srnpr_ck_ct_list', 'srnpr_ck_gridshow', 'srnpr_ck_listshow'],
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
              xmltype: 'srnpr_ck_gridshow_xmlid',
              imgsrc: '/srnpr_ckeditor/plugins/srnpr_ck_gridshow/gridshow.png',
              toolbar: '数据显示',
              title: '插入数据显示控件',
              xmlid: '数据显示编号',
              xmlemptymsg: '数据显示编号不能为空'
          }
          ,
           ct_list:
          {
              id: 'ct_list'
          },
           listshow:
          {
              id: 'listshow',
              xmltype: 'srnpr_ck_listshow_id',
              imgsrc: '/srnpr_ckeditor/plugins/srnpr_ck_listshow/listshow.png',
              toolbar: '列表显示',
              title: '插入列表显示控件',
              xmlid: '列表显示编号',
              xmlemptymsg: '列表显示编号不能为空',

              

              listshowtypetitle:'显示方式',
              listshowtype: 'srnpr_ck_listshow_listshowtype',
              listtype: 
              {
                  select: { title: '下拉框', value: 'select' },
                  radio: { title: '单选按钮', value: 'radio' } ,
                  checkbox: { title: '复选按钮', value: 'checkbox' }
              },

              defaluttitle:'默认值',
              defaulttypeid:'srnpr_ck_listshow_listshowdefault',
              defaulttype: 
              {
                  empty: { title: '无', value: 'empty' },
                  first: { title: '第一项', value: 'first' } ,
                  choose: { title: '请选择', value: 'choose' },
                  all: { title: '全部', value: 'all' }
              }

          }
       },




       //控件编号
       srnpr_srnpr_ck_control_type_id:
       {
           gridshow: 'gridshow',
           listshow: 'listshow',
           ct_list: 'ct_list'
       },
       ct_list:
       {

   },


   version: "1.0.0.0"

}

};


