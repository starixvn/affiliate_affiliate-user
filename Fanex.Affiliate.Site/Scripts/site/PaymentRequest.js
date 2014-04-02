/* global AFFILIATE, Lang */
"use strict";

var PAYMENTREQUEST = (function ($) {
    return {
        init: function () {
            // check constraints
            var maxAmount = parseFloat($("#tbMaxAmount").val());
            var minAmount = parseFloat($("#tbMinAmount").val());
            var message = Lang.AmountError + $("#minValue").text() + ' ' + Lang.And + ' ' + $("#maxValue").text();

            if (maxAmount <= 0 || maxAmount < minAmount) {
                $('#btnSubmit').attr("disabled", true);
                $('#tbAmount').attr("disabled", true);
            }

            $('#RequestPaymentForm').submit(function () {
                $('#tbAmount').val($('#tbAmount').val().replace(/,/g, ""));
                var reqAmount = $('#tbAmount').val();

                if ($('#tbAmount').val().length <= 0 || isNaN(reqAmount)) {
                    AFFILIATE.showErrorMsg(message);
                    return false;
                }
            });
        }
    };
} (jQuery));

$(function () {
    PAYMENTREQUEST.init();
});