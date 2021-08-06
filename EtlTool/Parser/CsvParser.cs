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
}

