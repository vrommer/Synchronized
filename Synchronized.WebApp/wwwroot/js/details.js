var answers = {};

var allComments = [];

$(() => {

    $.merge(allComments, question.comments);

    $(`#${questionViewModel.question.id} .vote-up-btn`).on("click", VoteUpQuestion.bind(questionViewModel.question))
    $(`#${questionViewModel.question.id} .vote-down-btn`).on("click", VoteDownQuestion.bind(questionViewModel.question))
    $(`#${questionViewModel.question.id} .synched-flag`).on("click", flagPost.bind(question))
    $(`#${questionViewModel.question.id} .comment-body`).on("keydown", submitComment.bind(question));
    $(`#${questionViewModel.question.id} .synched-delete`).on("click", deleteVotedPost.bind(question));

    for (var id in questionViewModel.answers) {
        var answer = questionViewModel.answers[id];
        $.merge(allComments, answer.comments);

        answers[answer.id] = answer;

        $(`#${answer.id} .vote-up-btn`).on("click", VoteUpAnswer.bind(answer))
        $(`#${answer.id} .vote-down-btn`).on("click", VoteDownAnswer.bind(answer))
        $(`#${answer.id} .synched-flag`).on("click", flagPost.bind(answer));
        $(`#${answer.id} .comment-body`).on("keydown", submitComment.bind(answer));     
        //$(`#${answer.id} .comment-body`).keydown(newLine);  
        $(`#${answer.id} .synched-delete`).on("click", deleteVotedPost.bind(answer));
    }

    //allComments.forEach(comment => {
    //    $(`#${comment.id} .synched-delete`).on("click", deleteVotedPost.bind(comment));
    //    $(`#${comment.id} .synched-flag`).on("click", flagPost.bind(answer));
    //    //$(`#${comment.id} .submit-comment`).on("click", submitComment.bind(comment));
    //});

    $(".synched-comment").on("click", showComment);

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

                statusCode: {
                    400: data => {
                        reject(data);
                    }
                },

                failure: data => {
                    console.log("DEBUG: Failure!")
                },

                error: data => {
                    console.log("DEBUG: Error!")
                }
            });
        });
    }

    function flagPost() {
        return ajaxRequest("POST", "/api/VotedPosts/FlagPost", this.id)
            .then(() => { alert("Your flag has been accapted!"); })
            .catch(xhr => { alert("User not permitted to flag!"); });
    }

    function VoteUpQuestion() {
        VoteForPost("/api/Questions/VoteUpQuestion", this.id);
    }

    function VoteDownQuestion() {
        VoteForPost("/api/Questions/VoteDownQuestion", this.id);
    }

    function VoteUpAnswer() {
        VoteForPost("/api/Questions/VoteUpAnswer", this.id);
    }

    function VoteDownAnswer() {
        VoteForPost("/api/Questions/VoteDownAnswer", this.id);
    }

    function VoteForPost(url, post) {
        ajaxRequest("POST", url, post)
            .then(updatePostVotes)
            .then(updatePointsInPage)
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
                    .html(questionViewModel.posts[updatedPost.id].points);
                resolve(updatedPost);
            } catch (err) {
                reject(err)
            }
        });
    }

    function submitComment(event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13' && event.ctrlKey) {
            var commentForm = $(`#${this.id} .synched-comment-form`);
            var commentBody = commentForm.find("textarea").val();
            ajaxRequest("POST", "/api/VotedPosts/CommentOnPost", { votedPostId: this.id, body: commentBody })
                .then(addNewCommentToPage.bind(this))
                .then(hideComment.bind(this))                
                .catch(function () { alert("Failure!") });
        }
    }

    function newLine(e) {
        if (e.keyCode === 13 && e.ctrlKey) {
            $(this).val(function (i, val) {
                return val + "\n";
            });
        } 
    }

    function showComment() {
        $(this).find("+ .synched-comment-form").show()
        .animate({
            height: "135px",
            width: "100%"
        }, 500);
    }

    function hideComment() {    
        $(`#${this.id} .synched-comment-form`).hide().find("textarea").val("");
    }

    function addNewCommentToPage(comment) { 
        var text = `
            <hr />
            <div class="comment-content" id="${comment.id}">
                <div>${comment.body}</div>
                <button type="button" class="btn btn-link synched-edit">Edit</button>
                <button type="button" class="btn btn-link synched-delete">Delete</button>
            </div>
        `
        var comments = $(`#${this.id} .synched-comments`)
        if (!comments.length) {
            comments = $('<div class="synched-comments"></div>')
            $(`#${this.id} .synched-meta`).after(comments);
        }
        comments.append(text);
    }

    function deleteVotedPost() {
        return ajaxRequest("POST", "/api/VotedPosts/DeletePost", this.id)
            .then(() => { alert("Your vote has been accapted!") })
            .catch(() => { alert("User can't delete this post!") });
    }
});