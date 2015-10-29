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
        private static List<Note> allNotes = new List<Note>();
        private static string path = @"Notes.txt";
        public static List<Note> AllNotes
        {
            get { return allNotes; }
            set { allNotes = value; }
        }

        public static void AddNote(string name, string content)
        {
            Note note = new Note();
            note.Name = name;
            note.Content = content;
            allNotes.Add(note);
        }

        public static void LoadNotes()
        {
            Console.WriteLine("\n\tLoading file...");
            if (!File.Exists(path))
            {
                Console.WriteLine("\tThe file does not exist. No notes loaded.");
                return;
            }

            string line;
            int counter = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                int separatorPosition = line.IndexOf(" ");
                Note note = new Note();
                note.Name = line.Substring(0, separatorPosition);
                note.Content = line.Substring(separatorPosition + 1);
                allNotes.Add(note);
                counter++;
            }
            file.Close();

            Console.WriteLine("\tFile loaded. {0} notes read", counter);
        }

        public static void DisplayNotes()
        {
            Console.WriteLine("\n\tDisplaying notes...");
            for (int i = 0; i < allNotes.Count; i++)
            {
                Console.WriteLine("\tName: {0}, content: {1}", allNotes[i].Name, allNotes[i].Content);
            }
            Console.WriteLine("\tEnd of list.");
        }

        public static void SaveNotes()
        {
            Console.WriteLine("\n\tSaving file...");
            StreamWriter file = new StreamWriter(path);

            for (int i = 0; i < allNotes.Count; i++)
            {
                file.Write(allNotes[i].Name);
                file.Write(" " + allNotes[i].Content);
                file.Write(file.NewLine);
            }

            file.Close();
            Console.WriteLine("\tFile saved. {0} notes saved", allNotes.Count);
        }
    }
}
