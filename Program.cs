using System;
using System.IO;
using Microsoft.Win32;


namespace CustomRunCommands
{
    class Program
    {
        static public void Main(String[] args)
        {
            var arguments = GetArguments(args);
            if (arguments == null) return;

            if(!VerifyProgram(arguments.Item2))
            {
                Console.WriteLine("Unable to create a shortcut. File path is incorrect.");
                return;
            }

            var storage = new Storage();
            var shortcut = new Shortcut(arguments);

            shortcut.Install();

        }

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
            if (args.Length != 2)
            {
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
            if (Console.ReadLine() != "y")
            {
                Console.WriteLine("Well be careful next time.");
                return null;
            }
            return new Tuple<string, string>(shortcut, location);
        }
    }
}
