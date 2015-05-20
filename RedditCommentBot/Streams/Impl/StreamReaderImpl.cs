using RedditCommentBot.Streams.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot.Streams
{
    public class StreamReaderImpl : IStreamReader, IDisposable
    {
        private StreamReader _stream;

        public StreamReaderImpl(string path)
        {
            _stream = new StreamReader(path);
        }

        public string ReadToEnd()
        {
            return _stream.ReadToEnd();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }   
    }
}
