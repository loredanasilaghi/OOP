using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class EditCommand: IOperation
    {
        string id;
        string content;

        public void Operation(Options options, Notes notes)
        {
                id = options.EditEnterId;
                content = options.Edit;
                notes.EditNote(id, content);
        }
    }
}
