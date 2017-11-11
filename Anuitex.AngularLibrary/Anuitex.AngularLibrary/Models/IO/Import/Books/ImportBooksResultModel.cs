using System.Collections.Generic;
using Anuitex.AngularLibrary.Models.IO.Export.Books;

namespace Anuitex.AngularLibrary.Models.IO.Import.Books
{
    public class ImportBooksResultModel
    {
        public List<ExportableBookModel> Books { get; set; }
        public ImportBooksResultModel()
        {}
    }
}