namespace Synchronized.SharedLib
{
    public class Constants
    {
        public const string SIGNED_USER = "SignedUser";
        public const string VOTER = "Voter";
        public const string EDITOR = "Editor";
        public const string MODERATOR = "Moderator";

        public const int VOTER_MINIMUM_RANK = 15;
        public const int EDITOR_MINIMUM_RANK = 500;
        public const int MODERATOR_MINIMUM_RANK = 3000;

        public const int VOTE_UP_POINTS = 15;
        public const int MARK_FOR_REVIEW_POINTS = 15;
        public const int COMMENT_POINTS = 50;
        public const int VOTE_DOWN_POINTS = 125;
        public const int REVIEW_POINST = 500;
        public const int CREATE_TAGS_POINTS = 1250;
        public const int EDIT_POINST = 2000;
        public const int DELETE_POINST = 3000;

        public const int QUESTION_UPVOTE_ASKER_BONUS = 5;
        public const int ANSWER_UPVOTE_ANSWERER_BONUS = 10;
        public const int ANSWER_ACCEPT_ANSWERER_BONUS = 15;
        public const int ANSWER_ACCEPT_ACCEPTER_BONUS = 2;
        public const int QUESTION_DOWNVOTE_AKSER_PENALTY = -2;
        public const int QUESTION_DOWNVOTE_VOTER_PENALTY = -1;
        public const int ANSWER_DOWNVOTE_ANSWERER_PNEALTY = -2;
        public const int ANSWER_DOWNVOTE_VOTER_PENALTY = -1;
    }
}
