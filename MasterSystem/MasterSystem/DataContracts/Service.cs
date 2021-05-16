using System;
using System.Runtime.Serialization;
using MasterSystem.Interfaces;
using Newtonsoft.Json;

namespace MasterSystem.DataContracts
{
    [DataContract]
    public class Service : IDomainObject
    {
        [DataMember]
        public string Number { get; set; }
        
        [DataMember]
        public DateTime StartDate { get; set; }
        
        [DataMember]
        public DateTime EndDate { get; set; }
        
        [DataMember]
        public int ReactionTime { get; set; }
        
        [DataMember]
        public int ResolvingTime { get; set; }
        
        public string ConvertToJson()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}