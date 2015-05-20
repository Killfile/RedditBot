using RedditCommentBot.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot
{
    public class Historian : IHistorian
    {
        private BotConfig config;
        private StreamFactory streamFactory;

        public Historian(BotConfig config, StreamFactory streamFactory)
        {
            this.config = config;
            this.streamFactory = streamFactory;
        }

        private string CommentIdFilePath
        {
            get
            {
                return Path.Combine(this.config.TempPath, "processedComments.txt");
            }
        }

        public ReplyHistory ReadHistory()
        {
            ReplyHistory history = new ReplyHistory();
            string commentIDsString = ReadStringFromFile();
            if (commentIDsString == null)
                return history;

            var commentList = commentIDsString.Split(' ').ToList();

            foreach(var comment in commentList)
                history.AddToLog(comment);

            return history;
        }

        private string ReadStringFromFile()
        {
            string commentIDsString;
 	        using (var file = streamFactory.GetReader(this.CommentIdFilePath))
            {
                commentIDsString = file.ReadToEnd();
            }
            return commentIDsString;
        }

        public void WriteHistory(ReplyHistory history)
        {
 	        using (var file = streamFactory.GetWriter(this.CommentIdFilePath))
                {
                    foreach (string commentId in history)
                    {
                        file.Write(" {0}", commentId);
                    }
                }
        }
    }

    public interface IHistorian {
        ReplyHistory ReadHistory();
        void WriteHistory(ReplyHistory history);
    }
}
