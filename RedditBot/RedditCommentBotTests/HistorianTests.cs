using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditCommentBot;
using NUnit.Framework;
using RedditCommentBotTests.TestBase;
using Moq;
using Ploeh.AutoFixture;
using RedditCommentBot.Streams;
using RedditCommentBotTests.Extensions;
using RedditCommentBot.Streams.Interfaces;

namespace RedditCommentBot.Tests
{
    [TestFixture()]
    public class HistorianTests : AutoFixtureTest
    {
        private Historian historian;
        private Mock<StreamFactory> streamFactory;
        private Mock<IStreamReader> readerMock;
        private Mock<IStreamWriter> writerMock;
        
        [SetUp]
        public void Setup()
        {
            streamFactory = autoFixture.RegisterMock<StreamFactory>();
            readerMock = new Mock<IStreamReader>();
            writerMock = new Mock<IStreamWriter>();
            streamFactory.Setup(m => m.GetReader(It.IsAny<string>())).Returns(readerMock.Object);
            streamFactory.Setup(m => m.GetWriter(It.IsAny<string>())).Returns(writerMock.Object);
            historian = autoFixture.Create<Historian>();
        }

        [Test()]
        public void HistorianTests_SetupPasses()
        {
            Assert.Pass();
        }

        [Test]
        public void WhenReadHistoryIsCalled_ReaderConstructedFromFactory()
        {
            historian.ReadHistory();
            streamFactory.Verify(f => f.GetReader(It.IsAny<string>()));
        }

        [Test]
        public void WhenReadHistoryIsCalled_ReadToEndCalledFromStringReader()
        {
            historian.ReadHistory();
            readerMock.Verify(m => m.ReadToEnd());
        }

        [Test]
        public void WhenReadHistoryIsCalled_ReplyHistoryIsReturned()
        {
            var result = historian.ReadHistory();
            Assert.That(result, Is.InstanceOf<ReplyHistory>());
        }

        [Test]
        public void WhenReadHistoryIsCalledWithOneEntry_ReplyHistoryContainsThatEntry()
        {
            string token = Given_ReadToEndReturnsOneToken();
            ReplyHistory history = historian.ReadHistory();
            bool stringFound = history.LogContains(token);
            Assert.That(stringFound, Is.True);
        }

        private string Given_ReadToEndReturnsOneToken()
        {
            string readToEnd = Guid.NewGuid().ToString();
            readerMock.Setup(m => m.ReadToEnd()).Returns(readToEnd);
            return readToEnd;
        }

        [Test]
        public void WhenReadHistoryIsCalledWithNoEntries_ReplyHistoryIsEmpty()
        {
            ReplyHistory history = historian.ReadHistory();
            Assert.That(history.ToList().Count, Is.EqualTo(0));
        }

        [Test]
        public void WhenReadHistoryIsCalledWithTokenizedEntries_ReplyHistoryContainsBothTokens()
        {
            List<string> tokens = new List<string>() {
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            };
            string fileOutput = string.Format("{0} {1}", tokens[0], tokens[1]);
        }
    }
}
