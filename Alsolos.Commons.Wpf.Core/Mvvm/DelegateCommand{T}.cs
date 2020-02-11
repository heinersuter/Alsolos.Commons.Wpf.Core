using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Alsolos.Commons.Wpf.Core.Mvvm
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _executeAction;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _executeAction = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            Application.Current.Dispatcher?.Invoke(DispatcherPriority.DataBind, new Action(CommandManager.InvalidateRequerySuggested));
        }
    }
}