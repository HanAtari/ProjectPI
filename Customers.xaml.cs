using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
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
    /// Логика взаимодействия для Customers.xaml
    /// </summary>
    public partial class Customers : Window
    {
        LibraryEntities1 newObj = new LibraryEntities1();
        DataView MyDataView;
        public Customers()
        {
            InitializeComponent();
            initializeTable();
        }

        public void initializeTable()
        {
            grid1.DataContext = DBProxy.tableClients();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                searchFunc();
        }

        public void searchFunc()
        {
            System.Data.DataTable dt = DBProxy.tableClients();
            MyDataView = new DataView(dt);
            MyDataView.RowFilter = $"([ID] LIKE ('{customerId.Text}*')) and ([Name] LIKE ('*{nameClient.Text}*'))";
            grid1.DataContext = MyDataView;
        }

        private void grid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddCustomer obj = new AddCustomer("add", "");
            obj.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (grid1.SelectedIndex != null)
            {
                AddCustomer obj = new AddCustomer("edit", grid1.SelectedIndex.ToString());
                obj.Show();
            }
            else
            {
                MessageBox.Show("Please, select user");
            }
        }
    }
}
