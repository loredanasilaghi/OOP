using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes
{
    public class Notes: IEnumerable<Note>
    {
        private List<Note> allNotesList = new List<Note>();

        public  IEnumerable<Note> AllNotes
        {
            get { return allNotesList; }
        }

        public IEnumerator<Note> GetEnumerator()
        {
            return allNotesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Length
        {
            get { return allNotesList.Count(); }
        }

        public Notes()
        {
            allNotesList = new List<Note>();
        }

        public Notes(List<Note> list)
        {
            allNotesList = list;
        }
    
        public void AddNote(string content, string name="")
        {
            Note note = new Note();
            note.Content = content;
            if (name == "")
            {
                note.Name = GenerateNoteName(content);
            }
            else
                note.Name = name;
            if (allNotesList.Count == 0)
            {
                note.Id = "1";
            }
            else
            {
                var lastElement = allNotesList[allNotesList.Count -1];
                note.Id = (int.Parse(lastElement.Id) + 1).ToString();
            }
            allNotesList.Add(note);
        }

        public string GenerateNoteName(string content)
        {
            string[] contentArray = content.Split(' ');
            string name = string.Empty;
            if (contentArray.Length == 1)
            {
                name = contentArray[0];
            }
            else
                name = contentArray[0] + " " + contentArray[1];
            return name;
        }

        public void RemoveNote(string id)
        {
            bool found = false;
            for (int i = 0; i <= allNotesList.Count - 1; i++)
            {
                if (String.Equals(allNotesList[i].Id, id) == true)
                {
                    found = true;
                    allNotesList.RemoveAt(i);
                    break;
                }
            }

            if (found == false)
            {
                Console.WriteLine("\tID invalid. There is no note with this ID.");
            }
        }
        
        public void DisplayNotes()
        {
            Console.WriteLine("\n\tDisplaying notes...");
            for (int i = 0; i < allNotesList.Count; i++)
            {
                Console.WriteLine("\tID: {0}", allNotesList[i].Id);
                Console.WriteLine("\tName: {0}", allNotesList[i].Name);
                allNotesList[i].Content = ReplaceContent(allNotesList[i].Content, "\\+", "\n" + "\t\t");
                Console.WriteLine("\tContent: {0}", allNotesList[i].Content);
            }
            Console.WriteLine("\n\tEnd of list.");
        }

        public void ExportNotesToHtml(string path, Notes notesList)
        {
            if (!path.Contains(".html"))
                path += ".html";
            Console.WriteLine("\n\tExporting file...");
            HtmlDocument htmlDoc = new HtmlDocument();
            string html = htmlDoc.CreateHtmlDocument(notesList);
            System.IO.File.WriteAllText(path, html);
            System.Diagnostics.Process.Start(path);
        }

        public void SearchNotes(string word)
        {
            List<Note> notesToRemove = new List<Note>();
            foreach (var note in allNotesList)
            {
                if (!note.Name.Contains(word) && (!note.Content.Contains(word)))
                {
                    notesToRemove.Add(note);
                }
            }
            for (int i = 0; i < notesToRemove.Count; i++)
            {
                allNotesList.Remove(notesToRemove[i]);
            }
        }
        
        public static string ReplaceContent(string content, string toBeReplaced, string toReplace)
        {
            content = content.Replace(toBeReplaced, toReplace);
            return content;
        }
        
    }
}
