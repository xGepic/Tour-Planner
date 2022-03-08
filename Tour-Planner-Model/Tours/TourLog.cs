namespace Tour_Planner_Model;
public class TourLog
{
    public Guid Id { get; set; }
    public DateTime TourDateAndTime { get; set; }
    public string? TourComment { get; set; }
    public TourDifficulty TourDifficulty { get; set; }
    public uint TourTimeInMin { get; set; }
    public TourRating TourRating { get; set; }
    public Guid RelatedTourID { get; set; }
    public Tour? RelatedTour { get; set; }
}