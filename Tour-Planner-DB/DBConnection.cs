namespace Tour_Planner_DB;

public class DBConnection
{
    private readonly NpgsqlConnection connection = new();
    private readonly IConfiguration _configuration;
    public DBConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        string SqlSDataSource = _configuration.GetConnectionString("DBConnection");
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
    public IEnumerable<TourLog> GetAllTourLogs()
    {
        Open();
        List<TourLog> list = new();
        NpgsqlCommand cmd = new("SELECT * FROM Tourlog", connection);
        NpgsqlDataReader myDataReader = cmd.ExecuteReader();
        if (myDataReader is not null)
        {
            while (myDataReader.Read())
            {
                list.Add(new TourLog(
                    myDataReader.GetGuid(0),
                    myDataReader.GetDateTime(2),
                    myDataReader.GetString(3),
                    (TourDifficulty)myDataReader.GetInt32(4),
                    Convert.ToUInt32(myDataReader.GetInt32(5)),
                    (TourRating)myDataReader.GetInt32(6)
                    ));
            }
            Close();
            return list;
        }
        Close();
        return new List<TourLog>();
    }
    public TourLog? GetTourLogByID(Guid id)
    {
        Open();
        NpgsqlCommand cmd = new("SELECT * FROM TourLOG WHERE TourLogID = @myid", connection);
        cmd.Parameters.AddWithValue("myid", id);
        NpgsqlDataReader myDataReader = cmd.ExecuteReader();
        if (myDataReader.HasRows)
        {
            myDataReader.Read();
            TourLog temp = new(myDataReader.GetGuid(0),
                myDataReader.GetDateTime(2),
                myDataReader.GetString(3),
                (TourDifficulty)myDataReader.GetInt32(4),
                Convert.ToUInt32(myDataReader.GetInt32(5)),
                (TourRating)myDataReader.GetInt32(6)
                );
            Close();
            return temp;
        }
        Close();
        return null;
    }
}