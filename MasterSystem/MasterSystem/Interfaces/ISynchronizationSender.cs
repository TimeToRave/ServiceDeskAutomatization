namespace MasterSystem.Interfaces
{
    public interface ISynchronizationSender
    {
        /// <summary>
        /// Осуществляет отправку данных в другую систему
        /// </summary>
        /// <param name="sendingObject">Отправляемый объект</param>
        void SendData(IDomainObject sendingObject);
    }
}