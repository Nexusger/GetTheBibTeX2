using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dblp.Data.Interfaces.Entities
{
    [CollectionDataContract]
    public class AuthorList:List<Author>
    {
    }
}
