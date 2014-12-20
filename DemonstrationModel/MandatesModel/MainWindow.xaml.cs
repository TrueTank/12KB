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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MandatesModel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int mode=-1;
        public string path;
        public string users;
        public string user;
        string pattern = @"(?<log>[A-z0-9_]+):(?<pas>[A-z0-9_]*):(?<mod>\d)";

        public MainWindow()
        {
            InitializeComponent();
            File.Create(
                @"texts\Журнал.txt");
            int counter = 0;         
            // Read the file and display it line by line.
            var logins =
                new StreamReader(@"texts\input.txt");
            users = logins.ReadLine();
            logins.Close();          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Regex mRegex=new Regex(pattern);
            var ms = mRegex.Matches(users);
            foreach (Match m in ms)
            {
                if (m.Groups[1].Value == tbLogin.Text && m.Groups[2].Value == tbPassw.Text)
                {
                    mode =int.Parse(m.Groups[3].Value);
                    user = tbLogin.Text;
                    using (StreamWriter file = new StreamWriter(@"texts\Журнал.txt", true))
                    {
                        file.WriteLine("Пользователь " + user + " вошёл в систему в " + DateTime.Now);
                    }
                    path =@"texts\" +@user;
                    MessageBox.Show("Произведён вход в систему", "Уведомление");
                    var manage = new manager(path,user,users);
                    this.Close();
                    manage.Show();
                }
            }
            if (mode == -1)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка!");
                using (StreamWriter file = new StreamWriter(@"texts\Журнал.txt", true))
                {
                    file.WriteLine("Безуспешная попытка войти в систему под именем "+tbLogin.Text+" в " + DateTime.Now);
                }
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            string newData = " " + tbLogin.Text + ":" + tbPassw.Text + ":3";
            if (Regex.IsMatch(newData, pattern) && !Regex.IsMatch(users, @tbLogin.Text))
            {
                using (StreamWriter file =new StreamWriter(@"texts\input.txt",true))
                {
                    file.Write(" " + tbLogin.Text + ":" + tbPassw.Text + ":3");
                }
                using (StreamWriter file = new StreamWriter(@"texts\Журнал.txt", true))
                {
                    file.WriteLine("Зарегистрирован новый пользователь " + tbLogin.Text + " в " + DateTime.Now);
                }
                users += " " + tbLogin.Text + ":" + tbPassw.Text + ":3";
                user = tbLogin.Text;
                mode = 3;
                path =@"texts\" +@user;
                DirectoryInfo dir = new DirectoryInfo(path);
                dir.Create();
                MessageBox.Show("Зарегистрирован новый пользователь", "Уведомление");
                var manage = new manager( path, user,users);
                this.Close();
                manage.Show();
            }
            else
            {
                using (StreamWriter file = new StreamWriter(@"texts\Журнал.txt", true))
                {
                    file.WriteLine("Безуспешная попытка зарегистрироваться под именем " + tbLogin.Text + " в " + DateTime.Now);
                }
                MessageBox.Show("Некорректные данные! Логин и пароль могут содержать только цифры, латинские буквы и нижние подчеркивания, либо проблема в том, что введёный Вами логин уже существует", "Ошибка!");
            }
        }
    }
}
