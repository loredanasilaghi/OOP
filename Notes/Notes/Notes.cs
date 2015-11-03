using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes
{
    public class Notes
    {
        private List<Note> allNotes = new List<Note>();
        private string path = @"Notes.txt";

        public List<Note> AllNotes
        {
            get { return allNotes; }
            private set { allNotes = value; }
        }

        public void AddNote(string content, string name = "")
        {
            Note note = new Note();
            note.Content = content;
            if (name == "")
            {
                note.Name = GenerateNoteName(content);
                note.Name = ChangeNoteNameIfAlreadyExists(note.Name);
            }
            else
                note.Name = name;
            note.Id = (allNotes.Count + 1).ToString();
            allNotes.Add(note);
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

        public void RegenerateIds()
        {
            for (int i = 0; i < allNotes.Count(); i++)
            {
                allNotes[i].Id = (i + 1).ToString();
            }
        }

        public string ChangeNoteNameIfAlreadyExists(string name)
        {
            int counter = 1;
            for (int i = 0; i <= allNotes.Count - 1; i++)
            {
                if(allNotes[i].Name.IndexOf(name, 0, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    counter++;
                }
            }
            if (counter != 1)
            {
                name = string.Format("{0} ({1})", name, counter);
            }
            return name;
        }

        public void RemoveNote(string id)
        {
            
            bool found = false;
            for (int i = 0; i <= allNotes.Count - 1; i++)
            {
                if (String.Equals(allNotes[i].Id, id) == true)
                {
                    found = true;
                    allNotes.RemoveAt(i);
                    break;
                }
            }

            if (found == false)
            {
                Console.WriteLine("\tID invalid. There is no note with this ID.");
            }
        }

        public void LoadNotes()
        {
            Console.WriteLine("\n\tLoading file...");
            if (!File.Exists(path))
            {
                Console.WriteLine("\tThe file does not exist. No notes loaded.");
                return;
            };
            string line;
            int counter = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                Note note = new Note(line);
                allNotes.Add(note);
                counter++;
            }
            
            file.Close();

            Console.WriteLine("\tFile loaded. {0} notes read", counter);
        }

        public void DisplayNotes()
        {
            Console.WriteLine("\n\tDisplaying notes...");
            for (int i = 0; i < allNotes.Count; i++)
            {
                Console.WriteLine("\tId: {0}, Name: {1}, content: {2}", allNotes[i].Id, allNotes[i].Name, allNotes[i].Content);
            }
            Console.WriteLine("\tEnd of list.");
        }

        public void SaveNotes()
        {
            Console.WriteLine("\n\tSaving file...");
            StreamWriter file = new StreamWriter(path, false, Encoding.UTF8);
            
            for (int i = 0; i < allNotes.Count; i++)
            {
                file.Write("Id:\"" + allNotes[i].Id + "\"");
                file.Write(" Name:\"" + allNotes[i].Name + "\"");
                file.Write(" Content:\"" + allNotes[i].Content + "\"");
                file.Write(file.NewLine);
            }

            file.Close();
            Console.WriteLine("\tFile saved. {0} notes saved", allNotes.Count);
        }
    }
}
