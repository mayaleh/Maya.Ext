using System;
using System.Collections.Generic;
using System.Linq;

namespace Maya.Ext.Rop
{

    public static class ResultExtensions
    {
        public static void Handle<TSuccess, TFailure>(this Result<TSuccess, TFailure> result,
            Action<TSuccess> onSuccess,
            Action<TFailure> onFailure)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (onSuccess == null)
            {
                throw new ArgumentNullException(nameof(onSuccess));
            }

            if (onFailure == null)
            {
                throw new ArgumentNullException(nameof(onFailure));
            }

            if (result.IsSuccess)
            {
                onSuccess(result.Success);
                return;
            }

            onFailure(result.Failure);
        }

        public static Result<TSuccess2, TFailure2> Either<TSuccess, TFailure, TSuccess2, TFailure2>(
            this Result<TSuccess, TFailure> x,
            Func<Result<TSuccess, TFailure>, Result<TSuccess2, TFailure2>> onSuccess,
            Func<Result<TSuccess, TFailure>, Result<TSuccess2, TFailure2>> onFailure)
        {
            return x.IsSuccess ? onSuccess(x) : onFailure(x);
        }

        public static void MatchFailureAction<TSuccess, TFailure>(
            this Result<TSuccess, TFailure> x,
            Action<TFailure> onFailure)
        {
            if (x.IsFailure)
            {
                onFailure.Invoke(x.Failure);
            }
        }

        public static void MatchSuccessAction<TSuccess, TFailure>(
            this Result<TSuccess, TFailure> x,
            Action<TSuccess> onSuccess)
        {
            if (x.IsFailure)
            {
                onSuccess.Invoke(x.Success);
            }
        }

        // Whatever x is, make it a failure.
        // The trick is that failure is an array type, can it can be made an empty array failure.
        public static Result<TSuccess, TFailure[]> ToFailure<TSuccess, TFailure>(
            this Result<TSuccess, TFailure[]> x)
        {
            return x.Either(
                a => Result<TSuccess, TFailure[]>.Failed(Array.Empty<TFailure>()),
                b => b
                );
        }

        // Put accumulator and next together.
        // If they are both successes, then put them together as a success.
        // If either/both are failures, then put them together as a failure.
        // Because success and failure is an array, they can be put together
        public static Result<TSuccess[], TFailure[]> Merge<TSuccess, TFailure>(
            this Result<TSuccess[], TFailure[]> accumulator,
            Result<TSuccess, TFailure[]> next)
        {
            return accumulator.IsSuccess && next.IsSuccess
                ? Result<TSuccess[], TFailure[]>
                    .Succeeded(accumulator.Success.Concat(new List<TSuccess>() { next.Success })
                        .ToArray())
                : Result<TSuccess[], TFailure[]>
                .Failed(accumulator.ToFailure().Failure.Concat(next.ToFailure().Failure).ToArray());
        }

        // Aggregate an array of results together.
        // If any of the results fail, return combined failures
        // Will only return success if all results succeed
        public static Result<TSuccess[], TFailure[]> Aggregate<TSuccess, TFailure>(
            this IEnumerable<Result<TSuccess, TFailure[]>> accumulator)
        {
            var emptySuccess = Result<TSuccess[], TFailure[]>.Succeeded(Array.Empty<TSuccess>());
            return accumulator.Aggregate(emptySuccess, (acc, o) => acc.Merge(o));
        }

        // Map: functional map
        // if x is a a success call f, otherwise pass it through as a failure
        public static Result<TSuccessNew, TFailure> Map<TSuccess, TFailure, TSuccessNew>(
            this Result<TSuccess, TFailure> x,
            Func<TSuccess, TSuccessNew> f)
        {
            return x.IsSuccess
                ? Result<TSuccessNew, TFailure>.Succeeded(f(x.Success))
                : Result<TSuccessNew, TFailure>.Failed(x.Failure);
        }

        // Bind: functional bind
        // Monadize it!
        public static Result<TSuccessNew, TFailure> Bind<TSuccess, TFailure, TSuccessNew>(
            this Result<TSuccess, TFailure> x,
            Func<TSuccess, Result<TSuccessNew, TFailure>> f)
        {
            return x.IsSuccess
                ? f(x.Success)
                : Result<TSuccessNew, TFailure>.Failed(x.Failure);
        }

        public static Result<TSuccess, TFailure> Tee<TSuccess, TFailure>(this Result<TSuccess, TFailure> x, Action<TSuccess> f)
        {
            if (x.IsSuccess)
            {
                f(x.Success);
            }

            return x;
        }
    }
}
