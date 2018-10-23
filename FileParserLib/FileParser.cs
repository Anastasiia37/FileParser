namespace FileParserLib
{
    public abstract class FileParser
    {
        public string Path
        {
            get;
        }

        public string StringForSearch
        {
            get;
            private set;
        }

        protected FileParser(string path, string stringForSearch)
        {
            this.Path = path;
            this.StringForSearch = stringForSearch;
        }

        public abstract int Parse();
    }
}