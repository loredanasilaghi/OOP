using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class SearchAllTagsCommand: IOperation
    {
        string[] tags;
        public void Operation(Options options, Notes notes)
        {
            tags = options.SearchAllTags;
            notes = notes.FindAllTags(tags);
            notes.Display();
        }
    }
}
