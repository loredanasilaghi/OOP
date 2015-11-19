using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class Addition: IOperation
    {
        string name;
        string content;

        void IOperation.Operation(string[] args, Notes notes)
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
            }
        }
        public static void InvalidCommand()
        {
            Console.WriteLine("\n\tInvalid command. Press -? for help.");
        }
    }
}
