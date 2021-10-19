using System;
using System.Collections.Generic;

namespace Alias_for_executables.Commands
{
    public class SampleCommand : Command
    {
        public SampleCommand(CommandParser parser) : base(parser, "sample", new List<string> { "s" })
        {
            Help = new HelpOutput() {
                Discription = "This is a sample command.",
                Arguments = "<none> [?none]",
                Example = $"{Globals.ProgramPrefix} sample none"
            };
        }

        public override void AfterInit()
        {
            throw new NotImplementedException();
        }

        public override CommandResponse TryExecute(string[] arguments)
        {
            throw new NotImplementedException();
        }

        public override CommandResponse OnExecute(string[] arguments)
        {
            throw new NotImplementedException();
        }
    }
}
