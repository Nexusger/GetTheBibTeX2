namespace Dblp.Data.Interfaces
{
    public interface IDblpBibTexRepository
    {
        string GetBibTex(string dblpKey);
    }
}
