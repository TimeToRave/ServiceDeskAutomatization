using System;
using System.Threading;
using MasterSystem.DataContracts;
using MasterSystem.Interfaces;

namespace MasterSystem.Classes
{
    public class DomainLogic
    {
        private readonly ISynchronizationSender _syncSender;
        private readonly IDataOperator DataOperator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dataOperator">Экземпляр класса, реализующий работу с данными</param>
        /// <param name="syncSender">Экземпляр класса, реализующий механизмы интеграции со сторонней системой</param>
        public DomainLogic(IDataOperator dataOperator, ISynchronizationSender syncSender)
        {
            DataOperator = dataOperator;
            _syncSender = syncSender;
        }

        /// <summary>
        /// Выполняет отправку обращения в стороннюю систему
        /// </summary>
        /// <param name="applicationNumber">Номер обращения</param>
        public void SendApplication(string applicationNumber)
        {
            Application application = DataOperator.GetApplication(applicationNumber);
            SendApplication(application);
        }
        
        /// <summary>
        /// Выполняет отправку обращения в стороннюю систему
        /// </summary>
        /// <param name="application">Обращение</param>
        public void SendApplication(Application application)
        {
            Console.WriteLine($">>> {application.Number}: {application.Title} -> {application.Status}");
            
            _syncSender.SendData(application);
        }

        /// <summary>
        /// "Сохраняет" данные по обращению
        /// </summary>
        /// <param name="application"></param>
        public void SaveApplication(Application application)
        {
            Console.WriteLine($"<<< {application.Number}: {application.Title} -> {application.Status}");
        }
    }
}