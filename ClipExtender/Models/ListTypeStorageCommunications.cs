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
using System.Collections.Generic;

namespace ClipExtender
{
    public class ListTypeStorageCommunications : IStorageCommunications
    {
        #region Fields

        List<string> _storage;

        #endregion

        #region Constructors

        public ListTypeStorageCommunications()
        {
            ClearClipboard();
        }

        #endregion

        #region Public Functions

        public void AddCopy(string textToAdd)
        {
            _storage.Add(textToAdd);

        }

        public void ClearClipboard()
        {
            _storage = new List<string>();
        }

        public bool Contains(string item)
        {
            return _storage.Contains(item);
        }

        public IEnumerable<string> GetStorageItems()
        {
            return _storage;
        }

        public void RemoveItemFromClipboard(string item)
        {
            if (Contains(item))
            {
                _storage.Remove(item);
            }
        }

        #endregion
    }
}
