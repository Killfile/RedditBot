using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot
{
    public class ReplyHistory : IEnumerable<string>
    {
        private List<String> commentIDs;

        public ReplyHistory()
        {
            commentIDs = new List<string>();
        }

        public void AddToLog(string commetID) 
        {
            commentIDs.Add(commetID);
        }

        public bool LogContains(string commentID)
        {
            return commentIDs.Contains(commentID);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return commentIDs.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return commentIDs.GetEnumerator();
        }
    }
}
