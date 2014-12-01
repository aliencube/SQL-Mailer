using Aliencube.SqlMailer.Clr.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Aliencube.SqlMailer.Clr
{
    /// <summary>
    /// This represents the <c>SqlMailerConfig</c> entity converted from SqlMailer.config.
    /// </summary>
    [Serializable]
    [XmlRoot("configuration")]
    public class SqlMailerConfig : ISqlMailerConfig
    {
        private bool _disposed;

        /// <summary>
        /// Gets or sets the <c>Smtp</c> object.
        /// </summary>
        [XmlElement(ElementName = "smtp", Type = typeof(Smtp), IsNullable = false)]
        public Smtp Smtp { get; set; }

        /// <summary>
        /// Gets or sets the list of <c>Application</c> object.
        /// </summary>
        [XmlArray(ElementName = "applications", IsNullable = false)]
        [XmlArrayItem(ElementName = "application", Type = typeof(Application), IsNullable = false)]
        public List<Application> Applications { get; set; }

        /// <summary>
        /// Creates the <c>SqlMailerConfig</c> instance from SqlMailer.config.
        /// </summary>
        /// <param name="filename">Configuration file name. Default value is "SqlMailer.config"</param>
        /// <returns>Returns the <c>SqlMailerConfig</c> instance.</returns>
        public static ISqlMailerConfig CreateInstance(string filename = null)
        {
            if (String.IsNullOrWhiteSpace(filename))
            {
                filename = "SqlMailer.config";
            }

            ISqlMailerConfig config;
            using (var stream = new FileStream(Path.Combine(Constants.APPLICATION_PATH, filename), FileMode.Open))
            {
                var serialiser = new XmlSerializer(typeof(SqlMailerConfig));
                config = serialiser.Deserialize(stream) as ISqlMailerConfig;
            }
            return config;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }

    /// <summary>
    /// This represents the <c>Smtp</c> entity from SqlMailer.config.
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "smtp")]
    public class Smtp
    {
        private bool? _enableSsl;

        /// <summary>
        /// Gets or sets the value that specifies whether to enable SSL connection or not.
        /// </summary>
        [XmlElement(ElementName = "enableSsl", DataType = "boolean", IsNullable = true)]
        public bool? EnableSsl
        {
            get
            {
                if (!this._enableSsl.HasValue)
                {
                    this._enableSsl = false;
                }
                return this._enableSsl;
            }
            set { this._enableSsl = value; }
        }

        private string _host;

        /// <summary>
        /// Gets or sets the host name of the SMTP server.
        /// </summary>
        [XmlElement(ElementName = "host", DataType = "string", IsNullable = true)]
        public string Host
        {
            get
            {
                if (String.IsNullOrWhiteSpace(this._host))
                {
                    this._host = "localhost";
                }
                return this._host;
            }
            set { this._host = value; }
        }

        private int? _port;

        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        [XmlElement(ElementName = "port", DataType = "int", IsNullable = true)]
        public int? Port
        {
            get
            {
                if (!this._port.HasValue)
                {
                    this._port = 25;
                }
                return this._port;
            }
            set { this._port = value; }
        }

        private bool? _defaultCredentials;

        /// <summary>
        /// Gets or sets the value that specifies whether to use default credentials or not.
        /// </summary>
        [XmlElement(ElementName = "defaultCredentials", DataType = "boolean", IsNullable = true)]
        public bool? DefaultCredentials
        {
            get
            {
                if (!this._defaultCredentials.HasValue)
                {
                    this._defaultCredentials = true;
                }
                return this._defaultCredentials;
            }
            set { this._defaultCredentials = value; }
        }
    }

    /// <summary>
    /// This represents the <c>Application</c> entity from SqlMailer.config.
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "application")]
    public class Application
    {
        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        [XmlElement(ElementName = "name", DataType = "string", IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sender's email address.
        /// </summary>
        [XmlElement(ElementName = "sender", DataType = "string", IsNullable = false)]
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the list of recipient email addresses.
        /// </summary>
        [XmlArray(ElementName = "recipients", IsNullable = false)]
        [XmlArrayItem(ElementName = "recipient", DataType = "string", IsNullable = false)]
        public List<string> Recipients { get; set; }

        /// <summary>
        /// Gets or sets the list of carbon-copied recipient email addresses.
        /// </summary>
        [XmlArray(ElementName = "carbonCopies", IsNullable = true)]
        [XmlArrayItem(ElementName = "carbonCopy", DataType = "string", IsNullable = true)]
        public List<string> CarbonCopies { get; set; }

        /// <summary>
        /// Gets or sets the list of blind carbon-copied email addresses.
        /// </summary>
        [XmlArray(ElementName = "blindCarbonCopies", IsNullable = true)]
        [XmlArrayItem(ElementName = "blindCarbonCopy", DataType = "string", IsNullable = true)]
        public List<string> BlindCarbonCopies { get; set; }

        /// <summary>
        /// Gets or sets the subject of the email.
        /// </summary>
        [XmlElement(ElementName = "subject", DataType = "string", IsNullable = false)]
        public string Subject { get; set; }

        private bool? _isBodyHtml;

        /// <summary>
        /// Gets or sets the value that specifies whether to send the email in HTML format or not.
        /// </summary>
        [XmlElement(ElementName = "isBodyHtml", DataType = "boolean", IsNullable = true)]
        public bool? IsBodyHtml
        {
            get
            {
                if (!this._isBodyHtml.HasValue)
                {
                    this._isBodyHtml = true;
                }
                return this._isBodyHtml;
            }
            set { this._isBodyHtml = value; }
        }
    }
}