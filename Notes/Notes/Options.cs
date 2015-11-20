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
        [Option('a', "add", Required = false, HelpText = "Add new note. E.g \"description\" \"name\". Name is optional. ")]
        [ValueOption(6)]
        public string AddContent { get; set; }
        [ValueOption(5)]
        public string AddName { get; set; }
        bool list = false;

        [Option('l', "list", Required = false, HelpText = "List all notes")]
        public bool List { get { return this.list; } set { this.list = true;} }

        [Option('e', "edit", Required = false, HelpText = "Edit the content of a note by entering the ID and new content")]
        [ValueOption(4)]
        public string EditEnterId { get; set; }
        [ValueOption(3)]
        public string Edit { get; set; }

        [Option('r', "rename", Required = false, HelpText = "Rename a note by entering the ID and the new name")]
        [ValueOption(2)]
        public string RenameEnterId { get; set; }
        [ValueOption(1)]
        public string Rename { get; set; }

        [Option('s', "search", Required = false, HelpText = "Search in all the notes for a certain word")]
        public string Search { get; set; }

        [Option('x', "export", Required = false, HelpText = "Export notes to an HTML file. Enter the destination path ")]
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
