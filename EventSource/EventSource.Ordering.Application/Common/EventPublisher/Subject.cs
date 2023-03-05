namespace EventSource.Ordering.Application.Common.EventPublisher
{
    internal class Subject  : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private string eventType { get; set; }
        public async Task SetEventPublished(string eventType)
        {
            this.eventType = eventType;
            await NotifyObservers();
        }
        public async Task NotifyObservers()
        {
            foreach (var o in observers) await o.Update(eventType);
        }

        public void RegisterObserver(IObserver observer) => observers.Add(observer);

        public void RemoveObserver(IObserver observer) => observers.Remove(observer);
    }
}
