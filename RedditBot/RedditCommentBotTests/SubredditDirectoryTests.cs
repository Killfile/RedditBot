using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditCommentBot;
using NUnit.Framework;
using RedditCommentBotTests.TestBase;
using RedditCommentBot.RedditAPI;
using Moq;
using Ploeh.AutoFixture;
using RedditCommentBotTests.Extensions;

namespace RedditCommentBot.Tests
{
    [TestFixture()]
    public class SubredditDirectoryTests : AutoFixtureTest 
    {
        private Mock<IRedditAPI> mockAPI;
        private SubredditDirectory directory;
        private string subredditRoot;
        private string prefixedSubredditName
        {
            get
            {
                return string.Format("/r/{0}", subredditRoot);
            }
        }

        [SetUp]
        public void Setup()
        {
            mockAPI = autoFixture.RegisterMock<IRedditAPI>();
            directory = autoFixture.Create<SubredditDirectory>();
            subredditRoot = autoFixture.Create<string>();

        }

        [Test()]
        public void SubredditDirectoryTests_CanSetupTestEnvironment()
        {
            Assert.Pass();
        }

        [Test()]
        public void WhenGetSubRedditCalled_GetSubredditCalledOnAPI()
        {
            directory.GetSubReddit(prefixedSubredditName);
            mockAPI.Verify(m => m.GetSubreddit(It.Is<string>(s=>s==prefixedSubredditName)));
        }

        [Test]
        public void WhenGetSubRedditCalled_UnprefixedArgumentsArePrefixed()
        {
            directory.GetSubReddit(subredditRoot);
            mockAPI.Verify(m => m.GetSubreddit(It.Is<string>(s => s == prefixedSubredditName)));
        }

        [Test]
        public void WhenGetSubRedditCalled_PrefixedArgumentsAreNotDoublePrefixed()
        {
            directory.GetSubReddit(prefixedSubredditName);
            mockAPI.Verify(m => m.GetSubreddit(It.Is<string>(s => !s.StartsWith("/r//r/"))));
        }
    }
}
