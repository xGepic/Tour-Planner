namespace Tour_Planner_Service;

public class ReportGenerator
{
    private const string fileName = "[Report] ";
    private readonly PdfWriter writer = new(fileName + DateTime.Now.ToString());
    public void GenerateTourReport(Tour myTour)
    {
        PdfDocument myReport = new(writer);
        Document document = new(myReport);

        //Header
        Paragraph header = new Paragraph(fileName + myTour.Id)
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
            .SetFontSize(24)
            .SetBold()
            .SetFontColor(ColorConstants.RED);
        document.Add(header);

        //TourInfo Paragraph
        Paragraph tourInfo = new Paragraph("Id: " + myTour.Id)
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
            .SetFontSize(14)
            .SetBold()
            .SetFontColor(ColorConstants.BLACK);
        tourInfo.Add("Name: " + myTour.TourName);
        tourInfo.Add("Description: " + myTour.TourDescription);
        tourInfo.Add("Starting Point: " + myTour.StartingPoint);
        tourInfo.Add("Destination: " + myTour.Destination);
        tourInfo.Add("Transport Type: " + myTour.TransportType);
        tourInfo.Add("Tour Distance: " + myTour.TourDistance);
        tourInfo.Add("Estimated Time In Min: " + myTour.EstimatedTimeInMin);
        tourInfo.Add("TourType: " + myTour.TourType);
        document.Add(tourInfo);

        //TourLogs
        if (myTour.TourLogs is null)
        {
            document.Close();
            return;
        }
        Paragraph tourLogs = new Paragraph()
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
            .SetFontSize(14)
            .SetFontColor(ColorConstants.BLACK);
        foreach (var item in myTour.TourLogs)
        {
            tourLogs.Add("Date and Time: " + item.TourDateAndTime);
            tourLogs.Add("Comment: " + item.TourComment);
            tourLogs.Add("Tour Difficulty [1-5]: " + item.TourDifficulty + 1);
            tourLogs.Add("Time In Min: " + item.TourTimeInMin);
            tourLogs.Add("Tour Rating [1-5]: " + item.TourRating + 1);
            tourLogs.Add("\n\n");
        }
        document.Close();
    }
}
