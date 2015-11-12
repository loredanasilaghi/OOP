using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.IO;

namespace Notes
{
    public class HtmlDocument
    {
        private List<Note> noteList = new List<Note>();
        
        public string CreateHtmlDocument(Notes notesList)
        {
            StringWriter stringWriter = new StringWriter();
            string htmlContent = "";

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Html);
                writer.RenderBeginTag(HtmlTextWriterTag.Head);
                writer.RenderBeginTag(HtmlTextWriterTag.Title);
                writer.WriteEncodedText("NotesList");
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Body);
                writer.RenderBeginTag(HtmlTextWriterTag.H1);
                writer.WriteEncodedText("NotesList");
                writer.RenderEndTag();
                string html = string.Empty;
                foreach (var note in notesList)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    html = "Id: " + note.Id + Environment.NewLine;
                    writer.WriteEncodedText(html);
                    writer.RenderEndTag();

                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    html = "Name: " + note.Name + Environment.NewLine;
                    writer.WriteEncodedText(html);
                    writer.RenderEndTag();

                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    note.Content = Notes.ReplaceContent(note.Content, "\\+", "<br/>");
                    html = "Content: " + note.Content + Environment.NewLine + Environment.NewLine;
                    writer.WriteEncodedText(html);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
                writer.RenderEndTag();

                htmlContent = writer.InnerWriter.ToString();
            }

            return htmlContent;           
        }
    }
}
