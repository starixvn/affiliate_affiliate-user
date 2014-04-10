/* global AFFILIATE */
"use strict";

var SIGNUP = (function ($) {
    return {
        init: function () {
            $('#captcha-signup').click(function () {
                SIGNUP.reloadCaptcha();
            });
            $('#btnRegister').click(function () {
                SIGNUP.submitForm();
            });
            $('#btnRegister').bind('keypress', function (e) {
                if (e.keyCode === 13) {
                    SIGNUP.submitForm();
                }
            });
            $('#btnAddSite').click(function () {
                SIGNUP.addUrl();
            });
        },
        submitForm: function () {
            var moreSiteUrls = '';
            $(".more-site").each(function () {
                if (moreSiteUrls !== '') {
                    moreSiteUrls += ';';
                }
                moreSiteUrls += $.trim($(this).val());
            });
            $("#moreSiteUrls").val(moreSiteUrls);
            document.forms[0].submit();
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
            var url = AFFILIATE.getAbsoluteUrl("User/CaptchaImage?" + currentDate);
            $('#captcha-signup').attr('src', url);
        },
        addUrl: function () {
            var sites = $(".sites");
            var numberSite = sites.length;
            if (sites[numberSite - 1]) {
                if ($(sites[numberSite - 1]).val() === '') {
                    alert($("#messageWebsite").attr("data-more-url") + numberSite);
                    sites[numberSite - 1].focus();
                    return;
                }
            }
            numberSite++;
            var rowHtml = "<tr id='site_" + numberSite + "'>" +
                "<td>" + $("#messageWebsite").attr("data-site-info") + ' ' + numberSite + "</td>" +
                "<td></td>" +
                "<td><input type='text' class='sites more-site' id='more_" + numberSite + "'/></td>" +
                "<td><input type='button' value='" + $("#messageWebsite").attr("data-del") + "' class='button' onclick='SIGNUP.delUrl(this);'/></td>" +
                "</tr>";
            $("#siteinfo").append(rowHtml);

            if ($("#more_" + numberSite)) {
                $("#more_" + numberSite).focus();
            }
        },
        delUrl: function (obj) {
            var index = $(obj).parents("tr").attr("id").replace("site_", "");
            $("#site_" + index).remove();
            SIGNUP.reOrderRows();
        },
        reOrderRows: function () {
            var rows = $("#siteinfo").find("tr");
            for (var rowIndex = 1; rowIndex < rows.length; rowIndex++) {
                var index = (rowIndex + 1);
                $(rows[rowIndex]).attr("id", 'site_' + index);
                $(rows[rowIndex]).find("td").filter(":first").html($("#messageWebsite").attr("data-site-info") + ' ' + index);
                $(rows[rowIndex]).find("input[type='text']").filter(":first").attr("id", "more_" + index);
            }
        }
    };
} (jQuery));

$(function () {
	SIGNUP.init();
});