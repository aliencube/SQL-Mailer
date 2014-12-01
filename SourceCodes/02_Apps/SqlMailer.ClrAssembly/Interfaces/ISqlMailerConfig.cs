using System;
using System.Collections.Generic;

namespace Aliencube.SqlMailer.Clr.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>SqlMailerConfig</c> class.
    /// </summary>
    public interface ISqlMailerConfig : IDisposable
    {
        /// <summary>
        /// Gets or sets the <c>Smtp</c> object.
        /// </summary>
        Smtp Smtp { get; set; }

        /// <summary>
        /// Gets or sets the list of <c>Application</c> object.
        /// </summary>
        List<Application> Applications { get; set; }
    }
}