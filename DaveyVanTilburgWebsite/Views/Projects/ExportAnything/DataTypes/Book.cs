using System;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything.DataTypes
{
    [Serializable]
    public class Book
    {
        public Book(string title, string body, DateTime creationDate)
        {
            Title = title;
            Body = body;
            CreationDate = creationDate;
        }

        public string Title { get; }
        public string Body { get; }
        public DateTime CreationDate { get; }
    }
}
