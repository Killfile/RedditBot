using RedditCommentBot.RedditAPI;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot
{
    public class SubredditDirectory
    {
        private const string SubRedditPrefix = "/r/";
        private readonly IRedditAPI reddit;

        public SubredditDirectory(IRedditAPI reddit)
        {
            this.reddit = reddit;
        }

        public Subreddit GetSubReddit(string targetSubreddit)
        {
            if (!targetSubreddit.StartsWith(SubRedditPrefix))
                targetSubreddit = SubRedditPrefix + targetSubreddit;

            var subreddit = reddit.GetSubreddit(targetSubreddit);
            return subreddit;
        }
    }
}
