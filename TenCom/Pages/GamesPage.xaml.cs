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
    /// Логика взаимодействия для GamesPage.xaml
    /// </summary>
    public partial class GamesPage : Page
    {
        public static ObservableCollection<Game> games { get; set; }
        private static Game currentGame = new Game();
        private static Student currentStud = new Student();
        public GamesPage()
        {
            InitializeComponent();
            games = new ObservableCollection<Game>(DBConnection.connection.Game.Where(a => a.IsDeleted == false).ToList());
            this.DataContext = this;
        }

        private void Games_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentGame = ((sender as ListView).SelectedItem as Game);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new PlayersPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StudToShow.SelectedItem != null)
            {
                var game = StudToShow.SelectedItem as Game;
                NavigationService.Navigate(new GameResultPage(game));
            }
            else MessageBox.Show("Выберите игру");
        }
    }
}
