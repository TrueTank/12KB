using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
        bool IsStart(string users, string login, string password, int mode, string path, string user);


        [OperationContract]
        void WriteToJournal(string a);

        [OperationContract]
        void AddInInput(string a);

        [OperationContract]
        bool IsRegistration(string users, string login, string password, int mode, string path, string user);
    }
}
