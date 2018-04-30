using System;
using System.Windows.Input;

namespace Silent_Update.Utilities
{
    public class MyICommand<T> : ICommand
    {
        /// <summary>
        /// Private 
        /// </summary>
        Action<T> _TargetExecuteMethod;
        Func<T, bool> _TargetCanExecuteMethod;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="executeMethod"></param>
        public MyICommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public MyICommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Public event
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Inteface Implementation
        /// </summary>

        // A delegate method is a type that safely encapsulates a method
        // Similar to function pointer in C/C++
        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                // If can execute method not empty
                T tpam = (T)parameter; // Type conversion (cast the type of the parameter to type T)
                return _TargetCanExecuteMethod(tpam);
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod.Invoke((T)parameter);
            }
        }
    }
}
