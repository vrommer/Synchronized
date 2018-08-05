var answers = {};

var allComments = [];

$(() => {
    $('input[id^=tags]').tagsInput({
        'height': '50px',
        'width': '400px',
        'interactive': false,
    });

    $('.tag a').remove();
    $('.tag').wrap('<a href="#"></a>');

    var votedPosts = {};
    votedPosts[questionViewModel.question.id] = questionViewModel.question;

    for (var pointer in questionViewModel.answers) {
        var answer = questionViewModel.answers[pointer];
        $.merge(allComments, answer.comments);
        votedPosts[answer.id] = answer;
    }

    for (var id in votedPosts) {
        if (id !== question.id) {
            $(`#${id} .vote-up-btn`).on("click", VoteUpAnswer.bind(votedPosts[id]))
            $(`#${id} .vote-down-btn`).on("click", VoteDownAnswer.bind(votedPosts[id]))
            $(`#${id} .synched-accept`).on("click", AcceptAnswer.bind(votedPosts[id]))
        } else 
        {
            $(`#${id} .vote-up-btn`).on("click", VoteUpQuestion.bind(votedPosts[id]))
            $(`#${id} .vote-down-btn`).on("click", VoteDownQuestion.bind(votedPosts[id]))
        }
        $(`#${id} .synched-action`).on("click", Act.bind(votedPosts[id]))
        $(`#${id} .synched-flag`).on("click", flagPost.bind(votedPosts[id]))
        $(`#${id} .comment-body`).on("keydown", submitComment.bind(votedPosts[id]));
        $(`#${id} .synched-delete`).on("click", deleteVotedPost.bind(votedPosts[id]));
    }

    $.merge(allComments, question.comments);

    for (var id in allComments) {
        $(`#${allComments[id].id} .synched-delete-comment`).on("click", deleteComment.bind(allComments[id]))
        $(`#${allComments[id].id} .synched-edit-comment`).on("click", editComment.bind(allComments[id]));
    }

    $(".synched-comment").on("click", showComment);    

    $("#synched-answer textarea").jqte({
        formats: [
            ["p", "Normal"],
            ["pre", "Code"]
        ]
    });

    function Act() {
        window.location.href = '/Account/Login';
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

    function AcceptAnswer() {
        ajaxRequest("POST", "/api/Questions/AcceptAnswer", this)
            .then(() => {                
                location.reload(); 
            })
            .catch(xhr => { console.log(xhr); });
    }

    function VoteForPost(url, post) {
        ajaxRequest("POST", url, post)
            .then(updatePostVotes)
            .then(updatePointsInPage)
            .catch(xhr => { alert("User not allowed to vote."); });
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
                .then(reloadPage)              
                .catch(function () { alert("User not alllowed to comment!") });
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
        $(this).find("+ .synched-comment-form")
            .show()
            .animate({
                height: "120px",
                width: "100%"
            }, 500);
    }

    function editComment() {
        var parent = $(`#${this.id}`);
        var commentBody = parent.find(".comment-body");
        var commentText = commentBody.text();
        var editForm = parent.find(".synched-edit-comment-form");
        var editBtn = parent.find(".synched-edit-comment");
        var deleteBtn = parent.find(".synched-delete-comment");
        var updateBtn = parent.find(".synched-update-comment");

        editBtn.hide();
        deleteBtn.hide();
        updateBtn.show();

        commentBody.hide();
        editForm.show();
        editForm.find("textarea").show().text(commentText);
    }

    function reloadPage() {
        location.reload();
    }

    function deleteVotedPost() {
        return ajaxRequest("POST", "/api/VotedPosts/DeletePost", this.id)
            .then(() => { alert("Your vote has been accapted!") })
            .catch(() => { alert("User can't delete this post!") });
    }

    function deleteComment() {
        return ajaxRequest("POST", "/api/Comments/DeletePost", this.id)
            .then(reloadPage)              
            .catch(function () { alert("Failure!") });
    }
});