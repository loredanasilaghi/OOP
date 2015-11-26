using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class ListTagsCommand:IOperation
    {
        public void Operation(Options options, Notes notes)
        {
            notes.DisplayTags();
        }
    }
}
