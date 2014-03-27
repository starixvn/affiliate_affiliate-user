var ContactUs = function ($) {
    return {
        init: function() {
            if ($("#Sender").val() == "") {
                $("#Sender").focus();
            } else {
                $("#Captcha").focus();
            }
            // Event reload Capchat
            $('#imgCaptchaContactUs').click(function() {
                ContactUs.reloadCaptcha();
            });
        },
        reloadCaptcha: function () {
            var currentDate;
            if (!Date.now) {
                currentDate = function () {
                    return new Date().valueOf();
                };
            } else {
                currentDate = Date.now();
            }
            $('#imgCaptchaContactUs').attr('src', '/ContactUs/CaptchaImage' + "?" + currentDate);
        }
    };
} (jQuery);
$(function () {
    ContactUs.init();
});