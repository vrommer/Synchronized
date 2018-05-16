var answers = {};

var allComments = [];

$(() => {

    $.merge(allComments, question.comments);

    $(`#${questionViewModel.question.id} .vote-up-btn`).on("click", VoteUpQuestion.bind(questionViewModel.question))
    $(`#${questionViewModel.question.id} .vote-down-btn`).on("click", VoteDownQuestion.bind(questionViewModel.question))
    $("#synched-quetion .comment-body").on("keypress", submitQuestionComment);
    $("#synched-quetion .synched-flag").on("click", flagPost.bind(question));
    $(`${questionViewModel.question.id} .synched-delete`).on("click", deletePost.bind(question));

    //questionViewModel.model.answers.forEach(answer => {

    //    $.merge(allComments, answer.comments);

    //    answers[answer.id] = answer;

    //    $(`#${answer.id} .vote-up-btn`).on("click", VoteUpAnswer.bind(answer))
    //    $(`#${answer.id} .vote-down-btn`).on("click", VoteDownAnswer.bind(answer))
    //    $(`#${answer.id} .comment-body`).on("keypress", submitAnswerComment.bind(answer));
    //    $(`#${answer.id} .synched-flag`).on("click", flagPost.bind(answer));
    //    $(`#${answer.id} .synched-delete`).on("click", deletePost.bind(answer));
    //});

    for (var id in questionViewModel.answers) {
        var answer = questionViewModel.answers[id];
        $.merge(allComments, answer.comments);

        answers[answer.id] = answer;

        $(`#${answer.id} .vote-up-btn`).on("click", VoteUpAnswer.bind(answer))
        $(`#${answer.id} .vote-down-btn`).on("click", VoteDownAnswer.bind(answer))
        $(`#${answer.id} .comment-body`).on("keypress", submitAnswerComment.bind(answer));
        $(`#${answer.id} .synched-flag`).on("click", flagPost.bind(answer));
        $(`#${answer.id} .synched-delete`).on("click", deletePost.bind(answer));
    }

    allComments.forEach(comment => {
        $(`#${comment.id} .synched-delete`).on("click", deletePost.bind(comment));
        //$(`#${comment.id} .submit-comment`).on("click", submitComment.bind(comment));
    });

    $(".synched-question-comment").on("click", function() {
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
        return new Promise((resolve, reject) => {
            $.ajax({
                type: method,
                url: url,
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                success: data => {
                    resolve(data);
                },

                failure: data => {
                    reject(data);
                },
                error: data => {
                    reject(data);
                }
            });
        });
    }

    function updateQuestionPoints(updatedQuestion) {
        question = updatedQuestion;
        $(`#${questionViewModel.question.id}`).html(updatedQuestion.points);
    }

    function updateAnswerPoints(updatedAnswer) {
        answers[updatedAnswer.id] = updatedAnswer;
        $(`#${updatedAnswer.id} .synched-points`).html(updatedAnswer.points);
    }

    function updateQuestionComments(newComment) {
        if (newComment) {
            var commentsContainer = $("#synched-quetion .synched-comments");
            var comment = $("<div></div>");
            comment.append(`<div>${newComment.body}</div>`);
            comment.append('<button type="button" class="btn btn-link synched-question-comment">Edit</button>');
            comment.append('<button type="button" class="btn btn-link synched-question-comment">Flag</button>');
            commentsContainer.append("<hr />");
            commentsContainer.append(comment);
        }

    }

    function updateAnswerComments(updatedAnswer) {
        alert("Comment added to answer!");
    }

    function updatePost(updatedPost) {
        questionViewModel.posts[updatedPost.id] = updatedPost;        
    }

    //---------------------------------------------------------------------------
    function VoteUpQuestion() {
        VoteForPost("/api/Posts/VoteUpQuestion", this);
    }

    function VoteDownQuestion() {
        VoteForPost("/api/Posts/VoteDownQuestion", this);
    }

    function VoteUpAnswer() {
        VoteForPost("/api/Posts/VoteUpAnswer", this);
    }

    function VoteDownAnswer() {
        VoteForPost("/api/Posts/VoteDownAnswer", this);
    }

    function VoteForPost(url, post) {
        ajaxRequest("POST", url, post)
            .then(updatePostVotes)
            .then(updatePointsInPage)
            .then(updateHandler)
            .catch(xhr => { console.log(xhr); });
    }

    function updatePostVotes(updatedPost) {
        return new Promise((resolve, reject) => {
            try {
                questionViewModel.updatePostVotes(updatedPost);
                resolve(updatedPost);
            } catch (err) {
                reject(err);
            }
        });
    }

    function updatePointsInPage(updatedPost) {
        return new Promise((resolve, reject) => {
            try {
                $(`#${updatedPost.id} .synched-points`)
                    .html(questionViewModel.posts[updatedPost.id].sumVotes);
                resolve(updatedPost);
            } catch (err) {
                reject(err)
            }
        });
    }

    function updateHandler(updatedPost) {
        $(`#${updatedPost.id} .vote-up-btn`).off("click", VoteUpQuestion);
        $(`#${updatedPost.id} .vote-up-btn`).on("click", VoteUpQuestion.bind(updatedPost));
    }
    //-----------------------------------------------------------------------------

    // TODO: Use this function
    //function submitComment() {
    //    var commentForm = $(`#${this.id} .synched-comment-form`);
    //    var commentBody = commentForm.find("textarea").val();
    //    ajaxRequest("POST", "/api/Posts/SubmitComment", comment)
    //        .then(updateAnswerComments)
    //        .then(hideComment.bind(this));
    //}

    function submitQuestionComment(event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var commentBody = $("#synched-quetion .synched-comment-form textarea").val();
            var comment = {
                body: commentBody,
                postId: question.id
            }
            ajaxRequest("POST", "/api/Posts/SubmitQuestionComment", comment)
                .then(updateQuestionComments)
                .then(hideQuestionComment);
        }
    }

    function submitAnswerComment(event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var commentBody = $(`#${this.id} .synched-comment-form textarea`).val();
            var comment = {
                body: commentBody,
                postId: this.id
            }
            ajaxRequest("POST", "/api/Posts/SubmitAnswerComment", comment)
                .then(updateAnswerComments)
        }
    }

    function updateAnswers() {
        alert("Answer saved!");
    }

    function flagPost() {
        return ajaxRequest("POST", "/api/Posts/FlagPost", this.id)
            .then(() => { console.log("success!"); })
            .catch(xhr => { console.log("error"); });            
    }

    function deletePost() {
        return ajaxRequest("POST", "/api/Posts/Delete", this.id)
            .then(() => { console.log("success!") })
            .catch(() => { console.log("faliure!") });
    }

    // TODO: Use these functions
    // ---------------------------------------------------------------
    //function showComment() {
    //    $(this).find("+ .synched-comment-form").show()
    //    .animate({
    //        height: "135px",
    //        width: "100%"
    //    }, 500);
    //}

    //function hideComment() {        
    //    $(this).find("+ .synched-comment-form").animate({
    //        height: "0",
    //        width: "0"
    //    }, 500, function () {
    //        $(this).find("textarea").val("");
    //        $(this).hide();
    //    });
    //}
    // ---------------------------------------------------------------

    function hideQuestionComment() {
        $(".synched-question-comment + .synched-comment-form").animate({
            width: "0",
            height: "0"
        }, 500, function () {
            $(this).find("textarea").val("");
            $(this).hide();
        });
    }
});