using DataManagement;
using System;
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

namespace GradeTracker
{
    /// <summary>
    /// Interaction logic for GradeView.xaml.
    /// </summary>
    public partial class GradeView : Window
    {
        public string ViewModel { get; set; }

        // List to store grades.
        List<ResultView> resultViews = new List<ResultView>();

        // Create the class to manage all database interactions.
        Adapter db = new Adapter();

        public GradeView()
        {
            InitializeComponent();
            LoadResultTable();
        }

        private void LoadResultTable()
        {
            resultViews = db.GetAllForResultView();
            dgvGrades.ItemsSource = resultViews;
        }
    }
}
