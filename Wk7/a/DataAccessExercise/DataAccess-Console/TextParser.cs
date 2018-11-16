// Written by Brian Bird, 5/15/12

using System;
using System.Collections.Generic;
using System.IO;

namespace DataAccess_Console
{
    /// <summary>
    /// Contains methods for parsing csd text files
    /// </summary>
    public class TextParser
    {
        string delimiter;
        int numFields;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAndParser.TextParser"/> class.
        /// </summary>
        /// <param name="columnDelimiter">String that separates columns in the text being parsed. Examples: ",", "\t", " ".</param>
        /// <param name="numberOfFields">Number of columns in the text being parsed.</param>
        public TextParser(string columnDelimiter, int numberOfFields)
        {
            delimiter = columnDelimiter;
            numFields = numberOfFields;
        }

        public List<string[]> ParseText(Stream stream)
        {
            List<string[]> rows = new List<string[]>();

            string[] delim = new string[1];
            delim[0] = delimiter;

            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    rows.Add(reader.ReadLine().Split(delim, numFields, StringSplitOptions.None));
                }
            }

            return rows;
        }
    }
}