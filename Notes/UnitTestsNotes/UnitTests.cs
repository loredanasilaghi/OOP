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
        public void ShouldAddNote()
        {
            string content = "Book list for me to read";
            string expectedName = "Book list";
            Notes notes = new Notes();
            notes.AddNote(content);

            notes.AllNotes[0].Name.ShouldContain(expectedName);
            notes.AllNotes[0].Content.ShouldContain(content);
        }

        [TestMethod]
        public void ShouldAddNoteWithAlmostTheSameName()
        {
            string contentFirstNote = "Book list for me to read in november";
            Notes notes = new Notes();
            notes.AddNote(contentFirstNote);

            string contentSecondNote = "Book list for me to read in december";
            notes.AddNote(contentSecondNote);
            notes.AllNotes[0].Name.ShouldContain("Book list");
            notes.AllNotes[0].Content.ShouldContain(contentFirstNote);
            notes.AllNotes[1].Name.ShouldContain("Book list 2");
            notes.AllNotes[1].Content.ShouldContain(contentSecondNote);
        }

        [TestMethod]
        public void ShouldRemoveNote()
        {
            string name = "Book list";
            string content = "Book list for me to read";
            Notes notes = new Notes();
            notes.AddNote(content);
            
            notes.RemoveNote(name);

            notes.AllNotes.ShouldBeEmpty();
        }

        [TestMethod]
        public void ShouldGiveMessageAtRemoveIfNoteDoesNotExist()
        {
            string content = "Book list for me to read";
            Notes notes = new Notes();
            notes.AddNote(content);

            notes.RemoveNote("Shopping list");

            string expected = "\n\tName invalid. There is no note with this name.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                expected.ShouldContain(stringWriter.ToString());
            }
        }

        [TestMethod]
        public void ShouldListNotes()
        {
            string content = "Book list for me to read";

            Notes notes = new Notes();
            notes.AllNotes.ShouldBeEmpty();
            notes.AddNote(content);

            string expected = "\n\tDisplaying notes...\r\n\tName: Book list, content: Book list for me to read\r\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                expected.ShouldNotBeSameAs(stringWriter.ToString());
                notes.DisplayNotes();
                expected.ShouldContain(stringWriter.ToString());
            }
        }

        [TestMethod]
        public void ShouldListMultipleNotes()
        {
            string contentFirstNote = "Book list for me to read";

            Notes notes = new Notes();
            notes.AllNotes.ShouldBeEmpty();
            notes.AddNote(contentFirstNote);
            
            string contentSecondNote = "Shopping list for this week";

            notes.AddNote(contentSecondNote);

            string expected = "\n\tDisplaying notes...\r\n\tName: Book list, content: Book list for me to read\r\n\tName: Shopping list, content: Shopping list for this week\r\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.DisplayNotes();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldPrelucrateNote()
        {
            string initial = "Name:\"Textul este\" Content:\"Textul este o succesiune ordonată de cuvinte, propoziţii, fraze prin care ni se comunică idei\"";
            string expectedName = "Textul este";
            string expectedContent = "Textul este o succesiune ordonată de cuvinte, propoziţii, fraze prin care ni se comunică idei";
            Note note = new Note(initial);
            note.Name.ShouldContain(expectedName);
            note.Content.ShouldContain(expectedContent);
        }
    }
}
