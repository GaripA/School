using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Notes.Models
{
    public class AllCourses
    {
        public ObservableCollection<Course> Courses { get; set; }

        public AllCourses()
        {
            Courses = LoadCourses() ?? new ObservableCollection<Course>();
        }

        private string GetCoursesFilePath()
        {
            // Crée et retourne le chemin du fichier pour les cours
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(folderPath, "courses.json");
        }

        private ObservableCollection<Course> LoadCourses()
        {
            string filePath = GetCoursesFilePath();

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<ObservableCollection<Course>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex}");
            }

            return null;
        }

        public void SaveCourses()
        {
            string filePath = GetCoursesFilePath();
            string json = JsonSerializer.Serialize(Courses);

            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture dans le fichier : {ex}");
            }
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
            SaveCourses();
        }

        public void RemoveCourse(Course course)
        {
            Courses.Remove(course);
            SaveCourses();
        }
    }
}
