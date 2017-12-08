using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectPI
{
    class DBProxy
    {

        static LibraryEntities1 context = new LibraryEntities1();
        public static List<string> retBookInfo(string id)//Все атрибуты книги кроме обложек(0-название,1- субтитл, 2=Дата, 3-описание,4-авторы,5 - метки)
        {
            List<string> l = new List<string>();
            var b = context.Books.Where(c => c.Key_B == id).FirstOrDefault();


            l.Add(b.Title);

            l.Add(b.Subtitle);

            l.Add(b.FirstPublishDate);

            l.Add(b.Description);

            string aut = "";
            foreach (var a in b.Authors)
            {
                aut += a.Name + ", ";
            }
            l.Add(aut);

            string str = "";
            var subject = context.BookSubjects.ToList();
            foreach (var s in subject)
            {
                if (s.Book_key == id)
                {
                    str += s.Subject + ", ";
                }
            }
            l.Add(str);

            return l;
        }

        public static List<string> retCover(string id)//Обложки книги по айди
        {
            List<string> str = new List<string>();
            var cover = context.BookCovers.ToList();
            foreach (var s in cover)
            {
                if (s.BookKey == id)
                {
                    str.Add(s.CoverFile.ToString());
                }
            }
            return str;
        }
        public static List<string> retBio(string id)//Атрибуты автора (0-имя, 1-биография, 2-дата рождения, 3 дата смерти, 4-вики)
        {
            List<string> str = new List<string>();
            var aut = context.Authors.Where(c => c.Key_a == id).FirstOrDefault();

            str.Add(aut.Name);
            if (aut.Bio != null)
            {
                str.Add(aut.Bio);
            }
            else
            {
                str.Add("");
            }

            if (aut.BirthDay != null)
            {
                str.Add(aut.BirthDay);
            }
            else
            {
                str.Add("");
            }

            if (aut.DeathDay != null)
            {
                str.Add(aut.DeathDay);
            }
            else
            {
                str.Add("");
            }

            if (aut.Wikipedia != null)
            {
                str.Add(aut.Wikipedia);
            }
            else
            {
                str.Add("");
            }



            return str;
        }

        public static string retHashPass(string login)//Возвращает хешированный пароль
        {
            List<string> str = new List<string>();
            var aut = context.Autoriz.ToList();
            foreach (var a in aut)
            {
                if (a.Login == login)
                {
                    return a.Password;
                }
            }

            return "";
        }

        public static DataTable comboboxauthor()// комбобокс с авторами
        {
            var cust = context.Authors.ToList();
            DataTable dt = new DataTable("Client");
            DataColumn column;
            DataRow row;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            foreach (var a in cust)
            {
                row = dt.NewRow();
                row[0] = a.Key_a;
                row[1] = a.Name;
                dt.Rows.Add(row);
            }
            return dt;
        }
        public static DataTable tableClients()//Таблица со списком клиентов
        {
            var cust = context.Klient_2.ToList();
            DataTable dt = new DataTable("Client");
            DataColumn column;
            DataRow row;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Address";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Zip";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "City";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            foreach (var a in cust)
            {
                row = dt.NewRow();
                row[0] = a.Id;
                row[1] = a.Name;
                row[2] = a.Adress;
                row[3] = a.Zip;
                row[4] = a.City;
                dt.Rows.Add(row);
            }
            return dt;
        }
        public static void addClient(string name, string phone, string address, string email, string zip, string city)//Добавление клиента
        {
            Klient_2 client = new Klient_2
            {
                Name = name,
                Phone = phone,
                Adress = address,
                Email = email,
                Zip = zip,
                City = city
            };
            context.Klient_2.Add(client);
            context.SaveChanges();
        }
        public static void upClient(string id, string name, string phone, string address, string email, string zip, string city)//Редактирование клиента
        {
            var client = context.Klient_2.ToList();
            foreach (var c in client)
            {
                if (c.Id == int.Parse(id))
                {
                    c.Name = name;
                    c.Phone = phone;
                    c.Adress = address;
                    c.Email = email;
                    c.Zip = zip;
                    c.City = city;
                }
            }
            context.SaveChanges();
        }

        public static DataTable tableIssues(string idcus)//Таблица с долгами по айди клиента
        {
            var cusis = context.CustomerIssue.ToList();
            DataTable dt = new DataTable("Issue");
            DataColumn column;
            DataRow row;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Title";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Date of issue";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Return until";
            column.ReadOnly = false;
            dt.Columns.Add(column);

            foreach (var c in cusis)
            {
                if ((c.ReturnDate == null) && (c.Cutomer_Id == int.Parse(idcus)))
                {
                    row = dt.NewRow();
                    row[0] = c.Books.Title;
                    row[1] = c.DateOfIssue;
                    row[2] = c.ReturnUntil;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static DataTable tableHistory(string idcus)//таблица истории сданных книг клианта по айди
        {
            var cusis = context.CustomerIssue.ToList();
            DataTable dt = new DataTable("History");
            DataColumn column;
            DataRow row;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Title";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Date of issue";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Return date";
            column.ReadOnly = false;
            dt.Columns.Add(column);

            foreach (var c in cusis)
            {
                if ((c.ReturnDate != null) && (c.Cutomer_Id == int.Parse(idcus)))
                {
                    row = dt.NewRow();
                    row[0] = c.Books.Title;
                    row[1] = c.DateOfIssue;
                    row[2] = c.ReturnDate;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        public static void addIssues(string idcl, string idbook)//Добавление долга клиенту(книжку взять с библотеки)
        {
            CustomerIssue cusbo = new CustomerIssue
            {
                Cutomer_Id = int.Parse(idcl),
                Book_Id = idcl,
                DateOfIssue = DateTime.Today,
                ReturnUntil = DateTime.Today.AddDays(21)
            };
            context.CustomerIssue.Add(cusbo);
            context.SaveChanges();
        }

        public static void returnBook(string idcl, string idbo)//возврат книги(ретурн дате становится не null)
        {
            var cusbo = context.CustomerIssue.ToList();
            foreach (var c in cusbo)
            {
                if ((c.Cutomer_Id == int.Parse(idcl)) && (c.Book_Id == idbo) && (c.ReturnDate == null))
                {
                    c.ReturnDate = DateTime.Today;
                }
            }
            context.SaveChanges();
        }
        public static DataTable tableBook()//Список книг
        {
            var books = context.Books.ToList();
            DataTable dt = new DataTable("Books");
            DataColumn column;
            DataRow row;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Title";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Author";
            column.ReadOnly = false;
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Subject";
            column.ReadOnly = false;
            dt.Columns.Add(column);

            foreach (var b in books)
            {
                row = dt.NewRow();
                row[0] = b.Key_B;
                row[1] = b.Title;
                var aut = b.Authors.ToList();
                foreach (var a in aut)
                {
                    row[2] += a.Name + " ";
                }
                row[3] = b.BookSubjects.Where(c => c.Book_key == b.Key_B).FirstOrDefault().Subject;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
