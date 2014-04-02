/* global QueryString */
"use strict";

var MESSAGE = (function ($) {
    return {
        init: function () {
            var disableCalendar = $('#IsCheckDate').is(":checked");
            $('.datepicker').datepicker("option", "disabled", !disableCalendar);

            $('#IsCheckDate').click(function () {
                if ($(this).is(":checked")) {
                    $('#FromDate').removeAttr("disabled");
                    $('#ToDate').removeAttr("disabled");
                    $('.datepicker').datepicker("option", "disabled", false);
                } else {
                    $('#FromDate').attr("disabled", "disabled");
                    $('#ToDate').attr("disabled", "disabled");
                    $('.datepicker').datepicker("option", "disabled", true);
                }
            });

            $('#submit2').click(function (e) {
                e.stopPropagation();
                
                var checkDate = $('#IsCheckDate').is(":checked");
                var searchOption = $('#SearchOption').val();
                var url = location.href;
                var pageSize = QueryString.getParam(url, "pagesize");
                var page = QueryString.getParam(url, "page");
                var questPos = url.indexOf('?');

                if (questPos > 0) {
                    url = url.substring(0, questPos);
                }

                if (searchOption !== "") {
                    url = QueryString.setParam(url, "SearchOption", searchOption);
                }

                if (checkDate === true) {
                    url = QueryString.setParam(url, "FromDate", $('#FromDate').val());
                    url = QueryString.setParam(url, "ToDate", $('#ToDate').val());
                }

                if (page > 0 && pageSize > 0) {
                    url = QueryString.setParam(url, "pagesize", pageSize);
                    url = QueryString.setParam(url, "page", page);
                }

                location.href = url;

                return false;
            });
        }
    };
} (jQuery));

$(function () {
    MESSAGE.init();
});