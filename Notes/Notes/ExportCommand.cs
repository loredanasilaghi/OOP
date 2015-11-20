using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class ExportCommand:IOperation
    {
        string filePath;
        public void Operation(Options options, Notes notes)
        {
            filePath = options.Export;
            notes.ExportToHtml(filePath, notes);
        }
    }
}
