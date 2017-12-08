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

namespace ProjectPI
{
    /// <summary>
    /// Логика взаимодействия для BookAuthorsDetails.xaml
    /// </summary>
    public partial class BookAuthorsDetails : Window
    {
        private string id;
        private string link_wiki;

        public BookAuthorsDetails(string id)
        {
            InitializeComponent();

            this.id = id; //получили ид автора

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!fill(id)) {

                MessageBox.Show("Ошибка! Скорее всего с БД=)");
            }
        }

        private bool fill(string id) {

            bool result = false;
            List<string> str = DBProxy.retBio(id);
            string name = str[0]; //какой то метод который возвращает имя автора. сейчас его нет

            cbAuth.SelectedValue = name;

            labDoB.Content = str[1]; // в каком формате хз естественно

            labDeath.Content = str[2]; // в каком формате хз естественно

            labBio.Content = str[3];

            this.link_wiki = str[4];

            if (name != "")
            {
                result = true;
            }
            
            return result;
        }

        private void labLM_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //при клике по лейблу хоп и открывается браузер
            System.Diagnostics.Process.Start(link_wiki);
        }

        private void cbAuth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
