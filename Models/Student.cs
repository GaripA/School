using System.Collections.ObjectModel;

namespace Notes.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Classe { get; set; }
        public ObservableCollection<Association> AssociatedCourses { get; set; } = new ObservableCollection<Association>();
    }

    public class Association
    {
        public string CourseName { get; set; }
        public ObservableCollection<string> Evaluations { get; set; } = new ObservableCollection<string>();
    }
}