using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes;
using System.Collections.Generic;

namespace Notes
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void NoArgumentGiven()
        {
            string[] args = new string[0];
            int result = Notes.Main(args);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void IncorrectArgumentForHelpGiven()
        {
            string[] args = new string[] { "- ?"};
            int result = Notes.Main(args);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CorrectArgumentForHelpGiven()
        {
            string[] args = new string[] { "-?" };
            int result = Notes.Main(args);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ArgumentForAddWithoutParameters()
        {
            string[] args = new string[] { "-add" };
            int result = Notes.Main(args);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CorrectArgumentForAdd()
        {
            string name = "NameFile";
            string content = "This is my content";

            List<Note> expected = new List<Note>();
            Note note1 = new Note();
            note1.Name = name;
            note1.Content = content;
            expected.Add(note1);

            Notes.AddNote(name, content);

            Assert.AreEqual(expected[0], Notes.AllNotes[0]);
        }
    }
}
