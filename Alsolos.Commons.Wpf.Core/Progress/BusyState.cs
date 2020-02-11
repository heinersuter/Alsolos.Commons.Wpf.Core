using System;
using Alsolos.Commons.Wpf.Core.Mvvm;

namespace Alsolos.Commons.Wpf.Core.Progress
{
    public class BusyState : BackingFieldsHolder, IDisposable
    {
        private Action _callback;

        public BusyState(Action callback)
        {
            _callback = callback;
        }

        public void Dispose()
        {
            _callback.Invoke();
            _callback = null;
        }
    }
}