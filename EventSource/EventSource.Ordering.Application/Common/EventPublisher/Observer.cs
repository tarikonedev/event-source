using EventSource.Ordering.Application.Common.Persistance;

namespace EventSource.Ordering.Application.Common.EventPublisher
{
    public class Observer : IObserver
    {
        private readonly IReadRepository repo;

        public Observer(IReadRepository repo, ISubject subject)
        {
            this.repo = repo;
            subject.RegisterObserver(this);
        }
        public Task Update(string eventType)
        {
            return repo.UpdateReadModelOnEventPublish(eventType);
        }
    }
}
