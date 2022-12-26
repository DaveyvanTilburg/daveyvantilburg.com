using System;

namespace DaveyVanTilburgWebsite.Views.Projects.ExportAnything.DataTypes
{
    [Serializable]
    public class Reservation
    {
        public Reservation(string restaurantName, DateTime reservationDate, string location)
        {
            RestaurantName = restaurantName;
            ReservationDate = reservationDate;
            Location = location;
        }

        public string RestaurantName { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Location { get; set; }
    }
}
