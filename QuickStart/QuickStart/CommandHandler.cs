using System;
using System.Windows.Input;

namespace QuickStart
{
    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _executed;
        public CommandHandler(Action action, bool executed)
        {
            _action = action;
            _executed = executed;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _executed;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
