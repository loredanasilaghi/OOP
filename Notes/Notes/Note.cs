﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class Note
    {
        private string name;
        private string content;
        
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
    }
}