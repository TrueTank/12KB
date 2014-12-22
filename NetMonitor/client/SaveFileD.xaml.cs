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
using client.ServiceReference1;

namespace client
{
    /// <summary>
    /// Логика взаимодействия для SaveFileD.xaml
    /// </summary>
    public partial class SaveFileD : Window
    {
        public SaveFileD(string text,string path, string user)
        {
            InitializeComponent();
            client=new Service1Client();
            t = text;
            p = path;
            u = user;
        }

        private string u;
        private string t;
        private string p;
        private Service1Client client;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tbName.Text.Length != 0)
            {
                client.SaveNewFile(t, @p + @"\" + @tbName.Text);
                client.WriteToJournal("Пользователь " + u + " создал файл: " + p + @"\" + tbName.Text + " в " + DateTime.Now);
                this.Close();
            }
            else MessageBox.Show("Введите имя файла!", "Ошибка!");
            
        }
    }
}
