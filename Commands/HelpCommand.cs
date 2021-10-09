using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRunCommands.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand(CommandParser parser) : base(parser, "help", new List<string>() { "-h" })
        {
            Help = new HelpOutput() {
                Discription = "Shows general help or help for a specific command",
                Arguments = "[?command]"
            };
        }

        public override Error TryExecute(string[] arguments)
        {
            if (arguments.Length > 1) {
                return new Error(CommandOutput.Too_Many_Arguments);
            }
            return OnExecute(arguments);
        }

        public override Error OnExecute(string[] arguments)
        {
            if(arguments.Length > 0) {
                var cmdName = arguments[0];
                var cmd = CMDParser.CommandsList.FirstOrDefault(command => command.Label == cmdName);
                if(cmd == null) {
                    return new Error(CommandOutput.Incorrect_Argument, $"Unknown command {cmdName}");
                }
                if(!cmd.Help.Avaliable) {
                    return new Error(CommandOutput.Fail, "No help avaliable for " + cmdName);
                }
                Console.WriteLine($"\ncrc {cmd.Label} {cmd.Help.Arguments} \t - {cmd.Help.Discription}");
            } else {
                Console.WriteLine("List of avaliable commands: \n");
                CMDParser.CommandsList.ForEach(cmd => {
                    if (cmd.Help.Avaliable) {
                        Console.WriteLine(string.Format("{0,3} {1,5} {2,10} \t - {3,20}", "crc", cmd.Label, cmd.Help.Arguments, cmd.Help.Discription));
                    }
                });
            }
            return new Error(CommandOutput.Success);
        }


    }
}
