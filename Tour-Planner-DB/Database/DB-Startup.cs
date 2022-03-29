namespace Tour_Planner_DB;

public class DB_Startup
{
    private readonly NpgsqlConnection defaultConnection = new();
    private readonly NpgsqlConnection startupConnection = new();
    public DB_Startup(IConfiguration config)
    {
        defaultConnection = new(config.GetConnectionString("DefaultConnection"));
        startupConnection = new(config.GetConnectionString("StartUpConnection"));
    }
    public bool CreateDatabase()
    {
        try
        {
            startupConnection.Open();
            NpgsqlCommand cmd = new("CREATE DATABASE tourplanner", startupConnection);
            cmd.ExecuteNonQuery();
            startupConnection.Close();
            return true;
        }
        catch
        {
            startupConnection.Close();
            return false;
        }
    }
    public bool CreateTables()
    {
        try
        {
            defaultConnection.Open();
            NpgsqlCommand createTours = new("CREATE TABLE IF NOT EXISTS tourplanner.public.Tours (" +
                "TourID uuid NOT NULL," +
                "TourName varchar(30) NOT NULL," +
                "TourDescription varchar(150)," +
                "TourStartingPoint varchar(30) NOT NULL," +
                "TourDestination varchar(30) NOT NULL," +
                "TourTransportType int NOT NULL," +
                "TourDistance float NOT NULL," +
                "TourTimeInMin int NOT NULL," +
                "TourType int NOT NULL);"
                , defaultConnection);
            NpgsqlCommand createTourLogs = new("CREATE TABLE IF NOT EXISTS tourplanner.public.Tourlogs (" +
                "TourLogID uuid NOT NULL," +
                "TourDateAndTime DATE NOT NULL," +
                "TourComment varchar(150)," +
                "TourDifficulty int NOT NULL," +
                "TourTimeInMin int NOT NULL," +
                "TourRating int NOT NULL," +
                "RelatedTourID uuid NOT NULL);"
                , defaultConnection);
            createTours.ExecuteNonQuery();
            createTourLogs.ExecuteNonQuery();
            defaultConnection.Close();
            return true;
        }
        catch
        {
            defaultConnection.Close();
            return false;
        }
    }
}
