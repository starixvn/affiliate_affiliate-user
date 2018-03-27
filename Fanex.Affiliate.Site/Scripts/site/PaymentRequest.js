/* global AFFILIATE, Lang */
"use strict";

var PAYMENTREQUEST = (function ($) {
    return {
        init: function () {
            // check constraints
            var maxAmount = parseFloat($("#tbMaxAmount").val());
            var minAmount = parseFloat($("#tbMinAmount").val());
            var message = Lang.AmountError + $("#minValue").text() + ' ' + Lang.And + ' ' + $("#maxValue").text();
            var hasPendingRequest = $("#tbTransID").length > 0;

            var disabledInput = maxAmount <= 0 || maxAmount < minAmount || hasPendingRequest;
            $('#btnSubmit').prop("disabled", disabledInput);
            $('#tbAmount').prop("disabled", truedisabledInput);

            $('#RequestPaymentForm').submit(function () {
                $.blockUI();
                $('#tbAmount').val($('#tbAmount').val().replace(/,/g, ""));
                var reqAmount = $('#tbAmount').val();

                if ($('#tbAmount').val().length <= 0 || isNaN(reqAmount)) {
                    AFFILIATE.showErrorMsg(message);
                    $.unblockUI();
                    return false;
                }
            });
        }
    };
} (jQuery));

$(function () {
    PAYMENTREQUEST.init();
});