using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    class FrenchLanguage : ILanguage
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

        public FrenchLanguage()
        {
            MainTitle = "SauvegardeFacile 2.0";
            Name = "Nom";
            SourcePath = "Chemin Source";
            DestinationPath = "Chemin de Destination";
            SaveType = "Type de Sauvegarde";
            CompleteType = "Complète";
            DifferencialType = "Différentielle";
            Encryption = "Encryptage";
            ID = "ID";
            French = "Français";
            English = "English";
            Create = "Créer";
            Launch = "Lancer";
            LaunchAll = "Lancer tout";
            Modify = "Modifier";
            Delete = "Supprimer";
        }
    }
}
