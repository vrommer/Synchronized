class Details {
    _answers = {};
    _allComments = [];

    questionEvent = "Question";
    answerEvent = "Answer";
    commentAction = "Comment";

    handlers = new Map([
        ["voteUpQuestion", this.voteUpQuestion],
        ["voteDownQuestion", this.voteDownQuestion],
        ["voteUpAnswer", this.voteUpAnswer],
        ["voteDownAnswer", this.voteDownAnswer],
        ["acceptAnswer", this.acceptAnswer],
        ["editComment", this.editComment],
        ["updateComment", this.updateComment],
        ["deleteComment", this.deleteComment],
        ["action", this.act],
        ["flag", this.flagPost],
        ["delete", this.deleteVotedPost]
    ]);

    constructor() {
        this.init();
    }

    destructor() {

    }

    init() {
        console.log("In init");
        $(() => {

            $('input[id^=tags]').tagsInput({
                'height': '50px',
                'width': '400px',
                'interactive': false,
            });

            $('.tag a').remove();
            $('.tag').wrap('<a href="#"></a>');

            let votedPosts = {};
            votedPosts[questionViewModel.question.id] = questionViewModel.question;

            for (let pointer in questionViewModel.answers) {
                let answer = questionViewModel.answers[pointer];
                $.merge(this.allComments, answer.comments);
                votedPosts[answer.id] = answer;
            }            

            $("#synched-question").on("click", event => {
                event.stopPropagation();
                let postId = $("#synched-question").attr("data-id");
                this.handleUserAction({
                    event: event,
                    postId: postId,
                    eventTarget: this.questionEvent
                });
            });

            $("#synched-answers").on("click", event => {
                event.stopPropagation();
                let target = event.target;
                while (target && target.dataset.type != "answer") {
                    target = target.parentElement;
                }
                if (target && target.dataset.id) {
                    this.handleUserAction({
                        event: event,
                        postId: target.dataset.id,
                        eventTarget: this.answerEvent
                    });
                }
            });
            
            $.merge(this.allComments, question.comments);
            /**
            
                $(".synched-comment").on("click", showComment);    

                ...

                $(`#${id} .comment-body`).on("keydown", submitComment.bind(votedPosts[id]));
                $(`#${id} .edit-comment-body`).on("keydown", submitComment.bind(votedPosts[id]));

            */
            $("#synched-answer textarea").jqte({
                formats: [
                    ["p", "Normal"],
                    ["pre", "Code"]
                ]
            });
        });
    }

    get answers() {
        return this._answers;
    }

    get allComments() {
        return this._allComments;
    }

    set answers(val) {
        this._answers = val;
    }

    set allComments(val) {
        this._allComments = val;
    }

    handleUserAction = (oArgs) => {
        if (!oArgs) {
            return;
        }
        let event = oArgs.event,
            classes,
            postId = oArgs.postId,
            eventTarget = oArgs.eventTarget,
            actionType,
            fnAction;
        console.log("handling for post: ", postId);
        let target = event.target;
        while (target && target.nodeName != "BUTTON") {
            target = target.parentElement;
        }       

        if (target) {
            actionType = target.dataset.type;
        }

        if (target && target.parentElement && target.parentElement.dataset.target == this.commentAction) {
            actionType = this.commentAction;
        } 

        if (!(actionType != "flag" && actionType != "delete" && actionType != "action")) {
            fnAction = this.handlers.get(actionType)
        }

        else if (actionType) {
            fnAction = this.handlers.get(actionType + eventTarget);
        }

        if (fnAction) {
            fnAction = fnAction.bind(this);
            fnAction(oArgs);
        }
    }         

    act() {
        window.location.href = '/Account/Login';
    }

    flagPost(oArgs) {
        return ajaxRequest("POST", "/api/VotedPosts/FlagPost", oArgs.postId)
            .then(() => { alert("Your flag has been accapted!"); })
            .catch(xhr => { alert("User not permitted to flag!"); });
    }

    voteUpQuestion(oArgs) {
        let questionId = oArgs.postId;
        this.voteForPost("/api/Questions/VoteUpQuestion", questionId);
    }

    voteDownQuestion(oArgs) {
        let questionId = oArgs.postId;
        this.voteForPost("/api/Questions/VoteDownQuestion", questionId);
    }

    voteUpAnswer(oArgs) {
        let answerId = oArgs.postId;
        this.voteForPost("/api/Questions/VoteUpAnswer", answerId);
    }

    voteDownAnswer(oArgs) {
        let answerId = oArgs.postId;
        this.voteForPost("/api/Questions/VoteDownAnswer", answerId);
    }

    acceptAnswer(oArgs) {
        ajaxRequest("POST", "/api/Questions/AcceptAnswer", oArgs.postId)
            .then(() => {
                location.reload();
            })
            .catch(xhr => { console.log(xhr); });
    }

    voteForPost(url, post) {
        ajaxRequest("POST", url, post)
            .then(this.updatePostVotes)
            .then(this.updatePointsInPage)
            .catch(xhr => { alert("User not allowed to vote."); });
    }

    updatePostVotes(updatedPost) {
        return new Promise((resolve, reject) => {
            try {
                questionViewModel.updatePostVotes(updatedPost);
                resolve(updatedPost);
            } catch (err) {
                reject(err);
            }
        });
    }

    updatePointsInPage(updatedPost) {
        return new Promise((resolve, reject) => {
            try {
                let elem = $(`div[data-id=${updatedPost.id}] .synched-points`);
                if (elem) {
                    elem.html(questionViewModel.posts[updatedPost.id].points);
                }
                resolve(updatedPost);
            } catch (err) {
                reject(err)
            }
        });
    }

    submitComment(event) {
        let keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13' && event.ctrlKey) {
            let commentForm = $(`#${this.id} .synched-comment-form`);
            let commentBody = commentForm.find("textarea").val();
            ajaxRequest("POST", "/api/VotedPosts/CommentOnPost", { votedPostId: this.id, body: commentBody })
                .then(reloadPage)
                .catch(function () { alert("User not alllowed to comment!") });
        }
    }

     newLine(e) {
        if (e.keyCode === 13 && e.ctrlKey) {
            $(this).val(function (i, val) {
                return val + "\n";
            });
        }
    }

     showComment() {
        $(this).find("+ .synched-comment-form")
            .show()
            .animate({
                height: "120px",
                width: "100%"
            }, 500);
    }

     editComment(oArgs) {
        let parent = $(`#${this.id}`);
        let commentBody = parent.find(".comment-body");
        let commentText = commentBody.text();
        let editForm = parent.find(".synched-edit-comment-form");
        let editBtn = parent.find(".synched-edit-comment");
        let deleteBtn = parent.find(".synched-delete-comment");
        let updateBtn = parent.find(".synched-update-comment");

        editBtn.hide();
        deleteBtn.hide();
        updateBtn.show();

        commentBody.hide();
        editForm.show();
        editForm.find("textarea").show().text(commentText);
    }

     reloadPage() {
        location.reload();
    }

     deleteVotedPost() {
        return ajaxRequest("POST", "/api/VotedPosts/DeletePost", this.id)
            .then(() => { alert("Your vote has been accapted!") })
            .catch(() => { alert("User can't delete this post!") });
    }

     deleteComment() {
        return ajaxRequest("POST", "/api/Comments/DeletePost", this.id)
            .then(reloadPage)
            .catch(function () { alert("Failure!") });
    }
}

const details = new Details();
                