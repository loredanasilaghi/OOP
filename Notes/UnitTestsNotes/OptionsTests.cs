using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandLine;
using CommandLine.Text;
using Should;
using Notes;

namespace UnitTestsNotes
{
    [TestClass]
    public class OptionsTests
    {
        [TestMethod]
        public void ShouldAddOnlyContent()
        {
            string[] args = { "-a", "content" };
            Options options = new Options();
            Parser parser = new Parser();
            parser.ParseArguments(args, options);
            options.AddContent.ShouldContain(args[1]);
        }

        [TestMethod]
        public void ShouldAddNameAndContent()
        {
            string[] args = { "-a", "content", "name" };
            Options options = new Options();
            Parser parser = new Parser();
            parser.ParseArguments(args, options);
            options.AddContent.ShouldContain(args[1]);
            options.AddName.ShouldContain(args[2]);
        }
    }
}
