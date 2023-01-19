using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Configuration;
using System.Data;

namespace VehicleTracking.WebAPI.Models
{
    public class CarRepository
    {
        public static IEnumerable<Car> GetAll()
        {
            var connectionString = "Server=DESKTOP-AT3ON5G\\BORINCI; Database=MonoDB; User Id=IvanMono; Password=mono123;";
            var sqlQuery = "SELECT * FROM Car";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            var cars = new List<Car>();
            while (reader.Read())
            {
                cars.Add(new Car
                {
                    Id = reader.GetGuid(0),
                    Manufacturer = reader.GetString(1),
                    Model = reader.GetString(2),
                    YearOfProduction = reader.GetInt32(3)
                });
            }
            connection.Close();
            return cars;
        }

        public static void Add(Car newCar)
        {
            var connectionString = "Server=DESKTOP-AT3ON5G\\BORINCI; Database=MonoDB; User Id=IvanMono; Password=mono123;";
            var sqlQuery = "INSERT INTO Car (Id, Manufacturer, Model, YearOfProduction) VALUES (@Id, @Manufacturer, @Model, @YearOfProduction)";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@Id", newCar.Id);
            command.Parameters.AddWithValue("@Manufacturer", newCar.Manufacturer);
            command.Parameters.AddWithValue("@Model", newCar.Model);
            command.Parameters.AddWithValue("@YearOfProduction", newCar.YearOfProduction);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            connection.Close();
        }

        public static void Update(Guid id, Car updatedCar)
        {
            var connectionString = "Server=DESKTOP-AT3ON5G\\BORINCI; Database=MonoDB; User Id=IvanMono; Password=mono123;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            //var checkSqlQuery = "SELECT COUNT(*) FROM Car WHERE Id = @Id";
            //SqlCommand checkCommand = new SqlCommand(checkSqlQuery, connection);
            //checkCommand.Parameters.AddWithValue("@Id", updatedCar.Id);
            //int count = (int)checkCommand.ExecuteScalar();
            //if (count == 0)
            //{
            //    return;
            //}
            var sqlQuery = "UPDATE Car SET Manufacturer = @Manufacturer, Model = @Model, YearOfProduction = @YearOfProduction WHERE Id = @Id";
            SqlCommand updateCommand = new SqlCommand(sqlQuery, connection);
            updateCommand.Parameters.AddWithValue("@Manufacturer", updatedCar.Manufacturer);
            updateCommand.Parameters.AddWithValue("@Model", updatedCar.Model);
            updateCommand.Parameters.AddWithValue("@YearOfProduction", updatedCar.YearOfProduction);
            updateCommand.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = updateCommand;
            adapter.UpdateCommand.ExecuteNonQuery(); //exception: System.Data.SqlClient.SqlException: 'Must declare the scalar variable "@Id".'
            connection.Close();
        }

        public static void Delete(Guid id)
        {
            var connectionString = "Server=DESKTOP-AT3ON5G\\BORINCI; Database=MonoDB; User Id=IvanMono; Password=mono123;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            var deleteSqlQuery = "DELETE FROM Car WHERE Id = @Id";
            SqlCommand deleteCommand = new SqlCommand(deleteSqlQuery, connection);
            deleteCommand.Parameters.AddWithValue("@Id", id);
            deleteCommand.ExecuteNonQuery();
            connection.Close();
        }
    }
}