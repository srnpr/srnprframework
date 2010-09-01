

CKEDITOR.dialog.add('srnpr_ck_listshow', function (editor)
{
    return {
        title: editor.config.srnprck.cklist.listshow.title,
        hiddenField: null,
        minWidth: 350,
        minHeight: 110,
        onShow: function ()
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
        onOk: function ()
        {

            var img = editor.document.createElement('img',
			{
			    attributes:
				{
				    id: this.getValueOf('info', '_cke_saved_name'),
				    src: editor.config.srnprck.cklist.listshow.imgsrc
				}
			});

            img.setAttribute(editor.config.srnprck.cktype, editor.config.srnprck.cklist.listshow.id);
            img.setAttribute(editor.config.srnprck.cklist.listshow.xmltype, this.getValueOf('info', '_xml_saved_id'));
            img.setAttribute(editor.config.srnprck.cklist.listshow.listshowtype, this.getValueOf('info', 'listshowtype'));
            img.setAttribute(editor.config.srnprck.cklist.listshow.defaulttypeid, this.getValueOf('info', 'listshowdefault'));
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
					    setup: function (element)
					    {
					        this.setValue(
									element.getAttribute('_cke_saved_name') ||
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
					    id: '_xml_saved_id',
					    type: 'text',
					    label: editor.config.srnprck.cklist.listshow.xmlid,
					    validate: CKEDITOR.dialog.validate.notEmpty(editor.config.srnprck.cklist.listshow.xmlemptymsg),
					    'default': '',
					    accessKey: 'V',
					    setup: function (element)
					    {
					        this.setValue(element.getAttribute(editor.config.srnprck.cklist.listshow.xmltype) || '');
					    },
					    commit: function (element)
					    {
					        if (this.getValue())
					            element.setAttribute(editor.config.srnprck.cklist.listshow.xmltype, this.getValue());
					        else
					            element.removeAttribute(editor.config.srnprck.cklist.listshow.xmltype);
					    }
					},
                    {
                        type: 'hbox',
                        widths: ['50%', '50%'],
                        children:
						[
							{
							    id: 'listshowtype',
							    type: 'select',
							    label: editor.config.srnprck.cklist.listshow.listshowtypetitle,
							    'default': 'text',
							    accessKey: 'M',
							    items:
						        [
							        [editor.config.srnprck.cklist.listshow.listtype.select.title, editor.config.srnprck.cklist.listshow.listtype.select.value],
							        [editor.config.srnprck.cklist.listshow.listtype.radio.title, editor.config.srnprck.cklist.listshow.listtype.radio.value],
                                    [editor.config.srnprck.cklist.listshow.listtype.checkbox.title, editor.config.srnprck.cklist.listshow.listtype.checkbox.value]
						        ],
							    setup: function (element)
							    {
							        this.setValue(element.getAttribute(editor.config.srnprck.cklist.listshow.listshowtype) || editor.config.srnprck.cklist.listshow.listtype.select.value);
							    },
							    commit: function (element)
							    {
							        if (this.getValue())
							            element.setAttribute(editor.config.srnprck.cklist.listshow.listshowtype, this.getValue());
							        else
							            element.removeAttribute(editor.config.srnprck.cklist.listshow.listshowtype);
							    }



							},
							{
							    id: 'listshowdefault',
							    type: 'select',
							    label: editor.config.srnprck.cklist.listshow.defaluttitle,
							    'default': 'text',
							    accessKey: 'M',
							    items:
						        [
							        [editor.config.srnprck.cklist.listshow.defaulttype.empty.title, editor.config.srnprck.cklist.listshow.defaulttype.empty.value],
							        [editor.config.srnprck.cklist.listshow.defaulttype.first.title, editor.config.srnprck.cklist.listshow.defaulttype.first.value],
                                    [editor.config.srnprck.cklist.listshow.defaulttype.choose.title, editor.config.srnprck.cklist.listshow.defaulttype.choose.value],
                                    [editor.config.srnprck.cklist.listshow.defaulttype.all.title, editor.config.srnprck.cklist.listshow.defaulttype.all.value]
						        ],
							    setup: function (element)
							    {
							        this.setValue(element.getAttribute(editor.config.srnprck.cklist.listshow.defaulttypeid) || editor.config.srnprck.cklist.listshow.defaulttype.empty.value);
							    },
							    commit: function (element)
							    {
							        if (this.getValue())
							            element.setAttribute(editor.config.srnprck.cklist.listshow.defaulttypeid, this.getValue());
							        else
							            element.removeAttribute(editor.config.srnprck.cklist.listshow.defaulttypeid);
							    }
							}
						]
                    }
				]
			}
		]
    };
});
