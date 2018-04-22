$("textarea").jqte({
    formats: [
        ["p", "Normal"],
        ["pre", "Code"]
    ]
});

$('input[id=TagNames]').tagsInput({
    'height': '44px',
    'width': '100%'
});

$('.tagsinput').addClass('form-control');