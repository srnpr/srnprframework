
CKEDITOR.plugins.add('srnpr_ck_tooldialog',
{
    requires: ['dialog'],

    init: function(editor)
    {

       // editor.config.srnpr_ck_gridshow_config.path = editor.config.srnpr_ck_gridshow_config.path || (this.path);

        editor.addCommand('srnpr_ck_tooldialog', new CKEDITOR.dialogCommand('srnpr_ck_tooldialog'));
        editor.ui.addButton('srnpr_ck_tooldialog',
			{
			    label: editor.config.srnprck.cklist.tooldialog.toolbar,
			    command: 'srnpr_ck_tooldialog',
			    icon: CKEDITOR.plugins.getPath('srnpr_ck_tooldialog') + 'srnpr_ck_tooldialog.gif'
			});
			CKEDITOR.dialog.add('srnpr_ck_tooldialog', this.path + 'dialogs/srnpr_ck_tooldialog.js');
        
        
        editor.on('doubleclick', function(evt)
        {
            var element = evt.data.element;

            if (element.is('img') && element.getAttribute(editor.config.srnprck.cktype) == editor.config.srnprck.cklist.tooldialog.id)
            {
                evt.data.dialog = 'srnpr_ck_tooldialog';
            }
        });


    }
});










