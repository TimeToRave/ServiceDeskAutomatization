using MasterSystem.DataContracts;

namespace MasterSystem.Interfaces
{
    /// <summary>
    /// Описывает операции для работы с данными
    /// </summary>
    public interface IDataOperator
    {
        /// <summary>
        /// Возвращает обращение по его номеру 
        /// </summary>
        /// <param name="applicationNumber">Номер обращения</param>
        /// <returns>Обращение</returns>
        Application GetApplication(string applicationNumber);
    }
}