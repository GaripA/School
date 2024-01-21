// StudentPage.xaml.cs
using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class StudentPage : ContentPage
    {
        private AllStudents allStudents;

        public StudentPage()
        {
            InitializeComponent();
            allStudents = new AllStudents();
            BindingContext = allStudents;
        }

        private async void Enregistrer_Clicked(object sender, EventArgs e)
        {
            try{// Retrieve name, surname, and class from input fields
            string nom = txtNom.Text;
            string prenom = txtPrenom.Text;
            string classe = txtClasse.Text;

            // Data validation
            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(prenom) || string.IsNullOrWhiteSpace(classe))
            {
                await DisplayAlert("Erreur", "Veuillez entrer le nom, le prénom et la classe de l'étudiant.", "OK");
                return;
            }

            // Create a Student object with the entered data
            var newStudent = new Student
            {
                Nom = nom,
                Prenom = prenom,
                Classe = classe
            };

            // Add the student to the list
            allStudents.Students.Add(newStudent);

            // You can also reset the input fields if necessary
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtClasse.Text = string.Empty;

            // Save students to the file
            allStudents.SaveStudents();

            // Navigate to the AllStudentsPage
            await Shell.Current.GoToAsync(nameof(AllStudentsPage));}
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Une erreur s'est produite : {ex.Message}", "OK");
            }
        }
    }
}