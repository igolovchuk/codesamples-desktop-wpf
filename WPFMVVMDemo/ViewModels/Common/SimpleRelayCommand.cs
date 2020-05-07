using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tracker_Software.ViewModels.Common
{
    public class SimpleRelayCommand : ICommand
    {
        private readonly Action<object> execute;

        public event EventHandler CanExecuteChanged;

        public SimpleRelayCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
