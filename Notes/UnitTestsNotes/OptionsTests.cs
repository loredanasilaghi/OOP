using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandLine;
using CommandLine.Text;
using Should;
using Notes;

namespace Notes
{
    [TestClass]
    public class OptionsTests
    {
        [TestMethod]
        public void ShouldAddOnlyContent()
        {
            string[] args = { "add", "-c", "content" };
            Options options = new Options();
            Parser parser = new Parser();

            string invokedVerb = "";
            object invokedVerbInstance = new object();

            parser.ParseArguments(args, options,
              (verb, subOptions) =>
              {
                  invokedVerb = verb;
                  invokedVerbInstance = subOptions;
              });
            options.Add.AddContent.ShouldContain(args[2]);
        }

        [TestMethod]
        public void ShouldAddNameAndContent()
        {
            string[] args = { "add", "-c", "content", "-n", "name" };
            Options options = new Options();
            Parser parser = new Parser();

            string invokedVerb = "";
            object invokedVerbInstance = new object();

            parser.ParseArguments(args, options,
              (verb, subOptions) =>
              {
                  invokedVerb = verb;
                  invokedVerbInstance = subOptions;
              });
            options.Add.AddContent.ShouldContain(args[2]);
            options.Add.AddName.ShouldContain(args[4]);
        }

        [TestMethod]
        public void ShouldEdit()
        {
            string[] args = { "edit", "-i", "1", "-c", "my new content" };
            Options options = new Options();
            Parser parser = new Parser();

            string invokedVerb = "";
            object invokedVerbInstance = new object();

            parser.ParseArguments(args, options,
              (verb, subOptions) =>
              {
                  invokedVerb = verb;
                  invokedVerbInstance = subOptions;
              });
            options.Edit.Id.ShouldContain(args[2]);
            options.Edit.Content.ShouldContain(args[4]);
        }

        [TestMethod]
        public void ShouldRename()
        {
            string[] args = { "rename", "-i", "1", "-n", "my new note title" };
            Options options = new Options();
            Parser parser = new Parser();

            string invokedVerb = "";
            object invokedVerbInstance = new object();

            parser.ParseArguments(args, options,
              (verb, subOptions) =>
              {
                  invokedVerb = verb;
                  invokedVerbInstance = subOptions;
              });
            options.Rename.Id.ShouldContain(args[2]);
            options.Rename.Name.ShouldContain(args[4]);
        }

        [TestMethod]
        public void ShouldListShortCommand()
        {
            string[] args = { "-l" };
            Options options = new Options();
            Parser parser = new Parser();

            parser.ParseArguments(args, options);

            options.List.ShouldBeTrue();
        }

        [TestMethod]
        public void ShouldListLongCommand()
        {
            string[] args = { "--list" };
            Options options = new Options();
            Parser parser = new Parser();

            parser.ParseArguments(args, options);

            options.List.ShouldBeTrue();
        }

        [TestMethod]
        public void ShouldSearchComand()
        {
            string[] args = { "-s", "content" };
            Options options = new Options();
            Parser parser = new Parser();

            parser.ParseArguments(args, options);

            options.Search.ShouldContain(args[1]);
        }

        [TestMethod]
        public void ShouldSearchAnyTagComand()
        {
            string[] args = { "--searchAnyTag", "#content #era" };
            Options options = new Options();
            Parser parser = new Parser();

            parser.ParseArguments(args, options);

            options.SearchAnyTags.ShouldContain(args[1]);
        }

        [TestMethod]
        public void ShouldSearchAllTagComand()
        {
            string[] args = { "--searchAllTags", "#content #era" };
            Options options = new Options();
            Parser parser = new Parser();

            parser.ParseArguments(args, options);

            options.SearchAllTags.ShouldContain(args[1]);
        }

        [TestMethod]
        public void ShouldExportComand()
        {
            string[] args = { "-x", "htmlFile.html" };
            Options options = new Options();
            Parser parser = new Parser();

            parser.ParseArguments(args, options);

            options.Export.ShouldContain(args[1]);
        }

        [TestMethod]
        public void ShouldSearchAndExportComand()
        {
            string[] args = { "-s", "content", "-x", "htmlFile.html" };
            Options options = new Options();
            Parser parser = new Parser();

            parser.ParseArguments(args, options);

            options.Search.ShouldContain(args[1]);
            options.Export.ShouldContain(args[3]);
        }

        [TestMethod]
        public void ShouldDeleteComand()
        {
            string[] args = { "-d", "1" };
            Options options = new Options();
            Parser parser = new Parser();

            parser.ParseArguments(args, options);

            options.Delete.ShouldContain(args[1]);
        }

        [TestMethod]
        public void ShouldListTagsCommand()
        {
            string[] args = { "--listTags" };
            Options options = new Options();
            Parser parser = new Parser();

            parser.ParseArguments(args, options);

            options.ListTags.ShouldBeTrue();
        }
    }
}
