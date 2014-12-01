using System;

namespace Aliencube.SqlMailer.Clr.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>SqlMailer</c> class.
    /// </summary>
    public interface ISqlMailer : IDisposable
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="applicationName">Application name.</param>
        /// <param name="body">Email body.</param>
        /// <returns>Returns <c>True</c>, if successfully sent; otherwise returns <c>False</c>.</returns>
        bool Send(string applicationName, string body);
    }
}