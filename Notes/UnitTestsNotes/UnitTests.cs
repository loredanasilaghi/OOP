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
            string expectedContent = "Book list for me to read";
            string expectedName = "Book list";
            string expectedId = "1";

            Notes notes = new Notes();
            notes.AddNote(expectedContent);

            Note note = new Note();
            note = GetCurrentEnumerator(notes, note);

            note.Id.ShouldContain(expectedId);
            note.Name.ShouldContain(expectedName);
            note.Content.ShouldContain(expectedContent);
        }

        [TestMethod]
        public void ShouldAddNoteWithSingleWordContent()
        {
            string expectedContent = "Book";
            string expectedName = "Book";
            string expectedId = "1";

            Notes notes = new Notes();
            notes.AddNote(expectedContent);

            Note note = new Note();
            note = GetCurrentEnumerator(notes, note);

            note.Id.ShouldContain(expectedId);
            note.Name.ShouldContain(expectedName);
            note.Content.ShouldContain(expectedContent);
        }

        [TestMethod]
        public void ShouldAddNoteWithName()
        {
            string content = "Book list for me to read";
            string name = "Book list";
            string id = "1";
            Notes notes = new Notes();
            notes.AddNote(content, name);

            Note note = new Note();
            note = GetCurrentEnumerator(notes, note);

            note.Id.ShouldContain(id);
            note.Name.ShouldContain(name);
            note.Content.ShouldContain(content);
        }

        [TestMethod]
        public void ShouldAddMultiline()
        {
            string content = @"Book list\r\n\tfor me\r\n\tto\r\n\tread";
            string name = "Book list";
            string id = "1";
            Notes notes = new Notes();
            notes.AddNote(content, name);

            Note note = new Note();
            note = GetCurrentEnumerator(notes, note);

            note.Id.ShouldContain(id);
            note.Name.ShouldContain(name);
            note.Content.ShouldContain(content);
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

            string contentSecondNote = "Book list2 for me to read in december";
            notes.AddNote(contentSecondNote);

            string contentThirdNote = "Book list3 for me to read in october";
            notes.AddNote(contentThirdNote);

            notes.RemoveNote("2");

            Note note = new Note();
            note = GetCurrentEnumerator(notes, note);
            note.Id.ShouldContain("1");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentFirstNote);
            
            note = GetCurrentEnumerator(notes, note, 1);
            note.Id.ShouldContain("3");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentThirdNote);
        }

        [TestMethod]
        public void ShouldAddNewNoteAfterDelete()
        {
            string contentFirstNote = "Book list for me to read in november";
            Notes notes = new Notes();
            notes.AddNote(contentFirstNote);

            string contentSecondNote = "Book list for me to read in december";
            notes.AddNote(contentSecondNote);

            string contentThirdNote = "Book list for me to read in october";
            notes.AddNote(contentThirdNote);

            notes.RemoveNote("2");
            
            notes.AddNote(contentSecondNote);

            Note note = new Note();
            note = GetCurrentEnumerator(notes, note);
            note.Id.ShouldContain("1");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentFirstNote);
            
            note = GetCurrentEnumerator(notes, note, 1);
            note.Id.ShouldContain("3");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentThirdNote);
            
            note = GetCurrentEnumerator(notes, note, 2);
            note.Id.ShouldContain("4");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentSecondNote);
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
            string expected = "\n\tDisplaying notes...\r\n\tID: 1\r\n\tName: Book list\r\n\tContent: Book list for me to read\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                expected.ShouldNotBeSameAs(stringWriter.ToString());
                notes.DisplayNotes();
                stringWriter.ToString().ShouldContain(expected);
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

            string expected = "\n\tDisplaying notes...\r\n\tID: 1\r\n\tName: Book list\r\n\tContent: Book list for me to read\r\n\tID: 2\r\n\tName: Shopping list\r\n\tContent: Shopping list for this week\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.DisplayNotes();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldLoadNotes()
        {
            int counter = 0;
            var myFileContent = @"#Id:1
#Name:magazin de
#Content:magazin de mezeluri
#Id:2
#Name:name
#Content:new\+content\+is given\+by";
            var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(myFileContent));
            Notes notes = new Notes();
            Note note = new Note();
            notes.ParseFileContent(ref counter, stream);
            note = GetCurrentEnumerator(notes, note);

            note.Id.ShouldContain("1");
            note.Name.ShouldContain("magazin de");
            note.Content.ShouldContain("magazin de mezeluri");

            note = GetCurrentEnumerator(notes, note, 1);
            note.Id.ShouldContain("2");
            note.Name.ShouldContain("name");
            note.Content.ShouldContain(@"new\+content\+is given\+by");
        }

        private Note GetCurrentEnumerator(Notes notes, Note note, int position = 0)
        {
            using (IEnumerator<Note> enumer = notes.AllNotes.GetEnumerator())
            {
                for (int i = 1; i <= position; i++)
                {
                    enumer.MoveNext();
                }
                if (enumer.MoveNext()) note = enumer.Current;
            }
            return note;
        }
    }
}
