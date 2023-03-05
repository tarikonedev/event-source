using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Domain.Common
{
    public class BaseReadModel
    {
        public string AggregateId { get; set; }
    }
}
