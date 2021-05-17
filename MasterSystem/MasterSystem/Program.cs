using System;
using System.Collections.Generic;
using System.Net.Mime;
using MasterSystem.Classes;
using MasterSystem.DataContracts;
using MasterSystem.Interfaces;

namespace MasterSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataOperator fileDataOperator = new FileDataOperator("ApplicationsInput.json", "ServicesInput.json");
            ISynchronizationSender synchronyzationSender =
                new RabbitMqSynchronizationSender("ServiceDeskMaster", "ServiceDeskMasterToSlave", "MastersApplication");
            ISynchronizationSubscriber synchronizationSubscriber =
                new RabbitMqSynchronizationSubscriber("ServiceDeskSlaveToMaster");

            List<Application> allApplications = new List<Application>();
            for (int i = 100001; i < 100411; i++)
            {
                allApplications.Add(fileDataOperator.GetApplication(i.ToString()));
            }

            var domainLogic = new DomainLogic(fileDataOperator, synchronyzationSender);

            synchronizationSubscriber.SubscribeOn(domainLogic);

            List<Application> applications = new List<Application>();
            ConsoleKeyInfo input;
            do
            {
                Console.WriteLine("Введите номер обращения");
                string number = Console.ReadLine();
                if (number == "panic")
                {
                    foreach (var application in allApplications)
                    {
                        domainLogic.SendApplication(application);
                    }
                }
                else
                {
                    Application application = fileDataOperator.GetApplication(number);
                    application.Status = "New";
                    domainLogic.SendApplication(application);
    
                }
                                
                input = Console.ReadKey();
            } while (input.Key != ConsoleKey.Escape);
            
            

            Console.ReadKey();
        }
    }
}