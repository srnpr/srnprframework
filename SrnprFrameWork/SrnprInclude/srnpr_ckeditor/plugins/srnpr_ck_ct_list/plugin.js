
CKEDITOR.plugins.add('srnpr_ck_ct_list',
{
    requires: ['dialog'],

    init: function(editor)
    {
        if(!editor.lang.srnpr_ck_ct_list)
        editor.lang.srnpr_ck_ct_list = { toolbar: '表情符', title: '插入DC控件', options: '表情图标选项' };


    editor.config.srnpr_ck_ct_list_path = editor.config.srnpr_ck_ct_list_path || (this.path + 'singel_select/');
        editor.addCommand('srnpr_ck_ct_list', new CKEDITOR.dialogCommand('srnpr_ck_ct_list'));
        editor.ui.addButton('srnpr_ck_ct_list',
			{
			    label: editor.lang.srnpr_ck_ct_list.toolbar,
			    command: 'srnpr_ck_ct_list',
			    icon: CKEDITOR.plugins.getPath('srnpr_ck_ct_list') + 'srnpr_ck_ct_list.gif'
			});
        CKEDITOR.dialog.add('srnpr_ck_ct_list', this.path + 'dialogs/srnpr_ck_ct_list.js');
    }
});






CKEDITOR.config.srnpr_ck_ct_list_images = [
	'peopleselect.gif'];

CKEDITOR.config.srnpr_ck_ct_list_descriptions =
	[
		'员工单选控件'
	];


