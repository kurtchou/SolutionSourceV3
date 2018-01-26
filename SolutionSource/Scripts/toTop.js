
; (function ($, window, document, undefined) {

    var pluginName = "toTop",
        defaults = {
            css: {
                position: "fixed",
                right: '2rem',
                bottom: '1rem',
                fontSize: '4rem',
                cursor: 'pointer',
                color: '#1e1e1e'
            },
            symbol: "<img src='../../Content/img/back2Top.png'/>",
            scrollUpSpeed: 200,
            triggerHeight: 50
        };

    // Plugin constructor
    function Plugin(element, options) {
        this.element = element;
        this.options = $.extend({}, defaults, options);
        this._rendered = false;
        this._defaults = defaults;
        this._name = pluginName;

        this.init();
    }

    Plugin.prototype = {
        init: function () {
            var self = this;
            var $toTop = $('<span>' + self.options.symbol + '</span>');


            $(window).scroll(function () {
                if (gPhoneViewFlag == 0) {
                    if ($(this).scrollTop() > self.options.triggerHeight && !self._rendered) {
                        $(self.element).append(
                        $toTop.css(self.options.css).fadeIn(1000).click(function () {
                            $("html, body").animate({ scrollTop: "0px" });
                        }));
                        self._rendered = true;
                    } else if ($(this).scrollTop() < self.options.triggerHeight && self._rendered) {
                        // Remove when at start of window
                        $toTop.fadeOut(1000, function () {
                            $toTop.delay(1000).remove();
                            self._rendered = false;
                        });

                    }
                } else {//phone
                    $toTop.remove();
                }


            })
        }
    };

    // A really lightweight plugin wrapper around the constructor,
    // preventing against multiple instantiations
    $.fn[pluginName] = function (options) {
        return this.each(function () {
            if (!$.data(this, "plugin_" + pluginName)) {
                $.data(this, "plugin_" + pluginName,
                new Plugin(this, options));
            }
        });
    };

})(jQuery, window, document);
