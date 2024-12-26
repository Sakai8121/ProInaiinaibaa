using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MobileLibrary.Function
{
    public class Error
    {
        public virtual string Message { get; }
    }
    
    public class Validation<T>
    {
        private readonly IEnumerable<Error> left;
        private readonly T right;
        private readonly bool isLeft;

        private Validation(IEnumerable<Error> left, T right, bool isLeft)
        {
            this.left = left;
            this.right = right;
            this.isLeft = isLeft;
        }

        public static Validation<T> Left(IEnumerable<Error> value)
        {
            return new Validation<T>(value, default, true);
        }

        public static Validation<T> Valid(T value)
        {
            return new Validation<T>(Enumerable.Empty<Error>(), value, false);
        }

        public U Match<U>(Func<IEnumerable<Error>, U> leftFunc, Func<T, U> rightFunc)
        {
            return isLeft ? leftFunc(left) : rightFunc(right);
        }

        public Validation<U> Map<U>(Func<T, U> f)
        {
            return isLeft ? Validation<U>.Left(left) : Validation<U>.Valid(f(right));
        }

        public Validation<U> Bind<U>(Func<T, Validation<U>> f)
        {
            return isLeft ? Validation<U>.Left(left) : f(right);
        }
        
        public static Func<T, Validation<T>> FailTest(IEnumerable<Func<T, Validation<T>>> validators)
            => t => validators.Aggregate(Valid(t), (acc, validator) => acc.Bind(validator));
    }
}