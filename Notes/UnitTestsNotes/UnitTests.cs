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
            Note expectedNote = new Note(expectedContent);
            notes.AddNote(expectedNote);

            Note actualNote = new Note();
            actualNote = GetCurrentEnumerator(notes);

            actualNote.Id.ShouldContain(expectedId);
            actualNote.Name.ShouldContain(expectedName);
            actualNote.Content.ShouldContain(expectedContent);
        }

        [TestMethod]
        public void ShouldAddNoteWithSingleWordContent()
        {
            string expectedContent = "Book";
            string expectedName = "Book";
            string expectedId = "1";

            Notes notes = new Notes();
            Note expectedNote = new Note(expectedContent);
            notes.AddNote(expectedNote);

            Note note = new Note();
            note = GetCurrentEnumerator(notes);

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
            Note expectedNote = new Note(content, name);
            notes.AddNote(expectedNote);

            Note note = new Note();
            note = GetCurrentEnumerator(notes);

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
            Note expectedNote = new Note(content, name);
            notes.AddNote(expectedNote);

            Note note = new Note();
            note = GetCurrentEnumerator(notes);

            note.Id.ShouldContain(id);
            note.Name.ShouldContain(name);
            note.Content.ShouldContain(content);
        }

        [TestMethod]
        public void ShouldRemoveNote()
        {
            string content = "Book list for me to read";
            Notes notes = new Notes();
            Note expectedNote = new Note(content);
            notes.AddNote(expectedNote);

            notes.RemoveNote("1");

            notes.AllNotes.ShouldBeEmpty();
        }

        [TestMethod]
        public void ShouldRemoveMiddleNote()
        {
            string contentFirstNote = "Book list for me to read in november";
            Notes notes = new Notes();
            Note expectedNote = new Note(contentFirstNote);
            notes.AddNote(expectedNote);

            string contentSecondNote = "Book list2 for me to read in december";
            Note expectedSecondNote = new Note(contentSecondNote);
            notes.AddNote(expectedSecondNote);

            string contentThirdNote = "Book list3 for me to read in october";
            Note expectedThirdNote = new Note(contentThirdNote);
            notes.AddNote(expectedThirdNote);

            notes.RemoveNote("2");

            Note note = new Note();
            note = GetCurrentEnumerator(notes);
            note.Id.ShouldContain("1");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentFirstNote);
            
            note = GetCurrentEnumerator(notes, 1);
            note.Id.ShouldContain("3");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentThirdNote);
        }

        [TestMethod]
        public void ShouldAddNewNoteAfterDelete()
        {
            string contentFirstNote = "Book list for me to read in november";
            Notes notes = new Notes();
            Note expectedNote = new Note(contentFirstNote);
            notes.AddNote(expectedNote);

            string contentSecondNote = "Book list for me to read in december";
            Note expectedSecondNote = new Note(contentSecondNote);
            notes.AddNote(expectedSecondNote);

            string contentThirdNote = "Book list for me to read in october";
            Note expectedThirdNote = new Note(contentThirdNote);
            notes.AddNote(expectedThirdNote);

            notes.RemoveNote("2");

            Note secondNote = new Note(contentSecondNote);
            notes.AddNote(secondNote);

            Note note = new Note();
            note = GetCurrentEnumerator(notes);
            note.Id.ShouldContain("1");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentFirstNote);

            note = GetCurrentEnumerator(notes, 1);
            note.Id.ShouldContain("3");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentThirdNote);

            note = GetCurrentEnumerator(notes, 2);
            note.Id.ShouldContain("4");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(contentSecondNote);
        }

        [TestMethod]
        public void ShouldGiveMessageAtRemoveIfNoteDoesNotExist()
        {
            string content = "Book list for me to read";
            Notes notes = new Notes();
            Note expectedNote = new Note(content);
            notes.AddNote(expectedNote);

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
            Note expectedNote = new Note(content);
            notes.AddNote(expectedNote);
            string expected = "\n\tDisplaying notes...\r\n\tID: 1\r\n\tName: Book list\r\n\tContent: Book list for me to read\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                expected.ShouldNotBeSameAs(stringWriter.ToString());
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldListMultipleNotes()
        {
            string contentFirstNote = "Book list for me to read";

            Notes notes = new Notes();
            notes.AllNotes.ShouldBeEmpty();
            Note expectedFirstNote = new Note(contentFirstNote);
            notes.AddNote(expectedFirstNote);

            string contentSecondNote = "Shopping list for this week";

            Note expectedSecondNote = new Note(contentSecondNote);
            notes.AddNote(expectedSecondNote);

            string expected = "\n\tDisplaying notes...\r\n\tID: 1\r\n\tName: Book list\r\n\tContent: Book list for me to read\r\n\tID: 2\r\n\tName: Shopping list\r\n\tContent: Shopping list for this week\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldProcessNotes()
        {
            int counter = 0;
            var myFileContent = @"#Id:1
        #Name:magazin de
        #Content:magazin de mezeluri
        #Id:2
        #Name:name
        #Content:new\+content\+is given\+by";
            var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(myFileContent));
            TxtFile file = new TxtFile();
            file.ProcessFileContent(ref counter, stream);
            var notes = file.GetList();

            notes[0].Id.ShouldContain("1");
            notes[0].Name.ShouldContain("magazin de");
            notes[0].Content.ShouldContain("magazin de mezeluri");

            notes[1].Id.ShouldContain("2");
            notes[1].Name.ShouldContain("name");
            notes[1].Content.ShouldContain(@"new\+content\+is given\+by");
        }

        [TestMethod]
        public void ShouldCreateHtmlContent()
        {
            Notes notes = new Notes();
            Note expectedFirstNote = new Note("Shopping list for today");
            notes.AddNote(expectedFirstNote);
            Note expectedSecondNote = new Note(@"new\+content\+is given\+by", "content");
            notes.AddNote(expectedSecondNote);
            HtmlDocument html = new HtmlDocument();
            string actualHtmlContent = html.CreateHtmlDocument(notes);
            string expectedHtmlContent = "<html>\r\n\t<head>\r\n\t\t<title>\r\n\t\t\tNotesList\r\n\t\t</title>\r\n\t</head><body>\r\n\t\t<h1>\r\n\t\t\tNotesList\r\n\t\t</h1><p>Id: 1\r\n</p><p>Name: Shopping list\r\n</p><p>Content: Shopping list for today\r\n\r\n</p><p>Id: 2\r\n</p><p>Name: content\r\n</p><p>Content: new&lt;br/&gt;content&lt;br/&gt;is given&lt;br/&gt;by\r\n\r\n</p>\r\n\t</body>\r\n</html>";
            actualHtmlContent.ShouldContain(expectedHtmlContent);
        }

        [TestMethod]
        public void ShouldReturnASingleMatchAfterSearch()
        {
            string contentFirstNote = "Learning is great!";
            string contentSecondNote = "Book list for me to read";
            Notes notes = new Notes();

            Note expectedFirstNote = new Note(contentFirstNote);
            notes.AddNote(expectedFirstNote);

            Note expectedSecondNote = new Note(contentSecondNote);
            notes.AddNote(expectedSecondNote);

            notes = notes.Search("Book");
            string expected = "\n\tDisplaying notes...\r\n\tID: 2\r\n\tName: Book list\r\n\tContent: Book list for me to read\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldSearchCaseInsensitive()
        {
            string content = "Book list for me to read";
            Notes notes = new Notes();
            Note expectedNote = new Note(content);
            notes.AddNote(expectedNote);
            notes = notes.Search("book");
            string expected = "\n\tDisplaying notes...\r\n\tID: 1\r\n\tName: Book list\r\n\tContent: Book list for me to read\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldReturnBothMatchesAfterSearch()
        {
            string contentFirstNote = "Books are great!";
            string contentSecondNote = "Book list for me to read";
            Notes notes = new Notes();
            Note expectedFirstNote = new Note(contentFirstNote);
            notes.AddNote(expectedFirstNote);

            Note expectedSecondNote = new Note(contentSecondNote);
            notes.AddNote(expectedSecondNote);
            notes = notes.Search("Book");
            string expected = "\n\tDisplaying notes...\r\n\tID: 1\r\n\tName: Books are\r\n\tContent: Books are great!\r\n\tID: 2\r\n\tName: Book list\r\n\tContent: Book list for me to read\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldEditNote()
        {
            string content = "Book list for me to read";
            string newContent = "I edited this note!";
            Notes notes = new Notes();
            Note expectedNote = new Note(content);
            notes.AddNote(expectedNote);

            notes.EditNote("1", newContent);
            Note note = new Note();
            note = GetCurrentEnumerator(notes);

            note.Id.ShouldContain("1");
            note.Name.ShouldContain("Book list");
            note.Content.ShouldContain(newContent);
        }

        [TestMethod]
        public void ShouldGiveMessageAtEditIfNoteDoesNotExist()
        {
            string content = "Book list for me to read";
            string newContent = "I edited this note!";
            Notes notes = new Notes();
            Note expectedNote = new Note(content);
            notes.AddNote(expectedNote);
            string expected = "\r\n\tID invalid. There is no note with this ID.";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.EditNote("2", newContent);
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldRenameNote()
        {
            string content = "Book list for me to read";
            string newName = "New Name";
            Notes notes = new Notes();
            Note expectedNote = new Note(content);
            notes.AddNote(expectedNote);
            notes.RenameNote("1", newName);
            Note note = new Note();
            note = GetCurrentEnumerator(notes);

            note.Id.ShouldContain("1");
            note.Name.ShouldContain(newName);
            note.Content.ShouldContain(content);
        }

        [TestMethod]
        public void ShouldGiveMessageAtRenameIfNoteDoesNotExist()
        {
            string content = "Book list for me to read";
            string newName = "New Name";
            Notes notes = new Notes();
            Note expectedNote = new Note(content);
            notes.AddNote(expectedNote);
            string expected = "\r\n\tID invalid. There is no note with this ID.";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.RenameNote("2", newName);
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        private Note GetCurrentEnumerator(Notes notes, int position = 0)
        {
            Note note = new Note();
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
