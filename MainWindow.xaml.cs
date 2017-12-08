using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
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
        DataView MyDataView;
        public MainWindow()
        {
            InitializeComponent();
            grid.DataContext = DBProxy.tableBook();
        }

        private void labAuth_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string id = idAuth.Content.ToString();

            BookAuthorsDetails BAD = new BookAuthorsDetails(id);
            BAD.Show();
        }

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
            System.Data.DataTable dt = DBProxy.tableBook();
            MyDataView = new DataView(dt);
            MyDataView.RowFilter = $"([Title] LIKE ('*{tbTitle.Text}*')) and ([Author] LIKE ('*{tbAuth.Text}*')) and ([Subject] LIKE ('*{tbSubj.Text}*'))";
            grid.DataContext = MyDataView;
            grid.Columns[3].Visibility = Visibility.Hidden;
            labBookCount.Content = grid.Items.Count.ToString() + " Books found";
        }

        private void grid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView drv = grid.SelectedItems[0] as DataRowView;
            List<string> str = DBProxy.retBookInfo(drv[0].ToString());
            labTitle.Content = str[0];
            labSubTitle.Content = str[1];
            labAuth.Content = str[4];
            labFP.Content = "first published: " + str[2];
            labDesc.Content = str[3];
            labSubj.Content = str[5];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grid.Columns[3].Visibility = Visibility.Hidden;
            grid.IsReadOnly = true;
            labBookCount.Content = grid.Items.Count.ToString() + " Books found";
        }
    }
}
