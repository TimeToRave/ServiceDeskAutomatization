using MasterSystem.Interfaces;

namespace MasterSystem.Classes
{
   public class RabbitMqSynchronizationSender : ISynchronizationSender
    {
        private readonly RabbitMqSender _rabbitMqSender;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="exchangeName">Название обменника</param>
        /// <param name="queueName">Название очереди</param>
        /// <param name="routingKey">Ключ маршрутизации</param>
        public RabbitMqSynchronizationSender(string exchangeName, string queueName, string routingKey)
        {
            _rabbitMqSender = new RabbitMqSender(exchangeName, queueName, routingKey);
        }

        /// <summary>
        /// Выполняет отправку бизнес-объекта в RabbitMQ
        /// </summary>
        /// <param name="sendingObject"></param>
        public void SendData(IDomainObject sendingObject)
        {
            var message = sendingObject.ConvertToJson();
            _rabbitMqSender.SendMessageToQueue(message);
        }
    }
}