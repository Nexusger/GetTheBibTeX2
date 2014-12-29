using System.Linq;
using System.Xml.Linq;
using Temp.Abstract;
using Temp.Entities;

namespace Temp.Factories
{
    public class ProceedingFactory
    {
        private readonly IDblpRepository _repository;

        public ProceedingFactory(IDblpRepository repository)
        {
            _repository = repository;
        }

        public Proceeding CreateProceeding(XElement element)
        {
            var proceeding = new Proceeding(element);
            var efProceeding = _repository.Proceedings.FirstOrDefault(p => p.ProceedingKey == proceeding.ProceedingKey);
            if (efProceeding != null)
                return efProceeding;

            //TODO Hier müssen noch die Autoren gefunden werden
            return proceeding;
        }



    }
}
