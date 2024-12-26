using System;
using static MobileLibrary.Function.Function;

namespace MobileLibrary.Function
{
    public static partial class Function
    {
        public static Func<R> Apply<T, R>(this Func<T, R> f, T t)
            => () => f(t);
        public static Func<T2, R> Apply<T1, T2, R>(this Func<T1, T2, R> f, T1 t1)
            => t2 => f(t1, t2);
        
        public static Func<T2, T3, R> Apply<T1, T2, T3, R>(this Func<T1, T2, T3, R> f, T1 t1)
            => (t2, t3) => f(t1, t2, t3);
        
        public static Func<T2, T3, T4, R> Apply<T1, T2, T3, T4, R>(this Func<T1, T2, T3, T4, R> f, T1 t1)
            => (t2, t3, t4) => f(t1, t2, t3, t4);
        
        public static Func<T2, T3, T4, T5, R> Apply<T1, T2, T3, T4, T5, R>(this Func<T1, T2, T3, T4, T5, R> f, T1 t1)
            => (t2, t3, t4, t5) => f(t1, t2, t3, t4, t5);
    }
}