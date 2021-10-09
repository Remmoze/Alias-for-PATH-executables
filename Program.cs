using System;
using System.Linq;
using System.IO;

namespace CustomRunCommands
{
    class Program
    {
        static public void Main(string[] args)
        {
#if DEBUG
            Console.Write("Gimme the args: \ncrc ");
            args = Console.ReadLine().Split(" ");
#endif

            var storage = new Storage();
            var CMDParser = new CommandParser(storage);

            CMDParser.ParseCommand(args);

            /*
            var shortcut = new Shortcut(arguments);

            if (shortcut.Install())
            {
                storage.AddShortcut(shortcut);
                storage.Save();
            }
            */
        }
    }
}
