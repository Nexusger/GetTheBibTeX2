using Dblp.Data.Interfaces.Entities;

namespace Dblp.Data.Saver
{
    public interface IDetailSaver
    {
        bool SaveLoadDetails(LoadDetails loadDetails);
    }
}
