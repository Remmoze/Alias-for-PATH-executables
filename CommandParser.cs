using System;
using System.Collections.Generic;
using System.Linq;
using Alias_for_executables.Commands;

namespace Alias_for_executables
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
                new InfoCommand(this),

                new AddCommand(this),
                new RemoveCommand(this),
                new ListCommand(this),

                new ExitCommand(this)
            };

            CommandsList.ForEach(cmd => cmd.AfterInit());
        }

        public Command FindCommand(string command)
        {
            return CommandsList.Find(cmd => cmd.IsMatch(command));
        }

        public void ParseCommand(string[] args)
        {
            if (args.Length == 0) {
                FindCommand("help").OnExecute(args);
                return;
            }
            var cmd = args[0];
            args = args.Skip(1).ToArray();
            ExecuteCommand(cmd, args);
        }

        public void ExecuteCommand(string command, string[] arguments)
        {
            var foundCommand = FindCommand(command);
            if (foundCommand == null) {
                Console.WriteLine($"Unknown command. Use \"{Globals.ProgramPrefix} help\" to see avaliable commands");
                return;
            }

            var error = foundCommand.TryExecute(arguments);
            if (!string.IsNullOrWhiteSpace(error.ToString())) {
                Console.WriteLine(error);
            }
        }
    }
}
