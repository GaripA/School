namespace Notes.Models
{
    internal class Teacher
    {
        public int TeacherId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        // Ajoutez la propriété AssociatedActivity
        public string AssociatedActivity { get; set; }
    }
}