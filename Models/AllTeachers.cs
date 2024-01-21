using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Notes.Models
{
    internal class AllTeachers
    {
        public ObservableCollection<Teacher> Teachers { get; set; } = new ObservableCollection<Teacher>();

        public AllTeachers()
        {
            LoadTeachers();
        }

        private string GetTeachersFilePath()
        {
            // Crée et retourne le chemin du fichier pour les enseignants
            string appDataPath = FileSystem.AppDataDirectory;
            return Path.Combine(appDataPath, "teacher.txt");
        }

        public void LoadTeachers()
        {
            Teachers.Clear();

            // Get the file path for teachers
            string teacherFilePath = GetTeachersFilePath();

            // Check if the file exists
            if (File.Exists(teacherFilePath))
            {
                // Read the JSON content from the file and deserialize it into a collection of teachers
                string jsonContent = File.ReadAllText(teacherFilePath);
                var teachers = JsonSerializer.Deserialize<ObservableCollection<Teacher>>(jsonContent);

                // Add each teacher into the ObservableCollection
                foreach (Teacher teacher in teachers)
                    Teachers.Add(teacher);
            }
        }

        public void SaveTeachers()
        {
            // Get the file path for teachers
            string teacherFilePath = GetTeachersFilePath();

            // Serialize the ObservableCollection of teachers to JSON
            string jsonContent = JsonSerializer.Serialize(Teachers);

            // Write the JSON content to the file
            File.WriteAllText(teacherFilePath, jsonContent);
        }

        public void AssociateTeacherAndActivity(Teacher teacher, string activityName)
        {
            // Créez une nouvelle instance de Teacher avec les mêmes propriétés que l'enseignant passé en paramètre
            var updatedTeacher = new Teacher
            {
                TeacherId = teacher.TeacherId,
                Nom = teacher.Nom,
                Prenom = teacher.Prenom,
                AssociatedActivity = activityName
            };

            // Mettez à jour l'enseignant dans la collection
            int index = Teachers.IndexOf(teacher);
            Teachers[index] = updatedTeacher;

            // Enregistrez les changements
            SaveTeachers();
        }
    }
}

