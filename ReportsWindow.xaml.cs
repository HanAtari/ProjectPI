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
    /// Логика взаимодействия для Reports.xaml 
    /// </summary> 
    public partial class Reports : Window
    {
        LibraryEntities1 newObj = new LibraryEntities1();
        public Reports()
        {
            InitializeComponent();
            showReminders();
            showBookHistory();
            labTitle.Content = "«Title»";
            labSubTitle.Content = "«Subtitle»";
        }

        public void showReminders()
        {
            var query = from c in newObj.CustomerIssue
                        select new { Title = c.Books.Title, Customer = c.Klient_2.Name, DateOfIssue = c.DateOfIssue, ReturnUntill = c.ReturnUntil };
            gridRem.ItemsSource = query.ToList();
        }

        public void showBookHistory()
        {
            var query = from c in newObj.CustomerIssue
                        select new { Customer = c.Klient_2.Name, DateOfIssue = c.DateOfIssue, ReturnDate = c.ReturnDate };
            gridHist.ItemsSource = query.ToList();
        }

        private void tbID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                foreach (var i in newObj.CustomerIssue)
                {
                    if (i.Book_Id == tbID.Text)
                    {
                        labTitle.Content = i.Books.Title;
                        labSubTitle.Content = i.Books.Subtitle;
                    }
                }

                var query = from c in newObj.CustomerIssue
                            where c.Book_Id == tbID.Text
                            select new { Customer = c.Klient_2.Name, DateOfIssue = c.DateOfIssue, ReturnDate = c.ReturnDate };
                gridHist.ItemsSource = query.ToList();

            }
        }

        private void gridHist_GotFocus(object sender, RoutedEventArgs e)
        {
            showBookHistory();
            labTitle.Content = "«Title»";
            labSubTitle.Content = "«Subtitle»";
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}