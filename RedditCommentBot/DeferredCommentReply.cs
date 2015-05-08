namespace RedditCommentBot
{
    using RedditSharp.Things;

    public class DeferredCommentReply
    {
        private readonly Comment parentComment;
        private readonly string reply;

        public DeferredCommentReply(Comment parentComment, string reply)
        {
            this.parentComment = parentComment;
            this.reply = reply;
        }

        public string ParentCommentID
        {
            get
            {
                return parentComment.Id;
            }
        }

        public void Post()
        {
            this.parentComment.Reply(this.reply);
            
        }
    }
}