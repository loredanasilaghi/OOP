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
        public void ShouldAddNoteWithoutName()
        {
            string content = "Book list for me to read";
            string expectedName = "Book list";
            Notes notes = new Notes();
            notes.AddNote(content);

            notes.AllNotes[0].Name.ShouldContain(expectedName);
            notes.AllNotes[0].Content.ShouldContain(content);
        }

        [TestMethod]
        public void ShouldAddNoteWithSingleWordContent()
        {
            string content = "Book";
            string expectedName = "Book";
            Notes notes = new Notes();
            notes.AddNote(content);

            notes.AllNotes[0].Name.ShouldContain(expectedName);
            notes.AllNotes[0].Content.ShouldContain(content);
        }

        [TestMethod]
        public void ShouldAddNoteWithName()
        {
            string content = "Book list for me to read";
            string name = "Book list";
            Notes notes = new Notes();
            notes.AddNote(content, name);

            notes.AllNotes[0].Name.ShouldContain(name);
            notes.AllNotes[0].Content.ShouldContain(content);
        }



        [TestMethod]
        public void ShouldAddNoteWithAlmostTheSameContent()
        {
            string contentFirstNote = "Book list for me to read in november";
            Notes notes = new Notes();
            notes.AddNote(contentFirstNote);

            string contentSecondNote = "Book list for me to read in december";
            notes.AddNote(contentSecondNote);
            
            string contentThirdNote = "Book list for me to read in october";
            notes.AddNote(contentThirdNote);

            notes.AllNotes[0].Name.ShouldContain("Book list");
            notes.AllNotes[0].Content.ShouldContain(contentFirstNote);
            notes.AllNotes[1].Name.ShouldContain("Book list (2)");
            notes.AllNotes[1].Content.ShouldContain(contentSecondNote);
            notes.AllNotes[2].Name.ShouldContain("Book list (3)");
            notes.AllNotes[2].Content.ShouldContain(contentThirdNote);
        }

        [TestMethod]
        public void ShouldRemoveNote()
        {
            string content = "Book list for me to read";
            Notes notes = new Notes();
            notes.AddNote(content);
            
            notes.RemoveNote("1");

            notes.AllNotes.ShouldBeEmpty();
        }

        [TestMethod]
        public void ShouldRemoveMiddleNote()
        {
            string contentFirstNote = "Book list for me to read in november";
            Notes notes = new Notes();
            notes.AddNote(contentFirstNote);

            string contentSecondNote = "Book list for me to read in december";
            notes.AddNote(contentSecondNote);

            string contentThirdNote = "Book list for me to read in october";
            notes.AddNote(contentThirdNote);

            notes.RemoveNote("2");

            notes.AllNotes[0].Name.ShouldContain("Book list");
            notes.AllNotes[0].Content.ShouldContain(contentFirstNote);

            notes.AllNotes[1].Name.ShouldContain("Book list (3)");
            notes.AllNotes[1].Content.ShouldContain(contentThirdNote);
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

            string expected = "\n\tDisplaying notes...\r\n\tId: 1, Name: Book list, content: Book list for me to read\r\n\tEnd of list.\r\n";
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

            string expected = "\n\tDisplaying notes...\r\n\tId: 1, Name: Book list, content: Book list for me to read\r\n\tId: 2, Name: Shopping list, content: Shopping list for this week\r\n\tEnd of list.\r\n";
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
            string exectedId = "1";
            string expectedName = "Textul este";
            string expectedContent = "Textul este o succesiune ordonată de cuvinte, propoziţii, fraze prin care ni se comunică idei";
            string initial = "Id:\""+ exectedId + "\", Name:\""+ expectedName + "\" Content:\""+ expectedContent+"\"";

            Note note = new Note(initial);
            note.Id.ShouldContain(exectedId);
            note.Name.ShouldContain(expectedName);
            note.Content.ShouldContain(expectedContent);
        }
    }
}
