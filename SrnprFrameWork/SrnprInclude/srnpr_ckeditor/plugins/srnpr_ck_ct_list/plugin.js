
CKEDITOR.plugins.add('srnpr_ck_ct_list',
{
    requires: ['dialog'],

    init: function(editor)
    {
        if (!editor.lang.srnpr_ck_ct_list)
            editor.lang.srnpr_ck_ct_list = { toolbar: '表情符', title: '插入DC控件', options: '表情图标选项' };


        editor.config.srnpr_ck_ct_list_path = editor.config.srnpr_ck_ct_list_path || (this.path);
        editor.addCommand('srnpr_ck_ct_list', new CKEDITOR.dialogCommand('srnpr_ck_ct_list'));
        editor.ui.addButton('srnpr_ck_ct_list',
			{
			    label: editor.lang.srnpr_ck_ct_list.toolbar,
			    command: 'srnpr_ck_ct_list',
			    icon: CKEDITOR.plugins.getPath('srnpr_ck_ct_list') + 'srnpr_ck_ct_list.gif'
			});
			CKEDITOR.dialog.add('srnpr_ck_ct_list', this.path + 'dialogs/srnpr_ck_ct_list.js');
        
        
        editor.on('doubleclick', function(evt)
        {
            var element = evt.data.element;

            if (element.is('img') && element.getAttribute('srnpr_srnpr_ck_control_type_id') == 'ct_list')
            {
                evt.data.dialog = 'srnpr_ck_ct_list';
            }
        });


    }
});






CKEDITOR.config.srnpr_ck_ct_list_config =
{
    srnpr_srnpr_ck_ct_list_control_id: "srnpr_srnpr_ck_ct_list_control_id",
group:
[
{
    title:'单选控件',
    imagespath:'singel_select',
    images: ['peopleselect.gif', 'peopleselect.gif', 'peopleselect.gif', 'peopleselect.gif', 'peopleselect.gif', 'peopleselect.gif', 'peopleselect.gif', 'peopleselect.gif', 'peopleselect.gif', 'peopleselect.gif', 'peopleselect.gif'],
    descriptions: ['人员单选', '', '', '', '', '', '', '', '', '', '']

},
{
    title: '多选控件',
    imagespath: 'mul_select',
    
    images: ['adicon1.gif', 'adicon2.gif'],
    descriptions: ['aa', 'bb']

}


]

}
;





