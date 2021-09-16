# ActionAsync

namespace `Maya.Ext.Func` includes the `ActionAsync` delegate.

Its simple generated delegate as Action, the difference is that the ActionAsync can be awaited AND its always return the `Unit` value.

## Example


```c#
using System;
using System.Threading.Tasks;
using Maya.Ext;
using Maya.Ext.Func;

namespace Maya.Job
{
    public class SomeJob
    {
        public SomeJob()
        {
            DoTheJobAction = DoTheJob;
            BestRegardAction = BestRegard;
        }

        async Task<Unit> DoTheJob()
        {
            await Task.Delay(100);
            return Unit.Default;
        }


        async Task<Unit> BestRegard(string input)
        {
            await Task.Delay(100);
            Console.WriteLine(input);
            return Unit.Default;
        }

        public ActionAsync DoTheJobAction { get; set; }


        public ActionAsync<string> BestRegardAction { get; set; }
    }


    public class JobRunner
    {
        public async Task RunTheJobs()
        {
            var someJob = new SomeJob();
            await someJob.DoTheJobAction.Invoke();
            await someJob.BestRegardAction.Invoke("Some input text");
        }
    }
}
```