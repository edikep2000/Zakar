﻿
function init(dateFormat, isMobileOrTablet) {

    //remove locaStorage keys from older versions of awesome; you can modify  ppk (awe.ppk = "myapp1_"), current value is "awe2_"
    try {
        for (var key in localStorage) {
            if (key.indexOf("awe") == 0) {
                if (key.indexOf(awe.ppk) != 0) {
                    localStorage.removeItem(key);
                }
            }
        }
    } catch (err) { }

    //by default jquery.validate doesn't validate hidden inputs
    if ($.validator) $.validator.setDefaults({
        ignore: []
    });

    setjQueryValidateDateFormat(dateFormat);

    // don't focus first input on mobile
    if (isMobileOrTablet) {
        awe.ff = function (o) {
            o.p.d.find(':tabbable').blur();//override jQueryUI dialog autofocus
        };
    }
}

//on document ready

function documentReady(root, popupMod) {
    consistentSearchTxt();
    $(document).ajaxComplete(consistentSearchTxt);
 
    //parsing the unobtrusive attributes when we get content via ajax
    $(document).ajaxComplete(function() {
        $.validator.unobtrusive.parse(document);
    });

  


    awe.popup = function(o) {
        return bootstrapPopup(o);
    };
    /*endpopup*/
}


// on ie hitting enter doesn't trigger change, 
// all searchtxt inputs will trigger change on enter in all browsers
function consistentSearchTxt() {
    $('.searchtxt').each(function () {
        if ($(this).data('searchtxth') != 1)
            $(this).data('searchtxth', 1)
                .data('myval', $(this).val())
                .on('change', function (e) {
                    if ($(this).val() != $(this).data('myval')) {
                        $(this).data('myval', $(this).val());
                    } else {
                        e.stopImmediatePropagation();
                    }
                })
                .on('keyup', function (e) {
                    if (e.which == 13) {
                        e.preventDefault();
                        if ($(this).val() != $(this).data('myval')) {

                            $(this).change();
                        }
                    }
                });
    });
}



var lastw = 0;



//wrap ajaxlists for horizontal scrolling on small screens
function wrapLists() {
    $('table.awe-ajaxlist:not([wrapped])').each(function () {
        var columns = $(this).find('thead th').length;
        var mw = $(this).data('mw');
        if (columns || mw) {
            mw = mw || columns * 120;

            $(this).wrap('<div style="width:100%; overflow:auto;"></div>')
                .wrap('<div style="min-width:' + mw + 'px;padding-bottom:2px;"></div>')
                .attr('wrapped', 'wrapped');
        }
    });
}


function setjQueryValidateDateFormat(format) {
    jQuery.validator.addMethod(
        'date',
        function (value, element, params) {
            if (this.optional(element)) {
                return true;
            };
            var result = false;
            try {
                $.datepicker.parseDate(format, value);
                result = true;
            } catch (err) {
                result = false;
            }
            return result;
        },
        ''
    );
}

/*begin*/
awe.err = function (o, xhr, textStatus, errorThrown) {
    var msg = "unexpected error occured";
    if (xhr) {
        msg = xhr.responseText;
    }
    msg += "(you see this message because in Site.js awe.err is set to a function that shows this popup)&nbsp; ";
    var btnHide = $('<button type="button" class="awe-btn"> hide </button>').click(function () {
        $(this).parent().remove();
    });

    var c = $('<div/>').html(msg).append(btnHide);

    //decide where to attach the inline popup
    if (o.p && o.p.isOpen) { // if helper has popup and is open
        o.p.d.prepend(c); // put msg inside popup div
    } else if (o.f) {
        o.f.html(c); // put msg inside control field
    } else $('body').prepend(c);
};/*end*/

function itemDeleted(gridId) {
    return function (res) {
        var $grid = $("#" + gridId);
        $grid.data('api').select(res.Id)[0].fadeOut(500, function () {
            $(this).remove();
            if (!$grid.find('.awe-row').length) $grid.data('api').load();
        });
    };
}

function itemUpdated(gridId) {
    return function (item) {
        var api = $('#' + gridId).data('api');
        var xhr = api.update(item.Id);
        $.when(xhr).done(function () {
            var $row = api.select(item.Id)[0];
            var altcl = $row.hasClass("awe-alt") ? "awe-alt" : "";
            $row.switchClass(altcl, "awe-changing", 1).switchClass("awe-changing", altcl, 1000);
        });
    };
}

function itemCreated(gridId) {
    return function (item) {
        var $grid = $("#" + gridId);
        var $row = $grid.data('api').renderRow(item);
        $grid.find(".awe-tbody").prepend($row);
        $row.addClass("awe-changing").removeClass("awe-changing", 1000);
    };
}


function refreshGrid(gridId) {
    return function () {
        $("#" + gridId).data('api').load();
    };
}
