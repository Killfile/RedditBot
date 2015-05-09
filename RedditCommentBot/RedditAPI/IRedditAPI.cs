using Newtonsoft.Json.Linq;
using RedditSharp;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot.RedditAPI
{
    public interface IRedditAPI
    {
        Subreddit FrontPage { get; }
        WebAgent.RateLimitMode RateLimit { get; set; }
        Subreddit RSlashAll { get; }
        AuthenticatedUser User { get; set; }

        void ComposePrivateMessage(string subject, string body, string to, string captchaId = "", string captchaAnswer = "");
        Comment GetComment(string subreddit, string name, string linkName);
        Domain GetDomain(string domain);
        [Obsolete("Use User property instead")]
        AuthenticatedUser GetMe();
        Post GetPost(Uri uri);
        Subreddit GetSubreddit(string name);
        
        Thing GetThingByFullname(string fullname);
        JToken GetToken(Uri uri);
        RedditUser GetUser(string name);
        void InitOrUpdateUser();
        AuthenticatedUser LogIn(string username, string password, bool useSsl = true);
        AuthenticatedUser RegisterAccount(string userName, string passwd, string email = "");
        Listing<T> Search<T>(string query) where T : Thing;
        Listing<T> SearchByUrl<T>(string url) where T : Thing;
    }
}
