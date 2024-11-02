namespace HotelDatabase;

public class Program
{
    public static void Main(string[] args)
    {
        DBClient dbClient = new DBClient();

        Console.WriteLine("Connecting to the database...");
        dbClient.Start();

        Console.WriteLine("\n--- Testing CRUD Operations on Facilities ---\n");

        // 1. Create
        Console.WriteLine("Creating new facility 'Sauna'...");
        dbClient.CreateFacility("Sauna");

        // 2. Read
        Console.WriteLine("\nReading all facilities:");
        dbClient.ReadFacilities();

        // 3. Update
        Console.WriteLine("\nUpdating facility ID 1 to 'Swimming Pool'...");
        dbClient.UpdateFacility(1, "Swimming Pool");

        // Verify Update
        Console.WriteLine("\nVerifying update by reading all facilities:");
        dbClient.ReadFacilities();

        // 4. Delete
        Console.WriteLine("\nDeleting facility ID 2 (if exists)...");
        dbClient.DeleteFacility(2);

        // Verify Delete
        Console.WriteLine("\nVerifying delete by reading all facilities:");
        dbClient.ReadFacilities();

        Console.WriteLine("\n--- End of CRUD Operations Test ---");
    }
}