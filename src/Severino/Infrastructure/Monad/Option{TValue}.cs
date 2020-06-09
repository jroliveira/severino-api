namespace Severino.Infrastructure.Monad
{
    using System;

    using Severino.Infrastructure.Monad.Extensions;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public readonly struct Option<TValue>
    {
        private readonly TValue value;

        internal Option(TValue value, bool isDefined)
        {
            this.value = value;
            this.IsDefined = isDefined;
        }

        public bool IsDefined { get; }

        public static implicit operator Option<TValue>(TValue value) => Some(value);

        public static implicit operator Option<TValue>(None none) => default;

        public static implicit operator Option<TValue>(Try<TValue> @try) => @try.ToOption();

        public static implicit operator bool(in Option<TValue> option) => option.ToBoolean();

        public TReturn Match<TReturn>(Func<TValue, TReturn> some, Func<TReturn> none) => this.IsDefined
            ? some(this.value)
            : none();

        public TValue Get() => this.value;

        public bool ToBoolean() => this.IsDefined;
    }
}
