using System;
using System.IO;
using System.Text;

namespace Aliencube.SqlMailer.Clr
{
    /// <summary>
    /// This represents the <c>ExeptionHandler</c> entity to handle exceptions.
    /// </summary>
    public static class ExceptionHandler
    {
        /// <summary>
        /// Saves the <c>Exception</c> details to the log files.
        /// </summary>
        /// <param name="ex"><c>Exception</c> instance.</param>
        public static void SaveException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            CreateLogDirectory();

            var sb = new StringBuilder();
            sb.AppendFormat("========== {0:yyyy-MM-dd hh:mm:ss}\n", DateTime.Now);
            sb.Append(GetErrorMessages(ex));
            sb.AppendFormat("---------- {0:yyyy-MM-dd hh:mm:ss}\n", DateTime.Now);

            File.AppendAllText(Path.Combine(Constants.APPLICATION_PATH, String.Format(@"logs\error-{0:yyyyMMdd}.log", DateTime.Now)), sb.ToString());
        }

        /// <summary>
        /// Creates the log directory, if not exist.
        /// </summary>
        private static void CreateLogDirectory()
        {
            if (!Directory.Exists(Path.Combine(Constants.APPLICATION_PATH, "logs")))
            {
                Directory.CreateDirectory(Path.Combine(Constants.APPLICATION_PATH, "logs"));
            }
        }

        /// <summary>
        /// Gets the error messages recursively.
        /// </summary>
        /// <param name="ex"><c>Exception</c> instance.</param>
        /// <returns>Returns the error messages recursively.</returns>
        private static string GetErrorMessages(Exception ex)
        {
            if (ex == null)
            {
                return null;
            }

            var sb = new StringBuilder();
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);

            if (ex.InnerException == null)
            {
                return sb.ToString();
            }

            sb.AppendLine("..........");
            sb.Append(GetErrorMessages(ex.InnerException));

            return sb.ToString();
        }
    }
}