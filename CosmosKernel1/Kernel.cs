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
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        String filename = filenames[i];
                        Console.WriteLine(filename);
                    }
                    break;
                case "set":
                    if (tokens.Length < 3)
                    {
                        Console.WriteLine("Error, must supply a valid name and int value.");
                        break;
                    } else if (tokens[1][0] != '$')
                    {
                        Console.WriteLine("Error, variable name must begin with a '$'");
                    } else
                    {
                        globalVars.setVar(tokens[1], int.Parse(tokens[2]));
                    }
                    break;
                case "add":
                    break;
                case "sub":
                    break;
                case "mul":
                    break;
                case "div":
                    break;
                default:
                    Console.WriteLine("Unkown Command: " + tokens[0]);
                    Console.WriteLine("Type help for a list of commands.");
                    break;
            }
        }
    }
}
