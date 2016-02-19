using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        private Variables globalVars;
        private FileSystem fs;

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type help for a list of commands.");
            fs = new FileSystem();
            globalVars = new Variables();
        }

        protected override void Run()
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            var tokens = input.Split(' ');

            switch (tokens[0])
            {
                case "help":
                    Console.WriteLine("The list of commands is as follows:");
                    Console.WriteLine("");
                    Console.WriteLine("help: Displays this list");
                    Console.WriteLine("echo: Any text after this command is displayed on the screen");
                    break;
                case "echo":
                    for(int ct = 1; ct < tokens.Length; ct++)
                    {
                        Console.Write(tokens[ct]+" ");
                    }
                    Console.WriteLine("");
                    break;
                case "create":
                    var args = tokens[1].Split('.');
                    fs.create(args[0],args[1]);
                    break;
                case "dir":
                case "ls":
                    string[] filenames = fs.list();
                    foreach(var filename in filenames)
                    {
                        Console.WriteLine(filename);
                    }
                    break;
                case "set":
                    if (!System.Text.RegularExpressions.Regex.Match(tokens[1],"^[$]").Success)
                    {
                        Console.WriteLine("Variables must start with $");
                    }
                    else
                    {

                    }
                    break;
                default:
                    Console.WriteLine("Unkown Command: " + tokens[0]);
                    Console.WriteLine("Type help for a list of commands.");
                    break;
            }
        }
    }
}
