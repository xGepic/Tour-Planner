namespace TourPlannerTests;

internal class CSVGeneratorTests
{
    //UnitOfWork_StateUnderTest_ExpectedBehavior
    [Test]
    public void ImportTourFromCSV_WithoutAnExistingTour_ReturnsNull()
    {
        //Arrange
        string nonexistentTour = "nonexistentTour";

        //Act
        var result = CSVGenerator.ImportTourFromCSV(nonexistentTour);

        //Assert
        result.Should().BeNull();
    }
}