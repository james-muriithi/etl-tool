using Cryptography;
using System.IO;
using System;
using System.Collections.Generic;

namespace Reader
{
    public class CsvReader : IFileReader
    {
        private readonly IDecoder _decoder;
        private string _delimeter = ",";

        public CsvReader(IDecoder decoder)
        {
            this._decoder = decoder;
        }

        public List<List<string>> Read(string filePath)
        {
            var encodedText = File.ReadAllText(filePath);
            var decodedText = _decoder.Decode(encodedText);

            // - parse data so you have rows with column names;

            var unsplittedRows = decodedText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var splittedRows = new List<List<string>>();

            foreach(var unsplitttedRow in unsplittedRows)
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
