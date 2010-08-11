/*
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
       param:
       {
           id: 'paramid',
           title: '参数名称',
           emptymsg: '参数名称不能为空！'
       },

       srnpr_srnpr_ck_control_type_id:
       {
            gridshow:'gridshow',
            ct_list:'ct_list'
        },
       
       imagePath:
       {
           baseUrl: 'http://f.xgou.com/atgang/CK_Site/'
       
       },
       
       
       ct_list:
       {



   }
}

};


