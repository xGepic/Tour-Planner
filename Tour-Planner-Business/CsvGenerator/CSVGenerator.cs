namespace Tour_Planner_Business;

public static class CSVGenerator
{
    private const string csvFolder = "./MyCSV/";
    private const string fileEnding = ".csv";
    private const string delim = ";";
    public static bool ExportTourToCSV(Tour myTour)
    {
        string data = string.Empty;
        try
        {
            string filePath = string.Concat(csvFolder, myTour.TourName, fileEnding);
            PropertyInfo[] props = myTour.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {

                if (prop.Name == "TransportType")
                {
                    data += Convert.ToInt32(prop.GetValue(myTour, null));
                    data += ";";
                }
                else if (prop.Name == "TourType")
                {
                    data += Convert.ToInt32(prop.GetValue(myTour, null));
                    break;
                }
                else
                {
                    data += prop.GetValue(myTour, null) + ";";
                }
            }
            File.WriteAllText(filePath, data);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static Tour? ImportTourFromCSV(string name)
    {
        try
        {
            using TextFieldParser parser = new(string.Concat(csvFolder, name, fileEnding));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(delim);
            string[]? fields = parser.ReadFields();
            if (fields is null)
            {
                throw new ArgumentException("CSV Reader is Empty");
            }
            return new Tour()
            {
                Id = new Guid(fields[0]),
                TourName = fields[1],
                TourDescription = fields[2],
                StartingPoint = fields[3],
                Destination = fields[4],
                TransportType = (TransportType)Convert.ToInt32(fields[5]),
                TourDistance = Convert.ToDouble(fields[6]),
                EstimatedTimeInMin = Convert.ToUInt32(fields[7]),
                TourType = (TourType)Convert.ToInt32(fields[8])
            };
        }
        catch
        {
            return null;
        }
    }
}
