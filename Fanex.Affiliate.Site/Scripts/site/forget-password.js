"use strict";

var FORGETPASSWORD = (function ($) {
    return {
        init: function() {
            if ($("#Email").val() === "") {
                $("#Email").focus();
            } else {
                $("#Captcha").focus();
            }
            // Event reload Capchat
            $('#img-captchaForgetPassword').click(function() {
                FORGETPASSWORD.reloadCaptcha();
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
            $('#img-captchaForgetPassword').attr('src', '/User/CaptchaImageForgetPwd' + "?" + currentDate);
        }
    };
} (jQuery));

$(function () {
	FORGETPASSWORD.init();
});