#nullable enable
using System;

namespace  MobileLibrary.Function
{
    public static partial class Function
    {
        // liftA2の実装
        public static Option<R> LiftA2<T1, T2, R>(
            Func<T1, T2, R> func,
            Option<T1> opt1,
            Option<T2> opt2)
        {
            return opt1.Match<Option<R>>(
                None: () => none,
                Some: val1 => opt2.Match<Option<R>>(
                    None: () => none, 
                    Some: val2 => Some(func(val1, val2))
                )
            );
        }
 
    }
}