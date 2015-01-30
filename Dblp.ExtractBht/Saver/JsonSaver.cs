using System.Collections.Generic;
using System.IO;
using Dblp.Domain.Entities;
using Dblp.ExtractBht.Interface;
using Newtonsoft.Json;

namespace Dblp.ExtractBht.Saver
{
    public class JsonSaver : IConferenceStructurSaver
    {
        private readonly string _fileName;

        public JsonSaver(string fileName)
        {
            _fileName = fileName;
        }
        public bool SaveConference(IEnumerable<Conference> conferences)
        {

            try
            {
                var serializedObject = JsonConvert.SerializeObject(conferences);
                File.WriteAllText(_fileName, serializedObject);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
