namespace Tour_Planner_UI.Observer;
internal interface ISubject
{
    void Attach(IObserver observer);
    void Notify();
}
