using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner_UI
{
    internal class Command : ICommand
    {
        Action<object> ExecuteMethod;
        Func<object, bool> CanExecuteMethod;
        public Command(Action<object> ExecuteMethod, Func<object, bool> CanExecuteMethod)
        {
            this.ExecuteMethod = ExecuteMethod;
            this.CanExecuteMethod = CanExecuteMethod;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ExecuteMethod(parameter);
        }
    }
}
