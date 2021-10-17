using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Security;

#pragma warning disable CA1416

namespace CustomRunCommands
{
    public class Shortcut
    {
        public string ShortName { get; set; }
        public string Path { get; set; }
        public DateTime CreationDate { get; set; }

        public string FileDirectory
        {
            get
            {
                var dirname = System.IO.Path.GetDirectoryName(Path);
                //GetDirectoryName doesn't return '\' in the end, but if the directory is 'c:', it does
                if (!dirname.EndsWith("\\")) dirname += "\\";
                return dirname;
            }
        }

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
                    Debug.WriteLine($"Tried removing shortcut \"{ShortName}\" from the registery, but it was absent.");
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
