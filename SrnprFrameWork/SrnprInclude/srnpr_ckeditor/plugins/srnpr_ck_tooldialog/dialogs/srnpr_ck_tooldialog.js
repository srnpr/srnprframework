

CKEDITOR.dialog.add('srnpr_ck_tooldialog', function (editor)
{

    var sck = editor.config.srnprck;

    return {
        title: sck.cklist.tooldialog.title,
        hiddenField: null,
        minWidth: 350,
        minHeight: 110,
        onShow: function ()
        {
            delete this.hiddenField;

            var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();
            if (element && element.getAttribute(sck.cktype) && element.getAttribute(sck.cktype) == sck.cklist.tooldialog.id)
            {
                this.hiddenField = element;
                this.setupContent(this.hiddenField);
            }
        },
        onOk: function ()
        {

            var img = editor.document.createElement('img',
			{
			    attributes:
				{
				    id: this.getValueOf('info', '_cke_saved_id'),
				    src: sck.cklist.tooldialog.imgsrc
				}
			});



            img.setAttribute(sck.cktype, sck.cklist.tooldialog.id);


            img.setAttribute(sck.cklist.tooldialog.url, this.getValueOf('info', '_select_url'));


            editor.insertElement(img);


            return true;
        },
        contents: [
			{
			    id: 'info',
			    label: sck.cklist.tooldialog.title,
			    title: sck.cklist.tooldialog.title,
			    elements: [
					{
					    id: '_cke_saved_id',
					    type: 'text',
					    label: sck.param.title,
					    validate: CKEDITOR.dialog.validate.notEmpty(sck.param.emptymsg),
					    'default': '',
					    accessKey: 'N',
					    setup: function (element)
					    {
					        this.setValue(
									element.getAttribute('_cke_saved_id') ||
									element.getAttribute('id') ||
									'');
					    },
					    commit: function (element)
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
                        type: 'hbox',
                        widths: ['50%', '50%'],
                        children:
						[
							{
							    id: '_select_url',
							    type: 'select',
							    label: sck.cklist.tooldialog.tooldialogtypetitle,
							    'default': 'text',
							    accessKey: 'M',
							    items:
						        sck.cklist.tooldialog.items,
							    setup: function (element)
							    {
							        this.setValue(element.getAttribute(sck.cklist.tooldialog.url) || '');
							    },
							    commit: function (element)
							    {
							        if (this.getValue())
							            element.setAttribute(sck.cklist.tooldialog.url, this.getValue());
							        else
							            element.removeAttribute(sck.cklist.tooldialog.url);
							    }



							},
							{
							    id: '_app_id',
							    type: 'text',
							    label: sck.cklist.tooldialog.xmlid,
							    
							    'default': '',
							    accessKey: 'V',
							    setup: function (element)
							    {
							        //this.setValue(element.getAttribute(sck.json.tooldialog.Id) || '');
							    },
							    commit: function (element)
							    {
                                /*
							        if (this.getValue())
							            element.setAttribute(sck.json.tooldialog.Id, this.getValue());
							        else
							            element.removeAttribute(sck.json.tooldialog.Id);
                                        */
							    }
							}
						]
                    }
				]
			}
		]
    };
});
