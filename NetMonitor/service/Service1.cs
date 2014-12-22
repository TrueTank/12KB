using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

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
            File.Create(@"texts\Journal.txt");
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
            
            using (StreamWriter file = new StreamWriter(@"texts\Journal.txt", true))
            {
                file.WriteLine(a + DateTime.Now);
            }
        }

        public void AddInInput(string a)
        {
            using (StreamWriter file = new StreamWriter(@"texts\input.txt", true))
            {
                file.Write(a);
            }
        }

        public bool IsRegistration(ref string users,  string login,  string password, ref int mode, ref string path, ref string user)
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

        public bool IsStart(ref string users,  string login,  string password, ref int mode, ref string path, ref string user)
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

        public string[] ReadFiles(string path)
        {
            return Directory.GetFiles(@path);
        }

        public string ReadFile(string path)
        {
            string temp;
            using (StreamReader sr = new StreamReader(@path))
            {
                temp = sr.ReadToEnd();
            }
            return temp;
        }

        public void SaveNewFile(string a, string r)
        {
            using (StreamWriter sw = new StreamWriter(r))
            {
                sw.Write(a);
                sw.Close();
            }
        }

        public void SendFile(string tmp, string a)
        {
            File.Copy(@a, @"texts\\" + tmp);
        }

        public void DeleteFile(string a)
        {
            File.Delete(@a);
        }
    }
}
