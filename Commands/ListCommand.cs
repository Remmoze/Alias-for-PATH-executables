using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias_for_executables.Commands
{
    public class ListCommand : Command
    {
        public ListCommand(CommandParser parser) : base(parser, "list", new List<string> { "l", "get", "-l" })
        {
            Help = new HelpOutput() {
                Discription = "List all shortcuts",
                Arguments = "",
                Example = $"{Globals.ProgramPrefix} list",
            };
        }

        public override CommandResponse TryExecute(string[] arguments)
        {
            return OnExecute(arguments);
        }

        public override CommandResponse OnExecute(string[] arguments)
        {
            if(CMDParser.Storage.Shortcuts.Count == 0) {
                return new CommandResponse(CommandOutput.Success, "No shortcuts were found.");
            }

            Console.WriteLine("List of avaliable shortcuts:\n");
            CMDParser.Storage.Shortcuts.ForEach(cmd => {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"  {cmd.ShortName}");
                Console.ResetColor();
                Console.WriteLine($"  {cmd.Path}");
                Console.WriteLine($"  {cmd.CreationDate:yyyy/MM/dd HH:mm:ss}");
                Console.WriteLine();
            });
            return new CommandResponse(CommandOutput.Success);
        }
    }
}
