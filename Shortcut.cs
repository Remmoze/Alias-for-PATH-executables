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

        public string FilePath { get {
                var dirname = System.IO.Path.GetDirectoryName(Path);
                //GetDirectoryName doesn't return '\' in the end, but if the directory is 'c:', it does
                if (!dirname.EndsWith("\\")) dirname += "\\"; 
                return dirname;
            } }
        public string FileName { get { return System.IO.Path.GetFileName(Path); } }
        public string FilePureName { get { return System.IO.Path.GetFileNameWithoutExtension(Path); } }
        public string ShortFilePath { get { return FilePath + ShortName + ".exe"; } }

        public bool DoesAlreadyExist { get { return File.Exists(ShortFilePath); } }
        public bool IsMatchingName { get { return FilePureName == ShortName; } }

        public Shortcut() { }
        public Shortcut(string name, string path) => (ShortName, Path) = (name, path);
        public Shortcut(Tuple<string, string> data) : this(data.Item1, data.Item2) { }

        public bool Install()
        {
            Debug.WriteLine($"Installing a new shortcut \"{ShortName}\" to {Path}");

            if(DoesAlreadyExist)
            {
                Console.WriteLine("Can not create a shortcut: program with that name already exists.");
                return false;
            }

            if(!IsMatchingName)
            {
                File.Copy(Path, ShortFilePath);
            }

            var reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths", true);
            reg = reg.CreateSubKey("crc." + ShortName);

            reg.SetValue("", ShortFilePath);
            reg.SetValue("Path", FilePath);
            return true;
        }
    }
}
