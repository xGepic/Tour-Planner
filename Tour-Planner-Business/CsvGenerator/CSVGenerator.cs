namespace Tour_Planner_Business;

public static class CSVGenerator
{
    private const string csvFolder = "./MyCSV/";
    private const string fileName = "[CSV] ";
    private const string fileEnding = ".csv";
    public static bool GenerateNewCSV(Tour myTour)
    {
        string data = string.Empty;
        try
        {
            string filePath = string.Concat(csvFolder, fileName, myTour.TourName, fileEnding);
            PropertyInfo[] props = myTour.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if(prop.Name == "TourType")
                {
                    data += prop.GetValue(myTour, null);
                    break;
                }
                data += prop.GetValue(myTour,null) + ",";
            }
            File.WriteAllText(filePath, data);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
