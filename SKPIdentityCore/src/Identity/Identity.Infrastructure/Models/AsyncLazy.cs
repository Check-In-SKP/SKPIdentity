using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Models
{
    // Class to create a lazy task
    // Lazy initialization is the tactic of delaying the creation of an object,
    // the calculation of a value, or some other expensive process until
    // the first time it is needed.
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
