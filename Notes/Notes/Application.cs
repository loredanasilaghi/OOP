using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Notes
{
    public class Application
    {
        
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("\n\tUse argument -? for help");
                return -1;
            }

            if (args[0] == "-?")
            {
                Console.WriteLine("\n\tPossible commands:");
                Console.WriteLine("\t\t-add <name> <content>");
                Console.WriteLine("\t\t-add <content>");
                Console.WriteLine("\t\t-delete <ID>");
                Console.WriteLine("\t\t-list");
                Console.WriteLine("\t\t-export <path>");
                Console.WriteLine("\t\t-search <word>");
                Console.WriteLine("\t\t-search <word> -export <path>");
                Console.WriteLine("\t\t-edit <ID> <newContent>");
                Console.WriteLine("\t\t-rename <ID> <newName>");

                return 1;
            }
            Notes notes = new Notes();
            TxtFile txt = new TxtFile();
            notes = txt.LoadNotes();
            Operations operation = new Operations();

            CommandLineParser parser = new CommandLineParser();
            parser.Parse(args, out operation);

            switch (operation.Operation)
            {
                case Operations.PossibleOperation.Add:
                    {
                        Note note = new Note(operation.AddParameter.Content, operation.AddParameter.Name);
                        notes.AddNote(note);
                        txt.SaveNotes(notes);
                        break;
                    }
            }


            switch (args[0])
            {
                case "-add":
                    {
                        if (CommandLineParser.Add(args, notes))
                            txt.SaveNotes(notes);
                        break;
                    }
                case "-edit":
                    {
                        if (CommandLineParser.Edit(args, notes))
                            txt.SaveNotes(notes);
                        break;
                    }
                case "-rename":
                    {
                        if (CommandLineParser.Rename(args, notes))
                            txt.SaveNotes(notes);
                        break;
                    }
                case "-list":
                    {
                        notes.Display();
                        break;
                    }
                case "-search":
                    {
                        CommandLineParser.Search(args, notes);
                        break;
                    }
                case "-export":
                    {
                        CommandLineParser.Export(args, notes);
                        break;
                    }
                case "-delete":
                    {
                        if (CommandLineParser.Delete(args, notes))
                            txt.SaveNotes(notes);
                        break;
                    }
                default:
                    {
                        CommandLineParser.InvalidCommand();
                        return -1;
                    }
            }
            return 1;
        }

    }
}
