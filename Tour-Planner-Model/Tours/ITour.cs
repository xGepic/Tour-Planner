namespace Tour_Planner_Model;

public interface ITour
{
    public string TourName { get; set; }
    public string TourDescription { get; set; }
    public string StartingPoint { get; set; }
    public string Destination { get; set; }
    public TransportType TransportType { get; set; } 
    public double TourDistance { get; set; }
    public uint EstimatedTimeInMin { get; set; }
}
