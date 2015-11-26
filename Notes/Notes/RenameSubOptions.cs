using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace Notes
{
    public class RenameSubOptions
    {
        [Option('i', "id", HelpText = "Enter note id")]
        public string Id { get; set; }
        [Option('n', "name", HelpText = "Enter new name")]
        public string Name { get; set; }
    }
}
