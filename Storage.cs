using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using Microsoft.Win32;

namespace Alias_for_executables
{
    public class Storage
    {
        public List<Alias> Aliases = new();
        public Storage()
        {
            var reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths", true);
            foreach (var key in reg.GetSubKeyNames()) {
                var regAlias = reg.OpenSubKey(key);
                if (regAlias.GetValue("Alias") == null)
                    continue;

                var cleanKey = key;
                if (key.EndsWith(".exe"))
                    cleanKey = key.Substring(0, key.Length - key.IndexOf(".exe") + 1);

                var date = regAlias.GetValue("CreationDate");

                Aliases.Add(new Alias() {
                    Name = cleanKey,
                    Path = (string)regAlias.GetValue(""),
                    CreationDate = date == null ? null : DateTime.Parse((string)date),
                });
            }
        }

        public bool AddAlias(Alias alias)
        {
            if (alias.Install()) {
                Aliases.Add(alias);
                Debug.WriteLine($"Added a new storage alias \"{alias.Name}\" to {alias.Path}");
                return true;
            }
            return false;
        }

        public bool RemoveAlias(string alias) =>
            RemoveAlias(Aliases.FirstOrDefault(sc => sc.Name.Equals(alias)));

        public bool RemoveAlias(Alias alias) =>
            alias.Uninstall() && Aliases.Remove(alias);
    }
}
