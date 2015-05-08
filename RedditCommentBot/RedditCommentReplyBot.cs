namespace RedditCommentBot
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using RedditSharp;
    using RedditSharp.Things;

    public class RedditCommentReplyBot
    {
        private readonly Reddit reddit = new Reddit();
        private readonly List<string> commentIds = new List<string>();
        private readonly string botTempPath;
        private readonly string username;
        private readonly string password;

        private readonly ICommentReplyGenerator replyGenerator;

        public RedditCommentReplyBot(string username, string password, string botTempPath, ICommentReplyGenerator replyGenerator)
        {
            this.username = username;
            this.password = password;
            this.botTempPath = botTempPath;
            this.replyGenerator = replyGenerator;
        }

        private string CommentIdFilePath
        {
            get
            {
                return Path.Combine(this.botTempPath, "processedComments.txt");
            }
        }

        public void ListenForPrompt(string triggerPhrase)
        {
            if (!HasLoggedIn()) return;

            var subreddit = reddit.GetSubreddit("/r/learnprogramming");
            foreach (var post in subreddit.New.Take(25))
            {
                Console.WriteLine("THREAD : {0}", post.Title);
                try
                {
                    IEnumerable<Comment> toProcess = post.Comments.Where(c => !commentIds.Contains(c.Id) && c.Body.Contains(triggerPhrase));
                    var enumeratedList = toProcess as IList<Comment> ?? toProcess.ToList();
                    var toPost = replyGenerator.ReplyToComments(enumeratedList);

                    foreach (var reply in toPost)
                    {
                        reply.Post();
                        AddIdToList(reply.ParentCommentID);
                        WriteIdsToFile();
                        Thread.Sleep(5000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("exception: {0}", ex.Message);
                }
            }
        }

        private bool HasLoggedIn()
        {
            try
            {
                this.reddit.LogIn(this.username, this.password);
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