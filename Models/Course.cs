namespace Notes.Models
{
    public class Course
    {
        public int CourseId { get; set; } // Ajout de cette propriété
        public string CourseName { get; set; }
        public int ResponsibleStudentId { get; set; }
    }
}