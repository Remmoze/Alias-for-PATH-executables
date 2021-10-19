using System.Collections.Generic;

namespace Alias_for_executables.Commands
{

    public class HelpOutput
    {
        public string Arguments = "";
        public string Discription = "";
        public string Example = "";

        public bool Avaliable => Arguments != "" || Discription != "";
    }

    public abstract class Command
    {
        protected CommandParser CMDParser;

        protected readonly List<string> Alias;
        public string Label { get { return Alias[0]; } }

        public HelpOutput Help = new();

        public abstract CommandResponse OnExecute(string[] arguments);
        public abstract CommandResponse TryExecute(string[] arguments);
        public virtual void AfterInit() { }

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
