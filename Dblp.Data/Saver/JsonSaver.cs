using System.Collections.Generic;
using System.IO;
using Dblp.Data.Interfaces;
using Dblp.Data.Interfaces.Entities;
using Newtonsoft.Json;

namespace Dblp.Data.Saver
{
    public class JsonSaver : IConferenceStructurSaver
    {
        private readonly string _fileName;

        public JsonSaver(string fileName)
        {
            _fileName = fileName;
        }
        public bool SaveConferences(IEnumerable<Conference> conferences)
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
