var AFFILIATE = (function ($) {
    return {
        scrollTop: function () {
            $('html, body').animate({ scrollTop: $('.container-fluid').offset().top }, 400);
        },
        initDatePicker: function () {
            var datePickers = $('.datepicker');

            if (datePickers.length > 0) {
                var isValidDate = function (dateString) {
                    // Parse the date parts to integers
                    var parts = dateString.split("/");
                    var day = parseInt(parts[1], 10);
                    var month = parseInt(parts[0], 10);
                    var year = parseInt(parts[2], 10);

                    // Check the ranges of month and year
                    if (year < 1000 || year > 3000 || month == 0 || month > 12)
                        return false;

                    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

                    // Adjust for leap years
                    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                        monthLength[1] = 29;

                    // Check the range of the day
                    return day > 0 && day <= monthLength[month - 1];
                };
                var validateDate = function (target) {
                    var minDate = target.datepicker("option", "minDate");
                    var maxDate = target.datepicker("option", "maxDate");
                    var inputDate = target.val(); // the visible value on screen
                    var currentDate = target.datepicker("getDate");
                    var errMsg = '';

                    // if selected date is invalid
                    if (!isValidDate(inputDate)) {
                        errMsg = ApoRes.InvalidDate;
                    }
                    if (errMsg.length == 0 && currentDate < minDate) {
                        errMsg = ApoRes.DateIsOutOfRange;
                    }
                    if (errMsg.length == 0 && maxDate < currentDate) {
                        errMsg = ApoRes.DateIsOutOfRange;
                    }

                    target.attr('title', errMsg).tooltip({
                        position: { my: "left top+2", at: "left bottom" },
                        tooltipClass: "warning"
                    });

                    if (errMsg.length > 0) {
                        target.tooltip('open');
                        return false;
                    }

                    target.tooltip('destroy');
                    target.removeAttr('title');
                };
                var limitDate = function (target, limit) { // limit: mindate/maxdate
                    var _limit = target.attr(limit);

                    if (_limit) { // if limit attribute exists
                        _limit = _limit.split("/");

                        var year = _limit[2],
								month = _limit[0],
								day = _limit[1];

                        return new Date(year, month - 1, day);
                    }

                    return null;
                };
                var shiftDate = function (target, increment) {
                    var currDate = target.datepicker('getDate');
                    var d = currDate.getDate() + increment;
                    var m = currDate.getMonth();
                    var y = currDate.getFullYear();

                    target.datepicker('setDate', new Date(y, m, d));
                };

                datePickers.each(function () {
                    var $this = $(this);

                    $this.datepicker({
                        defaultDate: new Date($this.val()),
                        dateFormat: "mm/dd/yy",
                        minDate: limitDate($this, "mindate"),
                        maxDate: limitDate($this, "maxdate"),
                        beforeShow: function (input, inst) {
                            //inst.dpDiv.css({ marginTop: '2px' });
                        },
                        onSelect: function (dateText, inst) {
                            //validateDate($this);
                        }
                    })
						.blur(function () {
						    //validateDate($this);
						})
						.parents('form').submit(function () {
						    //return validateDate($this);
						});

                    $this.next('.icon-calendar').click(function () {
                        $this.datepicker("show");
                    });
                });
            }
        },
        // Show language Box
        showLanguageBox: function () {
            $("#arrow").removeClass("arrUp");
            $("#arrow").addClass("arrDown");
            $("#langList").show();
        },
        // Hide language Box
        hideLanguageBox: function () {
            $("#arrow").removeClass("arrDown");
            $("#arrow").addClass("arrUp");
            $("#langList").hide();
        },
        // Change language
        changeLanguage: function (lang) {
            var param = {
                "lang": lang
            };
            $.ajax({
                type: "POST",
                url: AFFILIATE.getAbsoluteUrl("Header/ChangeLanguage"),
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(param),
                success: function (result) {
                    window.location.reload();
                }
            });
        },

        // Dialog
        hidePopupBlockLoading: function () {
            $("iframe").siblings('.block-loading').hide();
        },
        dialogOpt: {
            position: { my: "top+30%", at: "center top", of: window },
            modal: true,
            width: 'auto'
        },
        currentOpenDialog: null,
        openDialog: function (target, title, options) {
            options = options || {};
            if (title) options.title = title;

            if (target instanceof jQuery == false) target = $(target);

            AFFILIATE.currentOpenDialog = target.dialog($.extend({}, AFFILIATE.dialogOpt, options));

            var iframeLoading = target.find('iframe').get(0);
            if (iframeLoading) {
                iframeLoading.contentWindow.onload = function () {
                    AFFILIATE.hidePopupBlockLoading();

                };
            }

            return AFFILIATE.currentOpenDialog;
        },
        openDialogFrame: function (url, title, options) {
            var frameHtml = '<iframe src="' + url + '" width="100%" height="99%" frameborder="0" border="0"></iframe>';
            var frame = $("#aff-dialog-frame");

            if (frame.length > 0) {
                frame.html(frameHtml);
            } else {
                var blockLoading = '<div class="block-loading"></div>';
                frame = '<div id="aff-dialog-frame">' + blockLoading + frameHtml + '</div>';
            }
            return AFFILIATE.openDialog(frame, title, options);
        },
        closeDialog: function (target) {
            target.dialog("close");
        },
        resizeDialog: function (width, height) {
            if (AFFILIATE.currentOpenDialog && AFFILIATE.currentOpenDialog.dialog("isOpen")) {
                AFFILIATE.currentOpenDialog.dialog("option", { width: width + 30, height: height + 50 });
            }
        },
        setDialogHeight: function (target, height) {
            target.dialog("option", "height", height);
        },

        // Error message
        showErrorMsg: function (msg) {
            var msgWrp = $('.validation-summary-valid');
            msgWrp.show();
            if(msgWrp.hasClass('success')) {
                msgWrp.removeClass('success');
            }
            msgWrp.addClass('validation-summary-errors')
					.find('li:first').html(msg).show();
        },
        showSuccessMsg: function (msg) {
            var msgWrp = $('.validation-summary-valid');
            msgWrp.show();
            msgWrp.addClass('success')
			        .find('li:first').html(msg).show();
        },
        hideMessageRegion: function() {
            var msgWrp = $('.validation-summary-errors');
            msgWrp.hide();
        },
        isValidDate: function (year, month, day) {
            var beginDate = new Date(parseFloat(year) + "/" + month + "/" + day);
            if (beginDate.getYear() < 200)
                var begYear = beginDate.getYear() + 1900;
            else var begYear = beginDate.getYear();
            if (begYear != parseFloat(year) || beginDate.getMonth() + 1 != parseFloat(month) || beginDate.getDate() != parseFloat(day))
                return false;
            else return true;
        },

        //Clock 
        clock: function () {
            sysSecond++;
            if (sysSecond == 60) {
                sysSecond = 0;
                sysMinute++;
            }
            if (sysMinute == 60) {
                sysMinute = 0;
                sysHour++;
            }
            if (sysHour == 24) {
                sysHour = 0;
                sysSecond = 0;
                sysMinute = 0;
                sysDay++;
            }
            if (sysMinute < 10) {
                strMin = "0" + sysMinute;
            }
            else {
                strMin = sysMinute;
            }
            if (sysSecond < 10) {
                strSec = "0" + sysSecond;
            }
            else {
                strSec = sysSecond;
            }

            if (!AFFILIATE.isValidDate(sysYear, sysMonth, sysDay)) {
                sysMonth++;
                sysDay = 1;
            }
            if (!AFFILIATE.isValidDate(sysYear, sysMonth, sysDay)) {
                sysYear++;
                sysMonth = 1;
                sysDay = 1;
            }

            if (sysHour < 10) {
                strHrs = "0" + sysHour;
            }
            else {
                strHrs = sysHour;
            }
            $('#lblDateTime').html(strHrs + ":" + strMin + ":" + strSec + " " + sysDay + "-" + sysMonthName[sysMonth - 1] + "-" + sysYear + sysGmt);
        },

        getMaxHeight: function (element) {
            var height = 0;
            var title = 20;
            var footer = 20;
            var headerTitle = 26;
            var headerFilter = $("div.content-filter").outerHeight();
            var exHeight = footer + headerTitle + headerFilter + title;

            var body = window.document.body;
            if (window.innerHeight) {
                height = window.innerHeight;
            } else if (body.parentElement.clientHeight) {
                height = body.parentElement.clientHeight;
            } else if (body && body.clientHeight) {
                height = body.clientHeight;
            }
            var offset = element.offset();
            return height - offset.top - exHeight;
        },

        getMaxWidth: function (element) {
            var width = 0;
            var marginRight = 5;

            var body = window.document.body;
            if (window.innerWidth) {
                width = window.innerWidth;
            } else if (body.parentElement.clientWidth) {
                width = body.parentElement.clientWidth;
            } else if (body && body.clientWidth) {
                width = body.clientWidth;
            }

            return width - marginRight;
        },

        detectBrowser: function (userAgent) {
            if (userAgent === undefined) { userAgent = navigator.userAgent; }
            userAgent = userAgent.toLowerCase();
            var match = /(chrome)[ \/]([\w.]+)/.exec(userAgent) || /(webkit)[ \/]([\w.]+)/.exec(userAgent) || /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(userAgent) || /(msie) ([\w.]+)/.exec(userAgent) || userAgent.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(userAgent) || [];

            return {
                browser: match[1] || "",
                version: match[2] || "0"
            };
        },

        isNumber: function (text) {
            return !isNaN(parseFloat(text)) && isFinite(text);
        },

        isInteger: function (text) {
            return typeof AFFILIATE.isNumber(text) && parseFloat(text) == parseInt(text, 10) && !isNaN(text);
        },

        asBool: function (input) {
            if (typeof input == 'string') {
                return input.toLowerCase() == 'true';
            }

            return !!input;
        },


    };
})(jQuery);

$(function () {
    // scroll top page
    $('.link-top').click(function () {
        AFFILIATE.scrollTop();
    });

    //$('.datepicker').datepicker();
    AFFILIATE.initDatePicker();

    //clock
    if ($("#lblDateTime").length > 0) {
        AFFILIATE.clock();
        setInterval("AFFILIATE.clock();", 1000);

    }
    // Show or Hide language Box
    $('#box').mouseover(function () {
        AFFILIATE.showLanguageBox();
    });

    $('#box').mouseout(function () {
        AFFILIATE.hideLanguageBox();
    });

    // Change language
    $('ul[id=act-change-language] A').click(function () {
        AFFILIATE.changeLanguage($(this).attr('class'));
    });

    $.datepicker.setDefaults($.datepicker.regional[$('#selectedLang').attr('class')]);
});