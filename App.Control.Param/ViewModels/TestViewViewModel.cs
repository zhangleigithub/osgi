using MDIContainer.Control.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace App.Control.Param.ViewModels
{
    public class TestViewViewModel : ViewModelBase, IContent
    {
        private bool dirty;

        public string Title
        {
            get
            {
                return "Test";
            }
        }

        public bool CanClose
        {
            get
            {
                return this.dirty == false || MessageBox.Show("Changes were made. Do you want to close this window?", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            }
        }

        public event EventHandler Closing;

        public RelayCommand CloseCommand { get; private set; }

        public TestViewViewModel()
        {
            dirty = false;
            this.CloseCommand = new RelayCommand(CloseWindow);
        }

        private void CloseWindow(object p)
        {
            if (this.CanClose)
            {
                var hander = this.Closing;
                if (hander != null)
                {
                    hander(this, EventArgs.Empty);
                }
            }
        }
    }
}
