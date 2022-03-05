namespace Tour_Planner_Model;

public class Tour
{
    public string? TourName { get; set; }
    public string? TourDescription { get; set; }
    public string? StartingPoint { get; set; }
    public string? Destination { get; set; }
    public TransportType TransportType { get; set; }
    public double TourDistance { get; set; }
    public uint EstimatedTimeInMin { get; set; }
    public Tourtype TourType { get; set; }
    public List<TourLog>? TourLogs { get; set; }
    public int Popularity
    {
        get
        {
            if (TourLogs is null)
            {
                return 0;
            }
            if (TourLogs.Count >= 10)
            {
                return 100;
            }
            return 10 * TourLogs.Count;
        }
    }
    public int ChildFriendliness
    {
        get
        {
            if (TourDistance < 20 || EstimatedTimeInMin < 120)
            {
                return 1;
            }
            if (TourDistance < 50 || EstimatedTimeInMin < 180)
            {
                return 2;
            }
            return 3;
        }
    }
}