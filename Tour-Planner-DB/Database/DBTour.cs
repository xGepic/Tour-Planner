namespace Tour_Planner_DB;

public partial class DBConnection
{
    public IEnumerable<Tour>? GetAllTours()
    {
        Open();
        List<Tour> list = new();
        NpgsqlCommand cmd = new("SELECT * FROM Tours", defaultConnection);
        NpgsqlDataReader myDataReader = cmd.ExecuteReader();
        if (myDataReader is not null)
        {
            while (myDataReader.Read())
            {
                list.Add(new Tour()
                {
                    Id = myDataReader.GetGuid(0),
                    TourName = myDataReader.GetString(1),
                    TourDescription = myDataReader.GetString(2),
                    StartingPoint = myDataReader.GetString(3),
                    Destination = myDataReader.GetString(4),
                    TransportType = (TransportType)myDataReader.GetInt32(5),
                    TourDistance = myDataReader.GetDouble(6),
                    EstimatedTimeInMin = Convert.ToUInt32(myDataReader.GetInt32(7)),
                    TourType = (Tourtype)myDataReader.GetInt32(8)
                });
            }
        }
        Close();
        return list;
    }
    public Tour? GetTourByID(Guid id)
    {
        Open();
        NpgsqlCommand cmd = new("SELECT * FROM Tours WHERE TourID = @myid", defaultConnection);
        cmd.Parameters.AddWithValue("myid", id);
        NpgsqlDataReader myDataReader = cmd.ExecuteReader();
        Tour? temp = null;
        if (myDataReader.HasRows)
        {
            myDataReader.Read();
            temp = new()
            {
                Id = myDataReader.GetGuid(0),
                TourName = myDataReader.GetString(1),
                TourDescription = myDataReader.GetString(2),
                StartingPoint = myDataReader.GetString(3),
                Destination = myDataReader.GetString(4),
                TransportType = (TransportType)myDataReader.GetInt32(5),
                TourDistance = myDataReader.GetDouble(6),
                EstimatedTimeInMin = Convert.ToUInt32(myDataReader.GetInt32(7)),
                TourType = (Tourtype)myDataReader.GetInt32(8)
            };
        }
        Close();
        return temp;
    }
    public bool AddTour(Tour item)
    {
        Open();
        NpgsqlCommand cmd = new("INSERT INTO Tours (TourID, TourName, TourDescription, TourStartingPoint, TourDestination, TourTransportType, TourDistance, TourTimeInMin, TourType) " +
            "VALUES (@TourID, @TourName, @TourDescription, @TourStartingPoint, @TourDestination, @TourTransportType, @TourDistance, @TourTimeInMin, @TourType)", defaultConnection);
        cmd.Parameters.AddWithValue("TourID", item.Id);
        cmd.Parameters.AddWithValue("TourName", item.TourName ?? "");
        cmd.Parameters.AddWithValue("TourDescription", item.TourDescription ?? "");
        cmd.Parameters.AddWithValue("TourStartingPoint", item.StartingPoint ?? "");
        cmd.Parameters.AddWithValue("TourDestination", item.Destination ?? "");
        cmd.Parameters.AddWithValue("TourTransportType", (int)item.TransportType);
        cmd.Parameters.AddWithValue("TourDistance", item.TourDistance);
        cmd.Parameters.AddWithValue("TourTimeInMin", Convert.ToInt64(item.EstimatedTimeInMin));
        cmd.Parameters.AddWithValue("TourType", (int)item.TourType);
        cmd.ExecuteReader();
        Close();
        return true;
    }
    public bool UpdateTour(Tour item)
    {
        Open();
        NpgsqlCommand cmd = new("UPDATE Tours SET TourName =  @Tourname, " +
            "TourDescription = @TourDescription, " +
            "TourStartingPoint = @TourStartingPoint, " +
            "TourDestination = @TourDestination, " +
            "TourTransportType = @TourTransportType, " +
            "TourDistance = @TourDistance, " +
            "TourTimeInMin = @TourTimeInMin, " +
            "TourType = @TourType WHERE TourID = @TourID;", defaultConnection);
        cmd.Parameters.AddWithValue("Tourname", item.TourName ?? "");
        cmd.Parameters.AddWithValue("TourDescription", item.TourDescription ?? "");
        cmd.Parameters.AddWithValue("TourStartingPoint", item.StartingPoint ?? "");
        cmd.Parameters.AddWithValue("TourDestination", item.Destination ?? "");
        cmd.Parameters.AddWithValue("TourTransportType", (int)item.TransportType);
        cmd.Parameters.AddWithValue("TourDistance", item.TourDistance);
        cmd.Parameters.AddWithValue("TourTimeInMin", Convert.ToInt64(item.EstimatedTimeInMin));
        cmd.Parameters.AddWithValue("TourType", (int)item.TourType);
        cmd.Parameters.AddWithValue("TourID", item.Id);
        cmd.ExecuteNonQuery();
        Close();
        return true;
    }
    public bool DeleteTour(Guid id)
    {
        Open();
        NpgsqlCommand cmd = new("DELETE FROM Tours WHERE TourID = @TourID", defaultConnection);
        NpgsqlCommand cmd2 = new("DELETE FROM TourLogs WHERE RelatedTourID = @RelatedTourID", defaultConnection);
        cmd.Parameters.AddWithValue("TourID", id);
        cmd2.Parameters.AddWithValue("RelatedTourID", id);
        cmd.ExecuteScalar();
        cmd2.ExecuteScalar();
        Close();
        return true;
    }
}