using System;
using System.Windows.Input;

namespace MVVC_Binding.Utilities
{
    public class MyICommand<T> : ICommand
    {
        Action<T> _TargetExecuteMethod;
        Func<T, bool> _TargetCanExecuteMethod;

        public MyICommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public MyICommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }


        /// <summary>
        /// Interface Implemetation
        /// </summary>

        // A delegate method is a type that safely encapsulates a method
        // Similar to function pointer in C and C++
        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                T tpam = (T)parameter;
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
