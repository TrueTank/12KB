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
using client.ServiceReference1;

namespace client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int mode = -1;
        public string path;
        public string users;
        public string user;
        string pattern = @"(?<log>[A-z0-9_]+):(?<pas>[A-z0-9_]*):(?<mod>\d)";
        private Service1Client client;
        public MainWindow()
        {
            InitializeComponent();
             client=new Service1Client();
             //client.CleanJournal();
            int counter = 0;
            users = client.ReadLogins();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(client.IsStart(ref users,  tbLogin.Text,tbPassw.Text,ref mode,ref path,ref user))
            {
                
                client.WriteToJournal("Пользователь " + user + " вошёл в систему в "); 
                MessageBox.Show("Произведён вход в систему", "Уведомление");
                var manage = new manager(path, user, users);
                this.Close();
                manage.Show();
                }
            else
            {
                client.WriteToJournal("Безуспешная попытка войти в систему под именем " + tbLogin.Text + " в "); 
                MessageBox.Show("Неверный логин или пароль!", "Ошибка!");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            if (client.IsRegistration(ref users, tbLogin.Text, tbPassw.Text, ref mode, ref path, ref user))
            {
                client.AddInInput(" " + tbLogin.Text + ":" + tbPassw.Text + ":3");
                client.WriteToJournal("Зарегистрирован новый пользователь " + tbLogin.Text + " в " + DateTime.Now);           
                MessageBox.Show("Зарегистрирован новый пользователь", "Уведомление");
                var manage = new manager(path, user, users);
                this.Close();
                manage.Show();
            }
            else
            {
                client.WriteToJournal("Безуспешная попытка зарегистрироваться под именем " + tbLogin.Text + " в " + DateTime.Now);
                MessageBox.Show("Некорректные данные! Логин и пароль могут содержать только цифры, латинские буквы и нижние подчеркивания, либо проблема в том, что введёный Вами логин уже существует", "Ошибка!");
            }
        }
    }
}
