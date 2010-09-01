

CKEDITOR.dialog.add('srnpr_ck_listshow', function(editor)
{
    return {
        title: editor.config.srnprck.cklist.listshow.title,
        hiddenField: null,
        minWidth: 350,
        minHeight: 110,
        onShow: function()
        {
            delete this.hiddenField;

            var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();
            if (element && element.getAttribute(editor.config.srnprck.cktype) && element.getAttribute(editor.config.srnprck.cktype) == editor.config.srnprck.cklist.listshow.id)
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
				    src:editor.config.srnprck.cklist.listshow.imgsrc
				}
			});

			img.setAttribute(editor.config.srnprck.cktype, editor.config.srnprck.cklist.listshow.id);
			img.setAttribute(editor.config.srnprck.cklist.listshow.xmltype, this.getValueOf('info', '_xml_saved_id'));
            editor.insertElement(img);


            return true;
        },
        contents: [
			{
			    id: 'info',
			    label: editor.config.srnprck.cklist.listshow.title,
			    title: editor.config.srnprck.cklist.listshow.title,
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
					    label: editor.config.srnprck.cklist.listshow.xmlid,
					    validate: CKEDITOR.dialog.validate.notEmpty(editor.config.srnprck.cklist.listshow.xmlemptymsg),
					    'default': '',
					    accessKey: 'V',
					    setup: function(element)
					    {
					        this.setValue(element.getAttribute(editor.config.srnprck.cklist.listshow.xmltype) || '');
					    },
					    commit: function(element)
					    {
					        if (this.getValue())
					            element.setAttribute(editor.config.srnprck.cklist.listshow.xmltype, this.getValue());
					        else
					            element.removeAttribute(editor.config.srnprck.cklist.listshow.xmltype);
					    }
					}
				]
			}
		]
    };
});
