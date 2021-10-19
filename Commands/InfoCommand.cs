using System;
using System.Collections.Generic;

namespace Alias_for_executables.Commands
{
    class InfoCommand : Command
    {
        public InfoCommand(CommandParser parser) : base(parser, "info", new List<string>() { "version", "-v", "-info", "-version" })
        {
            Help = new HelpOutput() {
                Discription = "Show general info about the program"
            };
        }

        public override CommandResponse TryExecute(string[] arguments)
        {
            return OnExecute(arguments);
        }

        public override CommandResponse OnExecute(string[] arguments)
        {
            Console.WriteLine($"\t----\t Alias for PATH executables ({Globals.ProgramPrefix}) \t---- ");
            Console.WriteLine("\t\t Version: " + Globals.ProgramVersion);
            return new CommandResponse(CommandOutput.Success);
        }
    }
}
