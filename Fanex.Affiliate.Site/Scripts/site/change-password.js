var ChangePassword = function ($) {
    return {
        init: function() {
            $("#NewPassword").tooltip();
            $("#ConfirmPassword").tooltip();
        },
        validate: function(obj) {
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
        check: function(str) {
            numbers = new String("0123456789");
            var i = 0;
            var nCount = 0;
            while (i < str.length) {
                (numbers.indexOf(str.substring(i, i + 1)) >= 0) ? nCount++ : false;
                i++;
            }
            return ((nCount > (str.length - 2)) || nCount < 2) ? false : true;
        }
    };
} (jQuery);

$(function () {
    ChangePassword.init();
});

function HandleCancelEvent() {
    document.location.href = AFFILIATE.getAbsoluteUrl("Account/MyAccount");
}