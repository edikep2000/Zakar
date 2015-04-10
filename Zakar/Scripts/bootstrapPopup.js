//bootstrap popup mod
function bootstrapPopup(o) {
    var p = o.p; //popup properties
    var popup = p.d; //popup div
    var modal = $('<div class="modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">' +
        '<div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
        '<h4 class="modal-title"></h4></div></div></div></div>');

    var hasFooter = p.btns && p.btns.length;

    //minimum height of the lookup/multilookup content
    p.mlh = !p.f ? 250 : 0;

    if (!p.t) {
        p.t = "&nbsp;"; //put one space when no title
    }

    popup.addClass("modal-body");
    popup.css('overflow', 'auto');

    modal.find('.modal-content').append(popup);
    modal.find('.modal-title').html(p.t);
    popup.show();

    //use to resize the popup when fullscreen
    function autoResize() {
        var h = $(window).height() - 120;
        if (hasFooter) h -= 90;
        if (h < 400) h = 400;
        popup.height(h);
        popup.trigger('aweresize');
    }

    var api = function () { };
    api.open = function () {
        modal.appendTo($('body')); //appendTo each time to prevent modal to show under current top modal
        modal.modal('show');
        p.isOpen = 1;
        if (p.f) autoResize();
    };
    api.close = function () { modal.modal('hide'); };
    api.destroy = function () {
        api.close();
        $(window).off('resize', autoResize);
        popup.closest('.modal').remove();
    };

    popup.data('api', api);

    modal.on('hidden.bs.modal', function () {
        popup.trigger('aweclose');
    });

    popup.on('aweclose', function () {
        p.isOpen = 0;
        if (p.cl) {
            p.cl();
        }
        if (!p.dntr) {
            popup.find('*').remove();
            popup.closest('.modal').remove();
        }
    });

    $('body').append(modal);

    //fullscreen
    if (p.f) {
        modal.find('.modal-dialog').css('width', 'auto').css('margin', '10px');
        $(window).on('resize', autoResize);
    }

    //add buttons if any
    if (hasFooter) {
        var footer = $('<div class="modal-footer"></div>');
        modal.find('.modal-footer').remove();
        modal.find('.modal-content').append(footer);
        $.each(p.btns, function (i, e) {
            var btn = $('<button type="button" class="btn">' + e.text + '</button>');
            btn.click(function () { e.click.call(popup); });
            footer.append(btn);
        });
    }
}