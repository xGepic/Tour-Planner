namespace Tour_Planner_Model;

public record TourDTO
{
    [Required]
    public Guid Id { get; init; }
    [Required]
    public string? TourName { get; init; }
    public string? TourDescription { get; init; }
    [Required]
    public string? StartingPoint { get; init; }
    [Required]
    public string? Destination { get; init; }
    [Required, Range(0, 2)]
    public TransportType TransportType { get; init; }
    [Required]
    public double TourDistance { get; init; }
    [Required]
    public uint EstimatedTimeInMin { get; init; }
    [Required, Range(0, 3)]
    public TourType TourType { get; init; }
    public List<TourLog>? TourLogs { get; set; }
}
