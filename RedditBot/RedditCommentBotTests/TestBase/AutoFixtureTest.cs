using NUnit.Framework;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCommentBotTests.TestBase
{
    public class AutoFixtureTest
    {
        protected Fixture autoFixture;

        [SetUp]
        public void Setup()
        {
            autoFixture = new Fixture();
            
        }
    }
}
