using MDIContainer.Control.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace App.Control.Param.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<IContent> Items { get; private set; }

        private IContent _selectedWindow = null;
        public IContent SelectedWindow
        {
            get
            {
                return this._selectedWindow;
            }
            set
            {
                this._selectedWindow = value;
                this.RaisePropertyChanged("SelectedWindow");
            }
        }

        public RelayCommand NewCommand { get; private set; }

        public MainWindowViewModel()
        {
            this.Items = new ObservableCollection<IContent>();

            this.NewCommand = new RelayCommand(Action => { NewWindow(); }, x => x == null);
        }

        private void NewWindow()
        {
            var item = new TestViewViewModel();
            item.Closing += (s, e) => this.Items.Remove(item);
            this.Items.Add(item);
        }
    }
}
