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
    /// Логика взаимодействия для PlayersPage.xaml
    /// </summary>
    public partial class PlayersPage : Page
    {
        public static ObservableCollection<Student> stud { get; set; }
        public Game game;
        public Results res;
        int one { get; set; }
        int two { get; set; }
        public PlayersPage()
        {
            InitializeComponent();
            stud = new ObservableCollection<Student>(DBConnection.connection.Student.Where(a => a.InGame == false).ToList());
            this.DataContext = this;
        }
        private void cb_player1(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as Student;
            one = a.Id;
        }
        private void cb_player2(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as Student;
            two = a.Id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (one != two)
            {

                var game = new Game
                {
                    Date = dp.SelectedDate,
                    IsDeleted = false
                };
                game.Results.Add(new Results { StudentId = one });
                game.Results.Add(new Results { StudentId = two });
                DBConnection.connection.Game.Add(game);
                DBConnection.connection.SaveChanges();

                NavigationService.Navigate(new GamesPage());
            }
            else
            {
                MessageBox.Show("Попей колеса мудила", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
