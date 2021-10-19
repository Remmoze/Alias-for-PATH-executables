using System;
using System.Collections.Generic;

namespace Alias_for_executables.Commands
{
    public class ExitCommand : Command
    {
        public ExitCommand(CommandParser parser) : base(parser, "exit", new List<string> { })
        {
            Help = new HelpOutput() {
                Discription = "Exit the application"
            };
        }

        public override CommandResponse TryExecute(string[] arguments)
        {
            return OnExecute(arguments);
        }

        public override CommandResponse OnExecute(string[] arguments)
        {
            Environment.Exit(0);
            return new CommandResponse(CommandOutput.Success);
        }
    }
}
