var answers = {};

$(function () {
    $("#synched-quetion .vote-up-btn").on("click", VoteUpQuestion)
    $("#synched-quetion .vote-down-btn").on("click", VoteDownQuestion)

    $("#synched-quetion .submit-comment").on("click", submitQuestionComment);

    $("#synched-quetion .synched-flag").on("click", flag.bind(question));

    question.answers.forEach(function (answer) {
        answers[answer.id] = answer;
        var answerId = {
            id: answer.id
        }
        $(`#${answer.id} .vote-up-btn`).on("click", VoteUpAnswer.bind(answerId))
        $(`#${answer.id} .vote-down-btn`).on("click", VoteDownAnswer.bind(answerId))

        $(`#${answer.id} .submit-comment`).on("click", submitAnswerComment.bind(answerId));
    });



    $(".synched-question-comment").on("click", function (event) {
        $(this).find("+ .synched-comment-form").show();
        $(this).find("+ .synched-comment-form").animate({
            height: "135px",
            width: "100%"
        }, 500);
    });

    $("#synched-answer textarea").jqte({
        formats: [
            ["p", "Normal"],
            ["pre", "Code"]
        ]
    });

    function ajaxRequest(method, url, data) {
        return new Promise(function (success, failure) {
            $.ajax({
                type: method,
                url: url,
                data: JSON.stringify(data),
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

    function updateQuestionPoints(updatedQuestion) {
        question = updatedQuestion;
        $("#synched-quetion .synched-points").html(updatedQuestion.points);
    }

    function updateAnswerPoints(updatedAnswer) {
        answers[updatedAnswer.id] = updatedAnswer;
        $(`#${updatedAnswer.id} .synched-points`).html(updatedAnswer.points);
    }

    function updateQuestionComments(newComment) {
        var commentsContainer = $("#synched-quetion .synched-comments");
        var comment = $("<div></div>");
        comment.append(`<div>${newComment.body}</div>`);
        comment.append('<button type="button" class="btn btn-link synched-question-comment">Edit</button>');
        comment.append('<button type="button" class="btn btn-link synched-question-comment">Flag</button>');
        commentsContainer.append("<hr />");
        commentsContainer.append(comment);

    }

    function updateAnswerComments(updatedAnswer) {
        alert("Comment added to answer!");
    }

    function VoteUpQuestion() {
        ajaxRequest("POST", "/api/Questions/VoteUpQuestion", question)
            .then(updateQuestionPoints)
            .catch(function (xhr) { console.log(xhr); });
    }

    function VoteDownQuestion() {
        ajaxRequest("POST", "/api/Questions/VoteDownQuestion", question)
            .then(updateQuestionPoints);
    }

    function VoteUpAnswer() {
        ajaxRequest("POST", "/api/Questions/VoteUpAnswer", answers[this.id])
            .then(updateAnswerPoints);
    }

    function VoteDownAnswer() {
        ajaxRequest("POST", "/api/Questions/VoteDownAnswer", answers[this.id])
            .then(updateAnswerPoints);
    }

    function submitQuestionComment() {
        var commentBody = $("#synched-quetion .synched-comment-form textarea").val();
        var comment = {
            body: commentBody,
            postId: question.id
        }
        ajaxRequest("POST", "/api/Questions/SubmitQuestionComment", comment)
            .then(updateQuestionComments);
    }

    function submitAnswerComment() {
        var commentBody = $(`#${this.id} .synched-comment-form textarea`).val();
        var comment = {
            body: commentBody,
            postId: this.id
        }
        ajaxRequest("POST", "/api/Questions/SubmitAnswerComment", comment)
            .then(updateAnswerComments);
    }

    function flag() {
        var questionFlag = {
            question: this
        };
        ajaxRequest("GET", "/api/Questions/GetCurrentUser")
            .then(function (data) { questionFlag.user = data; return questionFlag; })
            .then(function (data) { question.questionFlags.push(data) })
            .then(ajaxRequest("POST", "/api/Questions/SaveChanges", question))
            .then(console.log("Success!"))
            .catch(function (xhr) {
                console.log(xhr);

            });
    }
});