using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class AddCommand: IOperation
    {
        string name;
        string content;

        public void Operation(Options options, Notes notes)
        {
            if (options.Add.AddName == null)
            {
                content = options.Add.AddContent;
                Note note = new Note(content);
                notes.AddNote(note);
            }
            else if (options.Add.AddName != null)
            {
                name = options.Add.AddName;
                content = options.Add.AddContent;
                Note note = new Note(content, name);
                notes.AddNote(note);
            }
        }
    }
}
