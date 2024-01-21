// AllTeachersPage.xaml.cs
using Microsoft.Maui.Controls;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Views
{
    public partial class AllTeachersPage : ContentPage
    {
        private AllTeachers allTeachers;
        private AllActivities allActivities;

        // Ajoutez une propriété pour stocker temporairement l'enseignant sélectionné
        private Teacher selectedTeacher;

        public AllTeachersPage()
        {
            InitializeComponent();
            allTeachers = new AllTeachers();
            allActivities = new AllActivities();
            BindingContext = allTeachers;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Recharger les données des cours
            allActivities = new AllActivities();
        }
        private void Afficher_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Teacher teacher)
            {
                // Stockez temporairement l'enseignant sélectionné
                selectedTeacher = teacher;

                // Affichez les activités pour cet enseignant
                DisplayTeacherActivities();
            }
        }

        private async void DisplayTeacherActivities()
        {
            // Récupérez toutes les activités disponibles
            List<Activity> allActivitiesList = allActivities.Activities.ToList();

            // Affichez une liste des activités pour que l'utilisateur en sélectionne une
            string selectedActivity = await DisplayActionSheet("Sélectionnez une activité", "Annuler", null, allActivitiesList.Select(a => a.ActivityName).ToArray());

            if (!string.IsNullOrEmpty(selectedActivity))
            {
                // Associez l'enseignant sélectionné à l'activité choisie
                allTeachers.AssociateTeacherAndActivity(selectedTeacher, selectedActivity);

                // Mise à jour de l'interface utilisateur pour refléter l'association
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            // Mise à jour de l'interface utilisateur (vous devez définir cela en fonction de votre structure de données)
            // ...

            // Réinitialiser l'enseignant sélectionné
            selectedTeacher = null;

            // Rafraîchir l'affichage des enseignants (peut être optionnel, en fonction de vos besoins)
            allTeachers.LoadTeachers();
        }

        private async void Details_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Teacher teacher)
            {
                // Affichez les détails de l'association (vous devez définir cela en fonction de votre structure de données)
                string details = GetAssociationDetails(teacher);
                await DisplayAlert("Détails de l'association", details, "OK");
            }
        }

        private string GetAssociationDetails(Teacher teacher)
        {
            // Logique pour récupérer les détails de l'association (vous devez définir cela en fonction de votre structure de données)
            // Par exemple, si vous avez une propriété AssociatedActivity dans la classe Teacher, vous pouvez la retourner ici
            // ...

            // Ajout d'une logique factice : récupération de l'activité associée
            return $"Nom: {teacher.Nom}\nPrenom: {teacher.Prenom}\nActivité associée: {teacher.AssociatedActivity}";
        }

        private void AddTeacher_Clicked(object sender, EventArgs e)
        {
            // Naviguer vers la page TeacherPage
            Navigation.PushAsync(new TeacherPage());
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Teacher teacher)
            {
                // Supprimer l'enseignant de la liste
                allTeachers.Teachers.Remove(teacher);

                // Enregistrer les enseignants dans le fichier
                allTeachers.SaveTeachers();
            }
        }
    }
}
