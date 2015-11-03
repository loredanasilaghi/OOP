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
        private string id;
        
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

        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public Note() { }

        public Note(string line)
        {
            string idKeyWord = "Id:";
            string nameKeyWord = "Name:";
            string contentKeyWord = "Content:";
            int positionId = line.IndexOf(idKeyWord);
            int positionName = line.IndexOf(nameKeyWord);
            int positionContent = line.IndexOf(contentKeyWord);
            
            int startPosition = positionId + idKeyWord.Length + 1;
            int endPosition = positionName - 2;
            id = line.Substring(startPosition, endPosition - startPosition);

            startPosition = positionName + nameKeyWord.Length + 1;
            endPosition = positionContent - 2;
            name = line.Substring(startPosition, endPosition -startPosition);
            
            startPosition = positionContent + contentKeyWord.Length + 1;
            endPosition = line.Length - 1;
            content = line.Substring(startPosition, endPosition - startPosition);
        }
    }
}
