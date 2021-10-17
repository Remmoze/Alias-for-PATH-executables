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
            catch (SecurityException SException) {
                Console.WriteLine("The user does not have the permissions required to create or open the registry key.");
                return false;
            }
            catch (UnauthorizedAccessException YAException) {
                Console.WriteLine("The RegistryKey cannot be written to; for example, it was not opened as a writable key , or the user does not have the necessary access rights.");
                return false;
            }
        }
    }
}
