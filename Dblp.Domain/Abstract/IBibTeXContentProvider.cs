namespace Dblp.Domain.Abstract
{
    public interface IBibTeXContentProvider
    {
        byte[] GetBibTexFileBytes(BibTeXContentOptions options);
    }
}