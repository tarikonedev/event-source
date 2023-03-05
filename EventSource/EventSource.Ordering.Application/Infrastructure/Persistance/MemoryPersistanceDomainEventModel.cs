using System.Text;

namespace EventSource.Ordering.Application.Infrastructure.Persistance
{
    public class MemoryPersistanceDomainEventModel
    {
        public string EventType { get; set; }
        public string AggregateId { get; set; }
        public DateTime EventDate { get; set; }
        public IDictionary<string, object> EventData { get; set; }
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("\n--------------------Published event " + EventType + "------------------\n");
            stringBuilder.Append("\naggregateId : " + AggregateId);
            stringBuilder.Append("\neventDate : " + EventDate.ToString());
            foreach (var key in EventData.Keys) stringBuilder.Append("\n" + key + " : " + (EventData[key] == null ? "" : EventData[key]));
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
