using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot
{
    public class ReplyHistorian
    {
        private List<String> commentIDs;
        private BotConfig config;

        public ReplyHistorian(BotConfig config)
        {
            this.config= config;
            
        }

        public void Init()
        {
            commentIDs = ReadIds();
        }

        private string CommentIdFilePath
        {
            get
            {
                return Path.Combine(this.config.TempPath, "processedComments.txt");
            }
        }

        public void AddToLog(string commetID) 
        {
            commentIDs.Add(commetID);
        }

        public List<string> ReadIds()
        {
            using (var file = new StreamReader(this.CommentIdFilePath))
            {
                string commentIDsString = file.ReadToEnd();
                return commentIDsString.Split(' ').ToList();
            }
            
        }

        public virtual void WriteIds()
        {
            using (var file = new StreamWriter(this.CommentIdFilePath))
            {
                foreach (string commentId in commentIDs)
                {
                    file.Write(" {0}", commentId);
                }
            }
        }

        public bool LogContains(string commentID)
        {
            return commentIDs.Contains(commentID);
        }
    }
}
