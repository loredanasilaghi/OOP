﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Notes
{
    public class Application
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("\n\tUse argument -? for help");
                return -1;
            }

            if (args[0] == "-?")
            {
                Console.WriteLine("\n\tPossible commands:");
                Console.WriteLine("\t\t-add <noteName> <content>");
                Console.WriteLine("\t\t-list");
                return 1;
            }

            Notes.LoadNotes();
            switch (args[0])
            {
                case "-add":
                    {
                        if (args.Length ==3)
                        {
                            string name = args[1];
                            string content = args[2];
                            Notes.AddNote(name, content);
                            Notes.SaveNotes();
                        }
                        else
                        {
                            InvalidCommand();
                            return -1;
                        }
                        break;
                    }
                case "-list":
                    {
                        Notes.DisplayNotes();
                        break;
                    }
                default:
                    {
                        InvalidCommand();
                        return -1;
                    }
            }
            return 1;
        }

        public static void InvalidCommand()
        {
            Console.WriteLine("\n\tInvalid command. Press -? for help.");
        }
        
    }
}
