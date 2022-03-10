namespace Tour_Planner_DB;

public partial class DBConnection
{
    public IEnumerable<TourLog>? GetAllTourLogs()
    {
        Open();
        List<TourLog> list = new();
        NpgsqlCommand cmd = new("SELECT * FROM Tourlog", defaultConnection);
        NpgsqlDataReader myDataReader = cmd.ExecuteReader();
        if (myDataReader is not null)
        {
            while (myDataReader.Read())
            {
                list.Add(new TourLog()
                {
                    Id = myDataReader.GetGuid(0),
                    TourDateAndTime = myDataReader.GetDateTime(2),
                    TourComment = myDataReader.GetString(3),
                    TourDifficulty = (TourDifficulty)myDataReader.GetInt32(4),
                    TourTimeInMin = Convert.ToUInt32(myDataReader.GetInt32(5)),
                    TourRating = (TourRating)myDataReader.GetInt32(6)
                });
            }
        }
        Close();
        return list;
    }
    public TourLog? GetTourLogByID(Guid id)
    {
        Open();
        NpgsqlCommand cmd = new("SELECT * FROM TourLOG WHERE TourLogID = @myid", defaultConnection);
        cmd.Parameters.AddWithValue("myid", id);
        NpgsqlDataReader myDataReader = cmd.ExecuteReader();
        TourLog? temp = null;
        if (myDataReader.HasRows)
        {
            myDataReader.Read();
            temp = new()
            {
                Id = myDataReader.GetGuid(0),
                TourDateAndTime = myDataReader.GetDateTime(2),
                TourComment = myDataReader.GetString(3),
                TourDifficulty = (TourDifficulty)myDataReader.GetInt32(4),
                TourTimeInMin = Convert.ToUInt32(myDataReader.GetInt32(5)),
                TourRating = (TourRating)myDataReader.GetInt32(6)
            };
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
            "VALUES (@TourDateAndTime, @TourComment, @TourDifficulty, @TourTimeInMin, @TourRating)", defaultConnection);
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
            "WHERE tourlogid = @logid;", defaultConnection);
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
        NpgsqlCommand cmd = new("DELETE FROM TourLog WHERE tourlogid = @logid;", defaultConnection);
        cmd.Parameters.AddWithValue("logid", id);
        cmd.ExecuteScalar();
        Close();
        return true;
    }
}
