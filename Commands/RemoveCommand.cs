using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias_for_executables.Commands
{
    class RemoveCommand : Command
    {
        private List<Shortcut> Shortcuts => CMDParser.Storage.Shortcuts;

        public RemoveCommand(CommandParser parser) : base(parser, "remove", new List<string>() { "r", "d", "delete" })
        {
            Help = new HelpOutput() {
                Arguments = "<shortcut>",
                Discription = "Remove a shortcut",
                Example = "crc remove photos"
            };
        }

        public override Error TryExecute(string[] arguments)
        {
            if (arguments.Length < 1) {
                return new Error(CommandOutput.Too_Few_Arguments);
            }
            else if (arguments.Length > 1) {
                return new Error(CommandOutput.Too_Many_Arguments);
            }

            if (Shortcuts.FirstOrDefault(sc => sc.ShortName == arguments[0]) == null) {
                return new Error(CommandOutput.Fail, $"Failed to remove shortcut \"{arguments[0]}\", it does not exists!");
            }

            return OnExecute(arguments);
        }

        public override Error OnExecute(string[] arguments)
        {
            if (!CMDParser.Storage.RemoveShortcut(arguments[0])) {
                return new Error(CommandOutput.Success);
            }
            return new Error(CommandOutput.Fail);
        }
    }
}


