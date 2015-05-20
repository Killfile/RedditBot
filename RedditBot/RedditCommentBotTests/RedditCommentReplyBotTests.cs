using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditCommentBot;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Moq;
using RedditCommentBot.RedditAPI;
using RedditCommentBotTests.Extensions;
using RedditCommentBotTests.TestBase;

namespace RedditCommentBot.Tests
{
    [TestFixture()]
    public class RedditCommentReplyBotTests : AutoFixtureTest 
    {
        private RedditCommentReplyBot replyBot;
        private Mock<IRedditAPI> mockAPI;

        [SetUp]
        public void Setup()
        {
            mockAPI = autoFixture.RegisterMock<IRedditAPI>();
            autoFixture.Register<ICommentReplyGenerator>(()=> new Mock<ICommentReplyGenerator>().Object);
            replyBot = autoFixture.Create<RedditCommentReplyBot>();

        }

        [Test]
        public void RedditCommentReplyBot_CanSetUpTestEnvironment()
        {
            Assert.Pass();
        }

        

        
    }
}
