using Moq;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBotTests.Extensions
{
    public static class AutoFixtureExtensions
    {
        public static Mock<T> RegisterMock<T>(this Fixture fixture) where T : class
        {
            var mock = new Mock<T>();
            fixture.Register<T>(()=>mock.Object);
            return mock;
        }
    }
}
