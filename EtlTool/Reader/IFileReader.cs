using System.Collections.Generic;

namespace Reader
{
    /// <summary>
    /// A file reader reader interface to read all types of files
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Implement this method and add functionality to read a particular type of file
        /// </summary>
        /// <param name="filePath">The location of the file to be read</param>
        /// <returns></returns>
        List<List<string>> Read(string filePath);
    }
}
