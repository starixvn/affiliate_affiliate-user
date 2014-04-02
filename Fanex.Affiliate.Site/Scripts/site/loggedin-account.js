/* global AFFILIATE */
"use strict";

var ACCOUNT = (function ($) {
    return {
        init: function () {
            // Event click on product link
            $("#lnkProduct").click(function () {
                ACCOUNT.ChangeProduct("#lnkProduct", "#ddlProduct", true);
                return false;
            });

            // Event click on dropdown product
            $("#ddlProduct").change(function () {
                ACCOUNT.ChangeProduct("#lnkProduct", "#ddlProduct", false);
            });

            // Event click on Amount in Statement page
            $(".act-view-amount").click(function () {
                var url = AFFILIATE.getAbsoluteUrl("Account/DailyEarnings");
                url += "?monthDate=" + $(this).attr('data-month-date');
                window.location.replace(url);
            });

            setInterval(function () {
                ACCOUNT.ReCheckIn();
            }, 15000);
        },
        ChangeProduct: function (lnkId, ddlId, isSel) {
            var select = $(ddlId);
            if ($(ddlId + " option").length === 0) {
                return;
            }

            if (isSel) {
                $(lnkId).hide();
                select.show();
                select.focus();
                select.value = $(lnkId).innerHTML;
            } else {
                $(lnkId).show();
                select.hide();
                $("#totalqualifiedmembers").html($('option:selected', select).attr('data-total-members'));
                $(lnkId).text(select.val());
            }
        },
        ReCheckIn: function () {
            $.ajax({
                type: "POST",
                url: AFFILIATE.getAbsoluteUrl("User/ReCheckIn"),
                contentType: "application/json",
                dataType: "json",
                success: function (result) {
                    if (result.Success) { //Use Success = IsKickedOut
                        if (result.ErrorMessage === "Expired") {
                            alert(result.Message);
                            parent.location.href = AFFILIATE.getAbsoluteUrl("");
                        } else if (result.ErrorMessage === "UM") {
                            parent.location.href = AFFILIATE.getAbsoluteUrl("/UnderMaintenance");
                        }
                    }
                },
                error: function () {
                },
                complete: function () {
                }
            });
        }
    };
} (jQuery));

$(function () {
	ACCOUNT.init();
});