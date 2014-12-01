using System.IO;
using System.Reflection;

namespace Aliencube.SqlMailer.Clr
{
    /// <summary>
    /// This represents the <c>Constancts</c> entity that holds all constant values commonly used.
    /// </summary>
    public static class Constants
    {
        // This MUST be hard-coded as CLR assemblies are converted and stored into SQL Server as a binary format,
        // so there is no way to get the application running path from it.
        // Therefore, the SqlMailer.config must be located under the location specified below.
#if DEBUG
        public static readonly string APPLICATION_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
#else
        public const string APPLICATION_PATH = @"C:\Temp\Aliencube.SqlMailer.Clr";
#endif
    }
}