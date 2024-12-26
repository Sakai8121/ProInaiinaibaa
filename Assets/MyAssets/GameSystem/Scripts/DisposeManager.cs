#nullable enable
using System;
using UniRx;

namespace General
{
    public class DisposeManager : IDisposable
    {
        public CompositeDisposable CompositeDisposable => _compositeDisposable;
        readonly CompositeDisposable _compositeDisposable = new();

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}