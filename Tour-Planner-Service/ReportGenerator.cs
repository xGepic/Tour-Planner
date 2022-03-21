namespace Tour_Planner_Service;

public static class ReportGenerator
{
    private const string file = "./Reports/";
    private const string fileName = "[Tour Report] ";
    private const string folderName = "./Uploads/";
    private const string fileEnding = ".jpg";
    public static void GenerateTourReport(Tour myTour)
    {
        PdfWriter writer = new(file + myTour.Id + ".pdf");
        PdfDocument myReport = new(writer);
        Document document = new(myReport);

        //Header
        Paragraph header = new Paragraph(fileName + myTour.TourName)
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20)
            .SetBold();
        document.Add(header);

        // Line separator
        LineSeparator ls = new(new SolidLine());
        document.Add(ls);

        //Tour Data List
        Paragraph TourListHeader = new Paragraph("Tour Data:")
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
            .SetFontSize(14)
            .SetBold();
        List tourList = new List()
          .SetSymbolIndent(12)
          .SetListSymbol("\u2022")
          .SetFontSize(13)
          .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
        tourList.Add(new ListItem("Name: " + myTour.TourName))
            .Add(new ListItem("Description: " + myTour.TourDescription))
            .Add(new ListItem("Starting Point: " + myTour.StartingPoint))
            .Add(new ListItem("Destination: " + myTour.Destination))
            .Add(new ListItem("Transport Type: " + myTour.TransportType))
            .Add(new ListItem("Tour Distance: " + myTour.TourDistance + " km"))
            .Add(new ListItem("Estimated Time In Min: " + myTour.EstimatedTimeInMin + " min"))
            .Add(new ListItem("TourType: " + myTour.TourType));
        document.Add(TourListHeader);
        document.Add(tourList);

        //Tour Logs List
        if (myTour.TourLogs is null)
        {
            document.Close();
            return;
        }
        Paragraph logListHeader = new Paragraph("\nTour Logs:")
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
            .SetFontSize(14)
            .SetBold();
        List logList = new List()
            .SetSymbolIndent(12)
            .SetListSymbol("\u2022")
            .SetFontSize(12)
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
        foreach (var item in myTour.TourLogs)
        {
            logList.Add("Date and Time: " + item.TourDateAndTime)
                .Add("Comment: " + item.TourComment)
                .Add("Tour Difficulty [0-4]: " + (int)item.TourDifficulty)
                .Add("Time In Min: " + item.TourTimeInMin + " min")
                .Add("Tour Rating [0-4]: " + (int)item.TourRating + "\n\n");
        }
        document.Add(logListHeader);
        document.Add(logList);

        //Image
        Paragraph imageHeader = new Paragraph("Tour Image: ")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(14)
                    .SetBold();
        ImageData myImage = ImageDataFactory.Create(folderName + myTour.Id + fileEnding);
        document.Add(imageHeader);
        document.Add(new Image(myImage));
        document.Close();
    }
}
