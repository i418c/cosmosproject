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
                    StringBuilder build = new StringBuilder("");
                    for (int ct = 1; ct < tokens.Length; ct++)
                    {
                        if (tokens[ct][0] == '$')
                        {
                            try {
                                build.Append(globalVars.getVar(tokens[ct]) + " ");
                            } catch (Exception e)
                            {
                                Console.WriteLine("ERROR: No such variable found \'" + tokens[ct] + "\'");
                                break;
                            }
                        } else
                        {
                           build.Append(tokens[ct] + " ");
                        }
                    }

                    Console.WriteLine(build.ToString());
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
                        break;
                    } else
                    {
                        globalVars.setVar(tokens[1], int.Parse(tokens[2]));
                    }
                    break;
                case "add":
                    {
                        if (tokens.Length < 4)
                        {
                            Console.WriteLine("Error, addition requires 3 params");
                            break;
                        }

                        int a;
                        int b;

                        if (tokens[1][0] == '$')
                        {
                            try
                            {
                                a = globalVars.getVar(tokens[1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: \'" + tokens[1] + "\' variable not found");
                                break;
                            }
                        }
                        else
                        {
                            a = int.Parse(tokens[1]);
                        }

                        if (tokens[2][0] == '$')
                        {
                            try
                            {
                                b = globalVars.getVar(tokens[2]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: \'" + tokens[2] + "\' variable not found");
                                break;
                            }
                        }
                        else
                        {
                            b = int.Parse(tokens[2]);
                        }

                        if (tokens[3][0] != '$' && tokens[3].Length > 1)
                        {
                            Console.WriteLine("Error: destination variable must begin with \'$\' and contain at least one character");
                            break;
                        }

                        globalVars.setVar(tokens[3], a + b);
                    }
                    break;
                case "sub":
                    {
                        if (tokens.Length < 4)
                        {
                            Console.WriteLine("Error, addition requires 3 params");
                            break;
                        }

                        int a;
                        int b;

                        if (tokens[1][0] == '$')
                        {
                            try
                            {
                                a = globalVars.getVar(tokens[1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: \'" + tokens[1] + "\' variable not found");
                                break;
                            }
                        }
                        else
                        {
                            a = int.Parse(tokens[1]);
                        }

                        if (tokens[2][0] == '$')
                        {
                            try
                            {
                                b = globalVars.getVar(tokens[2]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: \'" + tokens[2] + "\' variable not found");
                                break;
                            }
                        }
                        else
                        {
                            b = int.Parse(tokens[2]);
                        }

                        if (tokens[3][0] != '$' && tokens[3].Length > 1)
                        {
                            Console.WriteLine("Error: destination variable must begin with \'$\' and contain at least one character");
                            break;
                        }

                        globalVars.setVar(tokens[3], a - b);
                    }
                    break;
                case "mul":
                    {
                        if (tokens.Length < 4)
                        {
                            Console.WriteLine("Error, addition requires 3 params");
                            break;
                        }

                        int a;
                        int b;

                        if (tokens[1][0] == '$')
                        {
                            try
                            {
                                a = globalVars.getVar(tokens[1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: \'" + tokens[1] + "\' variable not found");
                                break;
                            }
                        }
                        else
                        {
                            a = int.Parse(tokens[1]);
                        }

                        if (tokens[2][0] == '$')
                        {
                            try
                            {
                                b = globalVars.getVar(tokens[2]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: \'" + tokens[2] + "\' variable not found");
                                break;
                            }
                        }
                        else
                        {
                            b = int.Parse(tokens[2]);
                        }

                        if (tokens[3][0] != '$' && tokens[3].Length > 1)
                        {
                            Console.WriteLine("Error: destination variable must begin with \'$\' and contain at least one character");
                            break;
                        }

                        globalVars.setVar(tokens[3], a * b);
                    }
                    break;
                case "div":
                    {
                        if (tokens.Length < 4)
                        {
                            Console.WriteLine("Error, addition requires 3 params");
                            break;
                        }

                        int a;
                        int b;

                        if (tokens[1][0] == '$')
                        {
                            try
                            {
                                a = globalVars.getVar(tokens[1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: \'" + tokens[1] + "\' variable not found");
                                break;
                            }
                        }
                        else
                        {
                            a = int.Parse(tokens[1]);
                        }

                        if (tokens[2][0] == '$')
                        {
                            try
                            {
                                b = globalVars.getVar(tokens[2]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: \'" + tokens[2] + "\' variable not found");
                                break;
                            }
                        }
                        else
                        {
                            b = int.Parse(tokens[2]);
                        }

                        if (tokens[3][0] != '$' && tokens[3].Length > 1)
                        {
                            Console.WriteLine("Error: destination variable must begin with \'$\' and contain at least one character");
                            break;
                        }

                        globalVars.setVar(tokens[3], a / b);
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
