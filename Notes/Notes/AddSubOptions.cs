using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace Notes
{
    public class AddSubOptions
    {
        [Option('c', "content", HelpText = "Enter note content")]
        public string AddContent { get; set; }
        [Option('n', "name", HelpText = "Enter note name")]
        public string AddName { get; set; }
    }
}
