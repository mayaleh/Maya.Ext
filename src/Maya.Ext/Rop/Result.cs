using System;

namespace Maya.Ext.Rop
{
    /// <summary>
    /// Railway Oriented Programming.
    /// https://www.youtube.com/watch?v=uM906cqdFWE
    /// https://github.com/habaneroofdoom/AltNetRop﻿
    /// </summary>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TFailure"></typeparam>
    public class Result<TSuccess, TFailure>
    {
        public static Result<TSuccess, TFailure> Succeeded(TSuccess success)
        {
            return success == null
                ? throw new ArgumentNullException(nameof(success))
                : new Result<TSuccess, TFailure>
                {
                    IsSuccessful = true,
                    Success = success
                };
        }

        public static Result<TSuccess, TFailure> Failed(TFailure failure)
        {
            return failure == null
                ? throw new ArgumentNullException(nameof(failure))
                : new Result<TSuccess, TFailure>
                {
                    IsSuccessful = false,
                    Failure = failure
                };
        }

        private Result()
        {
        }

        public bool IsSuccess => IsSuccessful;

        public bool IsFailure => !IsSuccessful;

        public TSuccess Success { get; private set; }

        public TFailure Failure { get; private set; }

        private bool IsSuccessful { get; set; }
    }
}
