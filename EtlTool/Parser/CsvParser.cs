using Cryptography;
using System.IO;
using System;
using System.Collections.Generic;

namespace Parser
{
    public class CsvParser : IFileParser
    {
        private string _delimeter = ",";

        /// <summary>
        /// A method to parse csv data
        /// </summary>
        /// <param name="data">The data to be parsed</param>
        /// <returns>A list of list strings</returns>
        public List<List<string>> Parse(string data)
        {
            // - parse data so you have rows with column names;
            var unsplittedRows = data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var splittedRows = new List<List<string>>();

            foreach (var unsplitttedRow in unsplittedRows)
            {
                var splittedRow = new List<string>();

                // What if value in some cell will contain the same symbol as delimiter?
                var columns = unsplitttedRow.Split(_delimeter);

                foreach (var column in columns)
                {
                    splittedRow.Add(column);
                }

                splittedRows.Add(splittedRow);
            }

            return splittedRows;
        }
    }

    /// <summary>
    /// Represents the implementation of <see cref="IFileParser"/> interface
    /// that is designed to parse data from TSV files.
    /// </summary>
    public class TsvParser : IFileParser
    {
        /// <summary>
        /// Parses given <see cref="data"/> and returns given a List of Lists of strings,
        /// that represent rows and columns from provided string.
        /// </summary>
        public List<List<string>> Parse(string data)
        {
            throw new NotImplementedException();
        }
    }
}

