
CKEDITOR.plugins.add('srnpr_ck_webtab',
{
    requires: ['dialog'],

    init: function(editor)
    {
        if (!editor.lang.srnpr_ck_webtab)
            editor.lang.srnpr_ck_webtab = { toolbar: '表情符', title: '插入DC控件', options: '表情图标选项' };


        editor.config.srnpr_ck_webtab_path = editor.config.srnpr_ck_webtab_path || (this.path);
        editor.addCommand('srnpr_ck_webtab', new CKEDITOR.dialogCommand('srnpr_ck_webtab'));
        editor.ui.addButton('srnpr_ck_webtab',
			{
			    label: editor.lang.srnpr_ck_webtab.toolbar,
			    command: 'srnpr_ck_webtab',
			    icon: CKEDITOR.plugins.getPath('srnpr_ck_webtab') + 'srnpr_ck_webtab.gif'
			});
			CKEDITOR.dialog.add('srnpr_ck_webtab', this.path + 'dialogs/srnpr_ck_webtab.js');
        
        
        editor.on('doubleclick', function(evt)
        {
            var element = evt.data.element;

            if (element.is('img') && element.getAttribute('srnpr_srnpr_ck_control_type_id') == 'ct_list')
            {
                evt.data.dialog = 'srnpr_ck_webtab';
            }
        });


    }
});

