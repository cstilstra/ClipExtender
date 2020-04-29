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

using System.Collections.Generic;

namespace ClipExtender.Models
{
    public interface IStorageCommunications
    {
        void AddCopy(string textToAdd);
        void ClearClipboard();
        void RemoveItemFromClipboard(string item);
        IEnumerable<string> GetStorageItems();
        bool Contains(string item);
    }
}
