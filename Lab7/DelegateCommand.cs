using System;
using System.Windows.Input;

namespace Paprotski.Lab7
{
    public class DelegateCommand : ICommand
    {
        private readonly Action executeMethod;
        private readonly Func<bool> canExecuteMethod;
        private readonly bool isAutomaticRequeryDisabled;

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
        {
            if (executeMethod == null)
            {
                return;
            }

            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
            this.isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return this.canExecuteMethod == null || this.canExecuteMethod();
        }

        void ICommand.Execute(object parameter)
        {
            if (this.executeMethod != null)
            {
                this.executeMethod();
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (!this.isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (!this.isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }
    } 
}
