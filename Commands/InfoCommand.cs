using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRunCommands.Commands
{
    class InfoCommand : Command
    {
        public string AppVersion = "1.0.0";

        public InfoCommand(CommandParser parser) : base(parser, "info", new List<string>() { "version", "-v", "-info", "-version" })
        {
            Help = new HelpOutput() {
                Discription = "Show general info about the program"
            };
        }

        public override Error TryExecute(string[] arguments)
        {
            return OnExecute(arguments);
        }

        public override Error OnExecute(string[] arguments)
        {
            Console.WriteLine("\t----\t Custom RUN Commands (crc) \t---- ");
            Console.WriteLine("\t\t Version: " + AppVersion);
            return new Error(CommandOutput.Success);
        }
    }
}
