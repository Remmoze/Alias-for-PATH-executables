using System.Collections.Generic;
using System.Linq;

namespace Alias_for_executables.Commands
{
    class RemoveCommand : Command
    {
        public RemoveCommand(CommandParser parser) : base(parser, "remove", new List<string>() { "r", "d", "delete" })
        {
            Help = new HelpOutput() {
                Arguments = "<alias>",
                Discription = "Remove an alias",
                Example = $"{Globals.ProgramPrefix} remove photos"
            };
        }

        public override CommandResponse TryExecute(string[] arguments)
        {
            var alias = arguments[0];
            if (arguments.Length < 1) {
                return new CommandResponse(CommandOutput.Too_Few_Arguments);
            }
            else if (arguments.Length > 1) {
                return new CommandResponse(CommandOutput.Too_Many_Arguments);
            }

            if (CMDParser.Storage.Aliases.FirstOrDefault(sc => sc.Name == alias) == null) {
                return new CommandResponse(CommandOutput.Fail, $"Failed to remove alias \"{arguments[0]}\", it does not exists!");
            }

            return OnExecute(arguments);
        }

        public override CommandResponse OnExecute(string[] arguments)
        {
            var alias = arguments[0];
            if (!CMDParser.Storage.RemoveAlias(alias))
                return new CommandResponse(CommandOutput.Fail, "Could not remove the alias.");

            return new CommandResponse(CommandOutput.Success, $"Alias \"{alias} has been removed!\"");
        }
    }
}


