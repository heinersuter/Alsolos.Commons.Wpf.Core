using System;
using System.Collections.Generic;
using Alsolos.Commons.Wpf.Core.Mvvm;

namespace Alsolos.Commons.Wpf.Core.Progress
{
    public class BusyHelper : BackingFieldsHolder
    {
        private readonly Stack<string> _stack = new Stack<string>();

        public event EventHandler<bool> IsBusyChanged;

        public bool IsBusy
        {
            get => BackingFields.GetValue<bool>();
            set => BackingFields.SetValue(value, OnIsBusyChanged);
        }

        public string Message
        {
            get => BackingFields.GetValue<string>();
            set => BackingFields.SetValue(value);
        }

        public BusyState Enter(string message)
        {
            _stack.Push(message);
            Message = message;
            IsBusy = true;
            return new BusyState(Leave);
        }

        protected virtual void OnIsBusyChanged(bool value)
        {
            var handler = IsBusyChanged;
            if (handler != null)
            {
                handler.Invoke(this, value);
            }
        }

        private void Leave()
        {
            _stack.Pop();
            Message = _stack.Count > 0 ? _stack.Peek() : null;
            IsBusy = _stack.Count > 0;
        }
    }
}