using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace Notes
{
    [TestClass]
    public class TxtFileTests
    {
        [TestMethod]
        public void ShouldProcessNotes()
        {
            int counter = 0;
            var myFileContent = @"*Id:1
        *Name:magazin de
        *Content:magazin de mezeluri
        *Id:2
        *Name:name
        *Content:new\+content\+is given\+by";
            var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(myFileContent));
            TxtFile file = new TxtFile();
            file.ProcessFileContent(ref counter, stream);
            var notes = file.GetList();

            notes[0].Id.ShouldContain("1");
            notes[0].Name.ShouldContain("magazin de");
            notes[0].Content.ShouldContain("magazin de mezeluri");

            notes[1].Id.ShouldContain("2");
            notes[1].Name.ShouldContain("name");
            notes[1].Content.ShouldContain(@"new\+content\+is given\+by");
        }
    }
}
