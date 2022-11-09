using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TenCom.DB;

namespace TenCom.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudPage.xaml
    /// </summary>
    public partial class StudPage : Page
    {
        public static ObservableCollection<Student> stud { get; set; }
        private static Student currentStud = new Student();
        public StudPage()
        {
            InitializeComponent();
            stud = new ObservableCollection<Student>(DBConnection.connection.Student.Where(a => a.IsDeleted == false).ToList());
            this.DataContext = this;
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            
            if (StudToShow.SelectedItem != null)
            {
                var n = currentStud;
                NavigationService.Navigate(new RedPage(n));
            }
            else MessageBox.Show("Выберите студента");

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RedPage(new Student()));
        }

        private void stud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentStud = ((sender as ListView).SelectedItem as Student);
        }
    }
}
