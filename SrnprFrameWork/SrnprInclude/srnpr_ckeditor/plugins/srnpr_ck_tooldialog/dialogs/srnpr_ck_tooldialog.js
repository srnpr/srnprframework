

CKEDITOR.dialog.add('srnpr_ck_listshow', function (editor)
{

    var sck = editor.config.srnprck;

    return {
        title: sck.cklist.listshow.title,
        hiddenField: null,
        minWidth: 350,
        minHeight: 110,
        onShow: function ()
        {
            delete this.hiddenField;

            var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();
            if (element && element.getAttribute(sck.cktype) && element.getAttribute(sck.cktype) == sck.cklist.listshow.id)
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
				    src: sck.cklist.listshow.imgsrc
				}
			});

            

            img.setAttribute(sck.cktype, sck.cklist.listshow.id);

            img.setAttribute(sck.json.listshow.WidgetType, sck.cklist.listshow.id);


            img.setAttribute(sck.json.listshow.Id, this.getValueOf('info', '_ls_json_id'));
            img.setAttribute(sck.json.listshow.ShowType, this.getValueOf('info', '_ls_json_showtype'));
            img.setAttribute(sck.json.listshow.ShowDefault, this.getValueOf('info', '_ls_json_showdefault'));

            img.setAttribute(sck.json.listshow.PId, this.getValueOf('info', '_cke_saved_id'));



            editor.insertElement(img);


            return true;
        },
        contents: [
			{
			    id: 'info',
			    label: sck.cklist.listshow.title,
			    title: sck.cklist.listshow.title,
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
					    id: '_ls_json_id',
					    type: 'text',
					    label: sck.cklist.listshow.xmlid,
					    validate: CKEDITOR.dialog.validate.notEmpty(sck.cklist.listshow.xmlemptymsg),
					    'default': '',
					    accessKey: 'V',
					    setup: function (element)
					    {
					        this.setValue(element.getAttribute(sck.json.listshow.Id) || '');
					    },
					    commit: function (element)
					    {
					        if (this.getValue())
					            element.setAttribute(sck.json.listshow.Id, this.getValue());
					        else
					            element.removeAttribute(sck.json.listshow.Id);
					    }
					},
                    {
                        type: 'hbox',
                        widths: ['50%', '50%'],
                        children:
						[
							{
							    id: '_ls_json_showtype',
							    type: 'select',
							    label: sck.cklist.listshow.listshowtypetitle,
							    'default': 'text',
							    accessKey: 'M',
							    items:
						        [
							        [sck.cklist.listshow.listtype.select.title, sck.cklist.listshow.listtype.select.value],
							        [sck.cklist.listshow.listtype.radio.title, sck.cklist.listshow.listtype.radio.value],
                                    [sck.cklist.listshow.listtype.checkbox.title, sck.cklist.listshow.listtype.checkbox.value]
						        ],
							    setup: function (element)
							    {
							        this.setValue(element.getAttribute(sck.json.listshow.ShowType) || sck.cklist.listshow.listtype.select.value);
							    },
							    commit: function (element)
							    {
							        if (this.getValue())
							            element.setAttribute(sck.json.listshow.ShowType, this.getValue());
							        else
							            element.removeAttribute(sck.json.listshow.ShowType);
							    }



							},
							{
							    id: '_ls_json_showdefault',
							    type: 'select',
							    label: sck.cklist.listshow.defaluttitle,
							    'default': 'text',
							    accessKey: 'M',
							    items:
						        [
							        [sck.cklist.listshow.defaulttype.empty.title, sck.cklist.listshow.defaulttype.empty.value],
							        [sck.cklist.listshow.defaulttype.first.title, sck.cklist.listshow.defaulttype.first.value],
                                    [sck.cklist.listshow.defaulttype.choose.title, sck.cklist.listshow.defaulttype.choose.value],
                                    [sck.cklist.listshow.defaulttype.all.title, sck.cklist.listshow.defaulttype.all.value]
						        ],
							    setup: function (element)
							    {
							        this.setValue(element.getAttribute(sck.json.listshow.ShowDefault) || sck.cklist.listshow.defaulttype.empty.value);
							    },
							    commit: function (element)
							    {
							        if (this.getValue())
							            element.setAttribute(sck.json.listshow.ShowDefault, this.getValue());
							        else
							            element.removeAttribute(sck.json.listshow.ShowDefault);
							    }
							}
						]
                    }
				]
			}
		]
    };
});
