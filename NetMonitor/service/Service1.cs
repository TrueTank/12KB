using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace service
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    
    public class Service1 : IService1
    {
        public void DoWork()
        {
        }
        public void CleanJournal()
        {
            File.Create(@"texts\Журнал.txt");
        }

        public string ReadLogins()
        {
            string users;
            var logins = new StreamReader(@"texts\input.txt");
            users = logins.ReadLine();
            logins.Close();
            return users;
        }

        public void WriteToJournal(string a)
        {
            var file = new StreamWriter(@"texts\Журнал.txt");
            //file.WriteLine(a + DateTime.Now);
            //file.Close();
        }

        public void AddInInput(string a)
        {
            using (StreamWriter file = new StreamWriter(@"texts\input.txt", true))
            {
                file.Write(a);
            }
        }

        public bool IsRegistration(string users, string login, string password, int mode, string path, string user)
        {
             string pattern = @"(?<log>[A-z0-9_]+):(?<pas>[A-z0-9_]*):(?<mod>\d)";
             string newData = " " + login + ":" + password + ":3";
            if (Regex.IsMatch(newData, pattern) && !Regex.IsMatch(users, @login))
            {
                users += newData;
                user = login;
                mode = 3;
                path = @"texts\" + @user;
                DirectoryInfo dir = new DirectoryInfo(path);
                dir.Create();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsStart(string users, string login, string password, int mode, string path, string user)
        {
            string pattern = @"(?<log>[A-z0-9_]+):(?<pas>[A-z0-9_]*):(?<mod>\d)";
            Regex mRegex = new Regex(pattern);
            var ms = mRegex.Matches(users);
            foreach (Match m in ms)
            {
                if (m.Groups[1].Value == login && m.Groups[2].Value == password)
                {
                    mode = int.Parse(m.Groups[3].Value);
                    user = login;
                   
                    path = @"texts\" + @user;
                    return true;
                }
            }
            if (mode == -1)
            {
               
                return false;
            }
            return false;
        }

    }
}
