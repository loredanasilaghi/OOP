﻿using System;
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
            id = options.Edit.Id;
            content = options.Edit.Content;
            notes.EditNote(id, content);
        }
    }
}
