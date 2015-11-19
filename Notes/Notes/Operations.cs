using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class Operations
    {
        public enum PossibleOperation
        {
            Add = 0,
            Edit,
            Rename,
            List,
            Search,
            SearchExport,
            Export,
            Delete
        }

        public struct AddParameters
        {
            string name;
            string content;
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

        private PossibleOperation operation;
        private AddParameters addParameters;

        public PossibleOperation Operation
        {
            get { return this.operation; }
            set { this.operation = value; }
        }

        public AddParameters AddParameter
        {
            get{ return this.addParameters; }
            set{ this.addParameters = value; }
        }


    }
}
