using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Alsolos.Commons.Wpf.Core.Mvvm
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _executeAction;

        public DelegateCommand(Action execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
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

            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(CommandManager.InvalidateRequerySuggested));
        }
    }
}