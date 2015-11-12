using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class TxtFile
    {
        private string path = @"Notes.txt";
        private List<Note> noteList = new List<Note>();

        public Notes LoadNotes()
        {
            Console.WriteLine("\n\tLoading file...");
            if (!File.Exists(path))
            {
                Console.WriteLine("\tThe file does not exist. No notes loaded.");
                return new Notes();
            };

            int counter = 0;
            byte[] fileContent = File.ReadAllBytes(path);
            using (MemoryStream stream = new MemoryStream(fileContent))
            { ProcessFileContent(ref counter, stream); }

            Notes notes = new Notes(noteList);
            Console.WriteLine("\tFile loaded. {0} notes read", counter);
            return notes;
        }

        public void ProcessFileContent(ref int counter, MemoryStream file)
        {
            StreamReader stream = new StreamReader(file);
            string id = string.Empty;
            string name = string.Empty;
            string content = string.Empty;

            string line;

            while ((line = stream.ReadLine()) != null)
            {
                ProcessLine(ref id, ref name, ref content, line, ref counter);
            }

            AddNoteAndIncreaseCounter(id, name, content, ref counter);
            stream.Close();
        }

        public void ProcessLine(ref string id, ref string name, ref string content, string line, ref int counter)
        {
            string idKeyWord = "#Id:";
            string nameKeyWord = "#Name:";
            string contentKeyWord = "#Content:";

            if (line.StartsWith(idKeyWord))
            {
                AddNoteAndIncreaseCounter(id, name, content, ref counter);
                id = line.Substring(idKeyWord.Length);
            }
            else if (line.StartsWith(nameKeyWord))
            {
                name = line.Substring(nameKeyWord.Length);
            }
            else if (line.StartsWith(contentKeyWord))
            {
                content = line.Substring(contentKeyWord.Length);
            }
            else
            {
                content += Environment.NewLine + line;
            }
        }

        public void AddNoteAndIncreaseCounter(string id, string name, string content, ref int counter)
        {
            if (id != string.Empty)
            {
                Note note = new Note(id, name, content);
                noteList.Add(note);
                counter++;
            }
        }

        public void SaveNotes(Notes notesList)
        {
            Console.WriteLine("\n\tSaving file...");
            using (StreamWriter file = new StreamWriter(path, false, Encoding.UTF8))
            {
                foreach (var note in notesList)
                {
                    file.WriteLine("#Id:" + note.Id);
                    file.WriteLine("#Name:" + note.Name);
                    note.Content = ReplaceContent(note.Content, "\n", "\\+");
                    file.WriteLine("#Content:" + note.Content);
                }
                Console.WriteLine("\tFile saved.");
                file.Close();
            }
        }

        public string ReplaceContent(string content, string toBeReplaced, string toReplace)
        {
            content = content.Replace(toBeReplaced, toReplace);
            return content;
        }

        public List<Note> GetList()
        {
            return noteList;
        }
    }
}
