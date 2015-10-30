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
            string name = "Books";
            string description = "This is a book list for me to read";
            string content = "Hunger Games 1, Hunger Games 2";
            Notes.AddNote(name, description, content);
            
            Notes.AllNotes[0].Name.ShouldBeSameAs(name);
            Notes.AllNotes[0].Description.ShouldBeSameAs(description);
            Notes.AllNotes[0].Content.ShouldBeSameAs(content);
        }

        [TestMethod]
        public void ApplicationShouldListNotes()
        {
            string name = "Books";
            string description = "This is a book list for me to read";
            string content = "Hunger Games 1, Hunger Games 2";

            Notes.AllNotes.ShouldBeEmpty();
            Notes.AddNote(name, description, content);

            string expected = "\n\tDisplaying notes...\r\n\tName: Books, description: This is a book list for me to read, content: Hunger Games 1, Hunger Games 2\r\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                expected.ShouldNotBeSameAs(stringWriter.ToString());
                Notes.DisplayNotes();
                expected.ShouldContain(stringWriter.ToString());
            }
        }

        [TestMethod]
        public void ApplicationShouldListMultipleNotes()
        {
            string name1 = "Books";
            string description1 = "This is a book list for me to read";
            string content1 = "Hunger Games 1, Hunger Games 2";

            Notes.AllNotes.ShouldBeEmpty();
            Notes.AddNote(name1, description1, content1);

            string name2 = "Shopping";
            string description2 = "This is the shopping list for this week";
            string content2 = "Tomatoes, potatoes, chicken breasts";
            
            Notes.AddNote(name2, description2, content2);

            string expected = "\n\tDisplaying notes...\r\n\tName: Books, description: This is a book list for me to read, content: Hunger Games 1, Hunger Games 2\r\n\tName: Shopping, description: This is the shopping list for this week, content: Tomatoes, potatoes, chicken breasts\r\n\tEnd of list.\r\n";
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
