using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    class EnglishLanguage : ILanguage
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

        public EnglishLanguage()
        {
            MainTitle = "EasySave 2.0";
            Name = "Name";
            SourcePath = "Source Path";
            DestinationPath = "Destination Path";
            SaveType = "Save Type";
            CompleteType = "Complete";
            DifferencialType = "Differencial";
            Encryption = "Encryption";
            ID = "ID";
            French = "Français";
            English = "English";
            Create = "Create";
            Launch = "Launch";
            LaunchAll = "Launch all";
            Modify = "Modify";
            Delete = "Delete";
        }
    }
}
