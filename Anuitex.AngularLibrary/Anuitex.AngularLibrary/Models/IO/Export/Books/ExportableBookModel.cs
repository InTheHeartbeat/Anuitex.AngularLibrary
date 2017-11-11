using System;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;

namespace Anuitex.AngularLibrary.Models.IO.Export.Books
{    
    public class ExportableBookModel : BookModel
    {
        public bool Selected { get; set; }

        public ExportableBookModel(Book baseBook) : base(baseBook)
        {}

        public ExportableBookModel()
        {}
    }
}