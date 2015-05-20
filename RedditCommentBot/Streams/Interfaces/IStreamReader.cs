using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBot.Streams.Interfaces
{
    public interface IStreamReader : IDisposable
    {
        string ReadToEnd();
    }
}
