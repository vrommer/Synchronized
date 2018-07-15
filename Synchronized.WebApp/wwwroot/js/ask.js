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
        'width': '100%',
        'onAddTag': validateTag
    });

    $('.question-tags').addClass('form-control');

    function intercept(event) {
        if (event.keyCode === 13) {
            return false;
        }
    }

    function validateTag() {
        var e = jQuery.Event("keypress", { keyCode: 20 });
        var tags = $('.tag');
        for (var i = 0; i < tags.length; i++) {
            var tagName = tags[i].textContent;
            if (!tagsAutocomplete.includes(tagName.match(/\S+/g)[0].trim())) {
                var xSign = tags[i].querySelector("a");
                xSign.click();
            }
        }
        $("#Question_Tags_tag").trigger(e);
    }

    (function flagPost() {
        return ajaxRequest("GET", "/api/Questions/TagsAutocomplete", this.id)
            .then((data) => {
                window.tagsAutocomplete = data;
                $(`#Question_Tags_tag`).autocomplete({
                    source: window.tagsAutocomplete
                });

            })
            .catch(xhr => { console.log(xhr); });
    })();
});