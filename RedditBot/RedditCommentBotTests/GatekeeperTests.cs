using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditCommentBot;
using NUnit.Framework;
using RedditCommentBotTests.TestBase;
using RedditCommentBotTests.Extensions;
using RedditCommentBot.RedditAPI;
using Moq;
using Ploeh.AutoFixture;

namespace RedditCommentBot.Tests
{
    [TestFixture()]
    public class GatekeeperTests : AutoFixtureTest 
    {
        private Mock<IRedditAPI> mockAPI;
        private Gatekeeper gatekeeper; 

        [SetUp]
        public void Setup()
        {
            mockAPI = autoFixture.RegisterMock<IRedditAPI>();
            gatekeeper = autoFixture.Create<Gatekeeper>();
        }

        [Test]
        public void GatekeeperTests_SetupCompleted()
        {
            Assert.Pass();
        }

        [Test]
        public void WhenIsUserLoggedInCalledForTheFirstTime_LoginIsCalledOnAPI() {
            gatekeeper.IsUserLoggedIn();
            mockAPI.Verify(m=>m.LogIn(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));
        }

        [Test()]
        public void WhenIsUserLoggedInCalledTwice_LoginFiresOnAPIOnce()
        {
            Given_LoginReturnsAuthenticatedUser();
            gatekeeper.IsUserLoggedIn();
            gatekeeper.IsUserLoggedIn();
            mockAPI.Verify(m => m.LogIn(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        private void Given_LoginReturnsAuthenticatedUser()
        {
            mockAPI.Setup(m => m.LogIn(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new RedditSharp.Things.AuthenticatedUser());
        }
    }
}
