using System;
using System.Net.Mime;
using System.Text;
using MasterSystem.DataContracts;
using MasterSystem.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MasterSystem.Classes
{
    public class RabbitMqSender
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

        public RabbitMqSender(string exchangeName, string queueName, string routingKey)
        {
            ExchangeName = exchangeName;
            QueueName = queueName;
            RoutingKey = routingKey;
        }

        public RabbitMqSender(string queueName) : this(
            String.Empty, 
            queueName, 
            string.Empty)
        {
        }

        /// <summary>
        /// Создание подключение к серверу RabbitMQ
        /// </summary>
        /// <returns>Соединение</returns>
        private IConnection GetRabbitMqConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = "localhost", UserName = "guest", Password = "guest"
            };

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
        /// <param name="message">Отправляемое сообщение</param>
        public void SendMessageToQueue(string message)
        {
            var model = PrepareModel();
            IBasicProperties basicProperties = model.CreateBasicProperties();
            basicProperties.Persistent = true;
            byte[] payload = Encoding.UTF8.GetBytes(message);
            PublicationAddress address = new PublicationAddress(ExchangeType.Topic, ExchangeName, RoutingKey);

            model.BasicPublish(address, basicProperties, payload);
        }

        /// <summary>
        /// Выполняет подписку на очередь
        /// </summary>
        /// <param name="domainLogic">Бизнес-логика</param>
        public void SubscribeOnQueue(DomainLogic domainLogic)
        {
            IConnection connection = GetRabbitMqConnection();
            IModel model = connection.CreateModel();

            model.BasicQos(0, 1, false);

            MessageReceiver messageReceiver = new MessageReceiver(model, domainLogic);

            model.BasicConsume(QueueName, false, messageReceiver);
        }
    }

    public class MessageReceiver : DefaultBasicConsumer
    {
        private readonly IModel _channel;
        private readonly DomainLogic _domainLogic;
        

        public MessageReceiver(IModel channel, DomainLogic domainLogic)
        {
            _channel = channel;
            _domainLogic = domainLogic;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered,
            string exchange, string routingKey,
            IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            string message = Encoding.UTF8.GetString(body.ToArray());

            Application application = JsonConvert.DeserializeObject<Application>(message);
            
            _domainLogic.SaveApplication(application);
            _channel.BasicAck(deliveryTag, false);
        }
    }
}