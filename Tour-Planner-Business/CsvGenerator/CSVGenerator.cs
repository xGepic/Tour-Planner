namespace Tour_Planner_Business;

public static class CSVGenerator
{
    private const string csvFolder = "./MyCSV/";
    private const string fileName = "[CSV] ";
    private const string fileEnding = ".csv";
    public static bool GenerateNewCSV(Tour myTour)
    {
        string data;
        try
        {
            string filePath = string.Concat(csvFolder, fileName, myTour.TourName, fileEnding);
            data = string.Concat(myTour.Id + ",", myTour.TourName);
            File.WriteAllText(filePath, data);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
