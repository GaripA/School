using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Notes.Models
{
    public class AllActivities
    {
        public ObservableCollection<Activity> Activities { get; set; }

        public AllActivities()
        {
            Activities = LoadActivities() ?? new ObservableCollection<Activity>();
        }

        private string GetActivitiesFilePath()
        {
            // Crée et retourne le chemin du fichier pour les activités
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(folderPath, "activities.json");
        }

        private ObservableCollection<Activity> LoadActivities()
        {
            string filePath = GetActivitiesFilePath();

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<ObservableCollection<Activity>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex}");
            }

            return null;
        }

        public void SaveActivities()
        {
            string filePath = GetActivitiesFilePath();
            string json = JsonSerializer.Serialize(Activities);

            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture dans le fichier : {ex}");
            }
        }

        public void AddActivity(Activity activity)
        {
            Activities.Add(activity);
            SaveActivities();
        }

        public void RemoveActivity(Activity activity)
        {
            Activities.Remove(activity);
            SaveActivities();
        }
    }
}
