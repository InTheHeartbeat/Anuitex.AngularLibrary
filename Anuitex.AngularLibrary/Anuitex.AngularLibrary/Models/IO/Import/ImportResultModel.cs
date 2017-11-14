using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.AngularLibrary.Models.IO.Export.Books;
using Anuitex.AngularLibrary.Models.IO.Export.Journals;
using Anuitex.AngularLibrary.Models.IO.Export.Newspapers;

namespace Anuitex.AngularLibrary.Models.IO.Import
{
    public class ImportResultModel
    {        
        public List<ExportableBookModel> Books { get; set; }
        public List<ExportableJournalModel> Journals { get; set; }
        public List<ExportableNewspaperModel> Newspapers { get; set; }
        public string Message { get; set; }
    }
}