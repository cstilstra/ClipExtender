// This file is part of ClipExtender.

// ClipExtender is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// ClipExtender is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with ClipExtender.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;

namespace ClipExtender.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private Extender _extender;
        private Timer _timer;
        private bool handledMessage = false;

        #endregion

        #region Properties

        private ObservableCollection<string> _CopiedItems;
        public ObservableCollection<string> CopiedItems
        {
            get { return _CopiedItems; }
            set { SetProperty(ref _CopiedItems, value); }
        }

        private string _SelectedItem;
        public string SelectedItem
        {
            get { return _SelectedItem; }
            set 
            {
                if (value != null)
                {
                    handledMessage = true;
                    Clipboard.SetDataObject(value);
                    SetProperty(ref _SelectedItem, value);
                    _timer.Start();
                }
            }
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
            _timer = new Timer(100);
            _timer.Elapsed += OnTimerTick;
        }

        #endregion

        #region Command Helpers

        public void HandleClipboardMessage(int message)
        {
            if (!handledMessage)
            {
                if (_extender.HandleMessage(message)) SetCopiedItemsFromStorage();            
            }
        }

        private void OnTimerTick(Object sender, ElapsedEventArgs e)
        {
            handledMessage = false;
        }

        private void OnRemove()
        {
            _extender.RemoveItemFromClipboard(SelectedItem);
            SetCopiedItemsFromStorage();
        }

        private void OnClear()
        {
            _extender.ClearClipboard();
            CopiedItems = new ObservableCollection<string>();
        }

        #endregion

        #region Private Helpers

        private void SetCopiedItemsFromStorage()
        {
            IEnumerable<string> items = _extender.GetCopiedItems();

            if (items != null)
            {
                CopiedItems = new ObservableCollection<string>(items);
                SelectedItem = CopiedItems.LastOrDefault();
            }
        }

        #endregion
    }


}
