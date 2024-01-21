// ActivityPage.xaml.cs
using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class ActivityPage : ContentPage
    {
        private AllActivities allActivities;

        public ActivityPage()
        {
            InitializeComponent();
            allActivities = new AllActivities();
            BindingContext = allActivities;
        }

        private async void Enregistrer_Clicked(object sender, EventArgs e)
        {
            // Récupérer le nom de l'activité depuis le champ de saisie
            string activityName = txtActivityName.Text;

            // Validation des données
            if (string.IsNullOrWhiteSpace(activityName))
            {
                await DisplayAlert("Erreur", "Veuillez entrer le nom de l'activité.", "OK");
                return;
            }

            // Créer un objet Activity avec le nom de l'activité saisie
            var newActivity = new Activity
            {
                ActivityName = activityName
            };

            // Ajouter l'activité à la liste
            allActivities.Activities.Add(newActivity);

            // Réinitialiser le champ de saisie si nécessaire
            txtActivityName.Text = string.Empty;

            // Enregistrer les activités dans le fichier
            allActivities.SaveActivities();

            // Revenir à la page précédente (AllActivitiesPage)
            await Navigation.PopAsync();
        }
    }
}