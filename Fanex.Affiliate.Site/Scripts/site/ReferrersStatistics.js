/* global AFFILIATE, Lang */
"use strict";

var ReferrersStatistics = (function ($) {
    return {
        Init: function() {
            // Event click on Domain referrer
            $(".domain-view-detail").click(function() {
                var param = {
                    'FromDate': $('#FromDate').val(),
                    'ToDate': $('#ToDate').val(),
                    'Domain': $(this).attr('data-domain')
                };

                var queryString = jQuery.param(param);
                var url = AFFILIATE.getAbsoluteUrl("Account/ReferrersStatisticsDetail?" + queryString);
                AFFILIATE.openDialogFrame(url, Lang.ReferrersStatisticsDetail, { width: 650, height: 450 });
            });
        }
    };
} (jQuery));

$(function () {
    ReferrersStatistics.Init();
});