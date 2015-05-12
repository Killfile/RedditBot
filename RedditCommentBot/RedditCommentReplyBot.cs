namespace RedditCommentBot
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using RedditSharp;
    using RedditSharp.Things;
    using RedditCommentBot.RedditAPI;

    public class RedditCommentReplyBot
    {
       
        private readonly IRedditAPI reddit;
        private List<string> commentIds = new List<string>();
        private Gatekeeper gatekeeper;
        private BotConfig config;
        private SubredditDirectory directory;
        private readonly ICommentReplyGenerator replyGenerator;
        private readonly ReplyHistorian historian;

        public RedditCommentReplyBot(BotConfig config, Gatekeeper gatekeeper, SubredditDirectory directory, ReplyHistorian historian, ICommentReplyGenerator replyGenerator, IRedditAPI reddit)
        {
            this.config = config;
            this.replyGenerator = replyGenerator;
            this.reddit = reddit;
            this.gatekeeper = gatekeeper;
            this.directory = directory;
            this.historian = historian;
            historian.Init();
        }

      

        public void ListenForPrompt(string triggerPhrase, string targetSubreddit)
        {
            

            if (!gatekeeper.IsUserLoggedIn()) return;

            var subreddit = directory.GetSubReddit(targetSubreddit);

            List<DeferredCommentReply> replies = TryGenerateRepliesOnNewestPosts(triggerPhrase, subreddit);

            ProcessReplies(replies);
        }

        

        private void ProcessReplies(List<DeferredCommentReply> toPost)
        {
            foreach (var reply in toPost)
            {
                reply.Post();
                AddIdToList(reply.ParentCommentID);
                historian.WriteIds();
                Thread.Sleep(5000);
            }
        }

        private List<DeferredCommentReply> TryGenerateRepliesOnNewestPosts(string triggerPhrase, Subreddit subreddit)
        {
            List<DeferredCommentReply> toPost = new List<DeferredCommentReply>();
            foreach (var post in subreddit.New.Take(25))
            {
                Console.WriteLine("THREAD : {0}", post.Title);
                try
                {
                    var enumeratedList = GetCommentsForReply(triggerPhrase, post);
                    var replies = replyGenerator.ReplyToComments(enumeratedList); 
                    toPost.AddRange(replies);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("exception generating replies: {0}", ex.Message);
                }
            }
            return toPost;
        }


        private IList<Comment> GetCommentsForReply(string triggerPhrase, Post post)
        {
            IEnumerable<Comment> toProcess = post.Comments.Where(c => !historian.LogContains(c.Id) && c.Body.Contains(triggerPhrase));
            var enumeratedList = toProcess as IList<Comment> ?? toProcess.ToList();
            return enumeratedList;
        }

        

        

        private void AddIdToList(string commentId)
        {
            if (!commentIds.Contains(commentId))
            {
                commentIds.Add(commentId);
            }
        }

        
    }
}