namespace Temp.Entities
{
    public enum PublicationTypeEnum
    {
        /// <summary>
        /// An article from a journal or magazine
        /// </summary>
        Article,
        /// <summary>
        /// A paper in a conference or workshop proceedings
        /// </summary>
        Inproceeding,
        /// <summary>
        /// The proceedings volume of a conference or workshop
        /// </summary>
        Proceeding,
        /// <summary>
        /// An authored monograph or an edited collection of articles
        /// </summary>
        Book,
        /// <summary>
        /// A part or chapter in a monograph
        /// </summary>
        Incollection,
        /// <summary>
        /// A PhD thesis
        /// </summary>
        PhdThesis,
        /// <summary>
        /// A Master's thesis. There are only very few Master's theses in dblp
        /// </summary>
        MasterThesis,
        /// <summary>
        /// A web page. There are only very few web pages in dblp
        /// </summary>
        Www

    }
}