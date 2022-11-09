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
    /// Логика взаимодействия для GameResultPage.xaml
    /// </summary>
    public partial class GameResultPage : Page
    {
        public static ObservableCollection<Results> res { get; set; }
        public static ObservableCollection<Student> stud { get; set; }
        public GameResultPage(Game game)
        {
            res = new ObservableCollection<Results>( DBConnection.connection.Results.Where(a => a.GameId == game.Id));
            this.DataContext = this;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (StudToShow.SelectedItem != null)
            {
                var result = StudToShow.SelectedItem as Results;
                result.Game.IsDeleted = true;
                var students = res.Where(a => a.GameId == result.Game.Id).Select(x=> x.Student);

                foreach (var student in students)
                {
                    if (student.Id == result.StudentId)
                        student.Score += 2;
                    student.InGame = false;
                }
                DBConnection.connection.SaveChanges();

                NavigationService.Navigate(new GamesPage());
            }
            else MessageBox.Show("Выберите игрока");
        }
    }
}
