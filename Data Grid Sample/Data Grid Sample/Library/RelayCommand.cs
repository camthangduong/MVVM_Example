using System;
using System.Windows.Input;

namespace Data_Grid_Sample.Library
{
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// Private 
        /// </summary>
        /// 
        private Action<T> TargetExecuteMethod;
        private readonly Func<T, bool> TargetCanExecuteMethod;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public RelayCommand(Action<T> executeMethod)
        {
            TargetExecuteMethod = executeMethod;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            TargetExecuteMethod = executeMethod;
            TargetCanExecuteMethod = canExecuteMethod;
        }

        #endregion

        #region ICommand Interface implementation

        /// <summary>
        /// Implement for the interface of the ICommand
        /// </summary>
        /// 

        /***
         * A delegate method is a type that safely encapsulates a method
         * Similar to function pointer in C/C+
        **/
        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            if (TargetCanExecuteMethod != null)
            {
                // If can execute method not empty
                T tpam = (T)parameter; // Type conversion (cast the type of the parameter to type T)
                return TargetCanExecuteMethod(tpam);
            }
            if (TargetExecuteMethod != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            if (TargetExecuteMethod != null)
            {
                TargetExecuteMethod.Invoke((T)parameter);
            }
        }
        #endregion

        #region Public event

        /// <summary>
        /// Executre the can execute event
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        #endregion
    }
}
