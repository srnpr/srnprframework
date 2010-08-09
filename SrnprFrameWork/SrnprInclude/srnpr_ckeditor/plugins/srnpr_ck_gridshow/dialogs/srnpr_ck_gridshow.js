

CKEDITOR.dialog.add('srnpr_ck_gridshow', function(editor)
{
    return {
        title: editor.lang.hidden.title,
        hiddenField: null,
        minWidth: 350,
        minHeight: 110,
         onShow: function()
        {
            delete this.hiddenField;

            var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();
            if (element && element.getAttribute('srnpr_srnpr_ck_control_type_id') && element.getAttribute('srnpr_srnpr_ck_control_type_id') == editor.config.srnprck.srnpr_srnpr_ck_control_type_id.gridshow)
            {
                this.hiddenField = element;
                this.setupContent(this.hiddenField);
            }
        },
        onOk: function()
        {

            var name = this.getValueOf('info', '_cke_saved_name'),
            srnpr_ck_gridshow_xmlid = this.getValueOf('info', 'srnpr_ck_gridshow_xmlid');

            var img = editor.document.createElement('img',
			{
			    attributes:
				{
				    srnpr_srnpr_ck_control_type_id: editor.config.srnprck.srnpr_srnpr_ck_control_type_id.gridshow,
				    id:name,
				    srnpr_ck_gridshow_xmlid: srnpr_ck_gridshow_xmlid
				}
			});

            editor.insertElement(img);


            return true;
        },
        contents: [
			{
			    id: 'info',
			    label: editor.lang.hidden.title,
			    title: editor.lang.hidden.title,
			    elements: [
					{
					    id: '_cke_saved_name',
					    type: 'text',
					    label: editor.config.srnprck.param.title,
					    validate: CKEDITOR.dialog.validate.notEmpty(editor.config.srnprck.param.emptymsg),
					    'default': '',
					    accessKey: 'N',
					    setup: function(element)
					    {
					        this.setValue(
									element.getAttribute('_cke_saved_name') ||
									element.getAttribute('id') ||
									'');
					    },
					    commit: function(element)
					    {
					        if (this.getValue())
					            element.setAttribute('id', this.getValue());
					        else
					        {
					            element.removeAttribute('id');
					        }
					        
					    }
					},
					{
					    id: 'srnpr_ck_gridshow_xmlid',
					    type: 'text',
					    label: editor.lang.hidden.value,
					    'default': '',
					    accessKey: 'V',
					    setup: function(element)
					    {
					        this.setValue(element.getAttribute('srnpr_ck_gridshow_xmlid') || '');
					    },
					    commit: function(element)
					    {
					        if (this.getValue())
					            element.setAttribute('srnpr_ck_gridshow_xmlid', this.getValue());
					        else
					            element.removeAttribute('srnpr_ck_gridshow_xmlid');
					    }
					}
				]
			}
		]
    };
});
