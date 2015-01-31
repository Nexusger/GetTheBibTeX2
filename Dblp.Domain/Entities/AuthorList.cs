using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dblp.Domain.Entities
{
    [CollectionDataContract]
    public class AuthorList:List<Author>
    {
    }
}
