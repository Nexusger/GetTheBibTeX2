namespace Dblp.Domain.Interfaces
{
    public interface IBibTeXContentProvider
    {
        byte[] GetBibTexFileBytes(BibTeXContentOptions options);
    }
}