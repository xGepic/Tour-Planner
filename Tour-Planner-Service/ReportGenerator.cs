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
        document.Close();
    }
    public void GenerateSummarizeReport()
    {

    }
}
