$(() => {

    $(`.question-tags`).on("keydown", intercept);
    $(`.question-tags`).on("keyup", intercept);
    $(`.question-tags`).on("keypress", intercept);
    $("textarea").jqte({
        formats: [
            ["p", "Normal"],
            ["pre", "Code"]
        ]
    });

    $('.question-tags').tagsInput({
        'height': '44px',
        'width': '100%'
    });

    $('.question-tags').addClass('form-control');

    function intercept(event) {
        if (event.keyCode === 13) {
            return false;
        }
    }
});