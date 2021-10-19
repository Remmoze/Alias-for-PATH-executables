using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Alias_for_executables.Commands
{
    class AddCommand : Command
    {
        public AddCommand(CommandParser parser) : base(parser, "add", new List<string>() { "install", "a" })
        {
            Help = new HelpOutput() {
                Arguments = "<alias> <path>",
                Discription = "Add an alias to the <path> executable",
                Example = $"{Globals.ProgramPrefix} add photos \"./PhotosApp/core/app.exe\""
            };
        }

        public override CommandResponse TryExecute(string[] arguments)
        {
            if (arguments.Length < 2) {
                return new CommandResponse(CommandOutput.Too_Few_Arguments);
            }

            var alias = arguments[0];
            if (!Regex.IsMatch(alias, @"^[a-zA-Z0-9_]+$")) {
                return new CommandResponse(CommandOutput.Incorrect_Argument, $"Alias \"{alias}\" should only contain letters, numbers, and _");
            }

            var path = Path.GetFullPath(string.Join(" ", arguments.Skip(1)).Trim('\"'));
            if (!VerifyProgram(path)) {
                return new CommandResponse(CommandOutput.Incorrect_Argument, $"Failed to verify path \"{path}\"");
            }

            if (CMDParser.Storage.Aliases.FirstOrDefault(sc => sc.Name == alias) != null) {
                return new CommandResponse(CommandOutput.Fail, $"Failed to add alias \"{alias}\", it already exists!");
            }

            if(Path.GetDirectoryName(path).Equals("c:\\")) {
                return new CommandResponse(CommandOutput.Fail, $"Failed to add alias \"{alias}\". Can not add aliases to root directory.");
            }

            return OnExecute(new string[] { alias, path });
        }

        public override CommandResponse OnExecute(string[] arguments)
        {
            var alias = new Alias(arguments[0], arguments[1]);

            if (!CMDParser.Storage.AddAlias(alias)) {
                return new CommandResponse(CommandOutput.Fail, "Unable to add an alias");
            }

            return new CommandResponse(CommandOutput.Success, $"Alias \"{alias.Name} has been installed!\"");
        }

        static public bool VerifyProgram(string path)
        {
            var program = Path.GetFileName(path);

            if (!File.Exists(path)) 
                return false;

            if (Path.GetExtension(path) != ".exe") 
                return false;

            if (string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(path))) 
                return false;

            //maybe add more checks later?

            return true;
        }
    }
}
