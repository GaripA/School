using Microsoft.Maui.Controls;
using Notes.Models;
using System;
using System.Collections.Generic;


namespace Notes.Views
{
    public partial class AllStudentsPage : ContentPage
    {
        private AllStudents allStudents;
        private AllCourses allCurses;
        private Student selectedStudent;
        private string selectedCourse;

        public AllStudentsPage()
        {
            InitializeComponent();
            allStudents = new AllStudents();
            allCurses = new AllCourses();
            BindingContext = allStudents;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Recharger les données des cours
            allCurses = new AllCourses();
        }


        private async void DisplayStudentAssociatedCoursesForEvaluation()
        {
            List<string> associatedCourses = selectedStudent.AssociatedCourses.Select(a => a.CourseName).ToList();

            selectedCourse = await DisplayActionSheet("Sélectionnez un cours", "Annuler", null, associatedCourses.ToArray());

            if (!string.IsNullOrEmpty(selectedCourse))
            {
                NavigateToAddEvalPage();
            }
        }

        private void AddEvaluation_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                selectedStudent = student;
                DisplayStudentAssociatedCoursesForEvaluation();
            }
        }

        private void NavigateToAddEvalPage()
        {
            if (selectedStudent != null && !string.IsNullOrEmpty(selectedCourse))
            {
                Navigation.PushAsync(new AddEvalPage(allStudents, selectedStudent, selectedCourse));
            }
        }
        private void UpdateUI()
        {
            // Rafraîchir l'affichage des étudiants
            allStudents.LoadStudents();

            // Mise à jour de la liaison des données de la collection
            studentsCollection.ItemsSource = null;
            studentsCollection.ItemsSource = allStudents.Students;

            selectedStudent = null;
        }
        private async void Details_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                string details = GetMultipleAssociationDetails(student);
                await DisplayAlert("Détails de l'association", details, "OK");
            }
        }

        private string GetMultipleAssociationDetails(Student student)
        {
            string details = $"Nom: {student.Nom}\nPrenom: {student.Prenom}\nClasse: {student.Classe}\n";

            foreach (var association in student.AssociatedCourses)
            {
                details += $"\nCours: {association.CourseName}\nÉvaluations: {string.Join(", ", association.Evaluations)}";

                if (association.Evaluations.Any())
                {
                    // Calculate and display the average for the specific course
                    double averageForCourse = association.Evaluations.Select(double.Parse).Average();
                    details += $"\nMoyenne pour le cours: {averageForCourse:F2}\n";
                }
            }

            if (student.AssociatedCourses.Any())
            {
                // Calculate and display the overall average for the student
                double overallAverage = student.AssociatedCourses
                    .SelectMany(a => a.Evaluations.Select(double.Parse))
                    .DefaultIfEmpty(0.0)
                    .Average();

                details += $"\nMoyenne générale: {overallAverage:F2}\n";
            }

            return details;
        }


        private async void DisplayStudentCourses()
        {
            List<Course> allCursesList = allCurses.Courses.ToList();

            string selectedCourse = await DisplayActionSheet("Sélectionnez un cours", "Annuler", null, allCursesList.Select(c => c.CourseName).ToArray());

            if (!string.IsNullOrEmpty(selectedCourse))
            {
                allStudents.AssociateStudentAndCourse(selectedStudent, selectedCourse);
                UpdateUI();
            }
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                allStudents.Students.Remove(student);

                
                allStudents.SaveStudents();
            }
        }
        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            // Naviguer vers la page StudentPage
            Navigation.PushAsync(new StudentPage());
        }
        private void AfficherCours_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                selectedStudent = student;
                DisplayStudentCourses();
            }
        }
       
    }
    
    }
