using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace Notes
{
    public class Options
    {
        [OptionList('a', "add", Required = false, Separator =' ', HelpText = "Add new note. E.g \"name\" \"description\". Name is optional. ")]
        public IList<string> Add { get; set; }
        [Option('l', "list", Required = false, HelpText = "List all notes")]
        public bool List { get; set; }
        [OptionList("edit", Required = false, HelpText = "Edit the content of a note by entering the ID and new content")]
        public IList<string> Edit { get; set; }
        [OptionList('r', "rename", Required = false, HelpText = "Rename a note by entering the ID and the new name")]
        public IList<string> Rename { get; set; }
        [Option('s', "search", Required = false, HelpText = "Search in all the notes for a certain word")]
        public string Search { get; set; }
        [Option("export", Required = false, HelpText = "Export notes to an HTML file. Enter the destination path ")]
        public string Export { get; set; }
        [Option('d', "delete", Required = false, HelpText = " Delete note by entering the id of the note you want to delete")]
        public string Delete { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText();
            help.AddOptions(this);
            return help;
        }
    }
}
