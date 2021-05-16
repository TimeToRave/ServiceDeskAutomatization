namespace MasterSystem.Interfaces
{
    public interface IDomainObject
    {
        /// <summary>
        /// Конвертация обхекта в JSON объект
        /// </summary>
        /// <returns></returns>
        public string ConvertToJson();
    }
}