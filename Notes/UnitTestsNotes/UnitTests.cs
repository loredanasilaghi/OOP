using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes;
using System.Collections.Generic;
using System.Diagnostics;
using Should;
using System.IO;

namespace Notes
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void ApplicationShouldAddNote()
        {
            string name = "NameFile";
            string content = "This is my content";
            
            Notes.AddNote(name, content);
            
            Notes.AllNotes[0].Name.ShouldBeSameAs(name);
            Notes.AllNotes[0].Content.ShouldBeSameAs(content);
        }

        [TestMethod]
        public void ApplicationShouldListNotes()
        {
            string name = "NameFile";
            string content = "This is my content";

            Notes.AllNotes.ShouldBeEmpty();
            Notes.AddNote(name, content);

            string expected = "\n\tDisplaying notes...\r\n\tName: NameFile, content: This is my content\r\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                expected.ShouldNotBeSameAs(stringWriter.ToString());
                Notes.DisplayNotes();
                expected.ShouldContain(stringWriter.ToString());
            }
        }
    }
}
