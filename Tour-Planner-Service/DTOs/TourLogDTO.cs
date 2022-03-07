namespace Tour_Planner_Service;

public record TourLogDTO
{
    [Required]
    public DateTime DateAndTime { get; init; }
    public string? Comment { get; set; }
    [Required]
    [Range(1, 5)]
    public int Difficulty { get; init; }
    [Required]
    public uint TimeInMin { get; init; }
    [Required]
    [Range(1, 5)]
    public int Rating { get; init; }
}
