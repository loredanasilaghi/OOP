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
        public Options()
        {
            Add = new AddSubOptions { };
            Edit = new EditSubOptions { };
            Rename = new RenameSubOptions { };
        }

        [VerbOption("add", HelpText = "Add new note. E.g -c \"content\" -n \"name\". Name is optional.")]
        public AddSubOptions Add{ get; set; }
       
        bool list = false;
        [Option('l', "list", Required = false, HelpText = "List all notes")]
        public bool List { get { return this.list; } set { this.list = true;} }

        [VerbOption("edit", HelpText = "Edit the content of a note by entering the ID and new content")]
        public EditSubOptions Edit { get; set; }

        [VerbOption("rename", HelpText = "Rename a note by entering the ID and the new name")]
        public RenameSubOptions Rename { get; set; }

        [Option('s', "search", Required = false, HelpText = "Search in all the notes for a certain word")]
        public string Search { get; set; }

        [Option('x', "export", Required = false, HelpText = "Export notes to an HTML file. Enter the destination path")]
        public string Export { get; set; }

        [Option('d', "delete", Required = false, HelpText = " Delete note by entering the id of the note you want to delete")]
        public string Delete { get; set; }

        [OptionArray("searchAnyTag", Required = false, HelpText = "Search in all the notes for certain tags")]
        public string[] SearchAnyTags { get; set; }

        [OptionArray("searchAllTags", Required = false, HelpText = "Search in all the notes for certain tags")]
        public string[] SearchAllTags { get; set; }

        bool listTags = false;
        [Option("listTags", Required = false, HelpText = "List all notes")]
        public bool ListTags { get { return this.listTags; } set { this.listTags = true; } }

        [HelpOption(HelpText = "Display this help screen.")]
        public string GetUsage()
        {
            string helpText = HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            return helpText;
        }
    }
}
