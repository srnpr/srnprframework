
CKEDITOR.plugins.add('srnpr_ck_gridshow',
{
    requires: ['dialog'],

    init: function(editor)
    {
        if (!editor.lang.srnpr_ck_gridshow)
            editor.lang.srnpr_ck_gridshow = { toolbar: '表情符', title: '插入DC控件', options: '表情图标选项' };


        editor.config.srnpr_ck_gridshow_path = editor.config.srnpr_ck_gridshow_path || (this.path);
        editor.addCommand('srnpr_ck_gridshow', new CKEDITOR.dialogCommand('srnpr_ck_gridshow'));
        editor.ui.addButton('srnpr_ck_gridshow',
			{
			    label: editor.lang.srnpr_ck_gridshow.toolbar,
			    command: 'srnpr_ck_gridshow',
			    icon: CKEDITOR.plugins.getPath('srnpr_ck_gridshow') + 'srnpr_ck_gridshow.gif'
			});
			CKEDITOR.dialog.add('srnpr_ck_gridshow', this.path + 'dialogs/srnpr_ck_gridshow.js');
        
        
        editor.on('doubleclick', function(evt)
        {
            var element = evt.data.element;

            if (element.is('img') && element.getAttribute('srnpr_srnpr_ck_control_type_id') == editor.config.srnprck.srnpr_srnpr_ck_control_type_id.gridshow)
            {
                evt.data.dialog = 'srnpr_ck_gridshow';
            }
        });


    }
});






CKEDITOR.config.srnpr_ck_gridshow_config =
{
    srnpr_srnpr_ck_gridshow_control_id: "srnpr_srnpr_ck_gridshow_control_id",
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





