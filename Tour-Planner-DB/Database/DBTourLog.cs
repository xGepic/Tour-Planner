namespace Tour_Planner_DB;

public class DBTourLog
{
    private readonly static DBTourLog DBInstance = new();
    private static NpgsqlConnection defaultConnection = new();
    private DBTourLog()
    {

    }
    private static void Open()
    {
        defaultConnection.Open();
    }
    private static void Close()
    {
        defaultConnection.Close();
    }
    public static DBTourLog GetInstance(IConfiguration config)
    {
        string SqlSDataSource = config.GetConnectionString("DefaultConnection");
        defaultConnection = new(SqlSDataSource);
        return DBInstance;
    }
    public IEnumerable<TourLog>? GetAllTourLogs(Guid id)
    {
        Open();
        List<TourLog> list = new();
        NpgsqlCommand cmd = new("SELECT * FROM Tourlogs WHERE RelatedTourID = @myid", defaultConnection);
        cmd.Parameters.AddWithValue("myid", id);
        NpgsqlDataReader myDataReader = cmd.ExecuteReader();
        if (myDataReader is not null)
        {
            while (myDataReader.Read())
            {
                list.Add(new TourLog()
                {
                    Id = myDataReader.GetGuid(0),
                    TourDateAndTime = myDataReader.GetDateTime(1),
                    TourComment = myDataReader.GetString(2),
                    TourDifficulty = (TourDifficulty)myDataReader.GetInt32(3),
                    TourTimeInMin = Convert.ToUInt32(myDataReader.GetInt32(4)),
                    TourRating = (TourRating)myDataReader.GetInt32(5),
                    RelatedTourID = myDataReader.GetGuid(6)
                });
            }
        }
        Close();
        return list;
    }
    public TourLog? GetTourLogByID(Guid id)
    {
        Open();
        NpgsqlCommand cmd = new("SELECT * FROM TourLogs WHERE TourLogID = @myid", defaultConnection);
        cmd.Parameters.AddWithValue("myid", id);
        NpgsqlDataReader myDataReader = cmd.ExecuteReader();
        TourLog? temp = null;
        if (myDataReader.HasRows)
        {
            myDataReader.Read();
            temp = new()
            {
                Id = myDataReader.GetGuid(0),
                TourDateAndTime = myDataReader.GetDateTime(1),
                TourComment = myDataReader.GetString(2),
                TourDifficulty = (TourDifficulty)myDataReader.GetInt32(3),
                TourTimeInMin = Convert.ToUInt32(myDataReader.GetInt32(4)),
                TourRating = (TourRating)myDataReader.GetInt32(5),
                RelatedTourID = myDataReader.GetGuid(6)
            };
        }
        Close();
        return temp;
    }
    public bool AddTourLog(TourLog item)
    {
        Open();
        NpgsqlCommand cmd = new("INSERT INTO TourLogs (TourLogId, TourDateAndTime, TourComment, TourDifficulty, TourTimeInMin, TourRating, RelatedTourID) " +
            "VALUES (@TourLogId, @TourDateAndTime, @TourComment, @TourDifficulty, @TourTimeInMin, @TourRating, @RelatedTourID)", defaultConnection);
        cmd.Parameters.AddWithValue("TourLogId", item.Id);
        cmd.Parameters.AddWithValue("TourDateAndTime", item.TourDateAndTime);
        cmd.Parameters.AddWithValue("TourComment", item.TourComment ?? "");
        cmd.Parameters.AddWithValue("TourDifficulty", (int)item.TourDifficulty);
        cmd.Parameters.AddWithValue("TourTimeInMin", Convert.ToInt64(item.TourTimeInMin));
        cmd.Parameters.AddWithValue("TourRating", (int)item.TourRating);
        cmd.Parameters.AddWithValue("RelatedTourID", item.RelatedTourID);
        cmd.ExecuteReader();
        Close();
        return true;
    }
    public bool UpdateTourLog(TourLog item)
    {
        Open();
        NpgsqlCommand cmd = new("UPDATE TourLogs SET TourDateAndTime = @TourDateAndTime, " +
            "TourComment = @TourComment, " +
            "TourDifficulty = @TourDifficulty, " +
            "TourTimeInMin = @TourTimeInMin, " +
            "TourRating = @TourRating , " +
            "RelatedTourID = @RelatedTourID WHERE tourlogid = @logid;", defaultConnection);
        cmd.Parameters.AddWithValue("TourDateAndTime", item.TourDateAndTime);
        cmd.Parameters.AddWithValue("TourComment", item.TourComment ?? "");
        cmd.Parameters.AddWithValue("TourDifficulty", (int)item.TourDifficulty);
        cmd.Parameters.AddWithValue("TourTimeInMin", Convert.ToInt64(item.TourTimeInMin));
        cmd.Parameters.AddWithValue("TourRating", (int)item.TourRating);
        cmd.Parameters.AddWithValue("RelatedTourID", item.RelatedTourID);
        cmd.Parameters.AddWithValue("logid", item.Id);
        cmd.ExecuteNonQuery();
        Close();
        return true;
    }
    public bool DeleteTourLog(Guid id)
    {
        Open();
        NpgsqlCommand cmd = new("DELETE FROM TourLogs WHERE tourlogid = @logid;", defaultConnection);
        cmd.Parameters.AddWithValue("logid", id);
        cmd.ExecuteScalar();
        Close();
        return true;
    }
    public bool CheckRelatedTourID(Guid checkID)
    {
        Open();
        NpgsqlCommand cmd = new("SELECT * FROM Tours WHERE TourID = @myID", defaultConnection);
        cmd.Parameters.AddWithValue("myID", checkID);
        object? response = cmd.ExecuteScalar();
        Close();
        return response is not null;
    }
}
