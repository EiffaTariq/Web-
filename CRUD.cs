using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Text.Json;
namespace FlightBookingSystem
{
    public class CRUD
    {
        public void AddFlight(Flight flight)
        {

            string connString = @"Data Source=(localdb)\\ProjectModels;Initial Catalog=FlightBooking;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            SqlConnection sqlConnection = new SqlConnection(connString);
            Console.WriteLine("Enter flight number");
            flight.FlightNumber = Console.ReadLine();
            Console.WriteLine("Enter destination");
            flight.Destination = Console.ReadLine();
            Console.WriteLine("Enter departure");
            flight.Departure = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter arrival");
            flight.Arrival = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter price");
            flight.Price = decimal.Parse(Console.ReadLine());
            sqlConnection.Open();
            string query = @"INSERT INTO Flights ( '{flightNumber}', '{destination}','{departure}','{arrival}','{price}') VALUES ('{flight.FlightNumber}', '{flight.Destination}', '{flight.Departure}', '{flight.Arrival}','{flight.Price}')";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            try
            {
                cmd.ExecuteNonQuery(); // Execute the command
                Console.WriteLine("Flight inserted successfully.");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("An error occurred while inserting the patient: " + sqlEx.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public void updateFlight(Flight flight)
        {
            string connString = @"Data Source=(localdb)\\ProjectModels;Initial Catalog=FlightBooking;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            SqlConnection sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();

            string query = $"UPDATE Flights set Id = '{flight.ID}', flightNumber = '{flight.flightNumber}',destination = '{flight.destination}',departure = '{flight.departure}',arrival = '{flight.arrival}',price = '{flight.price}'";
            //"UPDATE patient SET name = @Name, email = @Email, disease = @Disease WHERE patientId = @PatientId";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            try
            {
                cmd.ExecuteNonQuery(); // Execute the command
                Console.WriteLine("Flight updated successfully.");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("An error occurred while inserting the patient: " + sqlEx.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void deleteFlight(Flight flight)
        {
            string connString = @"Data Source=(localdb)\\ProjectModels;Initial Catalog=FlightBooking;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                sqlConnection.Open();
                Console.WriteLine("Enter id");
                flight.Id = int.Parse(Console.ReadLine());
                // Corrected query using parameterized query
                string query = "DELETE FROM Flights WHERE Id = '{flight.Id}'";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {

                    try
                    {
                        int count = sqlCommand.ExecuteNonQuery(); // Execute the command

                        if (count > 0)
                        {
                            Console.WriteLine("Patient deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No patient found with the given ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while deleting the patient");
                    }
                }
            }
        }

        public List<Flight> getAllFlights()
        {
            List<Flight> flights = new List<Flight>();

            string connString = @"Data Source=(localdb)\\ProjectModels;Initial Catalog=FlightBooking;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                sqlConnection.Open();

                string query = "SELECT Id, flightNumber, destination, departure, arrival, price FROM Flights";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight
                            {
                                Id = reader.GetInt32(0),
                                FlightNumber = reader.GetString(1),
                                Destination = reader.GetString(2),
                                Departure = reader.GetDateTime(3), 
                                Arrival = reader.GetDateTime(4), 
                                Price = reader.GetDecimal(5) 
                            };

                            flights.Add(flight);
                        }
                    }
                }
            }

            return flights;
        }

        public void saveFlightsToJson()
        {
            List<Flight>Flights = getAllFlights();
            //Flight flight = new Flight();

            string path = $"D:JsonFile.txt";
            FileStream f = new FileStream(path, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                string JsonFlight = JsonSerializer.Serialize(Flights[0]);
                for(int i = 0; i < Flights.Capacity; i++)
                {
                    writer.Write(JsonFlight);
                }
                //writer.WriteLine(JsonFlight);
                writer.Close();
            }
        }
    }
}



//Product p = new Product();
//p.ID = 1;
//p.Name = "Test";
//p.Description = "Some description";
//using (StreamWriter sw = new StreamWriter("lala.txt", append: true))
//{
//    string jsonProduct = JsonSerializer.Serialize(p);
//    sw.WriteLine(jsonProduct);
//    sw.Close();
//}


//using (SqlDataReader r = cmd.ExecuteReader())
//{
//    while (r.Read())
//    {
//        Console.WriteLine(r.GetString(0));
//        Console.WriteLine(r.GetString(1));
//        Console.WriteLine(r.GetString(2));
//    }
//}