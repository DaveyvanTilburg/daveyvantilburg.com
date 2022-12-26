using System;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything.DataTypes
{
    [Serializable]
    public class Customer
    {
        public Customer(string firstName, string lastName, DateTime birthDay)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDay = birthDay;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
