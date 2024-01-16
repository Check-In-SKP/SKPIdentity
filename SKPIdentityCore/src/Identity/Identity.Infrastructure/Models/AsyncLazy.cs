using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Models
{
    public class AsyncLazy<T>
    {
        private readonly Lazy<Task<T>> _lazyTask;

        public AsyncLazy(Func<Task<T>> taskFactory)
        {
            _lazyTask = new Lazy<Task<T>>(taskFactory);
        }

        public Task<T> Value => _lazyTask.Value;
    }
}
