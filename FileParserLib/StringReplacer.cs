using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace FileParserLib
{
    public class StringReplacer : FileParser
    {
        private StringReplacer(string path, string stringForSearch, string stringForReplace)
            : base(path, stringForSearch)
        {
            this.StringForReplace = stringForReplace;
        }

        public static StringReplacer Initialize(string path, string stringForSearch, string stringForReplace)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Invalid path to the file!");
            }

            return new StringReplacer(path, stringForSearch, stringForReplace);
        }

        public string StringForReplace
        {
            get;
            set;
        }

        public override int Parse()
        {
            string tempPath = System.IO.Path.GetTempFileName();
            try
            {
                int numberOfReplace = 0;
                FillTempFile(tempPath, out numberOfReplace);
                UpdateOriginalFile(tempPath);                               
                File.Delete(tempPath);
                return numberOfReplace;
            }
            catch (IOException)
            {
                throw new IOException("Can`t replace strings in file!");
            }
            finally
            {
                File.Delete(tempPath);
            }
        }

        private void FillTempFile(string tempPath, out int numberOfReplace)
        {
            numberOfReplace = 0;
            using (StreamWriter writer = new StreamWriter(tempPath, false, Encoding.Default))
            {
                using (StreamReader reader = new StreamReader(Path, Encoding.Default))
                {
                    string sourceLine;
                    while ((sourceLine = reader.ReadLine()) != null)
                    {
                        numberOfReplace += new Regex(StringForSearch).Matches(sourceLine).Count;
                        sourceLine = sourceLine.Replace(StringForSearch, StringForReplace);
                        writer.WriteLine(sourceLine);
                    }
                }
            }
        }

        private void UpdateOriginalFile(string tempPath)
        {
            using (StreamWriter writer = new StreamWriter(Path, false, Encoding.Default))
            {
                using (StreamReader reader = new StreamReader(tempPath, Encoding.Default))
                {
                    string sourceLine;
                    while ((sourceLine = reader.ReadLine()) != null)
                    {
                        writer.WriteLine(sourceLine);
                    }
                }
            }
        }
    }
}