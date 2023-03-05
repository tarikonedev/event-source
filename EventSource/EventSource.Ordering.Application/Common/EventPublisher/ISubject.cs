namespace EventSource.Ordering.Application.Common.EventPublisher
{
    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        Task NotifyObservers();
    }
}
