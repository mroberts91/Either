﻿using System;

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

        public TResult Resolve<TResult>(Func<T, TResult> success, Func<TError, TResult> error)
            => Value switch
            {
                T => success.Invoke((T)Value),
                TError => error.Invoke((TError)Value),
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

        public TResult Resolve<TResult>(Func<T, TResult> success, Func<TError1, TResult> error1, Func<TError2, TResult> error2)
            => Value switch
            {
                T => success.Invoke((T)Value),
                TError1 => error1.Invoke((TError1)Value),
                TError2 => error2.Invoke((TError2)Value),
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

        public TResult Resolve<TResult>(Func<T, TResult> success, Func<TError1, TResult> error1, Func<TError2, TResult> error2, Func<TError3, TResult> error3)
            => Value switch
            {
                T => success.Invoke((T)Value),
                TError1 => error1.Invoke((TError1)Value),
                TError2 => error2.Invoke((TError2)Value),
                TError3 => error3.Invoke((TError3)Value),
                _ => throw new InvalidOperationException($"Unable to resolve {GetType().Name} due to missing action handler.")
            };
    }

    #endregion

    #region<T, TError1, TError2, TError3, TError4>

    public struct Either<T, TError1, TError2, TError3, TError4>
        where TError1 : Exception
        where TError2 : Exception
        where TError3 : Exception
        where TError4 : Exception
    {
        public object Value { get; private set; }

        private Either(T value) { Value = value; }
        private Either(TError1 value) { Value = value; }
        private Either(TError2 value) { Value = value; }
        private Either(TError3 value) { Value = value; }
        private Either(TError4 value) { Value = value; }

        public static implicit operator Either<T, TError1, TError2, TError3, TError4>(T t) => new Either<T, TError1, TError2, TError3, TError4>(t);
        public static implicit operator Either<T, TError1, TError2, TError3, TError4>(TError1 t) => new Either<T, TError1, TError2, TError3, TError4>(t);
        public static implicit operator Either<T, TError1, TError2, TError3, TError4>(TError2 t) => new Either<T, TError1, TError2, TError3, TError4>(t);
        public static implicit operator Either<T, TError1, TError2, TError3, TError4>(TError3 t) => new Either<T, TError1, TError2, TError3, TError4>(t);
        public static implicit operator Either<T, TError1, TError2, TError3, TError4>(TError4 t) => new Either<T, TError1, TError2, TError3, TError4>(t);

        public TResult Resolve<TResult>(Func<T, TResult> success, Func<TError1, TResult> error1, Func<TError2, TResult> error2, Func<TError3, TResult> error3, Func<TError4, TResult> error4)
            => Value switch
            {
                T => success.Invoke((T)Value),
                TError1 => error1.Invoke((TError1)Value),
                TError2 => error2.Invoke((TError2)Value),
                TError3 => error3.Invoke((TError3)Value),
                TError4 => error4.Invoke((TError4)Value),
                _ => throw new InvalidOperationException($"Unable to resolve {GetType().Name} due to missing action handler.")
            };
    }

    #endregion
}
