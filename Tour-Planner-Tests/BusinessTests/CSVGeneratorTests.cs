namespace Tour_Planner_Tests;

internal class CSVGeneratorTests
{
    [Test]
    public void ImportTourFromCSV_WithoutExistingCSV_ShouldReturnNull()
    {
        //Arrange


        //Act
        var result = CSVGenerator.ImportTourFromCSV("test");

        //Assert
        result.Should().BeNull();
    }
}
