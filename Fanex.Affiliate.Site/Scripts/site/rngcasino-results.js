/*global AFFILIATE, QueryString*/
"use strict";

var RNGCasinoResults = (function ($) {
    return {
        initialize: function () {
            RNGCasinoResults.initEvent();
        },

        initEvent: function () {
            $("body").on("click", ".hand-history-detail-link", function () {
                var url = AFFILIATE.getAbsoluteUrl('Account/RNGCasinoDetail');
                url += $(this).attr("data-url");
                RNGCasinoResults.viewCasinoDetail(url);
            });

            $("#chkIsShowPic").on('click', function () {
                var isShowPic = $("#chkIsShowPic").is(':checked');
                var url = window.location.href;
                url = QueryString.setParam(url, "IsShowPic", isShowPic);
                window.location.href = url;
            });
        },

        viewCasinoDetail: function (url) {
            var title = window.MSG.HandHistoryDetails;
            AFFILIATE.openDialogFrame(url, title, { width: 900, height: 450 });
        }
    };
})(jQuery);

$(window).load(function () {
    RNGCasinoResults.initialize();
});