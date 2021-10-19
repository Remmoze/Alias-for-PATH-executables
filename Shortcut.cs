using System;
using System.Linq;
using System.Diagnostics;

using Microsoft.Win32;

namespace Alias_for_executables
{
    public class Shortcut
    {
        public string ShortName { get; set; }
        public string Path { get; set; }
        public DateTime? CreationDate { get; set; }

        public string FileDirectory => System.IO.Path.GetDirectoryName(Path);

        public Shortcut() { }
        public Shortcut(string name, string path) => (ShortName, Path) = (name, path);

        public bool Install()
        {
            Debug.WriteLine($"Installing a new shortcut \"{ShortName}\" to {Path}");

            try {
                var reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths", true);
                reg = reg.CreateSubKey(ShortName + ".exe");

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
            Debug.WriteLine($"Deleting a shortcut \"{ShortName}\" from {Path}");

            try {
                var reg = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\", true);

                if (!reg.GetSubKeyNames().Contains($"{ShortName}.exe")) {
                    Debug.WriteLine($"Shortcut \"{ShortName}\" was not found in registry");
                    return true;
                }

                reg.DeleteSubKey(ShortName + ".exe");
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
