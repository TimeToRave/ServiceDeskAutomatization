using System.Runtime.Serialization;
using MasterSystem.Interfaces;
using Newtonsoft.Json;

namespace MasterSystem.DataContracts
{
    [DataContract]
    public class Application : IDomainObject
    {
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Problem { get; set; }
        [DataMember]
        public string Contact { get; set; }
        [DataMember]
        public Service Service { get; set; }
        
        public string ConvertToJson()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}