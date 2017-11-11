using System.Collections.Generic;

namespace Anuitex.AngularLibrary.Models.IO.Export.Books
{
    public class ExportBooksModel
    {
        public List<ExportableBookModel> Books { get; set; }
        public bool IsXml { get; set; }
        public ExportBooksModel()
        {}     
    }
}