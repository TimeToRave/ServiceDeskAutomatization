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

        static Semaphore sem = new Semaphore(3, 3);

        public void SaveApplication(Application application)
        {
            Thread.Sleep(1000 * application.Service.ReactionTime);
            
            Thread th = new Thread(() =>
            {

            sem.WaitOne();

            Console.WriteLine($"Принято {application.Title}");

            
            application.Status = "InProgress";
            _syncSender.SendData(application);

            Thread.Sleep(1000 * application.Service.ResolvingTime);
            application.Status = "Completed";
            _syncSender.SendData(application);

            sem.Release();
        }
        );
        th.Name = $"Thread ID: {Guid.NewGuid()}";
        th.Start();
    }
}

}