class PostViewModel {
    constructor(model) {
        this.model = model;
        this.question = model.question;
        this.answers = model.answers;
        this.comments = model.comments;
        this.posts = {};

        this.posts[model.question.id] = this.question;
        for (var index in model.answers) {
            this.posts[index] = this.answers[index];
        };
        for (var index in model.comments) {
            this.posts[index] = this.comments[index];
        };
    }

    //updatePostVotes(newPost) {
    //    this.posts[newPost.id] = newPost;
    //}

    updatePostVotes(newPost) {
        this.posts[newPost.id].upVotes = newPost.upVotes;
        this.posts[newPost.id].downVotes = newPost.downVotes;
        this.posts[newPost.id].sumVotes = newPost.upVotes - newPost.downVotes;
        this.posts[newPost.id].voterIds = newPost.voterIds;
    }
}