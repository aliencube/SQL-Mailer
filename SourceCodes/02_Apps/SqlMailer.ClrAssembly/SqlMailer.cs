using Aliencube.SqlMailer.Clr.Interfaces;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Aliencube.SqlMailer.Clr
{
    /// <summary>
    /// This represents the entity consumed within SQL Server as CLR to handle mailing services.
    /// </summary>
    public class SqlMailer : ISqlMailer
    {
        private readonly ISqlMailerConfig _config;

        private bool _disposed;

        /// <summary>
        /// Creates a new instance of the <c>SqlMailer</c> class.
        /// </summary>
        /// <param name="config"><c>SqlMailerConfig</c> instance.</param>
        public SqlMailer(ISqlMailerConfig config = null)
        {
            if (config == null)
            {
                config = SqlMailerConfig.CreateInstance();
            }

            this._config = config;
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="applicationName">Application name.</param>
        /// <param name="body">Email body.</param>
        [SqlProcedure]
        public static void SendMail(SqlString applicationName, SqlString body)
        {
            using (var mail = new SqlMailer())
            {
                mail.Send(applicationName.Value, body.Value);
            }
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="applicationName">Application name.</param>
        /// <param name="body">Email body.</param>
        /// <returns>Returns <c>True</c>, if successfully sent; otherwise returns <c>False</c>.</returns>
        public bool Send(string applicationName, string body)
        {
            bool sent;
            try
            {
                if (String.IsNullOrWhiteSpace(applicationName))
                {
                    throw new ArgumentNullException("applicationName");
                }

                if (String.IsNullOrWhiteSpace(body))
                {
                    throw new ArgumentNullException("body");
                }

                using (var smtp = GetSmtpClient(this._config))
                using (var message = new MailMessage())
                {
                    var application = this._config
                                          .Applications
                                          .SingleOrDefault(p => String.Equals(p.Name, applicationName, StringComparison.CurrentCultureIgnoreCase));
                    if (application == null)
                    {
                        throw new InvalidOperationException("Application Settings Not Found");
                    }

                    var from = new MailAddress(application.Sender);
                    var tos = application.Recipients
                                         .Select(p => new MailAddress(p));

                    message.From = from;
                    message.To.AddRange(tos);

                    if (application.CarbonCopies != null && application.CarbonCopies.Any())
                    {
                        var ccs = application.CarbonCopies
                                             .Select(p => new MailAddress(p));
                        message.CC.AddRange(ccs);
                    }

                    if (application.BlindCarbonCopies != null && application.BlindCarbonCopies.Any())
                    {
                        var bccs = application.BlindCarbonCopies
                                              .Select(p => new MailAddress(p));
                        message.Bcc.AddRange(bccs);
                    }

                    message.Subject = application.Subject;
                    message.Body = body;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = application.IsBodyHtml.Value;

                    smtp.Send(message);
                    sent = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.SaveException(ex);
                sent = false;
            }
            return sent;
        }

        /// <summary>
        /// Gets the <c>SmtpClient</c> instance with configuration settings.
        /// </summary>
        /// <param name="config"><c>SqlMailerConfig</c> instance.</param>
        /// <returns>Returns the <c>SmtpClient</c> instance.</returns>
        private static SmtpClient GetSmtpClient(ISqlMailerConfig config)
        {
            var smtp = new SmtpClient()
                       {
                           Host = config.Smtp.Host,
                           Port = config.Smtp.Port.Value,
                           EnableSsl = config.Smtp.EnableSsl.Value,
                           UseDefaultCredentials = config.Smtp.DefaultCredentials.Value
                       };
            return smtp;
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
}