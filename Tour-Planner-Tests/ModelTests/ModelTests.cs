namespace TourPlannerTests
{
    internal class ModelTests
    {
        [Test]
        public void TourModelWithoutLogs_Popularity_ReturnsOne()
        {
            //Arrange
            Tour testTour = new()
            {
                Id = Guid.NewGuid(),
                TourName = "Test Tour",
                TourDescription = "Test",
                StartingPoint = "Berlin",
                Destination = "Wien",
                TransportType = Tour_Planner_Model.TransportType.byCar,
                TourDistance = 600,
                EstimatedTimeInMin = 300,
                TourType = TourType.Vacation
            };

            //Act
            var result = testTour.Popularity;

            //Assert
            result.Should().Be(1);
        }
        [Test]
        public void TourModel_ChildFriendliness_ReturnsTwo()
        {
            //Arrange
            Tour testTour = new()
            {
                Id = Guid.NewGuid(),
                TourName = "Test Tour",
                TourDescription = "Test",
                StartingPoint = "Berlin",
                Destination = "Wien",
                TransportType = Tour_Planner_Model.TransportType.byCar,
                TourDistance = 600,
                EstimatedTimeInMin = 300,
                TourType = TourType.Vacation
            };

            //Act
            var result = testTour.ChildFriendliness;

            //Assert
            result.Should().Be(2);
        }
    }
}
