using RedditCommentBot.RedditAPI;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot
{
    public class Gatekeeper
    {
        private AuthenticatedUser user;
        private  BotConfig config;
        private IRedditAPI reddit;
     
        public Gatekeeper(BotConfig config, IRedditAPI reddit)
        {
            this.reddit = reddit;
            this.config = config;
        }

        public bool IsUserLoggedIn() {
            if(user != null)
                return true;

            return CanUserLogIn();
        }

        private bool CanUserLogIn()
        {
            try
            {
                user = this.reddit.LogIn(this.config.UserName, this.config.Password);
                Console.WriteLine("User logged in");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
