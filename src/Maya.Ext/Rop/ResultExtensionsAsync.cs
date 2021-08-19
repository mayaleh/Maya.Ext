using System;
using System.Threading.Tasks;

namespace Maya.Ext.Rop
{

    public static class ResultExtensionsAsync
    {
        public static async Task<Result<TSuccess2, TFailure2>> EitherAsync<TSuccess, TFailure, TSuccess2, TFailure2>(
            this Task<Result<TSuccess, TFailure>> x,
            Func<Result<TSuccess, TFailure>, Result<TSuccess2, TFailure2>> onSuccess,
            Func<Result<TSuccess, TFailure>, Result<TSuccess2, TFailure2>> onFailure)
        {
            return x.GetAwaiter().GetResult().IsSuccess ?
                 await Task.FromResult(onSuccess.Invoke(x.GetAwaiter().GetResult()))
                : await Task.FromResult(onFailure.Invoke(x.GetAwaiter().GetResult()));
        }

        public static async Task HandleAsync<TSuccess, TFailure>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task> onSuccess, Action<TFailure> onFailure, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException(nameof(resultIn));
            }

            if (onSuccess == null)
            {
                throw new ArgumentNullException(nameof(onSuccess));
            }

            if (onFailure == null)
            {
                throw new ArgumentNullException(nameof(onFailure));
            }

            if (resultIn.IsSuccess)
            {
                await onSuccess(resultIn.Success).ConfigureAwait(continueOnCapturedContext);
                return;
            }

            onFailure(resultIn.Failure);
        }

        public static async Task HandleAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Func<TSuccess, Task> onSuccess, Action<TFailure> onFailure, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            if (onSuccess == null)
            {
                throw new ArgumentNullException(nameof(onSuccess));
            }

            if (onFailure == null)
            {
                throw new ArgumentNullException(nameof(onFailure));
            }

            await (await resultIn.ConfigureAwait(continueOnCapturedContext)).HandleAsync(onSuccess, onFailure, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<TSuccessNew, TFailure>> MapAsync<TSuccess, TFailure, TSuccessNew>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task<TSuccessNew>> mapTo, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException(nameof(resultIn));
            }

            if (mapTo == null)
            {
                throw new ArgumentNullException(nameof(mapTo));
            }

            return (!resultIn.IsSuccess) ? Result<TSuccessNew, TFailure>.Failed(resultIn.Failure) : Result<TSuccessNew, TFailure>.Succeeded(await mapTo(resultIn.Success).ConfigureAwait(continueOnCapturedContext));
        }

        public static async Task<Result<TSuccessNew, TFailure>> MapAsync<TSuccess, TFailure, TSuccessNew>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TSuccess, Task<TSuccessNew>> mapTo, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow(nameof(resultInTask));

            if (mapTo == null)
            {
                throw new ArgumentNullException(nameof(mapTo));
            }

            return await (await resultInTask.ConfigureAwait(continueOnCapturedContext)).MapAsync(mapTo, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<TSuccessNew, TFailure>> BindAsync<TSuccess, TFailure, TSuccessNew>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task<Result<TSuccessNew, TFailure>>> bindTo, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException(nameof(resultIn));
            }

            if (bindTo == null)
            {
                throw new ArgumentNullException(nameof(bindTo));
            }

            return (!resultIn.IsSuccess) ? Result<TSuccessNew, TFailure>.Failed(resultIn.Failure) : (await bindTo(resultIn.Success).ConfigureAwait(continueOnCapturedContext));
        }

        public static async Task<Result<TSuccessNew, TFailure>> BindAsync<TSuccess, TFailure, TSuccessNew>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TSuccess, Task<Result<TSuccessNew, TFailure>>> bindTo, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow(nameof(resultInTask));

            if (bindTo == null)
            {
                throw new ArgumentNullException(nameof(bindTo));
            }

            return await (await resultInTask.ConfigureAwait(continueOnCapturedContext)).BindAsync(bindTo, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<TSuccess2, TFailure2>> EitherAsync<TSuccess, TFailure, TSuccess2, TFailure2>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task<Result<TSuccess2, TFailure2>>> onSuccess, Func<TFailure, Task<Result<TSuccess2, TFailure2>>> onFailure, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException(nameof(resultIn));
            }

            if (onSuccess == null)
            {
                throw new ArgumentNullException(nameof(onSuccess));
            }

            if (onFailure == null)
            {
                throw new ArgumentNullException(nameof(onFailure));
            }

            return (!resultIn.IsSuccess) ? (await onFailure(resultIn.Failure).ConfigureAwait(continueOnCapturedContext)) : (await onSuccess(resultIn.Success).ConfigureAwait(continueOnCapturedContext));
        }

        public static async Task<Result<TSuccess2, TFailure2>> EitherAsync<TSuccess, TFailure, TSuccess2, TFailure2>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TSuccess, Task<Result<TSuccess2, TFailure2>>> onSuccess, Func<TFailure, Task<Result<TSuccess2, TFailure2>>> onFailure, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow(nameof(resultInTask));

            if (onSuccess == null)
            {
                throw new ArgumentNullException(nameof(onSuccess));
            }

            if (onFailure == null)
            {
                throw new ArgumentNullException(nameof(onFailure));
            }

            return await (await resultInTask.ConfigureAwait(continueOnCapturedContext)).EitherAsync(onSuccess, onFailure, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task MatchSuccessAsync<TSuccess, TFailure>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task> runOnSuccess, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException(nameof(resultIn));
            }

            if (runOnSuccess == null)
            {
                throw new ArgumentNullException(nameof(runOnSuccess));
            }

            if (!resultIn.IsFailure)
            {
                await runOnSuccess(resultIn.Success).ConfigureAwait(continueOnCapturedContext);
            }
        }

        public static async Task MatchSuccessAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TSuccess, Task> runOnSuccess, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow();

            if (runOnSuccess == null)
            {
                throw new ArgumentNullException(nameof(runOnSuccess));
            }

            await (await resultInTask.ConfigureAwait(continueOnCapturedContext)).MatchSuccessAsync(runOnSuccess, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task MatchFailureAsync<TSuccess, TFailure>(this Result<TSuccess, TFailure> resultIn, Func<TFailure, Task> runOnFailure, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException(nameof(resultIn));
            }

            if (runOnFailure == null)
            {
                throw new ArgumentNullException(nameof(runOnFailure));
            }

            if (!resultIn.IsSuccess)
            {
                await runOnFailure(resultIn.Failure).ConfigureAwait(continueOnCapturedContext);
            }
        }

        public static async Task MatchFailureAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TFailure, Task> runOnFailure, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow(nameof(resultInTask));

            if (runOnFailure == null)
            {
                throw new ArgumentNullException(nameof(runOnFailure));
            }

            await (await resultInTask.ConfigureAwait(continueOnCapturedContext)).MatchFailureAsync(runOnFailure, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<TSuccess> ValueOrAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, TSuccess alternative, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            var result = await resultIn.ConfigureAwait(continueOnCapturedContext);
            return result.IsSuccess ? result.Success : alternative;
        }

        public static async Task<Result<TSuccess, TFailure>> RunWhenSuccessAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Func<TSuccess, Task> runOnSuccess, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            if (runOnSuccess == null)
            {
                throw new ArgumentNullException(nameof(runOnSuccess));
            }

            var result = await resultIn.ConfigureAwait(continueOnCapturedContext);
            if (result.IsSuccess)
            {
                await runOnSuccess(result.Success).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<TSuccess, TFailure>> RunWhenSuccessAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Action<TSuccess> runOnSuccess, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            if (runOnSuccess == null)
            {
                throw new ArgumentNullException(nameof(runOnSuccess));
            }

            var result = await resultIn.ConfigureAwait(continueOnCapturedContext);
            if (result.IsSuccess)
            {
                runOnSuccess(result.Success);
            }

            return result;
        }

        public static async Task<Result<TSuccess, TFailure>> RunWhenFailureAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Func<TFailure, Task> runOnFailure, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            if (runOnFailure == null)
            {
                throw new ArgumentNullException(nameof(runOnFailure));
            }

            var result = await resultIn.ConfigureAwait(continueOnCapturedContext);
            if (result.IsFailure)
            {
                await runOnFailure(result.Failure).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<TSuccess, TFailure>> RunWhenFailureAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Action<TFailure> runOnFailure, bool continueOnCapturedContext = false)
        {

            resultIn.CheckNotNullOrThrow();

            if (runOnFailure == null)
            {
                throw new ArgumentNullException(nameof(runOnFailure));
            }

            var result = await resultIn.ConfigureAwait(continueOnCapturedContext);
            if (result.IsFailure)
            {
                runOnFailure(result.Failure);
            }

            return result;
        }

        private static void CheckNotNullOrThrow<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> result, string argName = "resultIn")
        {
            if (result == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

    }
}
