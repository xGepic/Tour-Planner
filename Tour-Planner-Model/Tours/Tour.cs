namespace Tour_Planner_Model;

public class Tour
{
    public Guid Id { get; set; }
    public string? TourName { get; set; }
    public string? TourDescription { get; set; }
    public string? StartingPoint { get; set; }
    public string? Destination { get; set; }
    public TransportType TransportType { get; set; }
    public double TourDistance { get; set; }
    public uint EstimatedTimeInMin { get; set; }
    public TourType TourType { get; set; }
    public List<TourLog>? TourLogs { get; set; }
    public int Popularity => TourLogs is not null ? TourLogs.Count * 10 : 1;
    public int ChildFriendliness => TourDistance < 20 && EstimatedTimeInMin < 120 ? 1 : 2;
}