class PostViewModel {
    constructor(model) {
        this.model = model;
    }

    updatePost(newPost) {
        this.model.posts[newPost.id] = newPost;
    }

    updatePostVotes(newVote) {
        if (newVote) {
            this.model.posts[newVote.postId].votes.push(newVote);
        }
    }
}