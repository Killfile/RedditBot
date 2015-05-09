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
        private const string SubRedditPrefix = "/r/";
        private readonly IRedditAPI reddit;
        private readonly List<string> commentIds = new List<string>();

        private BotConfig config;
        
        private readonly ICommentReplyGenerator replyGenerator;

        public RedditCommentReplyBot(BotConfig config, ICommentReplyGenerator replyGenerator, IRedditAPI reddit)
        {
            this.config = config;
            this.replyGenerator = replyGenerator;
            this.reddit = reddit;
        }

        private string CommentIdFilePath
        {
            get
            {
                return Path.Combine(this.config.TempPath, "processedComments.txt");
            }
        }

        public void ListenForPrompt(string triggerPhrase, string targetSubreddit)
        {
            if (!HasLoggedIn()) return;

            var subreddit = GetSubReddit(targetSubreddit);

            List<DeferredCommentReply> replies = TryGenerateRepliesOnNewestPosts(triggerPhrase, subreddit);

            ProcessReplies(replies);
        }

        private void ProcessReplies(List<DeferredCommentReply> toPost)
        {
            foreach (var reply in toPost)
            {
                reply.Post();
                AddIdToList(reply.ParentCommentID);
                WriteIdsToFile();
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
                    toPost.AddRange(GenerateRepliesToPostComments(triggerPhrase, post));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("exception generating replies: {0}", ex.Message);
                }
            }
            return toPost;
        }

        private IEnumerable<DeferredCommentReply> GenerateRepliesToPostComments(string triggerPhrase, Post post)
        {
                IEnumerable<Comment> toProcess = post.Comments.Where(c => !commentIds.Contains(c.Id) && c.Body.Contains(triggerPhrase));
                var enumeratedList = toProcess as IList<Comment> ?? toProcess.ToList();
                return replyGenerator.ReplyToComments(enumeratedList);   
        }

        private Subreddit GetSubReddit(string targetSubreddit)
        {
            if (!targetSubreddit.StartsWith(SubRedditPrefix))
                targetSubreddit = SubRedditPrefix + targetSubreddit;

            var subreddit = reddit.GetSubreddit(targetSubreddit);
            return subreddit;
        }

        private bool HasLoggedIn()
        {
            try
            {
                this.reddit.LogIn(this.config.UserName, this.config.Password);
                Console.WriteLine("User logged in");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void AddIdToList(string commentId)
        {
            if (!commentIds.Contains(commentId))
            {
                commentIds.Add(commentId);
            }
        }

        private void WriteIdsToFile()
        {
            using (var file = new StreamWriter(this.CommentIdFilePath))
            {
                foreach (string commentId in commentIds)
                {
                    file.Write(" {0}", commentId);
                }
            }
        }
    }
}