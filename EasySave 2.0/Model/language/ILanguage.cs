using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    /// <summary>
    /// Language Interface
    /// </summary>
    interface ILanguage
    {
        public string MainTitle { get; }
        public string Name { get; }
        public string SourcePath { get; }
        public string DestinationPath { get; }
        public string SaveType { get; }
        public string CompleteType { get; }
        public string DifferencialType { get; }
        public string Encryption { get; }
        public string ID { get; }
        public string French { get; }
        public string English { get; }
        public string Create { get; }
        public string Launch { get; }
        public string LaunchAll { get; }
        public string Modify { get; }
        public string Delete { get; }
    }
}
