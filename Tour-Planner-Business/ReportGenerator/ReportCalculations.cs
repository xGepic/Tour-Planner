namespace Tour_Planner_Business;

internal static class ReportCalculations
{
    public static int GetAverageTime(Tour myTour)
    {
        uint result = 0;
        if (myTour.TourLogs is null)
        {
            return 0;
        }
        if (myTour.TourLogs.Count > 0)
        {
            foreach (var item in myTour.TourLogs)
            {
                result += item.TourTimeInMin;
            }
            return (int)result / myTour.TourLogs.Count;
        }
        return 0;
    }
    public static int GetAverageRating(Tour myTour)
    {
        uint result = 0;
        if (myTour.TourLogs is null)
        {
            return 0;
        }
        if (myTour.TourLogs.Count > 0)
        {
            foreach (var item in myTour.TourLogs)
            {
                result += (uint)item.TourRating;
            }
            return (int)result / myTour.TourLogs.Count;
        }
        return 0;
    }
}
