using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias_for_executables.Commands
{
    public class HelpCommand : Command
    {
        public (int maxLabel, int maxArgument) OutputSpacing = (5, 10);
        public HelpCommand(CommandParser parser) : base(parser, "help", new List<string>() { "-h", "-help" })
        {
            Help = new HelpOutput() {
                Discription = "Shows general help or help for a specific command",
                Arguments = "[?command]",
                Example = $"{Globals.ProgramPrefix} help add"
            };
        }

        private (int maxLabel, int maxArgument) CalculateSpacing()
        {
            return (
                CMDParser.CommandsList.Max(cmd => cmd.Help.Avaliable ? cmd.Label.Length : 0),
                CMDParser.CommandsList.Max(cmd => cmd.Help.Avaliable ? cmd.Help.Arguments.Length : 0)
            );
        }

        public override void AfterInit()
        {
            OutputSpacing = CalculateSpacing();
        }

        public override CommandResponse TryExecute(string[] arguments)
        {
            if (arguments.Length > 1) {
                return new CommandResponse(CommandOutput.Too_Many_Arguments);
            }
            return OnExecute(arguments);
        }

        public override CommandResponse OnExecute(string[] arguments)
        {
            if (arguments.Length > 0) {
                var cmdName = arguments[0];
                var cmd = CMDParser.CommandsList.FirstOrDefault(command => command.Label == cmdName);
                if (cmd == null) {
                    return new CommandResponse(CommandOutput.Incorrect_Argument, $"Unknown command {cmdName}");
                }
                if (!cmd.Help.Avaliable) {
                    return new CommandResponse(CommandOutput.Fail, "No help avaliable for " + cmdName);
                }

                Console.WriteLine($"\n{Globals.ProgramPrefix} {cmd.Label} {cmd.Help.Arguments} \t - {cmd.Help.Discription}");
                if (cmd.Help.Example != "") {
                    Console.WriteLine("\nExample:");
                    Console.WriteLine(cmd.Help.Example);
                }
            }
            else {
                Console.WriteLine("List of avaliable commands: \n");
                CMDParser.CommandsList.ForEach(cmd => {
                    if (cmd.Help.Avaliable) {
                        Console.WriteLine($"{Globals.ProgramPrefix} {cmd.Label.PadRight(OutputSpacing.maxLabel)} {cmd.Help.Arguments.PadRight(OutputSpacing.maxArgument + 1)} - {cmd.Help.Discription}");
                    }
                });
            }
            return new CommandResponse(CommandOutput.Success);
        }
    }
}
