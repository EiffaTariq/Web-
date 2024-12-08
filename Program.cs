// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FlightBookingSystem;
using Microsoft.Data.SqlClient;
using System.Numerics;
namespace FlightBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
           CRUD c = new CRUD();
           Flight f = new Flight();
           c.AddFlight(f);
        }

    }
}




