﻿namespace System.Linq
{
    using System;

    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Monad.Extensions;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public static partial class LinqExtension
    {
        public static Option<TReturn> Select<T, TReturn>(this in Option<T> @this, Func<T, TReturn> selector) => @this.Match<Option<TReturn>>(
            some => selector(some),
            () => None());

        public static Option<TReturn> Select<T, TReturn>(this in Option<T> @this, Func<T, Option<TReturn>> selector) => @this.Match(
            selector,
            () => None());

        public static Option<TReturn> Select<T, TReturn>(this in Option<T> @this, Func<T, Try<TReturn>> selector) => @this.Match(
            some => selector(some).ToOption(),
            () => None());

        public static Func<Func<T, TReturn>, TReturn> Fold<T, TReturn>(this Option<T> @this, TReturn ifEmpty) => selector => @this.Match(
            selector,
            () => ifEmpty);

        public static Unit ForEach<T>(this in Option<T> @this, Action<T> selector) => @this.Match(
            some =>
            {
                selector(some);
                return Unit();
            },
            Unit);
    }
}
