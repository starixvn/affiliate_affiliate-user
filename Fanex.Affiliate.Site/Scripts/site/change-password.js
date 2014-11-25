/* global AFFILIATE, CryptoJS */
/* exported HandleCancelEvent */
"use strict";

var ChangePassword = (function ($) {
    return {
        init: function () {
            $("#NewPassword").tooltip();
            $("#ConfirmPassword").tooltip();

            // Event click login
            $("#btnSubmit").click(function () {
                var oldPassword = $("#OldPassword").val(),
                newPassword = $("#NewPassword").val(),
                confirmPassword = $("#ConfirmPassword").val(),
                secureToken = $("#SecureToken").val();

                $("#OldPassword").val(ChangePassword.hashPassword(oldPassword, secureToken, "OldPassword"));
                $("#NewPassword").val(ChangePassword.encryptPassword(newPassword, "NewPassword"));
                $("#ConfirmPassword").val(ChangePassword.encryptPassword(confirmPassword, "ConfirmPassword"));
            });
        },
        validate: function (obj) {
            var validator = $("#" + obj.id + "_validator");
            validator.show();
            var pass = obj.value;
            if (pass.length < 6) {
                validator.removeClass('validate-pass');
                validator.addClass('validate-fail');
            } else {
                var bol = ChangePassword.check(pass);
                if (bol) {
                    validator.removeClass('validate-fail');
                    validator.addClass('validate-pass');
                } else {
                    validator.removeClass('validate-pass');
                    validator.addClass('validate-fail');
                }
            }
        },
        check: function (str) {
            var numbers = "0123456789";
            var i = 0;
            var nCount = 0;
            while (i < str.length) {
                (numbers.indexOf(str.substring(i, i + 1)) >= 0) ? nCount++ : false;
                i++;
            }
            return ((nCount > (str.length - 2)) || nCount < 2) ? false : true;
        },
        submitForm: function () {
            var oldPassword = $("#OldPassword").val(),
                newPassword = $("#NewPassword").val(),
                confirmPassword = $("#ConfirmPassword").val(),
                secureToken = $("#SecureToken").val();

            $.blockUI();
            var param = {
                "OldPassword": ChangePassword.hashPassword(oldPassword, secureToken, "OldPassword"),
                "NewPassword": ChangePassword.encryptPassword(newPassword, "NewPassword"),
                "ConfirmPassword": ChangePassword.encryptPassword(confirmPassword, "ConfirmPassword"),
                "SecureToken": secureToken
            };

            $.ajax({
                type: "POST",
                url: AFFILIATE.getAbsoluteUrl("Account/ChangePassword"),
                contentType: "application/json",
                dataType: "text",
                data: JSON.stringify(param),
                success: function (response) {
                    console.log(response);
                },
                error: function () {
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        },
        encryptPassword: function (originalPassword, controlName) {
            var encryptedPassword = CryptoJS.CfsEnCode(originalPassword);

            $("#" + controlName).val(encryptedPassword);
            return decodeURI(encryptedPassword);
        },
        hashPassword: function (originalPassword, loginToken, controlName) {
            var encryptedPassword = CryptoJS.CfsEnCode(originalPassword);
            var hashedPassword = CryptoJS.SHA256(encryptedPassword + loginToken);

            $("#" + controlName).val(hashedPassword);
            return decodeURI(hashedPassword);
        }
    };
} (jQuery));

$(function () {
    ChangePassword.init();
});

function HandleCancelEvent() {
    document.location.href = AFFILIATE.getAbsoluteUrl("Account/MyAccount");
}