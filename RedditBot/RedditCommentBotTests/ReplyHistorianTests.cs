using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditCommentBot;
using NUnit.Framework;
using RedditCommentBotTests.TestBase;
using RedditCommentBot.RedditAPI;
using RedditCommentBotTests.Extensions;
using Moq;
using Ploeh.AutoFixture;

namespace RedditCommentBot.Tests
{
    [TestFixture()]
    public class ReplyHistorianTests : AutoFixtureTest 
    {
        private Mock<IRedditAPI> mockAPI;
        private Mock<ReplyHistorian> mockHistorian;
        private ReplyHistorian historian
        {
            get
            {
                return mockHistorian.Object;
            }
        }

        private List<string> historianIDs;

        [SetUp]
        public void Setup()
        {
            historianIDs = autoFixture.Create<List<string>>();
            mockAPI = autoFixture.RegisterMock<IRedditAPI>();
            mockHistorian = new Mock<ReplyHistorian>(null);
            mockHistorian.CallBase = true;
        }

        [Test()]
        public void WhenInitIsCalled_IdInitializedFromFile()
        {
            mockHistorian.Setup(m => m.ReadIds()).Returns(historianIDs);
            historian.Init();
        }

        [Test]
        public void GatekeeperTests_SetupCompleted()
        {
            Assert.Pass();
        }

    }
}
