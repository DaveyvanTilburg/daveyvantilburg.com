using System;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything.DataTypes
{
    [Serializable]
    public class Book
    {
        public Book(string title, DateTime publicationDate, string author, string bindingType, int pageCount)
        {
            Title = title;
            PublicationDate = publicationDate;
            Author = author;
            BindingType = bindingType;
            PageCount = pageCount;
        }

        public string Title { get; }
        public DateTime PublicationDate { get; }
        public string Author { get; }
        public string BindingType { get; }
        public int PageCount { get; }
    }
}