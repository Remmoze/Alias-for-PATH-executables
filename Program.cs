using System;
using System.Linq;
using System.IO;

namespace CustomRunCommands
{
    class Program
    {
        static public void Main(string[] args)
        {
            var storage = new Storage();
            var CMDParser = new CommandParser(storage);

#if DEBUG
            while(true) { 
                Console.Write("Gimme the args: \ncrc ");
                args = Console.ReadLine().Split(" ");

                CMDParser.ParseCommand(args);

                Console.ReadKey(true);
                Console.Clear();
            }
#endif

            CMDParser.ParseCommand(args);
        }
    }
}
