using System;
using System.Threading.Tasks;
using Maya.Ext.Func;

namespace Maya.Ext.Func.Rop
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

        public static async Task<Unit> HandleAsync<TSuccess, TFailure>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task<Unit>> onSuccess, Ext.Func.Action<TFailure> onFailure, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException("resultIn");
            }

            if (onSuccess == null)
            {
                throw new ArgumentNullException("onSuccess");
            }

            if (onFailure == null)
            {
                throw new ArgumentNullException("onFailure");
            }

            if (resultIn.IsSuccess)
            {
                return await onSuccess(resultIn.Success).ConfigureAwait(continueOnCapturedContext);
            }
            else
            {
                return onFailure(resultIn.Failure);
            }
        }

        public static async Task<Unit> HandleAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Func<TSuccess, Task<Unit>> onSuccess, Ext.Func.Action<TFailure> onFailure, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            if (onSuccess == null)
            {
                throw new ArgumentNullException("onSuccess");
            }

            if (onFailure == null)
            {
                throw new ArgumentNullException("onFailure");
            }

            return await (await resultIn.ConfigureAwait(continueOnCapturedContext))
                .HandleAsync(onSuccess, onFailure, continueOnCapturedContext)
                .ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<TSuccessNew, TFailure>> MapAsync<TSuccess, TFailure, TSuccessNew>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task<TSuccessNew>> mapTo, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException("resultIn");
            }

            if (mapTo == null)
            {
                throw new ArgumentNullException("mapTo");
            }

            return (!resultIn.IsSuccess) ? Result<TSuccessNew, TFailure>.Failed(resultIn.Failure) : Result<TSuccessNew, TFailure>.Succeeded(await mapTo(resultIn.Success).ConfigureAwait(continueOnCapturedContext));
        }

        public static async Task<Result<TSuccessNew, TFailure>> MapAsync<TSuccess, TFailure, TSuccessNew>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TSuccess, Task<TSuccessNew>> mapTo, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow(nameof(resultInTask));

            if (mapTo == null)
            {
                throw new ArgumentNullException("mapTo");
            }

            return await (await resultInTask.ConfigureAwait(continueOnCapturedContext)).MapAsync(mapTo, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<TSuccessNew, TFailure>> BindAsync<TSuccess, TFailure, TSuccessNew>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task<Result<TSuccessNew, TFailure>>> bindTo, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException("resultIn");
            }

            if (bindTo == null)
            {
                throw new ArgumentNullException("bindTo");
            }

            return (!resultIn.IsSuccess) ? Result<TSuccessNew, TFailure>.Failed(resultIn.Failure) : (await bindTo(resultIn.Success).ConfigureAwait(continueOnCapturedContext));
        }

        public static async Task<Result<TSuccessNew, TFailure>> BindAsync<TSuccess, TFailure, TSuccessNew>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TSuccess, Task<Result<TSuccessNew, TFailure>>> bindTo, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow(nameof(resultInTask));

            if (bindTo == null)
            {
                throw new ArgumentNullException("bindTo");
            }

            return await (await resultInTask.ConfigureAwait(continueOnCapturedContext)).BindAsync(bindTo, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<TSuccess2, TFailure2>> EitherAsync<TSuccess, TFailure, TSuccess2, TFailure2>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task<Result<TSuccess2, TFailure2>>> onSuccess, Func<TFailure, Task<Result<TSuccess2, TFailure2>>> onFailure, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException("resultIn");
            }

            if (onSuccess == null)
            {
                throw new ArgumentNullException("onSuccess");
            }

            if (onFailure == null)
            {
                throw new ArgumentNullException("onFailure");
            }

            return (!resultIn.IsSuccess) ? (await onFailure(resultIn.Failure).ConfigureAwait(continueOnCapturedContext)) : (await onSuccess(resultIn.Success).ConfigureAwait(continueOnCapturedContext));
        }

        public static async Task<Result<TSuccess2, TFailure2>> EitherAsync<TSuccess, TFailure, TSuccess2, TFailure2>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TSuccess, Task<Result<TSuccess2, TFailure2>>> onSuccess, Func<TFailure, Task<Result<TSuccess2, TFailure2>>> onFailure, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow(nameof(resultInTask));

            if (onSuccess == null)
            {
                throw new ArgumentNullException("onSuccess");
            }

            if (onFailure == null)
            {
                throw new ArgumentNullException("onFailure");
            }

            return await (await resultInTask.ConfigureAwait(continueOnCapturedContext)).EitherAsync(onSuccess, onFailure, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Unit> MatchSuccessAsync<TSuccess, TFailure>(this Result<TSuccess, TFailure> resultIn, Func<TSuccess, Task<Unit>> runOnSuccess, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException("resultIn");
            }

            if (runOnSuccess == null)
            {
                throw new ArgumentNullException("runOnSuccess");
            }

            if (!resultIn.IsFailure)
            {
                return await runOnSuccess(resultIn.Success).ConfigureAwait(continueOnCapturedContext);
            }

            return Unit.Default;
        }

        public static async Task<Unit> MatchSuccessAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TSuccess, Task<Unit>> runOnSuccess, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow();

            if (runOnSuccess == null)
            {
                throw new ArgumentNullException("runOnSuccess");
            }

            return await (await resultInTask.ConfigureAwait(continueOnCapturedContext))
                .MatchSuccessAsync(runOnSuccess, continueOnCapturedContext)
                .ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Unit> MatchFailureAsync<TSuccess, TFailure>(this Result<TSuccess, TFailure> resultIn, Func<TFailure, Task<Unit>> runOnFailure, bool continueOnCapturedContext = false)
        {
            if (resultIn == null)
            {
                throw new ArgumentNullException("resultIn");
            }

            if (runOnFailure == null)
            {
                throw new ArgumentNullException("runOnFailure");
            }

            if (!resultIn.IsSuccess)
            {
                return await runOnFailure(resultIn.Failure)
                    .ConfigureAwait(continueOnCapturedContext);
            }

            return Unit.Default;
        }

        public static async Task<Unit> MatchFailureAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultInTask, Func<TFailure, Task<Unit>> runOnFailure, bool continueOnCapturedContext = false)
        {
            resultInTask.CheckNotNullOrThrow(nameof(resultInTask));

            if (runOnFailure == null)
            {
                throw new ArgumentNullException("runOnFailure");
            }

            return await (await resultInTask.ConfigureAwait(continueOnCapturedContext))
                .MatchFailureAsync(runOnFailure, continueOnCapturedContext)
                .ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<TSuccess> ValueOrAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, TSuccess alternative, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            Result<TSuccess, TFailure> result = await resultIn.ConfigureAwait(continueOnCapturedContext);
            return result.IsSuccess ? result.Success : alternative;
        }

        public static async Task<Result<TSuccess, TFailure>> RunWhenSuccessAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Func<TSuccess, Task<Unit>> runOnSuccess, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            if (runOnSuccess == null)
            {
                throw new ArgumentNullException("runOnSuccess");
            }

            Result<TSuccess, TFailure> result = await resultIn.ConfigureAwait(continueOnCapturedContext);
            if (result.IsSuccess)
            {
                await runOnSuccess(result.Success).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<TSuccess, TFailure>> RunWhenSuccessAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Ext.Func.Action<TSuccess> runOnSuccess, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            if (runOnSuccess == null)
            {
                throw new ArgumentNullException("runOnSuccess");
            }

            Result<TSuccess, TFailure> result = await resultIn.ConfigureAwait(continueOnCapturedContext);
            if (result.IsSuccess)
            {
                runOnSuccess(result.Success);
            }

            return result;
        }

        public static async Task<Result<TSuccess, TFailure>> RunWhenFailureAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Func<TFailure, Task<Unit>> runOnFailure, bool continueOnCapturedContext = false)
        {
            resultIn.CheckNotNullOrThrow();

            if (runOnFailure == null)
            {
                throw new ArgumentNullException("runOnFailure");
            }

            Result<TSuccess, TFailure> result = await resultIn.ConfigureAwait(continueOnCapturedContext);
            if (result.IsFailure)
            {
                await runOnFailure(result.Failure).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<TSuccess, TFailure>> RunWhenFailureAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> resultIn, Ext.Func.Action<TFailure> runOnFailure, bool continueOnCapturedContext = false)
        {

            resultIn.CheckNotNullOrThrow();

            if (runOnFailure == null)
            {
                throw new ArgumentNullException("runOnFailure");
            }

            Result<TSuccess, TFailure> result = await resultIn.ConfigureAwait(continueOnCapturedContext);
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
