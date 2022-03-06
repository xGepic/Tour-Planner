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
        NpgsqlCommand myCommand = new("SELECT * FROM Tourlog", connection);
        NpgsqlDataReader myDataReader = myCommand.ExecuteReader();
        if (myDataReader is not null)
        {
            while (myDataReader.Read())
            {
                list.Add(new TourLog(
                    myDataReader.GetDateTime(1),
                    myDataReader.GetString(2),
                    (TourDifficulty)myDataReader.GetInt32(3),
                    Convert.ToUInt32(myDataReader.GetInt32(4)),
                    (TourRating)myDataReader.GetInt32(5)
                    ));
            }
            Close();
            return list;
        }
        Close();
        return new List<TourLog>();
    }
}