/* global AFFILIATE, Lang, CryptoJS */
"use strict";

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

var SIGNIN = (function ($) {
    return {
        init: function () {
            if ($("#Username").val() === "") {
                $("#Username").focus();
            } else {
                $("#Password").focus();
            }
            // Event reload Capchat
            $('#img-captcha').click(function () {
                SIGNIN.reloadCaptcha();
            });
            // Event click login
            $("#btnlogin").click(function () {
                SIGNIN.submitForm();
            });
            $('.login-block input').bind('keypress', function (e) {
                if (e.keyCode === 13) {
                    SIGNIN.submitForm();
                }
            });
            $('#btnReset').click(function () {
                window.location.href = document.referrer;
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
            $('#img-captcha').attr('src', '/User/CaptchaImage' + "?" + currentDate);
        },
        submitForm: function () {
            var username = $("#Username").val(),
                password = $("#Password").val(),
                captCha = $("#CaptchaLogin").val(),
                loginToken = $("#SecureToken").val();
            var returnUrl = getParameterByName("ReturnUrl");

            if (username === "" || password === "") {
                alert(Lang.EmptyUsernamePassword);
            } else if (captCha === "") {
                alert(Lang.EmptyCaptcha);
            } else {
                $.blockUI();
                //SIGNIN.encryptPassword(password, loginToken);
                var param = {
                    "Username": username,
                    "Password": SIGNIN.encryptPassword(password, loginToken),
                    "Captcha": captCha,
                    "loginToken": loginToken
                };

                $.ajax({
                    type: "POST",
                    url: AFFILIATE.getAbsoluteUrl("User/SignIn"),
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify(param),
                    success: function (result) {
                        if (!result.Success) {
                            alert(result.Message);
                            $("#CaptchaLogin").val("");
                            $("#Password").val("");
                            SIGNIN.reloadCaptcha();
                        } else {
                            if (returnUrl === "") {
                                parent.location.href = AFFILIATE.getAbsoluteUrl("Account/Overview");
                            }
                            else {
                                parent.location.href = AFFILIATE.getAbsoluteUrl(returnUrl.substring(1, returnUrl.length));
                            }
                        }
                    },
                    error: function () {
                    },
                    complete: function () {
                        $.unblockUI();
                    }
                });
            }
        },
        encryptPassword: function (originalPassword, loginToken) {
            var encryptedPassword = CryptoJS.CfsEnCode(originalPassword);
            var hashedPassword = CryptoJS.SHA256(encryptedPassword + loginToken);

            $("#Password").val(hashedPassword);
            return decodeURI(hashedPassword);
        }
    };
} (jQuery));

$(function () {
	SIGNIN.init();
});