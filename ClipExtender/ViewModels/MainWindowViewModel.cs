using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipExtender.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private Extender _extender;
        private ClipboardCommunication _clipboardCommunication;

        #endregion

        #region Properties

        private ObservableCollection<string> _CopiedItems;
        public ObservableCollection<string> CopiedItems
        {
            get { return _CopiedItems; }
            set { SetProperty(ref _CopiedItems, value); }
        }

        #region Commands

        private DelegateCommand _RemoveCommand;
        public DelegateCommand RemoveCommand
        {
            get
            {
                if(_RemoveCommand == null)
                {
                    _RemoveCommand = new DelegateCommand(
                            () => OnRemove()
                            );
                }
                return _RemoveCommand;
            }
        }

        private DelegateCommand _ClearCommand;
        public DelegateCommand ClearCommand
        {
            get
            {
                if (_ClearCommand == null)
                {
                    _ClearCommand = new DelegateCommand(
                            () => OnClear()
                            );
                }
                return _ClearCommand;
            }
        }

        #endregion

        #endregion

        #region Constructors

        public MainWindowViewModel(IntPtr viewHandle)
        {
            CopiedItems = new ObservableCollection<string>();
            _extender = new Extender(new ListTypeStorageCommunications(), viewHandle);
        }

        #endregion

        #region Command Helpers

        public void HandleClipboardMessage(int message)
        {
            IEnumerable<string> items = _extender.HandleMessage(message);

            if(items != null)
            {
                CopiedItems = new ObservableCollection<string>(items);
            }

            
        }

        private void OnRemove()
        {
            Console.WriteLine("OnRemove called");
            CopiedItems.Add("NewItem");
            RaisePropertyChanged("CopiedItems");
        }

        private void OnClear()
        {
            Console.WriteLine("OnClear called");
        }

        #endregion
    }


}
