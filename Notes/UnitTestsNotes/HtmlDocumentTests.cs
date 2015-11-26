using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes;
using Should;

namespace Notes
{
    [TestClass]
    public class HtmlDocumentTests
    {
        [TestMethod]
        public void ShouldCreateHtmlContent()
        {
            Notes notes = new Notes();
            Note expectedFirstNote = new Note("Shopping list for today");
            notes.AddNote(expectedFirstNote);
            Note expectedSecondNote = new Note(@"new\+content\+is given\+by", "content");
            notes.AddNote(expectedSecondNote);
            HtmlDocument html = new HtmlDocument();
            string actualHtmlContent = html.CreateHtmlDocument(notes);
            string expectedHtmlContent = "<html>\r\n\t<head>\r\n\t\t<title>\r\n\t\t\tNotesList\r\n\t\t</title>\r\n\t</head><body>\r\n\t\t<h1>\r\n\t\t\tNotesList\r\n\t\t</h1><p>Id: 1\r\n</p><p>Name: Shopping list\r\n</p><p>Content: Shopping list for today\r\n\r\n</p><p>Id: 2\r\n</p><p>Name: content\r\n</p><p>Content: new&lt;br/&gt;content&lt;br/&gt;is given&lt;br/&gt;by\r\n\r\n</p>\r\n\t</body>\r\n</html>";
            actualHtmlContent.ShouldContain(expectedHtmlContent);
        }
    }
}
