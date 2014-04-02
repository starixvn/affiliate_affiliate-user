/* global AFFILIATE, Lang, QueryString */
"use strict";

var MARKETING = (function ($) {
    return {
        init: function () {
            // Event click on media search
            $("#btnSearch").click(function () {
                MARKETING.loadData(1);
                return false;
            });
            $("#btnGenerateLink").click(function () {
                MARKETING.loadData(1);
                return false;
            });
            $('#slCategory').change(function () {
                var category = $(this).val();
                var haschildren = $(this).find("option:selected").attr("data-haschildren");
                if (haschildren > 0) {
                    $('#trSubCategory').show();
                    MARKETING.getSubCategory(category, $("#subCategory").val());
                } else {
                    $("#trSubCategory").children('td').eq(1).html("");
                    $('#trSubCategory').hide();
                    $('#subCategory').val(0);
                }
            }).change();
            $(".banner-link").click(function () {
                window.open($(this).attr('data-link') + "?ref=" + location.href, "_blank");
            });
        },
        loadData: function (value) {
            if ($("#page")) {
                $("#page").value = value;
            }
            location.href = MARKETING.getUrl();
        },
        getSubCategory: function (category, subCategory) {
            var td = $("#trSubCategory").children('td').eq(1);
            var param = {
                "GenAll": true,
                "ParentType": category,
                "ActiveValue": subCategory
            };
            $.ajax({
                type: "POST",
                url: AFFILIATE.getAbsoluteUrl("Account/MarketingSubCategory"),
                data: JSON.stringify(param),
                contentType: "application/json",
                dataType: "json",
                success: function (result) {
                    var html = "<select id='slSubCategory' name='BannerSubCategory'><option value=''>" + Lang.All + "</option>";
                    for (var index = 0; index < result.length; index++) {
                        html += "<option value='" + result[index].TypeId + "'" + (subCategory === result[index].TypeId ? "selected='selected'" : "");
                        html += ">" + result[index].Value + "</option>";
                    }
                    html += '</select>';
                    td.html(html);
                },
                error: function () {
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        },
        getUrl: function (url) {
            var result = url;
            if (result === undefined) {
                result = location.href.match(new RegExp("[^?]+")).toString();
            }
            if ($("#slCategory")) {
                result = QueryString.setParam(result, "bannercategory", $("#slCategory").val());
            }
            if ($("#slSubCategory")) {
                result = QueryString.setParam(result, "bannersubcategory", $("#slSubCategory").val());
            }
            if ($("#slLanguage")) {
                result = QueryString.setParam(result, "language", $("#slLanguage").val());
            }
            if ($("#slCurrency")) {
                result = QueryString.setParam(result, "currency", $("#slCurrency").val());
            }
            if ($("#slImageType")) {
                result = QueryString.setParam(result, "imagetype", $("#slImageType").val());
            }
            if ($("#slSize")) {
                result = QueryString.setParam(result, "size", $("#slSize").val());
            }
            if ($("#slDomain")) {
                result = QueryString.setParam(result, "domain", $("#slDomain").val());
            }
            if ($("#slLinkTo")) {
                result = QueryString.setParam(result, "linkto", $("#slLinkTo").val());
            }
            return result;
        }
    };
} (jQuery));

$(function () {
    MARKETING.init();
});