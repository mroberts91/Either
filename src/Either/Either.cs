using System;

namespace Either
{
    #region<T, TError>
    public struct Either<T, TError>
        where TError : Exception
    {
        public object Value { get; private set; }

        private Either(T value) { Value = value; }
        private Either(TError value) { Value = value; }

        public static implicit operator Either<T, TError>(T t) => new Either<T, TError>(t);
        public static implicit operator Either<T, TError>(TError t) => new Either<T, TError>(t);

        public TResult Resolve<TResult>((Func<T, TResult> success, Func<TError, TResult> error) actions)
            => (Value, actions) switch
            {
                (T, { success: not null }) => actions.success.Invoke((T)Value),
                (TError, { error: not null }) => actions.error.Invoke((TError)Value),
                _ => throw new InvalidOperationException($"Unable to resolve {GetType().Name} due to missing action handler.")
            };
    }

    #endregion
    
    #region<T, TError1, TError2>


    public struct Either<T, TError1, TError2>
        where TError1 : Exception
        where TError2 : Exception
    {
        public object Value { get; private set; }

        private Either(T value) { Value = value; }
        private Either(TError1 value) { Value = value; }
        private Either(TError2 value) { Value = value; }

        public static implicit operator Either<T, TError1, TError2>(T t) => new Either<T, TError1, TError2>(t);
        public static implicit operator Either<T, TError1, TError2>(TError1 t) => new Either<T, TError1, TError2>(t);
        public static implicit operator Either<T, TError1, TError2>(TError2 t) => new Either<T, TError1, TError2>(t);

        public TResult Resolve<TResult>((Func<T, TResult> success, Func<TError1, TResult> error1, Func<TError2, TResult> error2) actions)
            => (Value, actions) switch
            {
                (T, { success: not null }) => actions.success.Invoke((T)Value),
                (TError1, { error1: not null }) => actions.error1.Invoke((TError1)Value),
                (TError2, { error2: not null }) => actions.error2.Invoke((TError2)Value),
                _ => throw new InvalidOperationException($"Unable to resolve {GetType().Name} due to missing action handler.")
            };

    }

    #endregion
    
    #region<T, TError1, TError2, TError3>

    public struct Either<T, TError1, TError2, TError3>
        where TError1 : Exception
        where TError2 : Exception
        where TError3 : Exception
    {
        public object Value { get; private set; }

        private Either(T value) { Value = value; }
        private Either(TError1 value) { Value = value; }
        private Either(TError2 value) { Value = value; }
        private Either(TError3 value) { Value = value; }

        public static implicit operator Either<T, TError1, TError2, TError3>(T t) => new Either<T, TError1, TError2, TError3>(t);
        public static implicit operator Either<T, TError1, TError2, TError3>(TError1 t) => new Either<T, TError1, TError2, TError3>(t);
        public static implicit operator Either<T, TError1, TError2, TError3>(TError2 t) => new Either<T, TError1, TError2, TError3>(t);
        public static implicit operator Either<T, TError1, TError2, TError3>(TError3 t) => new Either<T, TError1, TError2, TError3>(t);

        public TResult Resolve<TResult>((Func<T, TResult> success, Func<TError1, TResult> error1, Func<TError2, TResult> error2, Func<TError3, TResult> error3) actions)
            => (Value, actions) switch
            {
                (T, { success: not null }) => actions.success.Invoke((T)Value),
                (TError1, { error1: not null }) => actions.error1.Invoke((TError1)Value),
                (TError2, { error2: not null }) => actions.error2.Invoke((TError2)Value),
                (TError3, { error3: not null }) => actions.error3.Invoke((TError3)Value),
                _ => throw new InvalidOperationException($"Unable to resolve {GetType().Name} due to missing action handler.")
            };
    }

    #endregion
}
