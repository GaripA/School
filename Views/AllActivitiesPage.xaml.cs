using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class AllActivitiesPage : ContentPage
    {
        private AllActivities allActivities;
        private AllTeachers allTeachers;

        public AllActivitiesPage()
        {
            InitializeComponent();
            allActivities = new AllActivities();
            allTeachers = new AllTeachers();
            BindingContext = allActivities;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load activities every time the page appears
            allActivities = new AllActivities();
            BindingContext = allActivities;
        }

        private async void AddActivity_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ActivityPage());
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Activity activity)
            {
                // Remove the activity from the list
                allActivities.Activities.Remove(activity);

                // Save activities to the file
                allActivities.SaveActivities();
            }
        }
    }
}