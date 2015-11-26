using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace Notes
{
    public class EditSubOptions
    {
        [Option('i', "id", HelpText = "Enter note id")]
        public string Id { get; set; }
        [Option('c', "content", HelpText = "Enter new content")]
        public string Content { get; set; }
    }
}
