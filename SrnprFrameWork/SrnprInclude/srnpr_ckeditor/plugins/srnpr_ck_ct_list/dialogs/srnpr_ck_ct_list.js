

CKEDITOR.dialog.add('srnpr_ck_ct_list', function(editor)
{
    var config = editor.config,
		lang = editor.lang.srnpr_ck_ct_list,

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
			title = target.getAttribute('title'),
			id = target.getAttribute('inputid');

        if (document.getElementById("srnpr_ck_ct_list_paramid_" + id) && document.getElementById("srnpr_ck_ct_list_paramid_" + id).value == "")
        {
            alert(editor.config.srnprck.param.emptymsg);
            return false;
        }



        var img = editor.document.createElement('img',
			{
			    attributes:
				{
				    src: src,
				    _cke_saved_src: src,
				    title: title,
				    alt: title,
				    id: document.getElementById("srnpr_ck_ct_list_paramid_" + id).value,
				    srnpr_srnpr_ck_control_type_id: "ct_list"
				}
			});

        editor.insertElement(img);
        document.getElementById("srnpr_ck_ct_list_paramid_" + id).value = "";
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


    var GetHtml = function(eConfig)
    {

        var inputId = eConfig.imagespath.replace("/", "");


        var html =
	[
		'<div>' +
		'<span id="' + labelId + '" class="cke_voice_label">' + lang.options + '</span>',
		'<table role="listbox" aria-labelledby="' + labelId + '" style="width:100%;height:100%" cellspacing="2" cellpadding="2"',
		CKEDITOR.env.ie && CKEDITOR.env.quirks ? ' style="position:absolute;"' : '',
		'><tbody>'
	];

        html.push('<tr><td colspan="100">' + editor.config.srnprck.param.title + '：<input type="text"  id="srnpr_ck_ct_list_paramid_' + inputId + '" style="border:solid 1px #999;background-color:#fff;" value=""/></td></tr>');

        var size = eConfig.images.length;
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
					'<img class="cke_hand" inputid="' + inputId + '" title="', eConfig.descriptions[i], '"' +
						' cke_src="', CKEDITOR.tools.htmlEncode(config.srnpr_ck_ct_list_path + eConfig.imagespath + "/" + eConfig.images[i]), '" alt="', eConfig.descriptions[i], '"',
						' src="', CKEDITOR.tools.htmlEncode(config.srnpr_ck_ct_list_path + eConfig.imagespath + "/" + eConfig.images[i]), '"',
            // IE BUG: Below is a workaround to an IE image loading bug to ensure the image sizes are correct.
						(CKEDITOR.env.ie ? ' onload="this.setAttribute(\'width\', 2); this.removeAttribute(\'width\');" ' : ''),
					'>' +
					'<span id="' + srnpr_ck_ct_listLabelId + '" class="cke_voice_label">' + eConfig.descriptions[i] + '</span>' +
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

        return html;

    }








    var GetSelector = function(eConfig)
    {
        return {
            type: 'html',
            html: GetHtml(eConfig).join(''),
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
    }



    var GetContents = function()
    {
        var vCont = new Array();

        for (var n = 0, m = config.srnpr_ck_ct_list_config.group.length; n < m; n++)
        {
            vCont.push(

            {
                id: 'tab' + n,
                label: config.srnpr_ck_ct_list_config.group[n].title,
                title: config.srnpr_ck_ct_list_config.group[n].title,
                expand: true,
                padding: 0,
                elements: [
						GetSelector(config.srnpr_ck_ct_list_config.group[n])
					]
            }

            );
        }


        return vCont;

    }







    return {
        title: editor.lang.srnpr_ck_ct_list.title,
        minWidth: 370,
        minHeight: 220,
        contents: GetContents(),
        buttons: [CKEDITOR.dialog.cancelButton],
        onShow: function()
        {


            var element = this.getParentEditor().getSelection().getSelectedElement();

            if (element && element.getName() == "img" && element.getId())
            {
                for (var n = 0, m = config.srnpr_ck_ct_list_config.group.length; n < m; n++)
                {

                    var inputId = config.srnpr_ck_ct_list_config.group[n].imagespath.replace("/", "");
                    if (document.getElementById("srnpr_ck_ct_list_paramid_" + inputId))
                    {
                        document.getElementById("srnpr_ck_ct_list_paramid_" + inputId).value = element.getId();
                    }
                }
            }
            else
            {
               
                for (var n = 0, m = config.srnpr_ck_ct_list_config.group.length; n < m; n++)
                {

                    var inputId = config.srnpr_ck_ct_list_config.group[n].imagespath.replace("/", "");
                    if (document.getElementById("srnpr_ck_ct_list_paramid_" + inputId))
                    {
                        document.getElementById("srnpr_ck_ct_list_paramid_" + inputId).value = "";
                    }
                }
                
            }
        }
    };
});
