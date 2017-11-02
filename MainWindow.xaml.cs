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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectPI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            grid.DataContext = DBProxy.tableBook();
        }

<<<<<<< HEAD
        private void labAuth_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string id = idAuth.Content.ToString();

            BookAuthorsDetails BAD = new BookAuthorsDetails(id);
            BAD.Show();
=======
        private void login_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void custom_Click(object sender, RoutedEventArgs e)
        {
            Customers cust = new Customers();
            cust.Show();
        }

        private void circ_Click(object sender, RoutedEventArgs e)
        {
            Circulation circ = new Circulation();
            circ.Show();
        }

        private void rep_Click(object sender, RoutedEventArgs e)
        {
            Reports rep = new Reports();
            rep.Show();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
>>>>>>> 7ed1d8835c6acc773bf9c1783bce8bbb8425de76

        }
    }
}
