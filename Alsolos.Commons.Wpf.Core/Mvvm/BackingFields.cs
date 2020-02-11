using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Alsolos.Commons.Wpf.Core.Mvvm
{
    public class BackingFields
    {
        private readonly Dictionary<string, PropertyChangedEventHandler> _observers;
        private readonly Dictionary<string, object> _properties;

        private readonly Action<string> _propertyChangedEventHandler;

        public BackingFields(Action<string> propertyChangedEventHandler)
        {
            _properties = new Dictionary<string, object>();
            _observers = new Dictionary<string, PropertyChangedEventHandler>();
            _propertyChangedEventHandler = propertyChangedEventHandler;
        }

        public T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return GetValue<T>(propertyName, null);
        }

        public T GetValue<T>(Func<T> initializeFunction, [CallerMemberName] string propertyName = null)
        {
            return GetValue(propertyName, initializeFunction);
        }

        public T GetValue<T>(string propertyName, Func<T> initializeFunction)
        {
            if (_properties.TryGetValue(propertyName, out var value))
            {
                return (T)value;
            }

            if (initializeFunction != null)
            {
                var initialValue = initializeFunction.Invoke();
                _properties[propertyName] = initialValue;
                return initialValue;
            }

            return default;
        }

        public T GetValueAndObserve<T>(Func<T> initializeFunction, PropertyChangedEventHandler observer, [CallerMemberName] string propertyName = null)
            where T : class, INotifyPropertyChanged
        {
            return GetValueAndObserve(propertyName, initializeFunction, observer);
        }

        public T GetValueAndObserve<T>(Func<T> initializeFunction, [CallerMemberName] string propertyName = null)
            where T : class, INotifyPropertyChanged
        {
            return GetValueAndObserve(propertyName, initializeFunction, GetObserver(propertyName));
        }

        public DelegateCommand GetCommand(Action executeMethod, [CallerMemberName] string propertyName = null)
        {
            return GetCommand(propertyName, executeMethod, null);
        }

        public DelegateCommand GetCommand(Action executeMethod, Func<bool> canExecuteMethod, [CallerMemberName] string propertyName = null)
        {
            return GetCommand(propertyName, executeMethod, canExecuteMethod);
        }

        public DelegateCommand<T> GetCommand<T>(Action<T> executeMethod, [CallerMemberName] string propertyName = null)
        {
            return GetCommand(propertyName, executeMethod, null);
        }

        public DelegateCommand<T> GetCommand<T>(Action<T> executeMethod, Func<T, bool> canExecuteMethod, [CallerMemberName] string propertyName = null)
        {
            return GetCommand(propertyName, executeMethod, canExecuteMethod);
        }

        public bool SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            return SetValue(propertyName, value, null);
        }

        public bool SetValue<T>(T value, Action<T> propertyChangedCallback, [CallerMemberName] string propertyName = null)
        {
            return SetValue(propertyName, value, propertyChangedCallback);
        }

        public bool SetValue<T>(string propertyName, T value, Action<T> propertyChangedCallback)
        {
            var previousValue = GetValue<T>(propertyName, null);

            if (!Equals(previousValue, value))
            {
                _properties[propertyName] = value;
                RaisePropertyChanged(propertyName);
                if (propertyChangedCallback != null)
                {
                    propertyChangedCallback.Invoke(value);
                }

                return true;
            }

            return false;
        }

        public bool SetValueAndObserve<T>(T value, PropertyChangedEventHandler observer, [CallerMemberName] string propertyName = null)
            where T : class, INotifyPropertyChanged
        {
            return SetValueAndObserve(propertyName, value, observer);
        }

        public bool SetValueAndObserve<T>(T value, [CallerMemberName] string propertyName = null)
            where T : class, INotifyPropertyChanged
        {
            return SetValueAndObserve(propertyName, value, GetObserver(propertyName));
        }

        private T GetValueAndObserve<T>(string propertyName, Func<T> initializeFunction, PropertyChangedEventHandler observer)
            where T : class, INotifyPropertyChanged
        {
            if (_properties.TryGetValue(propertyName, out var value))
            {
                return (T)value;
            }

            if (initializeFunction != null)
            {
                var initialValue = initializeFunction.Invoke();
                if (initialValue != null)
                {
                    initialValue.PropertyChanged += observer;
                }

                _properties[propertyName] = initialValue;
                return initialValue;
            }

            return default;
        }

        private DelegateCommand<T> GetCommand<T>(string propertyName, Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if (canExecuteMethod == null)
            {
                canExecuteMethod = value => true;
            }

            return GetValue(propertyName, () => new DelegateCommand<T>(executeMethod, canExecuteMethod));
        }

        private DelegateCommand GetCommand(string propertyName, Action executeMethod, Func<bool> canExecuteMethod)
        {
            if (canExecuteMethod == null)
            {
                canExecuteMethod = () => true;
            }

            return GetValue(propertyName, () => new DelegateCommand(executeMethod, canExecuteMethod));
        }

        private bool SetValueAndObserve<T>(string propertyName, T value, PropertyChangedEventHandler observer)
            where T : class, INotifyPropertyChanged
        {
            var previousValue = GetValue<T>(propertyName, null);

            if (previousValue != value)
            {
                if (observer != null)
                {
                    if (previousValue != null)
                    {
                        previousValue.PropertyChanged -= observer;
                    }

                    if (value != null)
                    {
                        value.PropertyChanged += observer;
                    }
                }

                _properties[propertyName] = value;
                RaisePropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        private PropertyChangedEventHandler GetObserver(string propertyName)
        {
            return GetObserver(propertyName, (sender, args) => RaisePropertyChanged(propertyName));
        }

        private PropertyChangedEventHandler GetObserver(string propertyName, PropertyChangedEventHandler initialObserver)
        {
            if (_observers.TryGetValue(propertyName, out var observer))
            {
                return observer;
            }

            _observers[propertyName] = initialObserver;
            return initialObserver;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (_propertyChangedEventHandler != null)
            {
                _propertyChangedEventHandler.Invoke(propertyName);
            }
        }
    }
}