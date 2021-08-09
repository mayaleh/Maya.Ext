using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maya.Ext.TaskUtilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class TaskExtension
    {
        /// <summary>
        /// Safe parallel tasks running. Handling all failures.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> WhenAll<T>(params Task<T>[] tasks)
        {
            var allTasks = Task.WhenAll(tasks);

            try
            {
                return await allTasks.ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignore
            }

            throw allTasks.Exception ?? throw new Exception("This can not possibly happen");
        }

        /// <summary>
        /// Safe parallel tasks running. Handling all failures.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static async Task WhenAll(params Task[] tasks)
        {
            var allTasks = Task.WhenAll(tasks);

            try
            {
                await allTasks.ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignore
            }

            throw allTasks.Exception ?? throw new Exception("This can not possibly happen");
        }


        /// <summary>
        /// Safe parallel tasks running. Handling all failures.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static async Task<Unit> WhenAll(params Task<Unit>[] tasks)
        {
            var allTasks = Task.WhenAll(tasks);

            try
            {
                await allTasks.ConfigureAwait(false);
                return Unit.Default;
            }
            catch (Exception)
            {
                // ignore
            }

            throw allTasks.Exception ?? throw new Exception("This can not possibly happen");
        }
    }
}
