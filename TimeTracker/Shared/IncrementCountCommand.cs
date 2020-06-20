using Coyote.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared
{
    public class IncrementCountCommand : Command
    {
        public int Seed { get; set; }

        public IncrementCountCommand(int seed)
        {
            Seed = seed;
        }
    }
}
