using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class SearchAnyTagCommand: IOperation
    {
        string[] tags;
        public void Operation(Options options, Notes notes)
        {
            tags = options.SearchAnyTags;
            notes = notes.FindAnyTag(tags);
            notes.Display();
        }
    }
}
