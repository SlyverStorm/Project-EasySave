using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    class EnglishLanguage : ILanguage
    {
        #region Main Window
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
        public string Settings { get; }
        #endregion

        #region Settings
        public string GlobalSettings { get; }
        public string PriorityFileExtensions { get; }
        public string MaximumFileSize { get; }
        public string MaximumFileSizeValue { get; }
        #endregion

        #region Save Status
        public string SaveStatusTitle { get; }
        public string CurrentSave { get; }
        public string SaveStatus { get; }
        public string RunningStatus { get; }
        public string DoneStatus { get; }
        public string SaveProgress { get; }
        public string ResumeSave { get; }
        public string PauseSave { get; }
        public string CancelSave { get; }
        #endregion

        public EnglishLanguage()
        {
            MainTitle = "EasySave 3.0";
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
            Settings = "Settings";
            GlobalSettings = "Global Settings";
            PriorityFileExtensions = "File extensions priority";
            MaximumFileSize = "Maximum file size for simultaneous transfert :";
            MaximumFileSizeValue = "Maximum Size :";
            SaveStatusTitle = "Save Status";
            CurrentSave = "Save in progress :";
            SaveStatus = "Status :";
            RunningStatus = "Running...";
            DoneStatus = "Done !";
            SaveProgress = "Progress";
            ResumeSave = "Resume";
            PauseSave = "Pause";
            CancelSave = "Cancel";
        }
    }
}
