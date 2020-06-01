using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared
{
    public class Command
    {
        public async Task Execute(Func<Task> function)
        {
            await function.Invoke();
        }
    }
}
