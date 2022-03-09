﻿namespace Tour_Planner_Service;

public record TourLogDTO
{
    public Guid Id { get; set; }
    [Required]
    public DateTime DateAndTime { get; init; }
    public string? Comment { get; set; }
    [Required, Range(0, 4)]
    public int Difficulty { get; init; }
    [Required]
    public uint TimeInMin { get; init; }
    [Required, Range(0, 4)]
    public int Rating { get; init; }
    [Required]
    public Tour? RelatedTour { get; init; }
    [Required]
    public Guid RelatedTourID { get; init; }
}
