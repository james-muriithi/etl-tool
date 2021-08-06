using System.Collections.Generic;
using System.IO;

namespace Parser
{
    /// <summary>
    /// A file reader reader interface to read all types of files
    /// </summary>
    public interface IFileParser
    {
        /// <summary>
        /// Implement this method and add functionality to read a particular type of file
        /// </summary>
        /// <param name="filePath">The location of the file to be read</param>
        /// <returns></returns>
        static string Read(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Implement this method and add functionality to parse the data
        /// </summary>
        /// <param name="data">Thw data to parse</param>
        /// <returns></returns>
        List<List<string>> Parse(string data);
    }
}
