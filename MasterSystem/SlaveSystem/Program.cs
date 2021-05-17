using System;
using System.Threading;
using MasterSystem.Classes;
using MasterSystem.Interfaces;

namespace SlaveSystem
{
    class Program
    {
        static void Main(string[] args)
        {
           
            IDataOperator fileDataOperator = new FileDataOperator("ApplicationsInput.json", "ServicesInput.json");
            
            ISynchronizationSender synchronyzationSender =
                new RabbitMqSynchronizationSender("ServiceDeskSlave", "ServiceDeskSlaveToMaster", "");
            ISynchronizationSubscriber synchronizationSubscriber =
                new RabbitMqSynchronizationSubscriber("ServiceDeskMasterToSlave");

            var domainLogic = new DomainLogic(fileDataOperator, synchronyzationSender);

            synchronizationSubscriber.SubscribeOn(domainLogic);

            Console.ReadKey();
        }
    }
}