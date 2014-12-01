using System.Collections.Generic;
using System.Net.Mail;

namespace Aliencube.SqlMailer.Clr
{
    /// <summary>
    /// This represents the extention entity for <c>MailAddressCollection</c>.
    /// </summary>
    public static class MailAddressCollectionExtension
    {
        /// <summary>
        /// Adds list of <c>MailAddress</c> objects.
        /// </summary>
        /// <param name="value"><c>MailAddressCollection</c> instance.</param>
        /// <param name="addresses">List of <c>MailAddress</c> objects.</param>
        public static void AddRange(this MailAddressCollection value, IEnumerable<MailAddress> addresses)
        {
            foreach (var address in addresses)
            {
                value.Add(address);
            }
        }
    }
}