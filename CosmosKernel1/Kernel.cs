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
        private Queue<Queue<String>> commandQueue;

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type help for a list of commands.");
            fs = new FileSystem();
            globalVars = new Variables();
            commandQueue = new Queue<Queue<string>>();
        }

        protected override void Run()
        {
            while(commandQueue.Count != 0)
            {
                Queue<String> currentBatch = commandQueue.Dequeue();
                String cmd = currentBatch.Dequeue();
                processCommand(cmd);
                if (currentBatch.Count != 0)
                {
                    commandQueue.Enqueue(currentBatch);
                }
            }

            Console.Write("> ");
            var input = Console.ReadLine();
            processCommand(input);
            return;
        }

        private void editFile(File file)
        {
            Console.WriteLine("Editing File " + file.getName() + "." + file.getFileExtension());

            StringBuilder builder = new StringBuilder(file.getContents());
            Console.WriteLine(file.getContents());

            int ct = 0;
            while (true)
            {
                Console.Write("["+ct+"]: ");
                var input = Console.ReadLine();
                if (input == "save")
                {
                    Console.WriteLine("Saving");
                    file.setContents(builder.ToString());
                    break;
                } else
                {
                    builder.Append("\n" + input);
                }
                ct++;
            }
        }

        private void processCommand(String input)
        {
            var tokens = input.Split(' ');

            switch (tokens[0])
            {
                case "help":
                    Console.WriteLine("The list of commands is as follows:");
                    Console.WriteLine("");
                    Console.WriteLine("help: Displays this list");
                    Console.WriteLine("create [filename].[extension]: Creates a new file with the given filename and extension.");
                    Console.WriteLine("echo: Any text after this command is displayed on the screen");
                    Console.WriteLine("dir: Displays list of all files and their basic information.");
                    Console.WriteLine("run [file]: Runs a specific set of batch commands.");
                    Console.WriteLine("set [varname] [value]: Sets a variable to some integer value.");
                    Console.WriteLine("add [varname/value] [varname/value] [varname]: Adds first two and stores in third.");
                    Console.WriteLine("sub [varname/value] [varname/value] [varname]: Subtracts first two and stores in third.");
                    Console.WriteLine("mul [varname/value] [varname/value] [varname]: Multiplies first two and stores in third.");
                    Console.WriteLine("div [varname/value] [varname/value] [varname]: Divides first two and stores in third.");
                    break;
                case "echo":
                    StringBuilder build = new StringBuilder("");
                    for (int ct = 1; ct < tokens.Length; ct++)
                    {
                        if (tokens[ct][0] == '$')
                        {
                            try
                            {
                                build.Append(globalVars.getVar(tokens[ct]) + " ");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: No such variable found \'" + tokens[ct] + "\'");
                                break;
                            }
                        }
                        else
                        {
                            build.Append(tokens[ct] + " ");
                        }
                    }

                    Console.WriteLine(build.ToString());
                    break;
                case "create":
                    var args = tokens[1].Split('.');
                    editFile(fs.create(args[0], args[1]));
                    break;
                case "dir":
                case "ls":
                    {
                        string[] filenames = fs.list();
                        Console.WriteLine("Number of files: " + filenames.Length);
                        for (int i = 0; i < filenames.Length; i++)
                        {
                            String filename = filenames[i];
                            Console.WriteLine("File name: " + filename);
                        }
                        break;
                    }
                case "run":
                    {
                        if (tokens.Length < 2)
                        {
                            Console.WriteLine("Error, must provide a valid file to run");
                            break;
                        }

                        for(int ct = 1; ct < tokens.Length; ct++)
                        {
                            if (!fs.exists(tokens[ct]))
                            {
                                Console.WriteLine("Error, file: \"" + tokens[ct] + "\" doesn't exist.");
                                break;
                            }

                            File file = fs.findFile(tokens[ct]);

                            if (file == null)
                            {
                                Console.WriteLine("Error, somehow we could not retrieve the file!");
                            }

                            var lines = file.getContents().Split('\n');

                            Queue<String> newBatch = new Queue<string>();
                            for (int i = 0; i < lines.Length; i++)
                            {
                                //Console.WriteLine(lines[i]);
                                if (lines[i] != "")
                                {
                                    newBatch.Enqueue(lines[i]);
                                }
                            }
                            if (newBatch.Count != 0)
                            {
                                commandQueue.Enqueue(newBatch);
                            }
                        }
                        break;
                    }
                case "set":
                    {
                        if (tokens.Length < 3)
                        {
                            Console.WriteLine("Error, must supply a valid name and int value.");
                            break;
                        }
                        else if (tokens[1][0] != '$')
                        {
                            Console.WriteLine("Error, variable name must begin with a '$'");
                            break;
                        }
                        else
                        {
                            globalVars.setVar(tokens[1], int.Parse(tokens[2]));
                        }
                        break;
                    }
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
