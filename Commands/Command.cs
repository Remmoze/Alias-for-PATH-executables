using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRunCommands.Commands
{

    public class HelpOutput
    {
        public bool Avaliable { get { return Arguments != "" || Discription != ""; } }
        public string Arguments = "";
        public string Discription = "";
    }

    public abstract class Command
    {
        protected CommandParser CMDParser;

        protected readonly List<string> Alias;
        public string Label { get { return Alias[0]; } }

        public HelpOutput Help = new HelpOutput();

        public abstract Error OnExecute(string[] arguments);
        public abstract Error TryExecute(string[] arguments);
        public Command(CommandParser parser, string defaultLabel, List<string> labels)
        {
            (CMDParser, Alias) = (parser, labels);
            Alias.Insert(0, defaultLabel);
        }

        public bool IsMatch(string command)
        {
            return Alias.Contains(command);
        }

    }
}
