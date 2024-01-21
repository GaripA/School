using Microsoft.Maui.Controls;
using Notes.Models;
using System;
using System.Collections.Generic;

namespace Notes.Views
{
    public partial class AddEvalPage : ContentPage
    {
        private AllStudents allStudents;
        private Student selectedStudent;
        private string selectedCurse;

        public AddEvalPage(AllStudents allStudents, Student selectedStudent, string selectedCurse)
        {
            InitializeComponent();
            this.allStudents = allStudents;
            this.selectedStudent = selectedStudent;
            this.selectedCurse = selectedCurse;
        }

        private void Enregistrer_Clicked(object sender, EventArgs e)
        {
            string evaluationInput = evaluationEntry.Text;

            // Try to convert to numeric value, treat as letter grade if unsuccessful
            if (TryConvertToNumeric(evaluationInput, out double evaluationNumeric))
            {
                // Successfully converted to numeric value
                AddStudentEvaluation(selectedStudent, selectedCurse, evaluationNumeric);
            }
            else
            {
                // Unable to convert to numeric value, treat it as a letter grade
                AddStudentEvaluation(selectedStudent, selectedCurse, evaluationInput);
            }

            // Retournez à la page AllStudentsPage
            Navigation.PopAsync();
        }

        private bool TryConvertToNumeric(string input, out double numericValue)
        {
            // Mapping of letter grades to numeric values
            var gradeMappings = new Dictionary<string, double>
            {
                { "N", 20 },
                { "C", 16 },
                { "B", 12 },
                { "TB", 8 },
                { "X", 4 }
            };

            if (gradeMappings.TryGetValue(input.ToUpper(), out numericValue))
            {
                return true;
            }

            // Try parsing as a numeric value
            return double.TryParse(input, out numericValue);
        }

        private void AddStudentEvaluation(Student student, string selectedCurse, object evaluation)
        {
            // Recherche de l'association pour le cours sélectionné
            var association = student.AssociatedCourses.FirstOrDefault(a => a.CourseName == selectedCurse);

            if (association == null)
            {
                // Si l'association n'existe pas, la créer
                association = new Association { CourseName = selectedCurse };
                student.AssociatedCourses.Add(association);
            }

            // Ajout de l'évaluation à la liste des évaluations pour le cours
            association.Evaluations.Add(evaluation.ToString());

            // Enregistrez les changements
            allStudents.SaveStudents();

            // Mise à jour de l'interface utilisateur
            // (Notez que dans ce scénario, vous pourriez également choisir de mettre à jour seulement l'item spécifique dans la collection au lieu de tout recharger)
        }
    }
}
