﻿using System;
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
                new InfoCommand(this),

                new AddCommand(this)
            };

            CommandsList.ForEach(cmd => cmd.AfterInit());
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
            if (args.Length == 0) {
                FindCommand("help").OnExecute(args);
                return;
            }
            else {
                var cmd = args[0];
                args = args.Skip(1).ToArray();
                ExecuteCommand(cmd, args);
            }
        }

    }
}