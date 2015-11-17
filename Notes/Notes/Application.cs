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
            switch (args[0])
            {
                case "-add":
                    {
                        if (args.Length == 2)
                        {
                            string content = args[1];
                            Note note = new Note(content);
                            notes.AddNote(note);
                            txt.SaveNotes(notes);
                        }
                        else if (args.Length == 3)
                        {
                            string name = args[1];
                            string content = args[2];
                            Note note = new Note(content, name);
                            notes.AddNote(note);
                            txt.SaveNotes(notes);
                        }
                        else
                        {
                            InvalidCommand();
                            return -1;
                        }
                        break;
                    }

                case "-edit":
                    {
                        if(args.Length == 3)
                        {
                            string id = args[1];
                            string content = args[2];
                            notes.EditNote(id, content);
                            txt.SaveNotes(notes);
                        }
                        else
                        {
                            InvalidCommand();
                            return -1;
                        }
                        break;
                    }

                case "-rename":
                    {
                        if (args.Length == 3)
                        {
                            string id = args[1];
                            string newName = args[2];
                            notes.RenameNote(id, newName);
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
                        notes.Display();
                        break;
                    }

                case "-search":
                    {
                        if (args.Length == 2)
                        {
                            string word = args[1];
                            notes = notes.Search(word);
                            notes.Display();
                        }
                        else if (args.Length == 4 && args[2] =="-export")
                        {
                            string word = args[1];
                            string path = args[3];
                            notes = notes.Search(word);
                            notes.ExportToHtml(path, notes);
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
                            notes.ExportToHtml(path, notes);
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
