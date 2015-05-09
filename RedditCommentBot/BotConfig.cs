using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot
{
    public class BotConfig
    {
        private readonly string username;
        private readonly string password;
        private readonly string tempPath;

        public BotConfig(string username, string password, string tempPath)
        {
            this.username = username;
            this.password = password;
            this.tempPath = tempPath;
        }

        public string UserName { get { return username; } }
        public string Password { get { return password; } }
        public string TempPath { get { return tempPath; } }
    }
}
