using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRunCommands.Commands
{
    public class ListCommand : Command
    {
        public ListCommand(CommandParser parser) : base(parser, "list", new List<string> { "l", "get", "-l" })
        {
            Help = new HelpOutput() {
                Discription = "List all shortcuts",
                Arguments = "",
                Example = "crc list",
            };
        }

        public override Error TryExecute(string[] arguments)
        {
            return OnExecute(arguments);
        }

        public override Error OnExecute(string[] arguments)
        {
            Console.WriteLine("List of avaliable shortcuts:");
            CMDParser.Storage.Shortcuts.ForEach(cmd => {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"  {cmd.ShortName}");
                Console.ResetColor();
                Console.WriteLine($"  {cmd.Path}");
                Console.WriteLine($"  {cmd.CreationDate:yyyy/MM/dd HH:mm:ss}");
                Console.WriteLine();
            });
            return new Error(CommandOutput.Success);
        }
    }
}
