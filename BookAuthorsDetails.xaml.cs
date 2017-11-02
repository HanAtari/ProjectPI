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
        private DBProxy info;
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

            string name = DBProxy.retNameAut(id).ToString(); //какой то метод который возвращает имя автора. сейчас его нет

            cbAuth.SelectedValue = name;

            labDoB.Content = DBProxy.retBirthday(id).ToString(); // в каком формате хз естественно

            labDeath.Content = DBProxy.retDeathday(id).ToString(); // в каком формате хз естественно

            labBio.Content = DBProxy.retBio(id).ToString();

            this.link_wiki = DBProxy.retWiki(id).ToString();

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
    }
}
