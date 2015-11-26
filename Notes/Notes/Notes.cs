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

        public delegate void UpdateNoteDelegate(Note note);

        public void UpdateNote(UpdateNoteDelegate updateNote, string id)
        {
            var note = allNotesList.Find(n => n.Id == id);
            if (note != null)
                updateNote(note);
            else
                Console.WriteLine("\r\n\tID invalid. There is no note with this ID.");
        }

        public Note FindId(string id)
        {
            var note = allNotesList.Find(n => n.Id == id);
            return note;
        }

        public void EditNote(string id, string newContent)
        {
            UpdateNote(n => n.Content = newContent, id);
        }

        public void RenameNote(string id, string newName)
        {
            UpdateNote(n => n.Name = newName, id);
        }

        public string GenerateId()
        {
            string id = "";
            if (allNotesList.Count == 0)
            {
                id = "1";
            }
            else
            {
                var lastElement = allNotesList[allNotesList.Count - 1];
                id = (int.Parse(lastElement.Id) + 1).ToString();
            }
            return id;
        }

        public void AddNote(Note note)
        {
            if(note.Id == "")
                note.Id = GenerateId();
            allNotesList.Add(note);
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
        
        public void Display()
        {
            if (allNotesList.Count == 0)
                Console.WriteLine("\r\n\tThere is no note.");
            else
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
        }

        public void DisplayTags()
        {
            List<string> tags = new List<string>();
            Console.WriteLine("\n\tDisplaying tags...");
            foreach (var note in allNotesList)
            {
                ExtractTag(tags, note.Name);
                ExtractTag(tags, note.Content);
            }
            tags.Sort();
            tags = tags.Distinct().ToList();
            Console.WriteLine("\r\n\tTags:");
            foreach (var tag in tags)
            {
                Console.WriteLine("\r\n\t" + tag);
            }
            Console.WriteLine("\n\tEnd of list.");
        }

        private static void ExtractTag(List<string> tags, string givenString)
        {
            string tag = string.Empty;
            bool isTag = false;
            for (int i = 0; i < givenString.Count(); i++)
            {
                if (givenString[i] == '#' || givenString[i] == '@')
                {
                    isTag = true;
                }
                if (isTag && (givenString[i] == ' ' || givenString[i] == '\n' ||givenString[i] == '\r'))
                {
                    isTag = false;
                    tags.Add(tag);
                    tag = string.Empty;
                }
                if (isTag)
                {
                    tag += givenString[i];
                }
            }
            if (isTag)
            {
                tags.Add(tag);
            }
        }

        public IEnumerable<string> GetWordFromString(string givenString)
        {
            return givenString.Split(' ', '\n', '\r').Where(i => i.Contains("#") || i.Contains("@"));
        }


        public void ExportToHtml(string path, Notes notesList)
        {
            if (!path.Contains(".html"))
                path += ".html";
            Console.WriteLine("\n\tExporting file...");
            HtmlDocument htmlDoc = new HtmlDocument();
            string html = htmlDoc.CreateHtmlDocument(notesList);
            System.IO.File.WriteAllText(path, html);
            System.Diagnostics.Process.Start(path);
        }

        public Notes Search(string word)
        {
            Notes searchResult = new Notes();
            foreach (var note in allNotesList)
            {
                if (note.Name.ToLower().Contains(word.ToLower()) || (note.Content.ToLower().Contains(word.ToLower())))
                {
                    Note enote = new Note(note.Id, note.Name, note.Content);
                    searchResult.AddNote(enote);
                }
                else
                    Console.WriteLine("\r\n\tThere is no note containing this search term.");
            }
            return searchResult;
        }

        public Notes FindAnyTag(string[] tags)
        {
            Notes searchResult = new Notes();
            foreach (var note in allNotesList)
            {
                foreach (var tag in tags)
                {
                    if (note.Name.ToLower().Contains(tag.ToLower()) || (note.Content.ToLower().Contains(tag.ToLower())))
                    {
                        Note enote = new Note(note.Id, note.Name, note.Content);
                        if(!ContainsNote(searchResult, enote))
                            searchResult.AddNote(enote);
                    }
                }
            }
            return searchResult;
        }

        public Notes FindAllTags(string[] tags)
        {
            Notes searchResult = new Notes();
            foreach (var note in allNotesList)
            {
                bool found = true;
                foreach (var tag in tags)
                {
                    if (!(note.Name.ToLower().Contains(tag.ToLower()) || (note.Content.ToLower().Contains(tag.ToLower()))))
                    {
                        found = false;
                    }
                }
                if (found)
                {
                    Note enote = new Note(note.Id, note.Name, note.Content);
                    if (!ContainsNote(searchResult, enote))
                        searchResult.AddNote(enote);
                }
            }
            return searchResult;
        }

        public bool ContainsNote(Notes notes, Note note)
        {
            foreach (var currentNote in notes.allNotesList)
            {
                if(currentNote.Id == note.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public static string ReplaceContent(string content, string toBeReplaced, string toReplace)
        {
            content = content.Replace(toBeReplaced, toReplace);
            return content;
        }
    }
}
