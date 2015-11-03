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
                return 1;
            }

            Notes notes = new Notes();

            notes.LoadNotes();

            switch (args[0])
            {
                case "-add":
                    {
                        if (args.Length == 2)
                        {
                            string content = args[1];
                            notes.AddNote(content);
                            notes.SaveNotes();
                        }
                        else if (args.Length == 3)
                        {
                            string name = args[1];
                            string content = args[2];
                            notes.AddNote(name, content);
                            notes.SaveNotes();
                        }
                        else
                        {
                            InvalidCommand();
                            return -1;
                        }
                        break;
                    }
                case "-list":
                    {
                        notes.DisplayNotes();
                        break;
                    }
                case "-delete":
                    {
                        if (args.Length == 2)
                        {
                            string id = args[1];
                            notes.RemoveNote(id);
                            notes.RegenerateIds();
                            notes.SaveNotes();
                        }
                        else
                        {
                            InvalidCommand();
                            return -1;
                        }
                        break;
                    }
                default:
                    {
                        InvalidCommand();
                        return -1;
                    }
            }
            return 1;
        }

        public static void InvalidCommand()
        {
            Console.WriteLine("\n\tInvalid command. Press -? for help.");
        }
    }
}
