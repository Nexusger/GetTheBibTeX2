
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Temp.Abstract;
using Temp.Entities;

namespace Temp.Factories
{
    public class InProceedingsFactory
    {
        private readonly IDblpRepository _repository;

        public InProceedingsFactory(IDblpRepository repository)
        {
            _repository = repository;
        }

        public Inproceeding CreateInproceeding(XElement element)
        {
            var proceedingKey = element.Attribute("key").Value;
            var proceeding = _repository.Proceedings.FirstOrDefault(i => i.ProceedingKey == proceedingKey);
            if (proceeding == null)
            {
                proceeding = new Proceeding() {ProceedingKey = proceedingKey};
            }
            var inproceeding = new Inproceeding(element)
            {
                Proceeding = proceeding
            };
            var efinproceeding = _repository.Inproceedings.FirstOrDefault(i => i.InproceedingKey == inproceeding.InproceedingKey);
            
            if (efinproceeding != null)
            {
                return efinproceeding;
            }
            return inproceeding;
        }
    }
}
