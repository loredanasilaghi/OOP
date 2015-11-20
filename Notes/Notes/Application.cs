using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommandLine;
using CommandLine.Text;

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

            Options options = new Options();
            Parser parser = new Parser();
            try
            {
                parser.ParseArguments(args, options);
            }
            catch (System.ArgumentNullException)
            {
                InvalidCommand();
            }

            if (options.AddContent != null)
            {
                AddCommand addNote = new AddCommand();
                addNote.Operation(options, notes);
                txt.SaveNotes(notes);
            }
            else if (options.EditEnterId != null && options.Edit != null)
            {
                EditCommand editNote = new EditCommand();
                editNote.Operation(options, notes);
                txt.SaveNotes(notes);
            }
            else if (options.List == true)
            {
                ListCommand listNotes = new ListCommand();
                listNotes.Operation(options, notes);
            }
            else if (options.RenameEnterId != null && options.Rename != null)
            {
                RenameCommand renameNote = new RenameCommand();
                renameNote.Operation(options, notes);
                txt.SaveNotes(notes);
            }
            else if (options.Search != null)
            {
                SearchCommand search = new SearchCommand();
                search.Operation(options, notes);
                notes.Display();
            }
            else if (options.Export != null)
            {
                ExportCommand export = new ExportCommand();
                export.Operation(options, notes);
            }
            else
                InvalidCommand();
            
                return 1;
        }

        public static void InvalidCommand()
        {
            Console.WriteLine("\n\tInvalid command. Press -? for help.");
        }

    }
}
