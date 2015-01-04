using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paprotski.Lab3
{
    public static class IniIO
    {
        /// <summary>
        /// Reading data from section 
        /// </summary>
        /// <param name="reader">object type StreamReader</param>
        /// <param name="nameSection">section name</param>
        /// <param name="numberLine">the number of lines to be read</param>
        /// <returns>an array of strings that contains the data section</returns>
        public static string[,] ReadSection(StreamReader reader, string nameSection, int numberLine)
        {
            var sectionReader = SearchSection(reader, nameSection); 

            if (sectionReader.EndOfStream)
            {
                throw new ArgumentException("Isn't correct " + nameSection + " or end of stream!");
            }

            var resultSection = new string[numberLine, 2];

            for (var i = 0; i < numberLine; i++)
            {
                var readLine = sectionReader.ReadLine();

                if (readLine != null)
                {
                    var splitReadLine = readLine.Split('=');

                    if (splitReadLine.Length == 0)
                    {
                        continue;
                    }

                    resultSection[i, 0] = splitReadLine[0]; 
                    resultSection[i, 1] = splitReadLine[1];
                }
            }

            return resultSection; 
        }

        /// <summary>
        /// Search Section on named 
        /// </summary>
        /// <param name="reader">object type StreamReader</param>
        /// <param name="nameSection">section name</param>
        /// <returns>object type StreamReader or null</returns>
        private static StreamReader SearchSection(StreamReader reader, string nameSection)
        {
            while (!reader.EndOfStream)
            {
                var readLine = reader.ReadLine(); 

                if (!string.IsNullOrWhiteSpace(readLine) && readLine.Contains("[" + nameSection + "]"))
                {
                    return reader; 
                }
            }
            return null; 
        }
    }
}
