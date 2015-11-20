using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class SearchCommand: IOperation
    {
        string word;
        public void Operation(Options options, Notes notes)
        {
            word = options.Search;
            notes = notes.Search(word);
            notes.Display();
        }
    }
}
