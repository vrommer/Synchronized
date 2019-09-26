class Details {
    _answers = {};
    _allComments = [];

    questionEvent = "Question";
    answerEvent = "Answer";
    commentAction = "Comment";
    votedPosts = {};

    handlers = new Map([
        ["voteUpQuestion", this.voteUpQuestion],
        ["voteDownQuestion", this.voteDownQuestion],
        ["comment", this.showComment],
        ["voteUpAnswer", this.voteUpAnswer],
        ["voteDownAnswer", this.voteDownAnswer],
        ["acceptAnswer", this.acceptAnswer],
        ["editComment", this.doEditComment],
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
            
            this.votedPosts[questionViewModel.question.id] = questionViewModel.question;

            for (let pointer in questionViewModel.answers) {
                let answer = questionViewModel.answers[pointer];
                $.merge(this.allComments, answer.comments);
                this.votedPosts[answer.id] = answer;
            }            

            $("#synched-question").on("click", event => {
                event.stopPropagation();
                let postId = $("#synched-question").attr("data-id");
                this.handleUserAction({
                    target: event.target,
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
                        target: event.target,
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
        let postId = oArgs.postId,
            eventTarget = oArgs.eventTarget,
            actionType,
            fnAction,
            target = oArgs.target;
        while (target && target.nodeName != "BUTTON") {
            target = target.parentElement;
        }       

        if (target) {
            actionType = target.dataset.type;
            oArgs.target = target;
        }
        
        if (target && target.parentElement && target.parentElement.dataset.target == this.commentAction) {
            oArgs.commentId = target.parentElement.dataset.id;
            oArgs.actionType = actionType;
            this.handleUserActionForComment(oArgs);
        }

        else {
            this.handleUserActionForVotedPosts(oArgs);
        }
    }   

    handleUserActionForVotedPosts(oArgs) {
        if (!oArgs) {
            return;
        }
        let postId = oArgs.postId,
            eventTarget = oArgs.eventTarget,
            actionType,
            target = oArgs.target,
            fnAction;
        while (target && target.nodeName != "BUTTON") {
            target = target.parentElement;
        }       

        if (target) {
            actionType = target.dataset.type;
        }

        if (!(actionType != "flag" && actionType != "delete" && actionType != "action" && actionType != "comment")) {
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

    handleUserActionForComment(oArgs) {
        if (!oArgs) {
            return;
        }
        let actionType = oArgs.actionType,
            fnAction = this.handlers.get(actionType + this.commentAction);
        if (fnAction) {
            fnAction.apply(this, [oArgs]);
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
        ajaxRequest("POST", "/api/Questions/AcceptAnswer", this.votedPosts[oArgs.postId])
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

    showComment(oArgs) {
        let elem = $(oArgs.target).find("+ .synched-comment-form");
        if (!elem.is(':visible')) {
            elem.show()
                .animate({
                    height: "120px",
                    width: "100%"
                }, 500);
        } else {
            elem.animate({
                height: "0",
                width: "0"
            }, 500, () => { elem.hide(); });
        }
    }

    doEditComment(oArgs) {
        let target = $(oArgs.target),
            comment = target.parent(),
            commentBody = comment.find(".comment-body"),
            commentText = commentBody.text(),
            editForm = comment.find(".synched-edit-comment-form"),
            updateBtn = comment.find(".synched-update-comment"),
            oArguments = {
                updateBtn: updateBtn,
                commentBody: commentBody,
                commentText: commentText,
                editForm: editForm
            };
        if (!updateBtn.is(':visible')) {
            this.showEditComment(oArguments);
        } else {
            this.hideEditComment(oArguments);
        }         
    }

    showEditComment(oArgs) {
        let updateBtn = oArgs.updateBtn,
            commentBody = oArgs.commentBody,
            commentText = oArgs.commentText,
            editForm = oArgs.editForm,
            textArea = editForm.find("textarea");

        updateBtn.show();
        updateBtn.animate({
           opacity: 1
        }, { duration: 200, queue: false });
        commentBody.hide();
        editForm.show();
        textArea.show().text(commentText);
    }

    hideEditComment(oArgs) {
        let updateBtn = oArgs.updateBtn,
            commentBody = oArgs.commentBody,
            editForm = oArgs.editForm;

        updateBtn.animate({
           opacity: 0
        }, { duration: 200, queue: false, complete: () =>  updateBtn.hide()});
        updateBtn.css("opacity", "0");
        commentBody.show();
        editForm.hide();
        editForm.find("textarea").hide().text("");
    }


    reloadPage() {
        location.reload();
    }

    deleteVotedPost(oArgs) {
        return ajaxRequest("POST", "/api/VotedPosts/DeletePost", oArgs.postId)
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
                