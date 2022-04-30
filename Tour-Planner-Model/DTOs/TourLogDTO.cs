namespace Tour_Planner_Model;

public record TourLogDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime TourDateAndTime { get; init; }
    public string? TourComment { get; init; }
    [Required, Range(0, 4)]
    public uint TourDifficulty { get; init; }
    [Required]
    public uint TourTimeInMin { get; init; }
    [Required, Range(0, 4)]
    public int TourRating { get; init; }
    [Required]
    public Guid RelatedTourID { get; init; }
}
