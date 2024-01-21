// TeacherPage.xaml.cs
using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class TeacherPage : ContentPage
    {
        private AllTeachers allTeachers;

        public TeacherPage()
        {
            InitializeComponent();
            allTeachers = new AllTeachers();
            BindingContext = allTeachers;
        }

        private async void Enregistrer_Clicked(object sender, EventArgs e)
        {
            // Récupérer le nom et le prénom depuis les champs de saisie
            string nom = txtNom.Text;
            string prenom = txtPrenom.Text;

            // Validation des données
            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(prenom))
            {
                await DisplayAlert("Erreur", "Veuillez entrer le nom et le prénom de l'enseignant.", "OK");
                return;
            }

            // Créer un objet Teacher avec les données saisies
            var newTeacher = new Teacher
            {
                Nom = nom,
                Prenom = prenom
            };

            // Ajouter l'enseignant à la liste
            allTeachers.Teachers.Add(newTeacher);

            // Vous pouvez également réinitialiser les champs de saisie si nécessaire
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;

            // Enregistrer les enseignants dans le fichier
            allTeachers.SaveTeachers();

            // Naviguer vers la page AllTeachersPage
            await Shell.Current.GoToAsync(nameof(AllTeachersPage));
        }
    }
}