using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameTool_3
{
    internal class PdfSplit
    {
        private string path { get; set; }

        public PdfSplit(string path)
        {
            this.path = path;
        }

        public string Split(int value)
        {
            if (value < 1)
            {
                value = 1;
            }

            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            var newfolder = System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path) + "\\" + name);
            if (newfolder.Exists)
            {
                newfolder.Delete(true);
                newfolder = System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path) + "\\" + name);

            }
            string newpath = newfolder.FullName;
            PdfDocument inputDocument = PdfReader.Open(path, PdfDocumentOpenMode.Import);

            for (int idx = 0; idx < inputDocument.PageCount; idx += value)
            {
                PdfDocument outputDocument = new PdfDocument();
                outputDocument.Version = inputDocument.Version;
                outputDocument.AddPage(inputDocument.Pages[idx]);
                string outputFileName = $"{newpath}\\{name}-Seite{idx + 1}.pdf";


                outputDocument.Save(outputFileName);
                

               
            }
            return newfolder.FullName;

        }
    }
}
