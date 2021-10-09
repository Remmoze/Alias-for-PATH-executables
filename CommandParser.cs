using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomRunCommands.Commands;
using System.IO;

namespace CustomRunCommands
{
    public class CommandParser
    {
        public List<Command> CommandsList { get; private set; }
        public Storage Storage { get; private set; }

        public CommandParser(Storage storage)
        {
            Storage = storage;
            InitCommands();
        }

        private void InitCommands()
        {
            CommandsList = new List<Command>
            {
                new HelpCommand(this),
                new InfoCommand(this)
            };
        }

        public Command FindCommand(string command)
        {
            return CommandsList.Find(cmd => cmd.IsMatch(command));
        }

        public void ExecuteCommand(string command, string[] arguments)
        {
            var foundCommand = FindCommand(command);
            if (foundCommand == null) {
                Console.WriteLine("Command wasn't found or something");
                return;
            }
            var error = foundCommand.TryExecute(arguments);
            if (error.Type != CommandOutput.Success) {
                Console.WriteLine(error);
            }
        }

        public void ParseCommand(string[] args)
        {
            if(args.Length == 0) {
                FindCommand("help").OnExecute(args);
                return;
            } else {
                var cmd = args[0];
                args = args.Skip(1).ToArray();
                ExecuteCommand(cmd, args);
            }
        }


        /*
        static public bool VerifyProgram(string path)
        {
            var program = Path.GetFileName(path);
            if (Path.GetExtension(path) != ".exe") return false;
            if (string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(path))) return false;

            //maybe add more checks later?

            return true;
        }

        static public Tuple<string, string> GetArguments(String[] args)
        {
            Console.WriteLine();
            if (args.Length != 2) {
                Console.WriteLine("Usage: \ncrc <shortcut> <location>");
                return null;
            }
            var shortcut = args[0];
            var location = Path.GetFullPath(args[1]);

            Console.WriteLine("Provided arguments:");
            Console.WriteLine();
            Console.WriteLine("Shortcut:\t" + shortcut);
            Console.WriteLine("Location:\t" + location);
            Console.WriteLine();

            Console.Write("Is this correct? (y/n): ");
            if (Console.ReadLine() != "y") {
                Console.WriteLine("Well be careful next time.");
                return null;
            }
            return new Tuple<string, string>(shortcut, location);
        }
        */
    }
}
