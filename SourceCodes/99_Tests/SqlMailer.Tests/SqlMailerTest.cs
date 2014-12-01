using Aliencube.SqlMailer.Clr;
using Aliencube.SqlMailer.Clr.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace Aliencube.SqlMailer.Tests
{
    [TestFixture]
    public class SqlMailerTest
    {
        #region Setup

        private ISqlMailerConfig _config;
        private ISqlMailer _mailer;

        [SetUp]
        public void Init()
        {
            this._config = SqlMailerConfig.CreateInstance();
            this._mailer = new Aliencube.SqlMailer.Clr.SqlMailer(this._config);
        }

        [TearDown]
        public void Dispose()
        {
            if (this._mailer != null)
            {
                this._mailer.Dispose();
            }

            if (this._config != null)
            {
                this._config.Dispose();
            }
        }

        #endregion Setup

        #region Tests

        [Test]
        [TestCase("Application Name", "<strong>body</strong>", true)]
        [TestCase("Application Name", "", false)]
        public void SendEmails_GivenConditions_ReturnsResult(string applicationName, string body, bool expected)
        {
            // **IMPORTANT**
            // Make sure that any dummy SMTP testing tool like Papercut (http://papercut.codeplex.com)
            // MUST be running before this test.
            var sent = this._mailer.Send(applicationName, body);
            sent.Should().Be(expected);
        }

        #endregion Tests
    }
}