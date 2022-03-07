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
    public IEnumerable<TourLog>? GetAllTourLogs()
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
        }
        Close();
        return list;
    }
    public TourLog? GetTourLogByID(Guid id)
    {
        Open();
        NpgsqlCommand cmd = new("SELECT * FROM TourLOG WHERE TourLogID = @myid", connection);
        cmd.Parameters.AddWithValue("myid", id);
        NpgsqlDataReader myDataReader = cmd.ExecuteReader();
        TourLog? temp = null;
        if (myDataReader.HasRows)
        {
            myDataReader.Read();
            temp = new(myDataReader.GetGuid(0),
                myDataReader.GetDateTime(2),
                myDataReader.GetString(3),
                (TourDifficulty)myDataReader.GetInt32(4),
                Convert.ToUInt32(myDataReader.GetInt32(5)),
                (TourRating)myDataReader.GetInt32(6)
                );
        }
        Close();
        return temp;
    }
    public bool AddTourLog(TourLog item)
    {
        if (item.TourComment is null)
        {
            item.TourComment = "";
        }
        Open();
        NpgsqlCommand cmd = new("INSERT INTO TourLog (TourDateAndTime, TourComment, TourDifficulty, TourTimeInMin, TourRating) " +
            "VALUES (@TourDateAndTime, @TourComment, @TourDifficulty, @TourTimeInMin, @TourRating)", connection);
        cmd.Parameters.AddWithValue("TourDateAndTime", item.TourDateAndTime);
        cmd.Parameters.AddWithValue("TourComment", item.TourComment);
        cmd.Parameters.AddWithValue("TourDifficulty", (int)item.TourDifficulty);
        cmd.Parameters.AddWithValue("TourTimeInMin", Convert.ToInt64(item.TourTimeInMin));
        cmd.Parameters.AddWithValue("TourRating", (int)item.TourRating);
        cmd.ExecuteReader();
        Close();
        return true;
    }
    public bool UpdateTourLog(TourLog item)
    {
        if (item.TourComment is null)
        {
            item.TourComment = "";
        }
        Open();
        NpgsqlCommand cmd = new("UPDATE TourLog SET TourDateAndTime =" +
            " @TourDateAndTime, TourComment = @TourComment, TourDifficulty = @TourDifficulty, TourTimeInMin = @TourTimeInMin, TourRating = @TourRating " +
            "WHERE tourlogid = @logid;", connection);
        cmd.Parameters.AddWithValue("TourDateAndTime", item.TourDateAndTime);
        cmd.Parameters.AddWithValue("TourComment", item.TourComment);
        cmd.Parameters.AddWithValue("TourDifficulty", (int)item.TourDifficulty);
        cmd.Parameters.AddWithValue("TourTimeInMin", Convert.ToInt64(item.TourTimeInMin));
        cmd.Parameters.AddWithValue("TourRating", (int)item.TourRating);
        cmd.Parameters.AddWithValue("logid", item.Id);
        cmd.ExecuteNonQuery();
        Close();
        return true;
    }
    public bool DeleteTourLog(Guid id)
    {
        Open();
        NpgsqlCommand cmd = new("DELETE FROM TourLog WHERE tourlogid = @logid;", connection);
        cmd.Parameters.AddWithValue("logid", id);
        cmd.ExecuteScalar();
        Close();
        return true;
    }
}