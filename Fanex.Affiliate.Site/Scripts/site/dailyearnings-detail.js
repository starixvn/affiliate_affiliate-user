var DailyEarningsDetails = function ($) {
    return {
        init: function () {
            $('.domain-view-detail').click(function () {
                var param = {
                    'username': $(this).attr('data-domain-username'),
                    'product': $(this).attr('data-domain-product'),
                    'winlostdate': $(this).attr('data-domain-winlostdate'),
                    'currency': $(this).attr('data-domain-currency')
                };
                var winlostdate = $(this).attr('data-domain-winlostdate').split(" ");
                var popupTitle = "Bet List " + winlostdate[0] + " - ";
                popupTitle += $(this).attr('data-domain-username') + " - View in " + $(this).attr('data-domain-affcurrency');
                var queryString = jQuery.param(param);
                var url = AFFILIATE.getAbsoluteUrl("Account/BetList?" + queryString);
                AFFILIATE.openDialogFrame(url, popupTitle, { width: 1000, height: 600 });
            });
        }
    };
} (jQuery);

function showBetlistTooltip()
{
    $('#betlistNote').tooltip();
}

$(function () {
    DailyEarningsDetails.init();
});