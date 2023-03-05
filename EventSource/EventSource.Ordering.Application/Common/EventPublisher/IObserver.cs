using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Application.Common.EventPublisher
{
    public interface IObserver
    {
        Task Update(string eventType);
    }
}
