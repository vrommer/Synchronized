$(() => {
    function showActivePosts() {
        $(this)
            .find("+ .user-active-posts")
            .show()
            .animate({
                height: "250px"
            }, 500);
    }

    $("#active-posts-button").on("click", showActivePosts);
});
