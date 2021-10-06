using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRunCommands
{
    class Shortcut
    {
        public Shortcut(JsonShortcut data)
        {

        }
    }
    class Shortcuts
    {
        public List<Shortcut> ShortcutsList = new List<Shortcut>();
        public void ImportStorage(JsonStorage storage)
        {
            foreach(var sc in storage.Shortcuts)
            {
                ShortcutsList.Add(new Shortcut(sc));
            }
        }
    }
}
