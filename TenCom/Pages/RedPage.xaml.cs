using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для RedPage.xaml
    /// </summary>
    public partial class RedPage : Page
    {
        public static ObservableCollection<Student> stud { get; set; }
        public static Student conststud;
        public RedPage(Student student)
        {
            conststud = student;
            this.DataContext = conststud;
            InitializeComponent();
            tb_name.Text = student.FullName;
            tb_description.Text = student.Age.ToString();

            cb_country.ItemsSource = DBConnection.connection.EducationalInstitution.ToList();
            cb_country.DisplayMemberPath = "Name";

        }

        private void btn_newphoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                conststud.Img = File.ReadAllBytes(openFile.FileName);
                img_prod.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            conststud.FullName = tb_name.Text;
            conststud.Age = Convert.ToInt32(tb_description.Text);
            conststud.IsDeleted = false;
            conststud.InGame = false;
            if (cb_country.SelectedIndex == 0)
            {
                conststud.IdEI = 1;
            }
            else if (cb_country.SelectedIndex == 1)
            {
                conststud.IdEI = 2;
            }
            else if (cb_country.SelectedIndex == 2)
            {
                conststud.IdEI = 3;
            }

            DBConnection.connection.SaveChanges();
            NavigationService.Navigate(new MainPage());
        }

        public static bool btn_delite_Click(Student student)
        {
            student.IsDeleted = true;
            try
            {
                DBConnection.connection.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btn_delite_Click(object sender, RoutedEventArgs e)
        {
            conststud.IsDeleted = true;
            DBConnection.connection.SaveChanges();
            NavigationService.Navigate(new StudPage());
        }
    }
}
