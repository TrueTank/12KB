using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Win32;

namespace service
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        void CleanJournal();

         [OperationContract]
          string ReadLogins();

        [OperationContract]
         bool IsStart(ref string users, string login, string password, ref int mode, ref string path, ref string user);


        [OperationContract]
        void WriteToJournal(string a);

        [OperationContract]
        void AddInInput(string a);

        [OperationContract]
        string[]  ReadFiles( string path);

        [OperationContract]
        string ReadFile(string path);

        [OperationContract]
        bool IsRegistration(ref string users,  string login, string password, ref int mode, ref string path, ref string user);
        [OperationContract]
        void SaveNewFile(string a, string r);
        [OperationContract]
        void SendFile(string tmp, string a);
        [OperationContract]
        void DeleteFile(string a);
    }
}
