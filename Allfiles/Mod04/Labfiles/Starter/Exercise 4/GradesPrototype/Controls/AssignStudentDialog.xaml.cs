﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Controls
{
    /// <summary>
    /// Interaction logic for AssignStudentDialog.xaml
    /// </summary>
    public partial class AssignStudentDialog : Window
    {
        public AssignStudentDialog()
        {
            InitializeComponent();
        }

        // TODO: Exercise 4: Task 3b: Refresh the display of unassigned students
        // DONE
        private void Refresh()
        {            
            var unassignedStudents = from s in DataSource.Students
                                     where s.TeacherID == 0
                                     select s;
                        
            if (unassignedStudents.Count() == 0)
            {
                txtMessage.Visibility = Visibility.Visible;
                list.Visibility = Visibility.Collapsed;
            }
            else
            {                                
                txtMessage.Visibility = Visibility.Collapsed;
                list.Visibility = Visibility.Visible;
                                
                list.ItemsSource = unassignedStudents;
            }
        }

        private void AssignStudentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        // TODO: Exercise 4: Task 3a: Enroll a student in the teacher's class
        // DONE
        private void Student_Click(object sender, RoutedEventArgs e)
        {
            try
            {                                
                Button studentClicked = sender as Button;
                int studentID = (int)studentClicked.Tag;

                Student student = (from s in DataSource.Students
                                   where s.StudentID == studentID
                                   select s).First();
                
                string message = String.Format("Add {0} {1} to your class?", student.FirstName, student.LastName);
                MessageBoxResult reply = MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (reply == MessageBoxResult.Yes)
                {
                    int teacherID = SessionContext.CurrentTeacher.TeacherID;

                    SessionContext.CurrentTeacher.EnrollInClass(student);
                    
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error enrolling student", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog box
            this.Close();
        }
    }
}
