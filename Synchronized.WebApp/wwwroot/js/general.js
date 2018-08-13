$(() => {
    function onElementHeightChange(elm, callback) {
        var body = document.body;
            html = document.documentElement;

        var lastHeight = Math.max(body.scrollHeight, body.offsetHeight,
                html.clientHeight, html.scrollHeight, html.offsetHeight);
        if (lastHeight < 950) {
            $("footer").css("position", "absolute");
        }
        else {
            $("footer").css("position", "relative");
        }
        (function run() {
            var newHeight = Math.max(body.scrollHeight, body.offsetHeight,
                html.clientHeight, html.scrollHeight, html.offsetHeight);
            callback(newHeight);
            lastHeight = newHeight;

            if (elm.onElementHeightChangeTimer)
                clearTimeout(elm.onElementHeightChangeTimer);

            elm.onElementHeightChangeTimer = setTimeout(run, 200);
        })();
    }

    onElementHeightChange(document.body, function (height) {
        if (height < 950) {
            $("footer").css("position", "absolute");
        }
        else {
            $("footer").css("position", "relative");
        }
    });    
});