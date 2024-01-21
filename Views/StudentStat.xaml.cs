using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class StudentStat : ContentPage
    {
        public StudentStat()
        {
            InitializeComponent();
            Appearing += OnPageAppearing; // Ajoutez le gestionnaire d'événements à l'événement Appearing
        }

        private void OnPageAppearing(object sender, EventArgs e)
        {
            LoadStudentStatistics(); // Chargez les statistiques chaque fois que la page devient visible
        }

        private void LoadStudentStatistics()
        {
            AllStudents allStudents = new AllStudents();

            // Calculer le nombre d'étudiants par classe
            var classStatistics = allStudents.Students
                .GroupBy(s => s.Classe)
                .Select(group => new { Classe = group.Key, StudentCount = group.Count() })
                .ToList();

            // Créer un texte formaté pour afficher les statistiques dans le Label
            string statisticsText = string.Join("\n", classStatistics.Select(stat => $"Class {stat.Classe}: {stat.StudentCount} étudiants"));

            // Mettre à jour le texte du Label avec les statistiques
            ClassStatisticsLabel.Text = statisticsText;
        }
    }
}