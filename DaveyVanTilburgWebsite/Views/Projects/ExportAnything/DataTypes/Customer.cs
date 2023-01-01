using System;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything.DataTypes
{
    [Serializable]
    public class Customer
    {
        public Customer(string firstName, string lastName, DateTime birthDay, DateTime creationDate, string email, DateTime lastLoginDate, int purchaseCount, decimal totalPurchaseAmount)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDay = birthDay;
            CreationDate = creationDate;
            Email = email;
            LastLoginDate = lastLoginDate;
            PurchaseCount = purchaseCount;
            TotalPurchaseAmount = totalPurchaseAmount;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public DateTime BirthDay { get; }
        public DateTime CreationDate { get; }
        public string Email { get; }
        public DateTime LastLoginDate { get; }
        public int PurchaseCount { get; }
        public decimal TotalPurchaseAmount { get; }
    }
}