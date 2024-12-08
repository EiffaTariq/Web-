using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Data.SqlClient;
namespace FlightBookingSystem
{
    public class Flight
    {
        public int Id;
        public string flightNumber;
        public string destination;
        public DateTime departure;
        public DateTime arrival;
        public decimal price;
        public int ID
            { get; set; }
        public string FlightNumber 
            { get; set; }
        public string Destination
            { get; set; }
        public DateTime Departure
        {
            get { return departure; }
            set { departure = value; } 
        }
        public DateTime Arrival
            { get; set; }
        public decimal Price
        {
            get { return price; }
            set { price = value; } 
        }
    }
}
