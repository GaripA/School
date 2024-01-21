using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class AllCoursesPage : ContentPage
    {
        private AllCourses allCourses;
        private AllStudents allStudents;

        public AllCoursesPage()
        {
            InitializeComponent();
            allCourses = new AllCourses();
            allStudents = new AllStudents();
            BindingContext = allCourses;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load courses every time the page appears
            allCourses = new AllCourses();
            BindingContext = allCourses;
        }

        private async void AddCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursePage());
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Course course)
            {
                // Remove the course from the list
                allCourses.Courses.Remove(course);

                // Save courses to the file
                allCourses.SaveCourses();
            }
        }
    }
}
