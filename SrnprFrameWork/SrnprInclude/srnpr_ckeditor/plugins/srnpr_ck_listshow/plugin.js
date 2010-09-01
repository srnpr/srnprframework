
CKEDITOR.plugins.add('srnpr_ck_gridshow',
{
    requires: ['dialog'],

    init: function(editor)
    {

       // editor.config.srnpr_ck_gridshow_config.path = editor.config.srnpr_ck_gridshow_config.path || (this.path);
        
        editor.addCommand('srnpr_ck_gridshow', new CKEDITOR.dialogCommand('srnpr_ck_gridshow'));
        editor.ui.addButton('srnpr_ck_gridshow',
			{
			    label: editor.config.srnprck.cklist.gridshow.toolbar,
			    command: 'srnpr_ck_gridshow',
			    icon: CKEDITOR.plugins.getPath('srnpr_ck_gridshow') + 'srnpr_ck_gridshow.gif'
			});
			CKEDITOR.dialog.add('srnpr_ck_gridshow', this.path + 'dialogs/srnpr_ck_gridshow.js');
        
        
        editor.on('doubleclick', function(evt)
        {
            var element = evt.data.element;

            if (element.is('img') && element.getAttribute(editor.config.srnprck.cktype) == editor.config.srnprck.cklist.gridshow.id)
            {
                evt.data.dialog = 'srnpr_ck_gridshow';
            }
        });


    }
});










