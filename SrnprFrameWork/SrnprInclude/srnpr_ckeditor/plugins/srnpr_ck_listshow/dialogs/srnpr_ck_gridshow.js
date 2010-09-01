

CKEDITOR.dialog.add('srnpr_ck_gridshow', function(editor)
{
    return {
        title: editor.config.srnprck.cklist.gridshow.title,
        hiddenField: null,
        minWidth: 350,
        minHeight: 110,
        onShow: function()
        {
            delete this.hiddenField;

            var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();
            if (element && element.getAttribute(editor.config.srnprck.cktype) && element.getAttribute(editor.config.srnprck.cktype) == editor.config.srnprck.cklist.gridshow.id)
            {
                this.hiddenField = element;
                this.setupContent(this.hiddenField);
            }
        },
        onOk: function()
        {

            var img = editor.document.createElement('img',
			{
			    attributes:
				{
				    id:  this.getValueOf('info', '_cke_saved_name'),
				    src:editor.config.srnprck.cklist.gridshow.imgsrc
				}
			});

			img.setAttribute(editor.config.srnprck.cktype, editor.config.srnprck.cklist.gridshow.id);
			img.setAttribute(editor.config.srnprck.cklist.gridshow.xmltype, this.getValueOf('info', '_xml_saved_id'));
            editor.insertElement(img);


            return true;
        },
        contents: [
			{
			    id: 'info',
			    label: editor.config.srnprck.cklist.gridshow.title,
			    title: editor.config.srnprck.cklist.gridshow.title,
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
					    id: '_xml_saved_id',
					    type: 'text',
					    label: editor.config.srnprck.cklist.gridshow.xmlid,
					    validate: CKEDITOR.dialog.validate.notEmpty(editor.config.srnprck.cklist.gridshow.xmlemptymsg),
					    'default': '',
					    accessKey: 'V',
					    setup: function(element)
					    {
					        this.setValue(element.getAttribute(editor.config.srnprck.cklist.gridshow.xmltype) || '');
					    },
					    commit: function(element)
					    {
					        if (this.getValue())
					            element.setAttribute(editor.config.srnprck.cklist.gridshow.xmltype, this.getValue());
					        else
					            element.removeAttribute(editor.config.srnprck.cklist.gridshow.xmltype);
					    }
					}
				]
			}
		]
    };
});
