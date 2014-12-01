using Aliencube.SqlMailer.Clr;
using Aliencube.SqlMailer.Clr.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace SqlMailer.Tests
{
    [TestFixture]
    public class SqlMailerConfigTest
    {
        #region Setup

        private ISqlMailerConfig _config;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Dispose()
        {
            if (this._config != null)
            {
                this._config.Dispose();
            }
        }

        #endregion Setup

        #region Tests

        [Test]
        [TestCase("SqlMailer.config")]
        public void GetDeserialisedObject_GivenConfig_ReturnDeserialisedObject(string filename)
        {
            this._config = SqlMailerConfig.CreateInstance(filename);
            this._config.Should().NotBeNull();

            var smtp = this._config.Smtp;
            smtp.Should().NotBeNull();
            smtp.EnableSsl.Value.Should().Be(false);
            smtp.Host.Should().Be("localhost");
            smtp.Port.Value.Should().Be(25);
            smtp.DefaultCredentials.Value.Should().Be(true);

            this._config.Applications.Count.Should().BeGreaterOrEqualTo(1);
            var application = this._config.Applications.FirstOrDefault();
            application.Should().NotBeNull();
            application.IsBodyHtml.Value.Should().Be(true);
        }

        #endregion Tests
    }
}