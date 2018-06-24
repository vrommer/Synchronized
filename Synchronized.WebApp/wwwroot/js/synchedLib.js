class PostViewModel {
    constructor(model) {
        this.question = model;
        this.answers = model.answers;
        this.comments = model.comments;
        this.posts = {};

        this.posts[model.id] = this.question;
        for (var index in model.answers) {
            this.posts[this.answers[index].id] = this.answers[index];
            for (var i in this.answers[index].comments) {
                this.posts[this.answers[index].comments[i].id] = this.answers[index].comments[i];
            }
        };
        for (var index in model.comments) {
            this.posts[model.comments[index].id] = this.comments[index];
        };
    }

    updatePostVotes(newPost) {
        this.posts[newPost.id].points = newPost.points;
    }
}