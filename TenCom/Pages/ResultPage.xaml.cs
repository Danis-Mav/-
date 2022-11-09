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
    /// Логика взаимодействия для ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public static ObservableCollection<Student> stud { get; set; }
        public ResultPage()
        {
            InitializeComponent();
            stud = new ObservableCollection<Student>(DBConnection.connection.Student.Where(a => a.IsDeleted == false).ToList().OrderBy(x => x.Score).Reverse());
            this.DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
