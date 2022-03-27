namespace TourPlannerTests;

internal class BL_Tests
{
    [Test]
    public void TestAverageTime()
    {
        //Arrange
        Tour testTour = new()
        {
            Id = Guid.NewGuid(),
            TourName = "TestTour",
            TourDescription = "nothing",
            StartingPoint = "Vienna",
            Destination = "Berlin",
            TransportType = TransportType.byCar,
            TourDistance = 600,
            EstimatedTimeInMin = 6,
            TourType = Tourtype.Vacation,
        };
        TourLog testLog = new()
        {
            Id = Guid.NewGuid(),
            TourDateAndTime = DateTime.Now,
            TourComment = null,
            TourDifficulty = TourDifficulty.medium,
            TourTimeInMin = 360,
            TourRating = TourRating.Satisfied,
            RelatedTourID = Guid.NewGuid()
        };
        TourLog testlog2 = new()
        {
            Id = Guid.NewGuid(),
            TourDateAndTime = DateTime.Now,
            TourComment = null,
            TourDifficulty = TourDifficulty.medium,
            TourTimeInMin = 400,
            TourRating = TourRating.Satisfied,
            RelatedTourID = Guid.NewGuid()
        };
        List<TourLog> logList = new() { testLog, testlog2 };
        testTour.TourLogs = logList;
        
        //Act
        var result = ReportCalculations.GetAverageTime(testTour);

        //Assert
        int actual = 380;
        result.Should().Be(actual);
    }
}
