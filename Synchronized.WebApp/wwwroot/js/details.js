class Details {
    _answers = {};
    _comments = {};
    _allComments = [];    

    animatingComments = false;
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
                for (let comment of answer.comments) {
                    this.comments[comment.id] = comment;
                };
                // $.merge(this.allComments, answer.comments);
                this.votedPosts[answer.id] = answer;
            }            

            $("#synched-question").on("click", event => {
                event.stopPropagation();
                let postId = $("#synched-question").attr("data-id");
                this.handleUserAction({
                    target: event.target,
                    postId: postId,
                    votedPostPublisherId: this.votedPosts[postId].publisherId,
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
                        votedPostPublisherId: this.votedPosts[target.dataset.id].publisherId,
                        eventTarget: this.answerEvent
                    });
                }
            });
            
            // $.merge(this.allComments, question.comments);
            for (let comment of question.comments) {
                this.comments[comment.id] = comment;
            };
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

    get comments() {
        return this._comments;
    }

    set answers(val) {
        this._answers = val;
    }

    set allComments(val) {
        this._allComments = val;
    }

    set comments(val) {
        this._comments = val;
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

    _submitComment(event) {        
        let commentForm = $(event.target.parentElement),
            commentBody = commentForm.find("textarea").val(),
            containingPost = commentForm.parent().parent(),
            containingPostId = containingPost[0].dataset.id;

        ajaxRequest("POST", "/api/VotedPosts/CommentOnPost", {
            votedPostId: containingPostId,
            body: commentBody
        }).then(() => {
            this.reloadPage()
        }).catch(() => alert("User not alllowed to comment!"));
        /*
        let keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13' && event.ctrlKey) {
            let commentForm = $(`#${this.id} .synched-comment-form`);
            let commentBody = commentForm.find("textarea").val();
            ajaxRequest("POST", "/api/VotedPosts/CommentOnPost", { votedPostId: this.id, body: commentBody })
                .then(this.reloadPage)
                .catch(function () { alert("User not alllowed to comment!") });
        }
        */
    }

    submitComment = this._submitComment.bind(this);

    updateComment(oArgs) {
        let target = $(oArgs.target),
            comment = target.parent(),
            commentBody = comment.find(".comment-body"),
            commentText = commentBody.text();
        ajaxRequest("POST", "/api/VotedPosts/CommentOnPost", { commentId: oArgs.commentId, body: commentText })
            .then(this.reloadPage)
            .catch(function () { alert("User not alllowed to comment!") });
    }

    deleteComment(oArgs) {
        let comment = this.comments[oArgs.commentId];
        comment.votedPostPublisherId = oArgs.votedPostPublisherId;
        return ajaxRequest("POST", "/api/VotedPosts/DeleteComment", this.comments[oArgs.commentId])
            .then(() => {
                comment = oArgs.target.parentElement;
                comment.parentElement.removeChild(comment);
            })
            .catch(ex => { alert("Failure!") });
    }

    newLine(e) {
        if (e.keyCode === 13 && e.ctrlKey) {
            $(this).val(function (i, val) {
                return val + "\n";
            });
        }
    }

    showComment(oArgs) {
        if (this.animatingComments) {
            return;
        }
        this.animatingComments = true;
        let elem = $(oArgs.target).find("+ .synched-comment-form"),
            submitBtn = elem.find(".synched-submit-comment");
        if (!elem.is(':visible')) {
            elem.show()
                .animate({
                    height: "120px",
                    width: "100%"
                }, 500, () => {
                        submitBtn.on("click", this.submitComment);
                        this.showBtn(submitBtn);
                        this.animatingComments = false;
                    });
        } else {
            elem.animate({
                height: "0",
                width: "0"
            }, 500, () => {
                    elem.hide();
                    submitBtn.off("click", this.submitComment);
                    this.hideBtn(submitBtn);
                    this.animatingComments = false;
                });
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

        this.showBtn(updateBtn);
        commentBody.hide();
        editForm.show();
        textArea.show().text(commentText);
    }

    hideEditComment(oArgs) {
        let updateBtn = oArgs.updateBtn,
            commentBody = oArgs.commentBody,
            editForm = oArgs.editForm;

        this.hideBtn(updateBtn);
        updateBtn.css("opacity", "0");
        commentBody.show();
        editForm.hide();
        editForm.find("textarea").hide().text("");
    }

    showBtn(btn) {
        btn.show();
        btn.animate({
           opacity: 1
        }, { duration: 200, queue: false });
    }

    hideBtn(btn) {
        btn.animate({
           opacity: 0
        }, { duration: 200, queue: false, complete: () =>  btn.hide()});
    }

    reloadPage() {
        location.reload();
    }

    deleteVotedPost(oArgs) {
        return ajaxRequest("POST", "/api/VotedPosts/DeletePost", oArgs.postId)
            .then(() => { alert("Your vote has been accapted!") })
            .catch(() => { alert("User can't delete this post!") });
    }
}

const details = new Details();
                