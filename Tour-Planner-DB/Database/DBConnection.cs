namespace Tour_Planner_DB;

public partial class DBConnection
{
    private readonly NpgsqlConnection connection = new();
    public DBConnection(IConfiguration configuration)
    {
        string SqlSDataSource = configuration.GetConnectionString("DBConnection");
        connection = new(SqlSDataSource);
    }
    private void Open()
    {
        connection.Open();
    }
    private void Close()
    {
        connection.Close();
    }
    public bool CreateDatabase()
    {
        try
        {
            Open();
            NpgsqlCommand cmd = new("CREATE DATABASE tourplanner", connection);
            cmd.ExecuteNonQuery();
            Close();
            return true;
        }
        catch
        {
            Close();
            return false;
        }
    }
    public bool CreateTables()
    {
        try
        {
            Open();
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
                , connection);
            NpgsqlCommand createTourLogs = new("CREATE TABLE IF NOT EXISTS tourplanner.public.Tourlogs (" +
                "TourLogID uuid NOT NULL," +
                "TourDateAndTime DATE NOT NULL," +
                "TourComment varchar(150)," +
                "TourDifficulty int NOT NULL," +
                "TourTimeInMin int NOT NULL," +
                "TourRating int NOT NULL," +
                "RelatedTourID uuid NOT NULL);"
                , connection);
            createTours.ExecuteNonQuery();
            createTourLogs.ExecuteNonQuery();
            Close();
            return true;
        }
        catch
        {
            Close();
            return false;
        }
    }
}