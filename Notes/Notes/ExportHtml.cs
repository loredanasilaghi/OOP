using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class ExportHtml
    {
        private List<Note> noteList = new List<Note>();

        public void ExportNotesToHtml(string path, Notes notesList)
        {
            if (!path.Contains(".html"))
                path += ".html";
            Console.WriteLine("\n\tExporting file...");
            DisposableResourceHolder resource = new DisposableResourceHolder();
            string html = CreateHtmlFile(notesList);
            System.IO.File.WriteAllText(path, html);
            resource.Dispose();
            System.Diagnostics.Process.Start(path);
        }

        public string CreateHtmlFile(Notes notesList)
        {
            string html;
            string htmlStart = @"<!DOCTYPE html>
<html>
<head>
<title>Notes List</title>
</head>

<body>

<h1>Notes list</h1>

";
            string htmlEnd = @"
</body>
</html>";

            html = htmlStart;
            foreach (var note in notesList)
            {
                html += "<p>Id: " + note.Id + "</p>\r\n";
                html += "<p>Name: " + note.Name + "</p>\r\n";
                note.Content = Notes.ReplaceContent(note.Content, "\\+", "<br/>");
                html += "<p>Content: " + note.Content + "</p>\r\n";
                html += "<br/>";
            }
            html += htmlEnd;

            return html;
        }
    }
}
