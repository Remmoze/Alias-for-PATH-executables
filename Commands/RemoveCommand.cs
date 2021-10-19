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
                Example = $"{Globals.ProgramPrefix} remove photos"
            };
        }

        public override CommandResponse TryExecute(string[] arguments)
        {
            var shortcut = arguments[0];
            if (arguments.Length < 1) {
                return new CommandResponse(CommandOutput.Too_Few_Arguments);
            }
            else if (arguments.Length > 1) {
                return new CommandResponse(CommandOutput.Too_Many_Arguments);
            }

            if (Shortcuts.FirstOrDefault(sc => sc.ShortName == shortcut) == null) {
                return new CommandResponse(CommandOutput.Fail, $"Failed to remove shortcut \"{arguments[0]}\", it does not exists!");
            }

            return OnExecute(arguments);
        }

        public override CommandResponse OnExecute(string[] arguments)
        {
            var shortcut = arguments[0];
            if (!CMDParser.Storage.RemoveShortcut(shortcut)) {
                return new CommandResponse(CommandOutput.Fail, "Could not remove the shortcut.");
            }
            return new CommandResponse(CommandOutput.Success, $"Shortcut \"{shortcut} has been removed!\"");
        }
    }
}


