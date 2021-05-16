using System.Linq;
using MasterSystem.DataContracts;
using MasterSystem.Interfaces;
using Newtonsoft.Json;

namespace MasterSystem.Classes
{
    public class FileDataOperator : IDataOperator
    {
        private string InputApplicationFile { get; }
        private string InputServiceFile { get; }

        public FileDataOperator(string inputApplicationFile, string inputServiceFile)
        {
            InputApplicationFile = inputApplicationFile;
            InputServiceFile = inputServiceFile;
        }
        public Application GetApplication(string applicationNumber)
        {
            FileOperator fileOperator = new FileOperator();
            string rawApplications = fileOperator.ReadTextFromFile(InputApplicationFile);
            Application [] applicationCollection = JsonConvert.DeserializeObject<Application[]>(rawApplications);

            Application result = applicationCollection.FirstOrDefault(app => app.Number == applicationNumber);
            return result;
        }
    } 
}