// AllStudents.cs
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Notes.Models
{
    public class AllStudents
    {
        public ObservableCollection<Student> Students { get; set; }

        public AllStudents()
        {
            Students = LoadStudents() ?? new ObservableCollection<Student>();
        }

        private string GetFilePath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(folderPath, "students.json");
        }

        public ObservableCollection<Student> LoadStudents()
        {
            string filePath = GetFilePath();

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var loadedStudents = JsonSerializer.Deserialize<ObservableCollection<Student>>(json);

                    foreach (var student in loadedStudents)
                    {
                        if (student.AssociatedCourses == null)
                        {
                            student.AssociatedCourses = new ObservableCollection<Association>();
                        }
                    }

                    return loadedStudents;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex}");
            }

            return null;
        }

        public void SaveStudents()
        {
            string filePath = GetFilePath();
            string json = JsonSerializer.Serialize(Students);

            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture dans le fichier : {ex}");
            }
        }

        public void AssociateStudentAndCourse(Student student, string courseName)
        {
            var association = student.AssociatedCourses.FirstOrDefault(a => a.CourseName == courseName);

            if (association == null)
            {
                association = new Association { CourseName = courseName };
                student.AssociatedCourses.Add(association);
            }

            SaveStudents();
        }

        public void AddEvaluation(Student student, string courseName, string evaluation)
        {
            var association = student.AssociatedCourses.FirstOrDefault(a => a.CourseName == courseName);

            if (association != null)
            {
                association.Evaluations.Add(evaluation);
                SaveStudents();
            }
        }
    }
}
