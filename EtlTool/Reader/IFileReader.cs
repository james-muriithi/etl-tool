using System.Collections.Generic;

namespace Reader
{
    /// <summary>
    /// You can use XML comments to provide summaries to describe interfaces, classes, methods or properties.
    /// This is very useful and will server a documentation purpose of your code.
    /// </summary>
    public interface IFileReader
    {
        List<List<string>> Read(string filePath);
    }
}
