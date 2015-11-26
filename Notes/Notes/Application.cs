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
            Notes notes = new Notes();
            TxtFile txt = new TxtFile();
            notes = txt.LoadNotes();

            Options options = new Options();
            Parser parser = new Parser();

            string invokedVerb = "";
            object invokedVerbInstance = new object();

            if (!CommandLine.Parser.Default.ParseArguments(args, options,
              (verb, subOptions) =>
              {
                  invokedVerb = verb;
                  invokedVerbInstance = subOptions;
              }))
            {
                parser.ParseArguments(args, options);
            }

            if (invokedVerb == "add")
            {
                var addSubOptions = (AddSubOptions)invokedVerbInstance;
            }
            if (invokedVerb == "edit")
            {
                var addSubOptions = (EditSubOptions)invokedVerbInstance;
            }
            if (invokedVerb == "rename")
            {
                var addSubOptions = (RenameSubOptions)invokedVerbInstance;
            }
            bool invalidCommand = true;
            if (options.Add.AddContent != null)
            {
                AddCommand addNote = new AddCommand();
                addNote.Operation(options, notes);
                txt.SaveNotes(notes);
                invalidCommand = false;
            }
           if (options.Edit.Id != null && options.Edit.Content != null)
            {
                EditCommand editNote = new EditCommand();
                editNote.Operation(options, notes);
                txt.SaveNotes(notes);
                invalidCommand = false;
            }
            if (options.List == true)
            {
                ListCommand listNotes = new ListCommand();
                listNotes.Operation(options, notes);
                invalidCommand = false;
            }
            if (options.Rename.Id != null && options.Rename.Name != null)
            {
                RenameCommand renameNote = new RenameCommand();
                renameNote.Operation(options, notes);
                txt.SaveNotes(notes);
                invalidCommand = false;
            }
            if (options.Search != null)
            {
                SearchCommand search = new SearchCommand();
                search.Operation(options, notes);
                invalidCommand = false;
            }
            if (options.SearchAnyTags != null)
            {
                SearchAnyTagCommand search = new SearchAnyTagCommand();
                search.Operation(options, notes);
                invalidCommand = false;
            }
            if (options.SearchAllTags != null)
            {
                SearchAllTagsCommand search = new SearchAllTagsCommand();
                search.Operation(options, notes);
                invalidCommand = false;
            }
            if (options.ListTags == true)
            {
                ListTagsCommand listNotes = new ListTagsCommand();
                listNotes.Operation(options, notes);
                invalidCommand = false;
            }
            if (options.Export != null)
            {
                ExportCommand export = new ExportCommand();
                export.Operation(options, notes);
                invalidCommand = false;
            }
            if (options.Delete != null)
            {
                DeleteCommand delete = new DeleteCommand();
                delete.Operation(options, notes);
                txt.SaveNotes(notes);
                invalidCommand = false;
            }
            if (invalidCommand)
                Console.WriteLine("\n\tInvalid command.");
            return 1;
        }
    }
}
