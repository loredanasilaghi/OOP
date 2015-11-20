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

        public void Operation(Options options,Notes notes)
        {
            if (options.AddName == null)
            {
                content = options.AddContent;
                Note note = new Note(content);
                notes.AddNote(note);
            }
            else if (options.AddName != null)
            {
                name = options.AddName;
                content = options.AddContent;
                Note note = new Note(content, name);
                notes.AddNote(note);
            }
        }
    }
}
