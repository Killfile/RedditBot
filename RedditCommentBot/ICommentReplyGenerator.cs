using System.Collections.Generic;

namespace RedditCommentBot
{
    using RedditSharp.Things;

    public interface ICommentReplyGenerator
    {
        IEnumerable<DeferredCommentReply> ReplyToComments(IEnumerable<Comment> comments);
    }
}
