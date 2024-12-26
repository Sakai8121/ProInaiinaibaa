#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static MobileLibrary.Function.Function;

namespace MobileLibrary.Function
{
    public struct Option<T>
    {
        private readonly bool isSome;
        private readonly T value;

        private Option(T value)
        {
            isSome = true;
            this.value = value;
            #if UNITY_EDITOR
            this.isErrorLogged = false;
            #endif
        }

        public static implicit operator Option<T>(Option.None _) => new Option<T>();
        public static implicit operator Option<T>(Option.Some<T> some) => new Option<T>(some.Value);
        public static implicit operator Option<T>(T value) => value == null ? none : Some(value);

        public R Match<R>(Func<R> None, Func<T, R> Some) => isSome ? Some(value) : None();
        
        public IEnumerable<T> AsEnumerable()
        {
            if (isSome) yield return value;
        }
        
        public Option<TResult> Map<TResult>(Func<T, TResult> mapper)
        {
            if (isSome)
            {
                return Some(mapper(value));
            }
            return none;
        }
        
        public bool? DoPredicate(Func<T, bool> predicate)
        {
            // OptionがNoneの場合、nullを返す
            if (!isSome) return null;

            // OptionがSomeの場合はpredicateの評価を返す
            return predicate(value);
        }
        
        public void Do(Action<T> someAction)
        {
            if (isSome && someAction != null)
            {
                someAction(value);
            }
            else
            {
#if  UNITY_EDITOR
                if(isErrorLogged) return;
                //Debug.LogWarning($"Noneに対してDoを行なっています: Class name : {typeof(T).Name}");   
                isErrorLogged = true;
#endif
            }
        }

        public void DoNone(Action noneAction)
        {
            if (!isSome && noneAction != null)
            {
                noneAction();
            }
        }
        
        public async UniTask DoAsync(Func<T, UniTask> someAction)
        {
            if (isSome && someAction != null)
            {
                await someAction(value);  // 非同期処理を実行
            }
            else
            {
#if UNITY_EDITOR
                if (isErrorLogged) return;
                Debug.LogWarning($"Noneに対してDoAsyncを行なっています: Class name : {typeof(T).Name}");
                isErrorLogged = true;
#endif
            }
        }
        #if UNITY_EDITOR
        private bool isErrorLogged;
#endif

    }

    [Serializable]
    public class OptionMono<T> where T : Component
    {
        [SerializeField]
        private T? value;

        private Option<T> ToOption()
        {
            if (value != null && !value.Equals(null))
            {
                return value;
            }

#if UNITY_EDITOR
            Debug.LogError($"{typeof(T)} is null");
#endif
            return Function.none;
        }

        // Value property returns Option<T>
        public Option<T> Value => ToOption();
    }

    public static class OptionMonoExtensions
    {
        public static R Match<T, R>(this OptionMono<T> optionMono, Func<R> None, Func<T, R> Some) where T : Component
        {
            return optionMono.Value.Match(None, Some);
        }

        public static void Do<T>(this OptionMono<T> optionMono, Action<T> someAction) where T : Component
        {
            optionMono.Value.Do(someAction);
        }

        public static Option<TResult> Map<T, TResult>(this OptionMono<T> optionMono, Func<T, TResult> mapper) where T : Component
        {
            return optionMono.Value.Map(mapper);
        }
    }

    [Serializable]
    public class OptionGameObject
    {
        [SerializeField] private GameObject value;

        public Option<GameObject> Value
        {
            get
            {
                if (value == null || value.Equals(null))
                {
#if UNITY_EDITOR
                    Debug.LogError("Null Ref");
#endif
                    return null;
                }
                return value;
            }
        }
    }



    public static partial class Function
    {
        public static Option.None none => Option.None.Default;
        public static Option.Some<T> Some<T>(T value) => new Option.Some<T>(value);
        
        public static Option<R> Bind<T, R>(this Option<T> option, Func<T, Option<R>> func) 
            => option.Match(() => none, v => func(v));
        
        public static Option<T> Where<T> (this Option<T> option, Func<T, bool> predicate) 
            => option.Match(() => none, v => predicate(v) ? option : none);
        
        public static int SomeCount<T>(this IEnumerable<Option<T>> list) => list.Count(opt => opt.Match(() => false, _ => true));

        public static IEnumerable<R> Bind<T, R>(this IEnumerable<T> list, Func<T, Option<R>> func) => list.SelectMany(t => func(t).AsEnumerable());
        public static IEnumerable<R> Bind<T, R>(this Option<T> opt, Func<T, IEnumerable<R>> func) => opt.AsEnumerable().SelectMany(func);
        
        public static IEnumerable<Option<T>> Where<T>(this IEnumerable<Option<T>> list, Func<T, bool> func)
            => list.Select(opt => opt.Where(func));
        
        public static void ForEach<T>(this IEnumerable<Option<T>> list, Action<T> action)
            => list.SelectMany(opt => opt.AsEnumerable()).ForEach(action);
        
        public static Option<T> FirstOrDefault<T>(this IEnumerable<Option<T>> list, Func<T, bool> func)
            => list.FirstOrDefault(opt => opt.Match(() => false, func));
        
        public static void DoAll<T1, T2>(Option<T1> option1, Option<T2> option2, Action<T1, T2> func)
        {
            option1.Do(v1 => option2.Do(v2 => func(v1, v2)));
        }
    }

    namespace Option
    {
        public struct None
        {
            internal static readonly None Default = new None();
        }

        public struct Some<T>
        {
            internal T Value { get; }
            internal Some(T value)
            {
                if(value == null)
                    throw new ArgumentNullException(nameof(value));
                Value = value;
            }
        }
    }
}