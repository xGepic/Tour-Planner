namespace Tour_Planner_Business;

public static class ReportCalculations
{
    public static int GetAverageTime(Tour myTour)
    {
        double result = 0;
        if (myTour.TourLogs is null)
        {
            return 0;
        }
        if (myTour.TourLogs.Count > 0)
        {
            foreach (TourLog? item in myTour.TourLogs)
            {
                result += item.TourTimeInMin;
            }
            double avg = result / myTour.TourLogs.Count;
            return Convert.ToInt32(Math.Round(avg, MidpointRounding.AwayFromZero));
        }
        return 0;
    }
    public static int GetAverageRating(Tour myTour)
    {
        double result = 0;
        if (myTour.TourLogs is null)
        {
            return 0;
        }
        if (myTour.TourLogs.Count > 0)
        {
            foreach (TourLog? item in myTour.TourLogs)
            {
                result += (uint)item.TourRating;
            }
            double avg = result / myTour.TourLogs.Count;
            return Convert.ToInt32(Math.Round(avg, MidpointRounding.AwayFromZero));
        }
        return 0;
    }
}
