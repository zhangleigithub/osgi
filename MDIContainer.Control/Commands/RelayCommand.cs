﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace MDIContainer.Control.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> ExecuteAction { get; set; }

        private Predicate<object> CanExecutePredicate { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommand(Action<object> executeAction)
           : this(executeAction, p => true)
        {
        }

        public RelayCommand(Action<object> executeAction, Predicate<object> canExecutePredicate)
        {
            this.ExecuteAction = executeAction;
            this.CanExecutePredicate = canExecutePredicate;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.CanExecutePredicate == null || this.CanExecutePredicate(parameter);
        }

        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            if (this.ExecuteAction != null)
            {
                this.ExecuteAction(parameter);
            }
        }
    }
}
