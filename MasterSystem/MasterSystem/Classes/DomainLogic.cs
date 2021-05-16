using MasterSystem.DataContracts;
using MasterSystem.Interfaces;

namespace MasterSystem.Classes
{
    public class DomainLogic
    {
        private readonly ISynchronizationModule SyncModule;
        private readonly IDataOperator DataOperator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dataOperator">Экземпляр класса, реализующий работу с данными</param>
        /// <param name="syncModule">Экземпляр класса, реализующий механизмы интеграции со сторонней системой</param>
        public DomainLogic(IDataOperator dataOperator, ISynchronizationModule syncModule)
        {
            DataOperator = dataOperator;
            SyncModule = syncModule;
        }

        /// <summary>
        /// Выполняет отправку обращения в стороннюю систему
        /// </summary>
        /// <param name="applicationNumber">Номер обращения</param>
        public void SendApplication(string applicationNumber)
        {
            Application application = DataOperator.GetApplication(applicationNumber);
            SyncModule.SendData(application);
        }
    }
}