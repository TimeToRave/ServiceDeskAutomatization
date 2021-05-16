using System;
using System.Text;
using MasterSystem.Interfaces;
using RabbitMQ.Client;

namespace MasterSystem.Classes
{
    public class RabbitMqSynchronyzationModule : ISynchronizationModule
    {
        /// <summary>
        /// Название обменника
        /// </summary>
        private string ExchangeName { get; }

        /// <summary>
        /// Название очереди
        /// </summary>
        private string QueueName { get; }

        /// <summary>
        /// Ключ маршрутизации
        /// </summary>
        private string RoutingKey { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="exchangeName">Название обменника</param>
        /// <param name="queueName">Название очереди</param>
        /// <param name="routingKey">Ключ маршрутизации</param>
        public RabbitMqSynchronyzationModule(string exchangeName, string queueName, string routingKey)
        {
            ExchangeName = exchangeName;
            QueueName = queueName;
            RoutingKey = routingKey;
        }

        /// <summary>
        /// Выполняет отправку бизнес-объекта в RabbitMQ
        /// </summary>
        /// <param name="sendingObject"></param>
        public void SendData(IDomainObject sendingObject)
        {
            var message = sendingObject.ConvertToJson();
            var model = PrepareModel();
            SendMessageToQueue(model, message);
        }

        public void RecieveData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создание подключение к серверу RabbitMQ
        /// </summary>
        /// <returns>Соединение</returns>
        private IConnection GetRabbitMqConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = "localhost";
            connectionFactory.UserName = "guest";
            connectionFactory.Password = "guest";

            return connectionFactory.CreateConnection();
        }

        /// <summary>
        /// Подготавливает AMQP-модель
        /// </summary>
        /// <returns>Модель</returns>
        private IModel PrepareModel()
        {
            IConnection connection = GetRabbitMqConnection();
            IModel model = connection.CreateModel();

            model.QueueDeclare(QueueName, true, false, false, null);

            model.ExchangeDeclare(ExchangeName, ExchangeType.Topic, true);
            model.QueueBind(QueueName, ExchangeName, RoutingKey);
            return model;
        }

        /// <summary>
        /// Выполняет отправку сообщений в RabbitMQ
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="message">Отправляемое сообщение</param>
        private void SendMessageToQueue(IModel model, string message)
        {
            IBasicProperties basicProperties = model.CreateBasicProperties();
            basicProperties.Persistent = true;
            byte[] payload = Encoding.UTF8.GetBytes(message);
            PublicationAddress address = new PublicationAddress(ExchangeType.Topic, ExchangeName, RoutingKey);

            model.BasicPublish(address, basicProperties, payload);
        }
    }
}