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
    public class NotesTests
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
        public void ShouldSearchAnyTagsContentWithSharp()
        {
            string firstNoteContent = "Book to #read";
            string secondNoteContent = "I have to #send these documents";
            string thirdNoteContent =  "Just something to read";
            string fourthNoteContent = "Something to #read and #send";
            string[] tags = { "#send", "#read" };
            Notes notes = new Notes();
            Note expectedFirstNote = new Note(firstNoteContent);
            Note expectedSecondNote = new Note(secondNoteContent);
            Note expectedThirdNote = new Note(thirdNoteContent);
            Note expectedFourthNote = new Note(fourthNoteContent);
            notes.AddNote(expectedFirstNote);
            notes.AddNote(expectedSecondNote);
            notes.AddNote(expectedThirdNote);
            notes.AddNote(expectedFourthNote);
            notes = notes.FindAnyTag(tags);
            string expected = "\n\tDisplaying notes...\r\n\tID: 1\r\n\tName: Book to\r\n\tContent: Book to #read\r\n\tID: 2\r\n\tName: I have\r\n\tContent: I have to #send these documents\r\n\tID: 4\r\n\tName: Something to\r\n\tContent: Something to #read and #send\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldSearchAllTagsContentWithSharp()
        {
            string firstNoteContent = "Book to #read";
            string secondNoteContent = "I have to #send these documents";
            string thirdNoteContent = "Just something to read";
            string fourthNoteContent = "Something to #read and #send";
            string[] tags = { "#send", "#read" };
            Notes notes = new Notes();
            Note expectedFirstNote = new Note(firstNoteContent);
            Note expectedSecondNote = new Note(secondNoteContent);
            Note expectedThirdNote = new Note(thirdNoteContent);
            Note expectedFourthNote = new Note(fourthNoteContent);
            notes.AddNote(expectedFirstNote);
            notes.AddNote(expectedSecondNote);
            notes.AddNote(expectedThirdNote);
            notes.AddNote(expectedFourthNote);
            notes = notes.FindAllTags(tags);
            string expected = "\n\tDisplaying notes...\r\n\tID: 4\r\n\tName: Something to\r\n\tContent: Something to #read and #send\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldSearchAllTagsContentWithAt()
        {
            string firstNoteContent = "Book to @read";
            string secondNoteContent = "I have to @send these documents";
            string thirdNoteContent = "Just something to read";
            string fourthNoteContent = "Something to @read and @send";
            string[] tags = { "@send", "@read" };
            Notes notes = new Notes();
            Note expectedFirstNote = new Note(firstNoteContent);
            Note expectedSecondNote = new Note(secondNoteContent);
            Note expectedThirdNote = new Note(thirdNoteContent);
            Note expectedFourthNote = new Note(fourthNoteContent);
            notes.AddNote(expectedFirstNote);
            notes.AddNote(expectedSecondNote);
            notes.AddNote(expectedThirdNote);
            notes.AddNote(expectedFourthNote);
            notes = notes.FindAllTags(tags);
            string expected = "\n\tDisplaying notes...\r\n\tID: 4\r\n\tName: Something to\r\n\tContent: Something to @read and @send\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldSearchAnyTagsContentWithAt()
        {
            string firstNoteContent = "Book to @read";
            string secondNoteContent = "I have to @send these documents";
            string[] tags = { "@send", "@read" };
            Notes notes = new Notes();
            Note expectedFirstNote = new Note(firstNoteContent);
            Note expectedSecondNote = new Note(secondNoteContent);
            notes.AddNote(expectedFirstNote);
            notes.AddNote(expectedSecondNote);
            notes = notes.FindAnyTag(tags);
            string expected = "\n\tDisplaying notes...\r\n\tID: 1\r\n\tName: Book to\r\n\tContent: Book to @read\r\n\tID: 2\r\n\tName: I have\r\n\tContent: I have to @send these documents\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldGiveMessageWhenSearchingTagsIfTagsDoNotExist()
        {
            string firstNoteContent = "Book to @read";
            string[] tags = {"@go"};
            Notes notes = new Notes();
            Note expectedFirstNote = new Note(firstNoteContent);
            notes.AddNote(expectedFirstNote);
            notes = notes.FindAnyTag(tags);
            string expected = "\r\n\tThere is no note.";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.Display();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldListTags()
        {
            string firstNoteContent = "Book to #read";
            string secondNoteContent = "I have to @send these documents";
            Notes notes = new Notes();
            Note expectedFirstNote = new Note(firstNoteContent);
            Note expectedSecondNote = new Note(secondNoteContent);
            notes.AddNote(expectedFirstNote);
            notes.AddNote(expectedSecondNote);
            string expected = "\n\tDisplaying tags...\r\n\r\n\tTags:\r\n\r\n\t#read\r\n\r\n\t@send\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.DisplayTags();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldListDistinctTags()
        {
            string firstNoteContent = "I have to @send these documents";
            string secondNoteContent = "Book to #read";
            string thirdNoteContent = "Something to #read and @send";
            Notes notes = new Notes();
            Note expectedFirstNote = new Note(firstNoteContent);
            Note expectedSecondNote = new Note(secondNoteContent);
            Note expectedThirdNote = new Note(thirdNoteContent);
            notes.AddNote(expectedFirstNote);
            notes.AddNote(expectedSecondNote);
            notes.AddNote(expectedThirdNote);
            string expected = "\n\tDisplaying tags...\r\n\r\n\tTags:\r\n\r\n\t#read\r\n\r\n\t@send\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.DisplayTags();
                stringWriter.ToString().ShouldContain(expected);
            }
        }

        [TestMethod]
        public void ShouldListTagsWithDifferentHash()
        {
            string firstNoteContent = "I have to @send these documents";
            string secondNoteContent = "Book to #read";
            string thirdNoteContent = "Something to @read and #send";
            Notes notes = new Notes();
            Note expectedFirstNote = new Note(firstNoteContent);
            Note expectedSecondNote = new Note(secondNoteContent);
            Note expectedThirdNote = new Note(thirdNoteContent);
            notes.AddNote(expectedFirstNote);
            notes.AddNote(expectedSecondNote);
            notes.AddNote(expectedThirdNote);
            string expected = "\n\tDisplaying tags...\r\n\r\n\tTags:\r\n\r\n\t#read\r\n\r\n\t#send\r\n\r\n\t@read\r\n\r\n\t@send\r\n\n\tEnd of list.\r\n";
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                stringWriter.ToString().ShouldNotContain(expected);
                notes.DisplayTags();
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
