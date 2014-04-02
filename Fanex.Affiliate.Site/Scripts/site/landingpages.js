/* global AFFILIATE, Lang */
"use strict";

var ClicksLandingPages = (function ($) {
    return {
        init: function() {
            $('.act-view-detail').click(function() {
                var param = {
                    'PageSelected': $(this).attr('productselected'),
                    'LanguageSelected': $(this).attr('languageselected'),
                    'FromDate': $('#data').attr('data-fromdate'),
                    'ToDate': $('#data').attr('data-todate')
                };
                var queryString = jQuery.param(param);
                var url = AFFILIATE.getAbsoluteUrl("Account/LandingPagesDetail?" + queryString);
                AFFILIATE.openDialogFrame(url, Lang.LandingPagesDetail, { width: 650, height: 462 });
            });
        }
    };
} (jQuery));

$(function () {
    ClicksLandingPages.init();
});