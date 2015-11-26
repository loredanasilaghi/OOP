using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public interface IOperation
    {
        void Operation(Options options, Notes notes);
    }
}
