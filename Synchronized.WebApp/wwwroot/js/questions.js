// Write your Javascript code.
$(function () {
    $('input[id^=tags]').tagsInput({
        'height': '50px',
        'width': '300px',
        'interactive': false,
    });

    $('.tag a').remove();
    $('.tag').wrap('<a href="#"></a>');

    // Non functioning because page is refreshed on click
    $('.synched-sort a').on('click', function () {
        $('.synched-sort a').removeClass('active');
        $(this).addClass('active');

    }).on('mouseleave', function () {
        // TODO: Doesn't work. Try jQuery UI?'
        $(this).animate({ backgroundColor: "#eaeaea", }, 500);
    });

    //$('.synched-sort a').mouseout(function () {
    //    $(this).animate({ marginTop: "-=20px", }, 500);
    //});
})