using RedditCommentBot.Streams.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot.Streams
{
    public class StreamFactory
    {
        public virtual IStreamReader GetReader(string path)
        {
            return new StreamReaderImpl(path);
        }

        public virtual IStreamWriter GetWriter(string path)
        {
            return new StreamWriterImpl(path);
        }
    }
}
