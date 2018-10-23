using System.IO;
using System.Text.RegularExpressions;

namespace FileParserLib
{
    public class StringCounter : FileParser
    {
        private StringCounter(string path, string stringForSearch) : base(path, stringForSearch)
        {
        }

        public static StringCounter Initialize(string path, string stringForSearch)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Invalid path to the file!");
            }

            return new StringCounter(path, stringForSearch);
        }

        public override int Parse()
        {
            try
            {
                int stringMatches = 0;
                using (StreamReader reader = new StreamReader(Path))
                {
                    string sourceLine;
                    while ((sourceLine = reader.ReadLine()) != null)
                    {
                        stringMatches += new Regex(StringForSearch).Matches(sourceLine).Count;
                    }
                }

                return stringMatches;
            }
            catch (IOException)
            {
                throw new IOException("Can`t count strings in file!");
            }
        }
    }
}