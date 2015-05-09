using RedditSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot.RedditAPI
{
    public class RedditAPIImpl : IRedditAPI
    {
        private Reddit reddit;

        public RedditAPIImpl() {
            reddit = new Reddit();
        }

        public RedditAPIImpl(string accessToken) {
            reddit = new Reddit(accessToken);
        }

        public RedditAPIImpl(WebAgent.RateLimitMode limitMode) {
            reddit = new Reddit(limitMode);
        }

        public RedditAPIImpl(string username, string password, bool useSsl = true)
        {
            reddit = new Reddit(username, password, useSsl);
        }


        public RedditSharp.Things.Subreddit FrontPage
        {
            get { return reddit.FrontPage; }
        }

        public RedditSharp.WebAgent.RateLimitMode RateLimit
        {
            get
            {
                return reddit.RateLimit;
            }
            set
            {
                reddit.RateLimit = value;
            }
        }

        public RedditSharp.Things.Subreddit RSlashAll
        {
            get { return reddit.RSlashAll; }
        }

        public RedditSharp.Things.AuthenticatedUser User
        {
            get
            {
                return reddit.User;
            }
            set
            {
                reddit.User = value;
            }
        }

        public void ComposePrivateMessage(string subject, string body, string to, string captchaId = "", string captchaAnswer = "")
        {
            reddit.ComposePrivateMessage(subject, body, to, captchaId, captchaAnswer);
        }

        public RedditSharp.Things.Comment GetComment(string subreddit, string name, string linkName)
        {
            return reddit.GetComment(subreddit, name, linkName);
        }

        public RedditSharp.Domain GetDomain(string domain)
        {
            return reddit.GetDomain(domain);
        }

        public RedditSharp.Things.AuthenticatedUser GetMe()
        {
            return reddit.User;
        }

        public RedditSharp.Things.Post GetPost(Uri uri)
        {
            return reddit.GetPost(uri);
        }

        public RedditSharp.Things.Subreddit GetSubreddit(string name)
        {
            return reddit.GetSubreddit(name);
        }

        public RedditSharp.Things.Thing GetThingByFullname(string fullname)
        {
            return reddit.GetThingByFullname(fullname);
        }

        public Newtonsoft.Json.Linq.JToken GetToken(Uri uri)
        {
            return GetToken(uri);
        }

        public RedditSharp.Things.RedditUser GetUser(string name)
        {
            return GetUser(name);
        }

        public void InitOrUpdateUser()
        {
            reddit.InitOrUpdateUser();
        }

        public RedditSharp.Things.AuthenticatedUser LogIn(string username, string password, bool useSsl = true)
        {
            return reddit.LogIn(username, password, useSsl);
        }

        public RedditSharp.Things.AuthenticatedUser RegisterAccount(string userName, string passwd, string email = "")
        {
            return reddit.RegisterAccount(userName, passwd, email);
        }

        public RedditSharp.Listing<T> Search<T>(string query) where T : RedditSharp.Things.Thing
        {
            return reddit.Search<T>(query);
        }

        public RedditSharp.Listing<T> SearchByUrl<T>(string url) where T : RedditSharp.Things.Thing
        {
            return reddit.SearchByUrl<T>(url);
        }
    }
}
