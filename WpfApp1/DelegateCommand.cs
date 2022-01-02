using System;
using System.Windows.Input;

namespace WpfApp1
{
    internal class DelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private static readonly Func<T,bool> defaultCanExecuteMethod = (T) => true;

        private readonly Func<T,bool> canExecuteMethod;
        private readonly Action<T> executeMethod;

        public DelegateCommand(Action<T> executeMethod) :
            this(executeMethod, defaultCanExecuteMethod)
        {}

        public DelegateCommand(Action<T> executeMethod, Func<T,bool> canExecuteMethod)
        {
            this.canExecuteMethod = canExecuteMethod;
            this.executeMethod = executeMethod;
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
            => CanExecuteChanged?.Invoke(this, e);
        

        public void RaiseCanExecuteChanged()
            => OnCanExecuteChanged(EventArgs.Empty);


        bool ICommand.CanExecute(object parameter)
        {
            return parameter is T ? CanExecute((T)parameter) : false;
        }
        

        void ICommand.Execute(object parameter)
        {
            if(parameter is T)
            {
                Execute((T)parameter);
            }
        }
        public virtual void Execute(T param)
            => executeMethod?.Invoke(param);

        bool CanExecute(T parameter)
            => canExecuteMethod(parameter);
    }
    internal class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private static readonly Func<bool> defaultCanExecuteMethod = () => true;

        private readonly Func<bool> canExecuteMethod;
        private readonly Action executeMethod;

        public DelegateCommand(Action executeMethod) :
            this(executeMethod, defaultCanExecuteMethod)
        { }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.canExecuteMethod = canExecuteMethod;
            this.executeMethod = executeMethod;
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
            => CanExecuteChanged?.Invoke(this, e);


        public void RaiseCanExecuteChanged()
            => OnCanExecuteChanged(EventArgs.Empty);


        bool ICommand.CanExecute(object parameter)
            => CanExecute();
        


        void ICommand.Execute(object parameter)
            => Execute();
        public virtual void Execute()
            => executeMethod?.Invoke();

        bool CanExecute()
            => canExecuteMethod();
    }
}