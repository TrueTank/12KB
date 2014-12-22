using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using client.ServiceReference1;
using Microsoft.Win32;

namespace client
{
    /// <summary>
    /// Логика взаимодействия для manager.xaml
    /// </summary>
    public partial class manager : Window
    {
        public manager(string _path, string _user, string _users)
        {
            InitializeComponent();
            client = new Service1Client();
            pathBox.SelectAll();
            pathBox.Text = _path;
            path = _path;

            user = _user;
            users = _users;
            var ms = Regex.Matches(users, pattern);
            mas = new List<string>();
            foreach (Match match in ms)
            {
                mas.Add(match.Groups[1].ToString());
            }
            if (mas.Count > UsersComboBox.Items.Count)
            {
                for (int i = UsersComboBox.Items.Count; i < mas.Count; i++)
                {
                    var temp = new ComboBoxItem();
                    temp.Content = mas[i];
                    UsersComboBox.Items.Add(temp);
                }
            }
        }

        public List<string> mas;
        public string pattern = @"(?<log>[A-z0-9_]+):(?<pas>[A-z0-9_]*)";
        public string users;
        public string user;
        public string path;
        public int count = 0;
        private string[] files;
        private Service1Client client;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Clear();
            client.WriteToJournal("Пользователь " + user + " прошёл по пути: " + path + " в " + DateTime.Now);
            files = client.ReadFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                ListBox1.Items.Add(files[i]);
            }
        }

        private void ListBox1_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selIts = ListBox1.SelectedItems;
            client.WriteToJournal("Пользователь " + user + " открыл файл: " + selIts[0].ToString() + " в " + DateTime.Now);
            client.ReadFile(@selIts[0].ToString());
            forFiletb.Text = client.ReadFile(@selIts[0].ToString());
            forFiletb.Focus();
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SaveFileD win = new SaveFileD(forFiletb.Text, @path,user);
            win.Show(); 
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
                var selIts = ListBox1.SelectedItems;
                if (selIts.Count != 0)
                {
                    var tmp = UsersComboBox.SelectedValue.ToString().Substring(38) + "\\from" + user +
                              selIts[0].ToString().Substring(selIts[0].ToString().Length - 5) + count;
                    client.SendFile(tmp, @selIts[0].ToString());
                    client.WriteToJournal("Пользователь " + user + " передал пользователю " +
                                   UsersComboBox.SelectedValue.ToString().Substring(38) + " файл" + selIts[0].ToString() +" в " + DateTime.Now);
                    MessageBox.Show("Файл был передан успешно", "Уведомление");
                }
                else MessageBox.Show("Не выделен файл для передачи!", "Ошибка!");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var selIts = ListBox1.SelectedItems;
            if (selIts.Count != 0)
            {
                client.DeleteFile(@selIts[0].ToString());
                client.WriteToJournal("Пользователь " + user + " удалил файл " + selIts[0].ToString() + " в " + DateTime.Now);
                MessageBox.Show("Файл был удалён", "Уведомление");
            }
            else MessageBox.Show("Не выделен файл для удаления!", "Ошибка!");
        }

    }
}
