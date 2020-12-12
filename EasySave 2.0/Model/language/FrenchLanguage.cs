using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    class FrenchLanguage : ILanguage
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

        public FrenchLanguage()
        {
            MainTitle = "EasySave 3.0";
            Name = "Nom";
            SourcePath = "Source";
            DestinationPath = "Destination";
            SaveType = "Type de Sauvegarde";
            CompleteType = "Complète";
            DifferencialType = "Différentielle";
            Encryption = "Chiffrement";
            ID = "ID";
            French = "Français";
            English = "English";
            Create = "Créer";
            Launch = "Lancer";
            LaunchAll = "Lancer tout";
            Modify = "Modifier";
            Delete = "Supprimer";
            Settings = "Options";
            GlobalSettings = "Options Globales";
            PriorityFileExtensions = "Priorité des extensions";
            MaximumFileSize = "Taille maximale des fichiers durant le transfert simultané :";
            MaximumFileSizeValue = "Taille maximale :";
            SaveStatusTitle = "État de la sauvegarde";
            CurrentSave = "Sauvegarde en cours :";
            SaveStatus = "Statut :";
            RunningStatus = "En cours...";
            DoneStatus = "Terminée !";
            SaveProgress = "Progression :";
            ResumeSave = "Reprendre";
            PauseSave = "Pause";
            CancelSave = "Annuler";
        }
    }
}
