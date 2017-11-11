using System.Collections.Generic;
using Anuitex.AngularLibrary.Models.IO.Export.Journals;

namespace Anuitex.AngularLibrary.Models.IO.Import.Journals
{
    public class ImportJournalsResultModel
    {
        public List<ExportableJournalModel> Journals { get; set; }

        public ImportJournalsResultModel()
        {}
    }
}