$(function () {
    $("textarea").jqte({
        formats: [
            ["p", "Normal"],
            ["pre", "Code"]
        ]
    });

    $(".vote-up-btn").on("click", voteUp)   
    $(".vote-down-btn").on("click", voteDown)

    function ajaxRequest(method, url) {
        return new Promise(function (success, failure) {
            $.ajax({
                type: method,
                url: url,
                data: JSON.stringify(question),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    success(data);
                },

                failure: function (data) {
                    failure(data);
                },
                error: function (data) {
                    failure(data);
                }
            });
        });
    }

    function updateQuestionPoinst(updatedQuestion) {
        question = updatedQuestion;
        $("#synched-quetion .synched-points").html(updatedQuestion.points);
    }

    function voteUp() {
        ajaxRequest("POST", "/api/Voter/VoteUp")
            .then(updateQuestionPoinst);
    }

    function voteDown() {
        ajaxRequest("POST", "/api/Voter/VoteDown")
            .then(updateQuestionPoinst);
    }

    function flag() {
        console.log("flagging!")
    }

});