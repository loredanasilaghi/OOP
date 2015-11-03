using System;
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

        public Note() { }

        public Note(string line)
        {
            string nameKeyWord = "Name:";
            string contentKeyWord = "Content:";
            int positionName = line.IndexOf(nameKeyWord);
            int positionContent = line.IndexOf(contentKeyWord);

            int startPosition = positionName + nameKeyWord.Length + 1;
            int endPosition = positionContent - 2;
            name = line.Substring(startPosition, endPosition -startPosition);
            
            startPosition = positionContent + contentKeyWord.Length + 1;
            endPosition = line.Length - 1;
            content = line.Substring(startPosition, endPosition - startPosition);
        }
    }
}
