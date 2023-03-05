namespace EventSource.Ordering.Domain.Common
{
    public abstract class  DomainEventBase<TAggregateId> : IDomainEvent<TAggregateId>
    {
        protected DomainEventBase()
        {
            EventId = Guid.NewGuid();
            EventDate = DateTime.UtcNow;
            EventType = this.GetType().Name;
        }

        protected DomainEventBase(TAggregateId aggregateId):this()
        {
            AggregateId = aggregateId;
        }

        public DomainEventBase(TAggregateId aggregateId, long aggregateVersion): this(aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }

        public Guid EventId { get; private set; }
        public TAggregateId AggregateId { get; private set; }
        public long AggregateVersion { get; private set; }
        public DateTime EventDate { get; private set; }
        public string EventType { get; private set; }

        public override bool Equals(object obj) => base.Equals(obj as DomainEventBase<TAggregateId>);

        public bool Equals(DomainEventBase<TAggregateId> other) => other != null && EventId.Equals(other.EventId);

        public override int GetHashCode()
        {
            return 290933282 + EqualityComparer<Guid>.Default.GetHashCode(EventId);
        }
        public abstract IDomainEvent<TAggregateId> WithAggregate(TAggregateId aggregateId, long aggregateVersion);
    }
}
