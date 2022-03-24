namespace Tour_Planner_Business;

internal static class ReportCalculations
{
    public static int GetAverageTime(Tour myTour)
    {
        if (myTour.TourLogs is null)
        {
            return 0;
        }
        uint result = 0;
        int counter = 0;
        if (myTour.TourLogs.Count > 0)
        {
            foreach (var item in myTour.TourLogs)
            {
                result += item.TourTimeInMin;
                counter++;
            }
            return (int)result / counter;
        }
        return 0;
    }
    public static int GetAverageRating(Tour myTour)
    {
        if (myTour.TourLogs is null)
        {
            return 0;
        }
        uint result = 0;
        int counter = 0;
        if (myTour.TourLogs.Count > 0)
        {
            foreach (var item in myTour.TourLogs)
            {
                result += (uint)item.TourRating;
                counter++;
            }
            return (int)result / counter;
        }
        return 0;
    }
}
