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

using ClipExtender.Models;
using System;
using System.Collections.Generic;

namespace ClipExtender
{
    public class Extender
    {
        #region Fields

        IStorageCommunications _storageComms;
        ClipboardCommunication _clipboardComms;

        #endregion

        #region Constructors

        public Extender(IStorageCommunications storage, IntPtr viewHandle)
        {
            SetUpReferences(storage);
            ClearClipboard();
            _clipboardComms.BeginListeningToClipboard(viewHandle);
        }

        #endregion

        #region Public Functions

        public void ClearClipboard()
        {
            _storageComms.ClearClipboard();
        }

        public void RemoveItemFromClipboard(string item)
        {
            _storageComms.RemoveItemFromClipboard(item);
        }

        public bool HandleMessage(int message)
        {
            string text = "";
            if (_clipboardComms.HandleUpdateMessage(message, ref text))
            {
                if (!_storageComms.Contains(text))
                {
                    _storageComms.AddCopy(text);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<string> GetCopiedItems()
        {
            return _storageComms.GetStorageItems();
        }

        #endregion

        #region Private Helpers

        private void SetUpReferences(IStorageCommunications storage)
        {
            _storageComms = storage;
            _clipboardComms = new ClipboardCommunication();
        }

        #endregion
    }
}
