﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Diagnostics;
using Microsoft.Win32;

namespace Alias_for_executables
{
    public class Storage
    {
        public List<Shortcut> Shortcuts = new();

        public Storage()
        {
            var reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths", true);
            foreach (var key in reg.GetSubKeyNames()) {
                var regShortcut = reg.OpenSubKey(key);
                if (regShortcut.GetValue("Alias") != null) {

                    var cleanKey = key;
                    if (key.EndsWith(".exe"))
                        cleanKey = key.Substring(0, key.Length - key.IndexOf(".exe") + 1);

                    var date = regShortcut.GetValue("CreationDate");

                    Shortcuts.Add(new Shortcut() {
                        ShortName = cleanKey,
                        Path = (string)regShortcut.GetValue(""),
                        CreationDate = date == null ? null : DateTime.Parse((string)date),
                    });
                }
            }
        }

        public bool AddShortcut(Shortcut shortcut)
        {
            if (shortcut.Install()) {
                Shortcuts.Add(shortcut);
                Debug.WriteLine($"Added a new storage shortcut \"{shortcut.ShortName}\" to {shortcut.Path}");
                return true;
            }
            return false;
        }

        public bool RemoveShortcut(string shortcut) =>
            RemoveShortcut(Shortcuts.FirstOrDefault(sc => sc.ShortName.Equals(shortcut)));

        public bool RemoveShortcut(Shortcut shortcut)
        {
            return shortcut.Uninstall() && Shortcuts.Remove(shortcut);
        }
    }
}
