using MasterSystem.Interfaces;

namespace MasterSystem.Classes
{
    public class RabbitMqSynchronizationSubscriber : ISynchronizationSubscriber
    {
        private readonly RabbitMqSyncModule _rabbitMqClient;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="queueName">Название очереди</param>
        public RabbitMqSynchronizationSubscriber(string queueName)
        {
            _rabbitMqClient = new RabbitMqSyncModule(queueName);
        }

        /// <summary>
        /// Выполняет подписку на получение новых сообщений 
        /// </summary>
        /// <param name="domainLogic"></param>
        public void SubscribeOn(DomainLogic domainLogic)
        {
            _rabbitMqClient.SubscribeOnQueue(domainLogic);
        }

    }
}