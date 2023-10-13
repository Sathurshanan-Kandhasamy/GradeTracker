using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataManagement;

namespace GradeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        // List to store all out student records.
        List<Student> myStudents = new List<Student>();
        // List to store grades.
        List<ResultView> resultViews = new List<ResultView>();
        // Create the class to manage all our database interactions.
        Adapter db = new Adapter();
        // Acts as a flag to determine which version of the save method is used.
        bool isNewEntry = true;
        bool isGradeWindow = false;

        public MainWindow()
        {
            InitializeComponent();
            dgvStudents.Visibility = Visibility.Visible;
            dgvGrades.Visibility = Visibility.Collapsed;
            GetStudentTable();
            LoadResultTable();
            
            
        }

        private void GetStudentTable()
        {

            // Get the list of objects mapped with rows of the SQL database table.
            myStudents = db.GetAllStudents();

            // Fill the DataGrid.
            // Assigning the 'MyStudent' list to the ItemSource of the DataGrid.
            dgvStudents.ItemsSource = myStudents;
            
            // Fill the ComboBox.
            // Assigning the 'MyStudent' list to the ItemSource of the ComboBox.
            cboStudentList.ItemsSource = myStudents;
            // Tell the combobox which property from the itemsource to show in each entry.
            cboStudentList.DisplayMemberPath = "FirstName";
            // Tells the combobox which ptoperty to return when an option is selected.
            cboStudentList.SelectedValuePath = "StudentId";
            // Sets which index to display as the default.
            cboStudentList.SelectedIndex = 0;
            dgvStudents.Visibility = Visibility.Visible;
            isGradeWindow = false;


        }

        private void cboStudentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Student selected = (Student)cboStudentList.SelectedItem;

            if (selected != null) 
            {
                txtId.Text = Convert.ToString(selected.StudentId);
                txtFirst.Text = selected.FirstName;
                txtLast.Text = selected.LastName;
                txtEmail.Text = selected.Email;
                isNewEntry = false;
                dgvStudents.Visibility = Visibility.Visible;
                dgvGrades.Visibility = Visibility.Collapsed;
                dgvStudents.SelectedIndex = cboStudentList.SelectedIndex;
                isGradeWindow = false; 
            }            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!isGradeWindow)
            {
                // Create new object with updated information.
                Student student = new Student();
            
                // Taking all othe information from txt.
                student.FirstName = txtFirst.Text;
                student.LastName = txtLast.Text;
                student.Email = txtEmail.Text;
            
                if (!isNewEntry)
                {
                    // Using studentId from the combobox.
                    student.StudentId = Convert.ToInt32(cboStudentList.SelectedValue);
                    // Update sql table.
                    db.UpdateStudent(student);

                }
                else
                {
                    // Send the details to the database to be saved as a new entry.
                    db.AddNewStudent(student);
                }
                // ReRead SQL table and refresh the DataGrid.
                GetStudentTable();
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {

            if (!isGradeWindow)
            {
                dgvStudents.Visibility = Visibility.Visible;
                // Set the form fields to blank.
                txtId.Text = string.Empty;
                txtFirst.Text = string.Empty;
                txtLast.Text = string.Empty;
                txtEmail.Text = string.Empty;
                // Sets the save mode to use the method for an new save.
                isNewEntry = true;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!isGradeWindow)
            {
                int selItem = Convert.ToInt32(cboStudentList.SelectedValue);
                db.DeleteStudent(selItem);
                // ReRead SQL table and refresh the DataGrid.
                GetStudentTable();
            }
        }

       
        private void dgvStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student selected = (Student)dgvStudents.SelectedItem;

            if (selected != null)
            {
                txtId.Text = Convert.ToString(selected.StudentId);
                txtFirst.Text = selected.FirstName;
                txtLast.Text = selected.LastName;
                txtEmail.Text = selected.Email;
                isNewEntry = false;
                dgvStudents.Visibility = Visibility.Visible;
                dgvGrades.Visibility = Visibility.Collapsed;
                cboStudentList.SelectedIndex = dgvStudents.SelectedIndex;
                isGradeWindow = false;
            }
            cboStudentList.SelectedIndex = dgvStudents.SelectedIndex;
            // Sets the save mode to use the method for an update save type.
            isNewEntry = false;
        }

        private void btnGrades_Click(object sender, RoutedEventArgs e)
        {
            if(isGradeWindow)
            {
                dgvStudents.Visibility = Visibility.Visible;
                dgvGrades.Visibility = Visibility.Collapsed;
                isGradeWindow = false;
            }
            else
            {
                dgvStudents.Visibility = Visibility.Collapsed;
                dgvGrades.Visibility = Visibility.Visible;
                isGradeWindow = true;
            }
           
        }
        private void LoadResultTable()
        {
            resultViews = db.GetAllForResultView();
            dgvGrades.ItemsSource = resultViews;
        }
        private void dgvGrades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResultView selected = (ResultView)dgvGrades.SelectedItem;
        }

        private void dgvGrades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Student selected = (Student)dgvGrades.SelectedItem;
            
        }

        private void btnNewWindow_Click(object sender, RoutedEventArgs e)
        {
            
            GradeView GradeWindow = new GradeView();
            GradeWindow.Owner = this;
            GradeWindow.Show();
            
        }
    }
  
}
