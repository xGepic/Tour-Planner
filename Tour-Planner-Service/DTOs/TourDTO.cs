namespace Tour_Planner_Service;

public record TourDTO
{
    [Required]
    public Guid ID { get; init; }
    [Required]
    public string? Name { get; init; }
    public string? Description { get; init; }
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
    public Tourtype TourType { get; init; }
    public List<TourLog>? TourLogs { get; init; }
}
