using System.IO;

namespace EtlTool
{
    /// <summary>
    /// A file reader class
    /// </summary>
    public class FileReader
    {
        /// <summary>
        /// A method to read the file in the given filepath
        /// </summary>
        /// <param name="filePath">The location of the file to be read</param>
        /// <returns>returns the content of the file</returns>
        public static string Read(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
