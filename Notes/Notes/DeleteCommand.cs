using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class DeleteCommand: IOperation
    {
        string id;
        public void Operation(Options options, Notes notes)
        {
            id = options.Delete;
            notes.RemoveNote(id);
        }
    }
}
