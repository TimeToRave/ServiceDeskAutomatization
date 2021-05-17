using MasterSystem.Interfaces;

namespace MasterSystem.Classes
{
   public class RabbitMqSynchronizationSender : ISynchronizationSender
    {
        private readonly RabbitMqSyncModule _rabbitMqClient;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="exchangeName">Название обменника</param>
        /// <param name="queueName">Название очереди</param>
        /// <param name="routingKey">Ключ маршрутизации</param>
        public RabbitMqSynchronizationSender(string exchangeName, string queueName, string routingKey)
        {
            _rabbitMqClient = new RabbitMqSyncModule(exchangeName, queueName, routingKey);
        }

        /// <summary>
        /// Выполняет отправку бизнес-объекта в RabbitMQ
        /// </summary>
        /// <param name="sendingObject"></param>
        public void SendData(IDomainObject sendingObject)
        {
            var message = sendingObject.ConvertToJson();
            _rabbitMqClient.SendMessageToQueue(message);
        }
    }
}