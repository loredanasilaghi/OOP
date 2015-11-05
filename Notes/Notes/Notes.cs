﻿using System;
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

        public  IEnumerable<Note> AllNotes
        {
            get { return allNotes; }
        }

        public void AddNote(string content, string name = "")
        {
            Note note = new Note();
            note.Content = content;
            if (name == "")
            {
                note.Name = GenerateNoteName(content);
            }
            else
                note.Name = name;
            if (allNotes.Count == 0)
            {
                note.Id = "1";
            }
            else
            {
                var lastElement = allNotes[allNotes.Count -1];
                note.Id = (int.Parse(lastElement.Id) + 1).ToString();
            }
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
            string id = string.Empty;
            string name = string.Empty;
            string content = string.Empty;

            string idKeyWord = "#Id:";
            string nameKeyWord = "#Name:";
            string contentKeyWord = "#Content:";
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
                
                if (line.StartsWith(idKeyWord))
                {
                    if (id != string.Empty)
                    {
                        Note note = new Note(id, name, content);
                        allNotes.Add(note);
                        counter++;
                    }

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

            if (id != string.Empty)
            {
                Note note = new Note(id, name, content);
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
                Console.WriteLine("\tID: {0}", allNotes[i].Id);
                Console.WriteLine("\tName: {0}", allNotes[i].Name);
                allNotes[i].Content = ReplaceContent(allNotes[i].Content, "\\+", "\n" + "\t\t");
                Console.WriteLine("\tContent: {0}", allNotes[i].Content);
            }
            Console.WriteLine("\n\tEnd of list.");
        }

        public void SaveNotes()
        {
            Console.WriteLine("\n\tSaving file...");
            StreamWriter file = new StreamWriter(path, false, Encoding.UTF8);
            
            for (int i = 0; i < allNotes.Count; i++)
            {
                file.WriteLine("#Id:" + allNotes[i].Id);
                file.WriteLine("#Name:" + allNotes[i].Name);
                allNotes[i].Content = ReplaceContent(allNotes[i].Content, "\n", "\\+");
                file.WriteLine("#Content:" + allNotes[i].Content);
            }

            file.Close();
            Console.WriteLine("\tFile saved. {0} notes saved", allNotes.Count);
        }

        public string ReplaceContent(string content, string toBeReplaced, string toReplace)
        {
            content = content.Replace(toBeReplaced, toReplace);
            return content;
        }
    }
}
