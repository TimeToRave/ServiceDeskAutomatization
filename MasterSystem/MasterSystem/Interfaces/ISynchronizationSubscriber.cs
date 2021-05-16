using MasterSystem.Classes;

namespace MasterSystem.Interfaces
{
    public interface ISynchronizationSubscriber
    {
        /// <summary>
        /// Осуществляет подписку на внешние события
        /// </summary>
        /// <param name="domainLogic">Ссылка на бизнес-логику</param>
        public void SubscribeOn(DomainLogic domainLogic);
    }
}