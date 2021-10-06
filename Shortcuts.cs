using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;

#pragma warning disable CA1416

namespace CustomRunCommands
{
    class Shortcut
    {
        public string ShortName { get; set; }
        public string Path { get; set; }
        public Shortcut(string name, string path)
        {
            (ShortName, Path) = (name, path);
        }
        public Shortcut(Tuple<string, string> data) : this(data.Item1, data.Item2) { }
        public Shortcut(JsonShortcut data) : this(data.ShortName, data.Path) { }

        public void Install()
        {
            Debug.WriteLine($"Installing a new shortcut \"{ShortName}\" to {Path}");

            var reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths");
            Console.WriteLine(reg);

        }
    }
    class Shortcuts
    {
        public List<Shortcut> ShortcutsList = new List<Shortcut>();


        public void ImportStorage(JsonStorage storage)
        {
            storage.Shortcuts.ForEach(shortcut => ShortcutsList.Add(new Shortcut(shortcut)));
        }
    }
}
