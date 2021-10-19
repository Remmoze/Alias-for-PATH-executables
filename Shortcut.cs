using System;
using System.Linq;
using System.Diagnostics;

using Microsoft.Win32;

namespace Alias_for_executables
{
    public class Alias
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime? CreationDate { get; set; }

        public string FileDirectory => System.IO.Path.GetDirectoryName(Path);

        public Alias() { }
        public Alias(string name, string path) => (Name, Path) = (name, path);

        public bool Install()
        {
            Debug.WriteLine($"Installing a new alias \"{Name}\" to {Path}");

            try {
                var reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths", true);
                reg = reg.CreateSubKey(Name + ".exe");

                reg.SetValue("", Path);
                reg.SetValue("Path", FileDirectory);
                reg.SetValue("Alias", true);

                CreationDate = DateTime.Now;
                reg.SetValue("CreationDate", DateTime.Now);
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Uninstall()
        {
            Debug.WriteLine($"Deleting an alias \"{Name}\" from {Path}");

            try {
                var reg = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\", true);

                if (!reg.GetSubKeyNames().Contains($"{Name}.exe")) {
                    Debug.WriteLine($"Alias \"{Name}\" was not found in registry");
                    return true;
                }

                reg.DeleteSubKey(Name + ".exe");
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
