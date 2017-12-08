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
using System.Windows.Shapes;

namespace ProjectPI
{
    /// <summary>
    /// Логика взаимодействия для Circulation.xaml
    /// </summary>
    public partial class Circulation : Window
    {
        LibraryEntities1 newObj = new LibraryEntities1();
        public Circulation()
        {
            InitializeComponent();
            initializeFunc();

        }

        public void initializeFunc()
        {
            var query = from с in newObj.CustomerIssue
                        select new { Title = с.Books.Title, DateOfIssue = с.DateOfIssue, ReturnUntill = с.ReturnUntil };
            grid1.ItemsSource = query.ToList();
        }

        private void Circulation1_Click(object sender, RoutedEventArgs e)
        {
            int a = int.Parse(tbID1.Text); 
            ///need if statement
            var query = from c in newObj.CustomerIssue
                        where c.Cutomer_Id == a
                        select new { Title = c.Books.Title, DateOfIssue = c.DateOfIssue, ReturnDate = c.ReturnDate }
                  
                        ;
            grid2.ItemsSource = query.ToList();
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            initializeFunc();
        }

        private void Issue_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in newObj.CustomerIssue
                        where c.Book_Id == tbID.Text
                        select new { Title = c.Books.Title, DateOfIssue = c.DateOfIssue, ReturnDate = c.ReturnUntil };
            grid1.ItemsSource = query.ToList();
        }

        private void Renew_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
