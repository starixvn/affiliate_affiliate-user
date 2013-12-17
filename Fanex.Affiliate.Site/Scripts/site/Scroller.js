var SCROLLER = function ($) {
    var scrollInterval = null;
    var scrollerPaused = false;
    var scroller;
    var topnews;
    var maskWidth;
    var offsetLeft;
    return {
        start: function() {
            var text_news = $('#text_news');
            var scroller = $('#scroller');
            var topnews = $('#topnews');

            if (topnews.length == 1) {
                topnews.mouseover(function() { SCROLLER.scrollerPaused = true; });
                topnews.mouseout(function() { SCROLLER.scrollerPaused = false; });

                SCROLLER.scroller = scroller;
                SCROLLER.topnews = topnews;
                SCROLLER.maskWidth = parseInt(text_news.width());
                SCROLLER.offsetLeft = parseInt(scroller.offset().left);

                scrollInterval = setInterval(SCROLLER.scrolling, 20);
            }
        },
        scrolling: function() {
            if (SCROLLER.scrollerPaused) return;

            var pos = SCROLLER.scroller.offset();
            var top = parseInt(pos.top);
            var left = parseInt(pos.left) - 1;
            var width = parseInt(SCROLLER.topnews.width());
            if (left + width < SCROLLER.offsetLeft) left = SCROLLER.offsetLeft + SCROLLER.maskWidth;

            SCROLLER.scroller.offset({ top: top, left: left });
        }
    };
} (jQuery);

$(function () {
    SCROLLER.start();
});