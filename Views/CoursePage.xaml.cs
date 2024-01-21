// CoursePage.xaml.cs
using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class CoursePage : ContentPage
    {
        private AllCourses allCourses;

        public CoursePage()
        {
            InitializeComponent();
            allCourses = new AllCourses();
            BindingContext = allCourses;
        }

        private async void Enregistrer_Clicked(object sender, EventArgs e)
        {
            // Récupérer le nom du cours depuis le champ de saisie
            string courseName = txtCourseName.Text;

            // Validation des données
            if (string.IsNullOrWhiteSpace(courseName))
            {
                await DisplayAlert("Erreur", "Veuillez entrer le nom du cours.", "OK");
                return;
            }

            // Créer un objet Course avec le nom du cours saisi
            var newCourse = new Course
            {
                CourseName = courseName
            };

            // Ajouter le cours à la liste
            allCourses.Courses.Add(newCourse);

            // Réinitialiser le champ de saisie si nécessaire
            txtCourseName.Text = string.Empty;

            // Enregistrer les cours dans le fichier
            allCourses.SaveCourses();

            // Revenir à la page précédente (AllActivitiesPage)
            await Navigation.PopAsync();
        }
    }
}
