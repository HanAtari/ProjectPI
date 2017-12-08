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
using System.Data;

namespace ProjectPI
{
    /// <summary>
    /// Логика взаимодействия для AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        string status;
        string id;

        public AddCustomer(string status, string id)
        {
            InitializeComponent();

            //edit или add в зависимости от режима. если add то передается еще и ид элемента редактирования
            //можно передать 0 если режим доабвления
            this.status = status;
            this.id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("У вас остались несохраненные данные", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (!(result.ToString() == DialogResult.Value.ToString()))
            {
                this.Close();
            }

        }

        private void client() {

            //ид по макету поле для чтения, поэтому его тут нет
            string name = tname.Text;
            string address = taddress.Text;
            string phone = tphone.Text;
            string email = temail.Text;
            string zip = tzip.Text;
            string city = tcity.Text;

            if (name != "" && address != "" && phone != "" && email != "" && zip != "" && city != "")
            {
                if (this.status == "edit") {
                    DBProxy.upClient(this.id, name, phone, address, email, zip, city);
                }
                if (this.status == "add") {

                    //по сути этот метот должен возвращать тру или фолс и отталкиваясь от этого
                    // мой метод возвращал бы статус добавления но увы
                    DBProxy.addClient(name, phone, address, email, zip, city);

                }
            }
            else
            {
                MessageBox.Show("Не заполнены все поля!");
            }

        }

        //save
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client();
        }


        /*
         * Если форма открылась со статусом edit, то поля
         * перед показом пользователю будут заполнены соответствующими
         * данными из айди, который так же пришел на форму вместе со статусом edit
         * еслм статус add форма останется пустой
         */

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.status == "edit")
            {
                DataTable clients = DBProxy.tableClients();

                string expression;
                expression = "ID=" + this.id;
                DataRow[] current_customer;

                current_customer = clients.Select(expression);

                tname.Text = current_customer[1].ToString();
                taddress.Text = current_customer[2].ToString();

                //телефона и маила у Дмитрия нет
                tphone.Text = current_customer[500].ToString();
                temail.Text = current_customer[500].ToString();
                //-----

                tzip.Text = current_customer[3].ToString();
                tcity.Text = current_customer[4].ToString();
            }
            
        }
    }
}
