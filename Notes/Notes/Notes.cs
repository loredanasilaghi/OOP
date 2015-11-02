using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            set { allNotes = value; }
        }

        public void AddNote(string content)
        {
            Note note = new Note();
            note.Content = content;
            note.Name = GenerateNoteName(content);
            note.Name = ChangeNoteNameIfAlreadyExists(note.Name);
            allNotes.Add(note);
        }

        public string GenerateNoteName(string content)
        {
            string[] contentArray = content.Split(' ');
            string name = contentArray[0] + " " + contentArray[1];
            return name;
        }

        public string ChangeNoteNameIfAlreadyExists(string name)
        {
            int counter = 0;
            for (int i = 0; i <= allNotes.Count - 1; i++)
            {
                if (String.Equals(allNotes[i].Name, name, StringComparison.OrdinalIgnoreCase) == true)
                {
                    counter++;
                    name = name + " " + (counter + 1);
                    break;
                }
            }
            return name;
        }

        public void RemoveNote(string name)
        {
            bool found = false;
            for (int i = 0; i <= allNotes.Count - 1; i++)
            {
                if (String.Equals(allNotes[i].Name, name, StringComparison.OrdinalIgnoreCase) == true)
                {
                    found = true;
                    allNotes.RemoveAt(i);
                    break;
                }
            }

            if (found == false)
            {
                Console.WriteLine("\tName invalid. There is no note with this name.");
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
                Console.WriteLine("\tName: {0}, content: {1}", allNotes[i].Name, allNotes[i].Content);
            }
            Console.WriteLine("\tEnd of list.");
        }

        public void SaveNotes()
        {
            Console.WriteLine("\n\tSaving file...");
            StreamWriter file = new StreamWriter(path, false, Encoding.UTF8);
            
            for (int i = 0; i < allNotes.Count; i++)
            {
                file.Write("Name:\"" + allNotes[i].Name + "\"");
                file.Write(" Content:\"" + allNotes[i].Content + "\"");
                file.Write(file.NewLine);
            }

            file.Close();
            Console.WriteLine("\tFile saved. {0} notes saved", allNotes.Count);
        }
    }
}
