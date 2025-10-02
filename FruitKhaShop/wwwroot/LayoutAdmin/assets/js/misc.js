(function ($) {
    'use strict';
    $(function () {
        var body = $('body');
        var sidebar = $('.sidebar');

        function addActiveClass(element) {
            var current = location.pathname.split("/").slice(-1)[0].replace(/^\/|\/$/g, '');
            if (current === "") {
                if (element.attr('href').indexOf("index.html") !== -1) {
                    element.parents('.nav-item').last().addClass('active');
                    if (element.parents('.sub-menu').length) {
                        element.closest('.collapse').addClass('show');
                        element.addClass('active');
                    }
                }
            } else {
                if (element.attr('href').indexOf(current) !== -1) {
                    element.parents('.nav-item').last().addClass('active');
                    if (element.parents('.sub-menu').length) {
                        element.closest('.collapse').addClass('show');
                        element.addClass('active');
                    }
                }
            }
        }

        $('.nav li a', sidebar).each(function () {
            addActiveClass($(this));
        });

        sidebar.on('show.bs.collapse', '.collapse', function () {
            sidebar.find('.collapse.show').collapse('hide');
        });

        $('[data-toggle="minimize"]').on("click", function () {
            if (body.hasClass('sidebar-toggle-display') || body.hasClass('sidebar-absolute')) {
                body.toggleClass('sidebar-hidden');
            } else {
                body.toggleClass('sidebar-icon-only');
            }
        });

        // checkbox and radios
        $(".form-check label,.form-radio label").append('<i class="input-helper"></i>');

        // fullscreen
        $("#fullscreen-button").on("click", function toggleFullScreen() {
            if (!document.fullscreenElement) {
                document.documentElement.requestFullscreen();
            } else {
                if (document.exitFullscreen) {
                    document.exitFullscreen();
                }
            }
        });

        // Fix lỗi #bannerClose
        var bannerClose = document.querySelector('#bannerClose');
        if (bannerClose) {
            bannerClose.addEventListener('click', function () {
                document.querySelector('#proBanner').classList.add('d-none');
                document.querySelector('#proBanner').classList.remove('d-flex');
                document.querySelector('.navbar').classList.remove('pt-5');
                document.querySelector('.navbar').classList.add('fixed-top');
                document.querySelector('.page-body-wrapper').classList.add('proBanner-padding-top');
                document.querySelector('.navbar').classList.remove('mt-3');
            });
        }
    });
})(jQuery);
