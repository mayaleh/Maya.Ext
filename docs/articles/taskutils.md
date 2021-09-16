# Tasks utilities usage

namespace `Maya.Ext.TaskUtilities` includes `TaskExtension`. `TaskExtension` containing extention methods for `System.Threading.Tasks.Task`.

## Safe parallel tasks runnig

For handling all failures, because the original ext method provided in 
System.Threading.Tasks is not safe to use.