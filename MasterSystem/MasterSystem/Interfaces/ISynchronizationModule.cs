namespace MasterSystem.Interfaces
{
    public interface ISynchronizationModule
    {
        /// <summary>
        /// Осуществляет отправку данных в другую систему
        /// </summary>
        /// <param name="sendingObject">Отправляемый объект</param>
        void SendData(IDomainObject sendingObject);
        
        /// <summary>
        /// Осуществляет прием данных
        /// </summary>
        void RecieveData();
    }
}