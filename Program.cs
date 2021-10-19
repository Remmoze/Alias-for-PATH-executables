using System;
using System.IO;

namespace Alias_for_executables
{
    class Program
    {
        static public void Main(string[] args)
        {
            var storage = new Storage();
            var CMDParser = new CommandParser(storage);

            Console.CancelKeyPress += new ConsoleCancelEventHandler(delegate (object sender, ConsoleCancelEventArgs args) {
                Environment.Exit(0);
            });

            if (args.Length > 0) {
                Console.WriteLine($"{Directory.GetCurrentDirectory()}> {Globals.ProgramPrefix} {string.Join(" ", args)}\n");
                CMDParser.ParseCommand(args);
                Console.WriteLine();
            }

            while (true) {
                Console.Write($"{Directory.GetCurrentDirectory()}> {Globals.ProgramPrefix} ");
                args = Console.ReadLine().Split(" ");
                Console.WriteLine();
                CMDParser.ParseCommand(args);
                Console.WriteLine();
            }
        }
    }
}
