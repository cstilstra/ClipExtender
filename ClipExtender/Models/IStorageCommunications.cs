using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipExtender.Models
{
    public interface IStorageCommunications
    {

        void AddCopy(string textToAdd);
        void ClearClipboard();
        void RemoveItemFromClipboard(int clipboardLineID);
        IEnumerable<string> GetStorageItems();

    }
}
