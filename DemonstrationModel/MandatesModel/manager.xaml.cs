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
using Microsoft.Win32;

namespace MandatesModel
{
    /// <summary>
    /// Логика взаимодействия для manager.xaml
    /// </summary>
    public partial class manager : Window
    {
        public manager(string _path,string _user,string _users)
        {
            InitializeComponent();
            pathBox.SelectAll();
            pathBox.Text = _path;
            path = _path;
       
            user = _user;
            users = _users;
            var ms = Regex.Matches(users, pattern);
            mas=new List<string>();
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
        //public int mode1;
        private string[] files;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Clear();
            using (StreamWriter file = new StreamWriter(@"texts\Журнал.txt",true))
            {
                file.WriteLine("Пользователь " + user + " прошёл по пути: " + path+" в "+DateTime.Now);
                files = Directory.GetFiles(@path);
                for (int i = 0; i < files.Length; i++)
                {
                    FileAttributes attributes = File.GetAttributes(@files[i]);
                    ListBox1.Items.Add(files[i]);
                }
            }
        }

        private void ListBox1_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (StreamWriter file = new StreamWriter(@"texts\Журнал.txt",true))
            {
                var selIts = ListBox1.SelectedItems;
                file.WriteLine("Пользователь " + user + " открыл файл: " + selIts[0].ToString() + " в " + DateTime.Now);

                    using (StreamReader sr = new StreamReader(@selIts[0].ToString()))
                    {
                        forFiletb.Text = sr.ReadToEnd();
                        forFiletb.Focus();
                    }
                }
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (forFiletb.IsReadOnly == false)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = path;
                saveFileDialog1.Filter = "Текст (*.txt)|*.txt";
                using (StreamWriter file = new StreamWriter(@"texts\Журнал.txt", true))
                {
                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        string pat;
                        using (
                            StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile(), System.Text.Encoding.Default)
                            )
                        {
                            sw.Write(forFiletb.Text);
                            sw.Close();
                            file.WriteLine("Пользователь " + user + " создал файл: " + saveFileDialog1.FileName + " в " + DateTime.Now);
                        }
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (StreamWriter file = new StreamWriter(@"texts\Журнал.txt",true))
            {
                var selIts = ListBox1.SelectedItems;
                if (selIts.Count != 0)
                {
                    var tmp = UsersComboBox.SelectedValue.ToString().Substring(38) + "\\from" + user +
                              selIts[0].ToString().Substring(selIts[0].ToString().Length - 5) + count;
                    File.Copy(@selIts[0].ToString(), @"texts\\" + tmp);
                    file.WriteLine("Пользователь " + user + " передал пользователю " +
                                   UsersComboBox.SelectedValue.ToString().Substring(38) + " файл" + selIts[0].ToString() +
                                   " в " + DateTime.Now);
                }
                else MessageBox.Show("Не выделен файл для передачи!", "Ошибка!");

            }
        }


    }
}
