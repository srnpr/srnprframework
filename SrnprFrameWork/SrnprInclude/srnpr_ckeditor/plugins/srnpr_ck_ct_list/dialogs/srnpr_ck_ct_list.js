

CKEDITOR.dialog.add('srnpr_ck_ct_list', function(editor)
{
    var config = editor.config,
		lang = editor.lang.srnpr_ck_ct_list,
		images = config.srnpr_ck_ct_list_images,
		columns = config.srnpr_ck_ct_list_columns || 8,
		i;


    var dialog;
    var onClick = function(evt)
    {
        var target = evt.data.getTarget(),
			targetName = target.getName();

        if (targetName == 'a')
            target = target.getChild(0);
        else if (targetName != 'img')
            return;

        var src = target.getAttribute('cke_src'),
			title = target.getAttribute('title');

        var img = editor.document.createElement('img',
			{
			    attributes:
				{
				    src: src,
				    _cke_saved_src: src,
				    title: title,
				    alt: title,
				    srnpr_srnpr_ck_ct_list_control_id: "aa"
				}
			});

        editor.insertElement(img);

        dialog.hide();
        evt.data.preventDefault();
    };

    var onKeydown = CKEDITOR.tools.addFunction(function(ev, element)
    {
        ev = new CKEDITOR.dom.event(ev);
        element = new CKEDITOR.dom.element(element);
        var relative, nodeToMove;

        var keystroke = ev.getKeystroke();
        var rtl = editor.lang.dir == 'rtl';
        switch (keystroke)
        {
            // UP-ARROW  
            case 38:
                // relative is TR
                if ((relative = element.getParent().getParent().getPrevious()))
                {
                    nodeToMove = relative.getChild([element.getParent().getIndex(), 0]);
                    nodeToMove.focus();
                }
                ev.preventDefault();
                break;
            // DOWN-ARROW  
            case 40:
                // relative is TR
                if ((relative = element.getParent().getParent().getNext()))
                {
                    nodeToMove = relative.getChild([element.getParent().getIndex(), 0]);
                    if (nodeToMove)
                        nodeToMove.focus();
                }
                ev.preventDefault();
                break;
            // ENTER  
            // SPACE  
            case 32:
                onClick({ data: ev });
                ev.preventDefault();
                break;

            // RIGHT-ARROW  
            case rtl ? 37 : 39:
                // TAB
            case 9:
                // relative is TD
                if ((relative = element.getParent().getNext()))
                {
                    nodeToMove = relative.getChild(0);
                    nodeToMove.focus();
                    ev.preventDefault(true);
                }
                // relative is TR
                else if ((relative = element.getParent().getParent().getNext()))
                {
                    nodeToMove = relative.getChild([0, 0]);
                    if (nodeToMove)
                        nodeToMove.focus();
                    ev.preventDefault(true);
                }
                break;

            // LEFT-ARROW  
            case rtl ? 39 : 37:
                // SHIFT + TAB
            case CKEDITOR.SHIFT + 9:
                // relative is TD
                if ((relative = element.getParent().getPrevious()))
                {
                    nodeToMove = relative.getChild(0);
                    nodeToMove.focus();
                    ev.preventDefault(true);
                }
                // relative is TR
                else if ((relative = element.getParent().getParent().getPrevious()))
                {
                    nodeToMove = relative.getLast().getChild(0);
                    nodeToMove.focus();
                    ev.preventDefault(true);
                }
                break;
            default:
                // Do not stop not handled events.
                return;
        }
    });

    // Build the HTML for the srnpr_ck_ct_list images table.
    var labelId = 'srnpr_ck_ct_list_emtions_label' + CKEDITOR.tools.getNextNumber();


    var GetHtml = function()
    {

        var html =
	[
		'<div>' +
		'<span id="' + labelId + '" class="cke_voice_label">' + lang.options + '</span>',
		'<table role="listbox" aria-labelledby="' + labelId + '" style="width:100%;height:100%" cellspacing="2" cellpadding="2"',
		CKEDITOR.env.ie && CKEDITOR.env.quirks ? ' style="position:absolute;"' : '',
		'><tbody>'
	];

        var size = images.length;
        for (i = 0; i < size; i++)
        {
            if (i % columns === 0)
                html.push('<tr>');

            var srnpr_ck_ct_listLabelId = 'cke_smile_label_' + i + '_' + CKEDITOR.tools.getNextNumber();
            html.push(
			'<td class="cke_dark_background cke_centered" style="vertical-align: middle;">' +
				'<a href="javascript:void(0)" role="option"',
					' aria-posinset="' + (i + 1) + '"',
					' aria-setsize="' + size + '"',
					' aria-labelledby="' + srnpr_ck_ct_listLabelId + '"',
					' class="cke_smile cke_hand" tabindex="-1" onkeydown="CKEDITOR.tools.callFunction( ', onKeydown, ', event, this );">',
					'<img class="cke_hand" title="', config.srnpr_ck_ct_list_descriptions[i], '"' +
						' cke_src="', CKEDITOR.tools.htmlEncode(config.srnpr_ck_ct_list_path + images[i]), '" alt="', config.srnpr_ck_ct_list_descriptions[i], '"',
						' src="', CKEDITOR.tools.htmlEncode(config.srnpr_ck_ct_list_path + images[i]), '"',
            // IE BUG: Below is a workaround to an IE image loading bug to ensure the image sizes are correct.
						(CKEDITOR.env.ie ? ' onload="this.setAttribute(\'width\', 2); this.removeAttribute(\'width\');" ' : ''),
					'>' +
					'<span id="' + srnpr_ck_ct_listLabelId + '" class="cke_voice_label">' + config.srnpr_ck_ct_list_descriptions[i] + '</span>' +
				'</a>',
 			'</td>');

            if (i % columns == columns - 1)
                html.push('</tr>');
        }

        if (i < columns - 1)
        {
            for (; i < columns - 1; i++)
                html.push('<td></td>');
            html.push('</tr>');
        }

        html.push('</tbody></table></div>');

    }




    alert(GetHtml());
    var srnpr_ck_ct_listSelector =
	{
	    type: 'html',
	    html: GetHtml().join(''),
	    onLoad: function(event)
	    {
	        dialog = event.sender;
	    },
	    focus: function()
	    {
	        var firstSmile = this.getElement().getElementsByTag('a').getItem(0);
	        firstSmile.focus();
	    },
	    onClick: onClick,
	    style: 'width: 100%; border-collapse: separate;'
	};

    return {
        title: editor.lang.srnpr_ck_ct_list.title,
        minWidth: 270,
        minHeight: 120,
        contents: [
			{
			    id: 'tab1',
			    label: '单选一号',
			    title: '',
			    expand: true,
			    padding: 0,
			    elements: [
						srnpr_ck_ct_listSelector
					]
			}, {
			    id: 'tab2',
			    label: '单选二号',
			    title: '',
			    expand: true,
			    padding: 0,
			    elements: [
						srnpr_ck_ct_listSelector
					]
			}
		],
        buttons: [CKEDITOR.dialog.cancelButton]
    };
});
