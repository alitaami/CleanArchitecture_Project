using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Behaviors.Contracts
{
    public interface IRemovalCacheable
    {
        public TimeSpan? SlidingExpiration { get; set; }
        public string CacheKey { get; set; }
        public bool ByPassCache { get; set; }
    }
}
