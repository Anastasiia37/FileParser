// <copyright file="FileParser.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

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