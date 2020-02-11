using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Alsolos.Commons.Wpf.Core.Mvvm
{
    public abstract class BackingFieldsHolder : INotifyPropertyChanged
    {
        private BackingFields _backingFields;

        protected BackingFieldsHolder()
        {
            PropertyChanged += OnPropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public BackingFields BackingFields
        {
            get { return PropertyHelper.CreateIfNeeded(ref _backingFields, () => new BackingFields(RaisePropertyChanged)); }
        }

        public T CreateIfNeeded<T>(Func<T> newInstanceCreateMethod, [CallerMemberName] string propertyName = null)
        {
            if (newInstanceCreateMethod == null)
            {
                return default;
            }

            if (Equals(BackingFields.GetValue<T>(propertyName, null), default(T)))
            {
                BackingFields.SetValue(propertyName, newInstanceCreateMethod.Invoke(), null);
            }

            return BackingFields.GetValue<T>(propertyName, null);
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return PropertyHelper.GetName(propertyExpression);
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = GetPropertyName(propertyExpression);
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var copy = PropertyChanged;
            if (copy != null)
            {
                copy.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}