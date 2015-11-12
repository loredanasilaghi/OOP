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
                return 1;
            }

            Notes notes = new Notes();
            TxtFile txt = new TxtFile();
            notes = txt.LoadNotes();

            switch (args[0])
            {
                case "-add":
                    {
                        if (args.Length == 2)
                        {
                            string content = args[1];
                            notes.AddNote(content);
                            txt.SaveNotes(notes);
                        }
                        else if (args.Length == 3)
                        {
                            string name = args[1];
                            string content = args[2];
                            notes.AddNote(name, content);
                            txt.SaveNotes(notes);
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

                case "-search":
                    {
                        if (args.Length == 2)
                        {
                            string word = args[1];
                            notes.SearchNotes(word);
                            notes.DisplayNotes();
                        }
                        else if (args.Length == 4 && args[2] =="-export")
                        {
                            string word = args[1];
                            string path = args[3];
                            notes.SearchNotes(word);
                            notes.ExportNotesToHtml(path, notes);
                        }
                        else
                        {
                            InvalidCommand();
                            return -1;
                        }
                        break;
                    }

                case "-export":
                    {
                        if (args.Length == 2)
                        {
                            string path = args[1];
                            notes.ExportNotesToHtml(path, notes);
                        }
                        else
                        {
                            InvalidCommand();
                            return -1;
                        }
                        break;
                    }
                case "-delete":
                    {
                        if (args.Length == 2)
                        {
                            string id = args[1];
                            notes.RemoveNote(id);
                            txt.SaveNotes(notes);
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
