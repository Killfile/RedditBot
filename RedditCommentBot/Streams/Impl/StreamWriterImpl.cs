using RedditCommentBot.Streams.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot.Streams
{
    public class StreamWriterImpl : IStreamWriter, IDisposable
    {
        private StreamWriter _stream;

        public StreamWriterImpl(string path)
        {
            _stream = new StreamWriter(path);
        }

        public void Write(string toWrite)
        {
            
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Write(string p, string commentId)
        {
            _stream.Write(p, commentId);
        }
    }
}
