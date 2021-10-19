using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Alias_for_executables.Commands
{
    class AddCommand : Command
    {
        public AddCommand(CommandParser parser) : base(parser, "add", new List<string>() { "install", "a" })
        {
            Help = new HelpOutput() {
                Arguments = "<shortcut> <path>",
                Discription = "Add a shortcut to the <path> executable",
                Example = "crc add photos \"./PhotosApp/core/app.exe\""
            };
        }

        public override Error TryExecute(string[] arguments)
        {
            if (arguments.Length < 2) {
                return new Error(CommandOutput.Too_Few_Arguments);
            }

            var shortcut = arguments[0];
            if (!Regex.IsMatch(shortcut, @"^[a-zA-Z0-9_]+$")) {
                return new Error(CommandOutput.Incorrect_Argument, $"Shortcut \"{shortcut}\" should only contain letters, numbers, and _");
            }

            var path = Path.GetFullPath(string.Join(" ", arguments.Skip(1)).Trim('\"'));
            if (!VerifyProgram(path)) {
                return new Error(CommandOutput.Incorrect_Argument, $"Failed to verify path \"{path}\"");
            }

            if (CMDParser.Storage.Shortcuts.FirstOrDefault(sc => sc.ShortName == shortcut) != null) {
                return new Error(CommandOutput.Fail, $"Failed to add shortcut \"{shortcut}\", it already exists!");
            }

            return OnExecute(new string[] { shortcut, path });
        }

        public override Error OnExecute(string[] arguments)
        {
            var shortcut = new Shortcut(arguments[0], arguments[1]);

            if (!CMDParser.Storage.AddShortcut(shortcut)) {
                return new Error(CommandOutput.Fail, "Unable to add a shortcut");
            }

            return new Error(CommandOutput.Success, $"Shortcut \"{shortcut.ShortName} has been installed!\"");
        }

        static public bool VerifyProgram(string path)
        {
            var program = Path.GetFileName(path);
            if (Path.GetExtension(path) != ".exe") return false;
            if (string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(path))) return false;
            if (!File.Exists(path)) return false;

            //maybe add more checks later?

            return true;
        }
    }
}
