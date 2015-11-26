using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class RenameCommand : IOperation
    {
        string id;
        string name;

        public void Operation(Options options, Notes notes)
        {
            id = options.Rename.Id;
            name = options.Rename.Name;
            notes.RenameNote(id, name);
        }
    }
}
