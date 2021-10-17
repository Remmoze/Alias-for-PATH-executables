using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

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
        public string OriginalFileName => System.IO.Path.GetFileName(Path);
        public string OriginalFileNameWithoutExtention => System.IO.Path.GetFileNameWithoutExtension(Path);
        public string ShortcutPath => FileDirectory + ShortName + ".exe";

        public bool DoesAlreadyExist => File.Exists(ShortcutPath);
        public bool IsMatchingName => OriginalFileNameWithoutExtention.Equals(ShortName, StringComparison.OrdinalIgnoreCase);

        public Shortcut() { }
        public Shortcut(string name, string path) => (ShortName, Path) = (name, path);

        public bool Install()
        {
            Debug.WriteLine($"Installing a new shortcut \"{ShortName}\" to {Path}");

            // case 1: shortname matches executable name
            if (IsMatchingName) {
                InstallRegistry();
                return true;
            }

            // case 2: shortname matches another program in the same directory
            if (DoesAlreadyExist) {
                if (CompareFiles(ShortcutPath, Path)) {
                    //shortcut already exists O_o
                    InstallRegistry();
                    return true;
                }
                else {
                    Console.WriteLine("Can not create a shortcut: program with that name already exists.");
                    return false;
                }
            }

            // case 3: shortname is unique
            File.Copy(Path, ShortcutPath);
            InstallRegistry();
            return true;
        }

        private void InstallRegistry()
        {

            // Need to check if registery key already exists!!

            var reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths", true);
            reg = reg.CreateSubKey("crc." + ShortName);

            reg.SetValue("", ShortcutPath);
            reg.SetValue("Path", FileDirectory);
        }

        private static bool CompareFileSizes(string file1, string file2)
        {
            return ((new FileInfo(file1)).Length == (new FileInfo(file2)).Length);
        }

        //https://developpaper.com/the-fastest-way-to-compare-the-contents-of-two-files-in-net-core/
        private static bool CompareFiles(string file1, string file2)
        {
            if (!CompareFileSizes(file1, file2)) {
                return false;
            }

            const int BYTES_TO_READ = 1024 * 10;

            using (FileStream fs1 = File.Open(file1, FileMode.Open))
            using (FileStream fs2 = File.Open(file2, FileMode.Open)) {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];
                while (true) {
                    int len1 = fs1.Read(one, 0, BYTES_TO_READ);
                    int len2 = fs2.Read(two, 0, BYTES_TO_READ);

                    if (!((ReadOnlySpan<byte>)one).SequenceEqual((ReadOnlySpan<byte>)two))
                        return false;

                    if (len1 == 0 || len2 == 0)
                        return true;
                }
            }
        }
    }
}
