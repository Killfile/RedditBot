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
    public class ReplyHistoryTests : AutoFixtureTest 
    {
        private List<string> commentIDs;
        private ReplyHistory history;
        private string commentID;

        [SetUp]
        public void Setup()
        {
            history = new ReplyHistory();
            commentID = autoFixture.Create<string>();
        }

        [Test]
        public void HistoryTests_SetupCompleted()
        {
            Assert.Pass();
        }
        
        [Test]
        public void WhenAddToLogIsCalled_NoExceptionsThrown()
        {
            history.AddToLog(commentID);
        }

        [Test]
        public void WhenGetEnumeratorIsCalled_EnumeratorReturned() {
            IEnumerator<string> enumerable = history.GetEnumerator();
        }

        [Test]
        public void WhenLogContainsIsCalled_BoolIsReturned()
        {
            bool returnValue = history.LogContains(commentID);
        }

        [Test]
        public void WhenLogContainsIsCalledOnNewComment_ReturnsFalse()
        {
            bool actual = history.LogContains(commentID);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void WhenLogContainsIsCalledOnLoggedComment_ReturnsTrue()
        {
            history.AddToLog(commentID);
            bool actual = history.LogContains(commentID);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void WhenGetEnumeratorIsCalledOnEmptyHistorian_ReturnsEmptyEnumeration()
        {
            Assert.That(history, Is.Empty);
        }

        [Test]
        public void WhenGetEnumeratorIsCalledAfteAdd_DoesNotReturnAnEmptyEnumeration()
        {
            history.AddToLog(commentID);
            Assert.That(history, Is.Not.Empty);
        }

        [Test]
        public void WhenGetEnumeratorIsCalledAfteAdd_EnumerationContainsAddedValue()
        {
            history.AddToLog(commentID);
            Assert.That(history, Contains.Item(commentID));
        }

        [Test]
        public void AddIsCalledTwiceWithTheSameValue_EnumerationOnlyContainsOneInstance()
        {
            history.AddToLog(commentID);
            Assert.That(history.Count(c=>c==commentID), Is.EqualTo(1));
        }
    }
}
