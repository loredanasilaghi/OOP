using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class CommandLineParser
    {
        public bool Parse(string [] args, out Operations operation)
        {
            operation = new Operations();
            
            switch (args[0])
            {
                case "-add":
                    {
                        var addParameter = new Operations.AddParameters();
                        if (args.Length == 2)
                        {
                            addParameter.Content = args[1];
                        }
                        else if(args.Length == 3)
                        {
                            addParameter.Name = args[1];
                            addParameter.Content = args[2];
                        }
                        operation.Operation = Operations.PossibleOperation.Add;
                        operation.AddParameter = addParameter;

                        break;
                    }
            }
            return true;
        }
        public static bool Add(string[] args, Notes notes)
        {
            if (args.Length == 2)
            {
                string content = args[1];
                Note note = new Note(content);
                notes.AddNote(note);
            }
            else if (args.Length == 3)
            {
                string name = args[1];
                string content = args[2];
                Note note = new Note(content, name);
                notes.AddNote(note);
            }
            else
            {
                InvalidCommand();
                return false;
            }
            return true;
        }

        public static bool Edit(string[] args, Notes notes)
        {
            if (args.Length == 3)
            {
                string id = args[1];
                string content = args[2];
                notes.EditNote(id, content);
            }
            else
            {
                InvalidCommand();
                return false;
            }
            return true;
        }

        public static bool Rename(string[] args, Notes notes)
        {
            if (args.Length == 3)
            {
                string id = args[1];
                string newName = args[2];
                notes.RenameNote(id, newName);
            }
            else
            {
                InvalidCommand();
                return false;
            }
            return true;
        }

        public static void Search(string[] args, Notes notes)
        {
            if (args.Length == 2)
            {
                string word = args[1];
                notes = notes.Search(word);
                notes.Display();
            }
            else if (args.Length == 4 && args[2] == "-export")
            {
                string word = args[1];
                string path = args[3];
                notes = notes.Search(word);
                notes.ExportToHtml(path, notes);
            }
            else
            {
                InvalidCommand();
            }
        }

        public static void Export(string[] args, Notes notes)
        {
            if (args.Length == 2)
            {
                string path = args[1];
                notes.ExportToHtml(path, notes);
            }
            else
            {
                InvalidCommand();
            }
        }

        public static bool Delete(string[] args, Notes notes)
        {
            if (args.Length == 2)
            {
                string id = args[1];
                notes.RemoveNote(id);
            }
            else
            {
                InvalidCommand();
                return false;
            }
            return true;
        }

        public static void InvalidCommand()
        {
            Console.WriteLine("\n\tInvalid command. Press -? for help.");
        }
    }
}
