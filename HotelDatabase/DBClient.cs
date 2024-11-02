using System;
using System.Data.SqlClient;

namespace HotelDatabase;

public class DBClient
{
    // Opdateret connection string
    private string connectionString = "Server=127.0.0.1;Database=master;User Id=sa;Password=Xak35xzb;";
    public void Start()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Connected to the database");
        }
    }
    
    public void CreateFacility(string facilityName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Facility (Facility_Name) VALUES (@FacilityName)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FacilityName", facilityName);
                command.ExecuteNonQuery();
                Console.WriteLine($"Facility '{facilityName}' added.");
            }
        }
    }
    
    public void ReadFacilities()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Facility";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Facility ID: {reader["Facility_Id"]}, Name: {reader["Facility_Name"]}");
                }
            }
        }
    }
    
    public void UpdateFacility(int facilityId, string newFacilityName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE Facility SET Facility_Name = @NewFacilityName WHERE Facility_Id = @FacilityId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@NewFacilityName", newFacilityName);
                command.Parameters.AddWithValue("@FacilityId", facilityId);
                command.ExecuteNonQuery();
                Console.WriteLine($"Facility ID {facilityId} updated to '{newFacilityName}'.");
            }
        }
    }
    
    public void DeleteFacility(int facilityId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
        
            // Først slet referencer fra HotelFacility-tabellen
            string deleteHotelFacilityQuery = "DELETE FROM HotelFacility WHERE Facility_Id = @FacilityId";
            using (SqlCommand deleteHotelFacilityCommand = new SqlCommand(deleteHotelFacilityQuery, connection))
            {
                deleteHotelFacilityCommand.Parameters.AddWithValue("@FacilityId", facilityId);
                deleteHotelFacilityCommand.ExecuteNonQuery();
                Console.WriteLine($"Deleted references for Facility ID {facilityId} in HotelFacility.");
            }

            // Derefter slet faciliteten fra Facility-tabellen
            string deleteFacilityQuery = "DELETE FROM Facility WHERE Facility_Id = @FacilityId";
            using (SqlCommand deleteFacilityCommand = new SqlCommand(deleteFacilityQuery, connection))
            {
                deleteFacilityCommand.Parameters.AddWithValue("@FacilityId", facilityId);
                deleteFacilityCommand.ExecuteNonQuery();
                Console.WriteLine($"Facility ID {facilityId} deleted.");
            }
        }
    }
    
}




